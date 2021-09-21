using Digitbridge.QuickbooksOnline.Db.Infrastructure;
using Digitbridge.QuickbooksOnline.Db.Model;
using Digitbridge.QuickbooksOnline.DbToQuickbooksMdl.Infrastructure;
using Digitbridge.QuickbooksOnline.QuickBooksConnection;
using Digitbridge.QuickbooksOnline.QuickBooksConnection.Model;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;

namespace Digitbridge.QuickbooksOnline.DbToQuickbooksMdl
{
    public class CentralOrdersExport
    {
        private QboUniversal _qboUniversal;
        private MsSqlUniversal _msSql;
        private QboDbConfig _dbConfig;
        private Command _command;
        private QboIntegrationSetting _setting;
        private QboOrderDb _qboOrderDb;
        private QboOrderItemLineDb _qboOrderItemLineDb;
        private System.Diagnostics.Stopwatch _instanceWatch;

        private CentralOrdersExport(MsSqlUniversal msSql, QboDbConfig dbConfig, 
            QboUniversal qboUniversal, Command command, QboIntegrationSetting setting,
            QboOrderDb qboOrderDb, QboOrderItemLineDb qboOrderItemLineDb, System.Diagnostics.Stopwatch instanceWatch)
        {
            _msSql = msSql;
            _dbConfig = dbConfig;
            _qboUniversal = qboUniversal;
            _command = command;
            _setting = setting;
            _qboOrderDb = qboOrderDb;
            _qboOrderItemLineDb = qboOrderItemLineDb;
            _instanceWatch = instanceWatch;
        }

        public static async Task<CentralOrdersExport> CreatAsync(QboDbConfig dbConfig
            , QboConnectionConfig qboConnectionConfig, Command command,
            System.Diagnostics.Stopwatch instanceWatch, MsSqlUniversal msSql = null)
        {
            try
            {
                if (msSql == null)
                {
                    msSql = await MsSqlUniversal.CreateAsync(
                        dbConfig.QuickBooksDbConnectionString
                        , dbConfig.UseAzureManagedIdentity
                        , dbConfig.TokenProviderConnectionString
                        , dbConfig.AzureTenantId
                        );
                }

                QboConnectionInfoDb qboConnectionInfoDb = new QboConnectionInfoDb(
                    dbConfig.QuickBooksDbConnectionInfoTableName, msSql, dbConfig.CryptKey);

                // Get user's Qbo connection informations from Database.
                QboConnectionInfo qboConnectionInfo = await DbToQuickbooksUtility.GetQboConnectionInfoAndDecrypt(
                    qboConnectionInfoDb, dbConfig.CryptKey, command);

                // Get User Export Settings.
                QboIntegrationSettingDb _qboSettingDb = new QboIntegrationSettingDb(dbConfig.QuickBooksDbIntegrationSettingTableName, msSql);
                DataTable settingTb = await _qboSettingDb.GetIntegrationSettingAsync(command);
                QboIntegrationSetting qboIntergrationSetting = ComlexTypeConvertExtension.
                                                                DatatableToList<QboIntegrationSetting>(settingTb).FirstOrDefault();

                // To be change to !String.isNullOrEmpty( qboConnectionInfo.authCode )
                if (qboIntergrationSetting != null && qboConnectionInfo.ClientId != null && qboConnectionInfo.ClientSecret != null)
                {
                    // If the ExportOrderToDate is default then handle all orders before this moment.
                    if (qboIntergrationSetting.ExportOrderToDate.Equals(DateTime.MinValue))
                    {
                        qboIntergrationSetting.ExportOrderToDate = DateTime.UtcNow;
                    }
                    // Initialize Qbo connection.
                    QboUniversal qboUniversal = await QboUniversal.CreateAsync(qboConnectionInfo, qboConnectionConfig);

                    // Udpate Refresh Token in Database if it got updated during Qbo connection initailizaion.
                    if (qboUniversal._qboConnectionTokenStatus.RefreshTokenStatus == ConnectionTokenStatus.Updated)
                    {
                        await qboConnectionInfoDb.UpdateQboRefreshTokenAsync(qboUniversal._qboConnectionInfo.RefreshToken,
                            qboUniversal._qboConnectionInfo.LastRefreshTokUpdate, command);
                    }
                    // Udpate Access Token in Database if it got refreshed during Qbo connection initailizaion.
                    if (qboUniversal._qboConnectionTokenStatus.AccessTokenStatus == ConnectionTokenStatus.Updated)
                    {
                        await qboConnectionInfoDb.UpdateQboAccessTokenAsync(qboUniversal._qboConnectionInfo.AccessToken,
                            qboUniversal._qboConnectionInfo.LastAccessTokUpdate, command);
                    }

                    QboOrderDb qboOrderDb = new QboOrderDb(dbConfig.QuickBooksDbOrderCentralTableName, msSql);
                    QboOrderItemLineDb qboOrderItemLineDb = new QboOrderItemLineDb(dbConfig.QuickBooksDbItemLineCentralTableName, msSql);

                    var centralOrdersExport =
                        new CentralOrdersExport
                        (
                            msSql, dbConfig,
                            qboUniversal, command,
                            qboIntergrationSetting,
                            qboOrderDb, qboOrderItemLineDb,
                            instanceWatch
                        );

                    return centralOrdersExport;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Pass "True" if aim to export Daily Summary Report for the Schedule Triggered Azure Function.
        /// </summary>
        /// <param name="callFromDailySummaryFunc"></param>
        /// <returns></returns>
        public async Task<DetailTransferResult> HandleOrderExportToQboAsync(OrderTransferType orderTransferType = OrderTransferType.Undefined)
        {
            DetailTransferResult result = new DetailTransferResult();
            try
            {
                DateTime lastQboSyncStatusUpdate =
                    await _qboOrderDb.GetLatestSyncStatusUpdateTime(_command, true, QboSyncStatus.UnSynced);

                // Execute only if time difference is more than 20 sec to prevent 2 instances running in same time.
                if( (DateTime.UtcNow - lastQboSyncStatusUpdate).TotalSeconds >= 20 )
                {
                    if (orderTransferType == OrderTransferType.CreateDailySummary)
                    {
                        // Transfer orders as Daily Summary report( as single Invoice or Sales Receipt ).
                        result = await TransferOrdersAsDailySummaryAsync();
                    }
                    else
                    {
                        switch (_setting.ExportOrderAs)
                        {
                            case (int)OrderExportRule.Invoice:
                            case (int)OrderExportRule.SalesReceipt:

                                DetailTransferResult createResult = await TransferOrdersAsync();
                                DetailTransferResult updateResult = await UpdateOrdersAsync();
                                if(!String.IsNullOrEmpty(createResult.Message)) 
                                {
                                    result.Message += $"Create: {createResult.Message} ; ";
                                }
                                if (!String.IsNullOrEmpty(updateResult.Message))
                                {
                                    result.Message += $"Update: {updateResult.Message}";
                                }
                                result.Status = createResult.Status == updateResult.Status ?
                                                createResult.Status : DetailResultStatus.SuccessPartial;
                                result.PendingCount = createResult.PendingCount + updateResult.PendingCount;
                                result.ProcessedCount = createResult.ProcessedCount + updateResult.ProcessedCount;
                                break;
                                
                            case (int)OrderExportRule.DailySummarySalesReceipt:
                            case (int)OrderExportRule.DailySummaryInvoice:

                                result = await TransferOrdersAsync();
                                // await UpdateOrdersStatusAsync(QboSyncStatus.UnSynced, QboSyncStatus.PendingSummary);
                                break;
                        }
                    }
                }
                else
                {
                    Thread.Sleep(120000);
                    return new DetailTransferResult(DetailResultStatus.SkipForTimeGap, "Transfer Skipped Due to Short Time Gap.");
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        public async Task<DetailTransferResult> UpdateOrdersAsync()
        {
            string returnMsg = "";
            DetailResultStatus status = DetailResultStatus.Success;
            
            List<string> errMsgs = new List<string>();

            bool noException = true;
            string excepMsg = "";

            try
            {
                DataTable updatedOrdersDtb =
                    await _qboOrderDb.GetOrdersByStatusInRangeAsync(QboSyncStatus.ToBeUpdated, _command, 
                    _setting.ExportOrderFromDate, _setting.ExportOrderToDate);
                List<QboSalesOrder> updatedOrderHeaders = ComlexTypeConvertExtension.DatatableToList<QboSalesOrder>(updatedOrdersDtb);

                int proceesedOrderCount = 0;
                int pendingOrderCount = updatedOrderHeaders.Count;

                foreach (QboSalesOrder qboOrderHeader in updatedOrderHeaders)
                {
                    // Prevent flag loss because instance get killed by execution time
                    if(_instanceWatch.ElapsedMilliseconds > 250000)
                    {
                        break;
                    }

                    //////////////////////////////////////////////
                    var watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    //////////////////////////////////////////////
                    
                    string curErrMsg = "";

                    try
                    {
                        await _qboOrderDb.UpdateCentralOrderSyncStatus(qboOrderHeader.SalesOrderNum, QboSyncStatus.UpdateStarted);

                        dynamic curOrder = new ExpandoObject();
                        // Get Invoice or SalesReceipt from QBO
                        switch (qboOrderHeader.SaleOrderQboType)
                        {
                            case (int)SaleOrderQboType.Invoice:
                                curOrder = await _qboUniversal.GetInvoice(qboOrderHeader.DigitbridgeOrderId);
                                break;
                            case (int)SaleOrderQboType.SalesReceipt:
                                curOrder = await _qboUniversal.GetSalesReceipt(qboOrderHeader.DigitbridgeOrderId);
                                break;
                        }

                        if(curOrder == null)
                        {
                            curErrMsg += QboOrderExportErrorMsgs.OrderUpdateErrorPrefix + qboOrderHeader.DigitbridgeOrderId +
                                QboOrderExportErrorMsgs.OrderUpdateNotFoundErrorPostfix;
                            errMsgs.Add(curErrMsg);
                            await _qboOrderDb.UpdateCentralOrderSyncStatus(
                                qboOrderHeader.SalesOrderNum, QboSyncStatus.UpdatedWithError);
                            continue;
                        }
                        // Assign shipping Info
                        curOrder.ShipDate = qboOrderHeader.ShipDate;
                        curOrder.ShipDateSpecified = true;
                        curOrder.TrackingNum = qboOrderHeader.TrackingNum;
                        curOrder.ShipMethodRef = new ReferenceType() { Value = qboOrderHeader.ShipMethodRef };

                        // Update Invoice/SalesReceipt to QBO
                        bool isUpdateSuccess = false;

                        (bool isTransactionExist, bool isTransactionUpToDate) =
                                    await _qboUniversal.IsSalesTransactionExistAndUpToDate((SalesTransaction)curOrder);

                        // Order exists in QBO and is up to date, we do nothing if there is a newer version,
                        // which happens when there was another instance which has already updated the current Order
                        if (isTransactionUpToDate)
                        {
                            switch (qboOrderHeader.SaleOrderQboType)
                            {
                                case (int)SaleOrderQboType.Invoice:
                                    isUpdateSuccess = await _qboUniversal.UpdateInvoice(curOrder) == null;
                                    break;
                                case (int)SaleOrderQboType.SalesReceipt:
                                    isUpdateSuccess = await _qboUniversal.UpdateSalesReceiptIfLatest(curOrder) == null;
                                    break;
                            }
                            await _qboOrderDb.UpdateCentralOrderSyncStatus(
                                qboOrderHeader.SalesOrderNum, QboSyncStatus.SyncedSuccess);
                        }

                        // Put syncStatus to "Unsynced" if the order is not in QBO, 
                        // this probablily due to Order Shipping Info got updated before synced to QBO
                        if (!isTransactionExist)
                        {
                            await _qboOrderDb.UpdateCentralOrderSyncStatus(
                                qboOrderHeader.SalesOrderNum, QboSyncStatus.UnSynced);
                        }
                    }
                    catch (Exception ex)
                    {
                        noException = false;
                        excepMsg += TextUtility.NewLine + QboOrderExportErrorMsgs.OrderTransferExceptionPrefix +
                            qboOrderHeader.DigitbridgeOrderId + " : " + ex.Message + TextUtility.NewLine;
                        try
                        {
                            await _qboOrderDb.UpdateCentralOrderSyncStatus(
                            qboOrderHeader.SalesOrderNum, QboSyncStatus.UpdatedWithError);
                        }
                        catch (Exception iex)
                        {
                            throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), iex);
                        }
                    }

                    proceesedOrderCount++;
                    pendingOrderCount--;
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    watch.Stop();
                    Console.WriteLine($"Execution Time for export update {qboOrderHeader.DigitbridgeOrderId} : {watch.ElapsedMilliseconds} ms");
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                }

                if (noException == false)
                {
                    throw new Exception(excepMsg);
                }

                foreach (string str in errMsgs) returnMsg += str;

                if (errMsgs.Count > 0)
                {
                    status = errMsgs.Count == proceesedOrderCount ? DetailResultStatus.Fail : DetailResultStatus.SuccessPartial;
                }
                return new DetailTransferResult(status, returnMsg, pendingOrderCount, proceesedOrderCount - errMsgs.Count);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<DetailTransferResult> TransferOrdersAsync()
        {
            string returnMsg = "";
            DetailResultStatus status = DetailResultStatus.Success;

            List<string> errMsgs = new List<string>();

            OrderTransferType transferType = (_setting.ExportOrderAs == (int)OrderExportRule.Invoice ||
                _setting.ExportOrderAs == (int)OrderExportRule.SalesReceipt) ?
                OrderTransferType.CreateTransaction : OrderTransferType.PreprocessSummaryOrder;

            bool noException = true;
            string excepMsg = "";

            try
            {
                DataTable unSyncedOrdersDtb =
                    await _qboOrderDb.GetOrdersByStatusInRangeAsync(QboSyncStatus.UnSynced, _command,
                        _setting.ExportOrderFromDate, _setting.ExportOrderToDate);

                List<QboSalesOrder> pendingOrderHeaders = 
                    ComlexTypeConvertExtension.DatatableToList<QboSalesOrder>(unSyncedOrdersDtb);

                if(transferType == OrderTransferType.PreprocessSummaryOrder)
                {
                    DataTable toBeUpdatedOrdersDtb =
                        await _qboOrderDb.GetOrdersByStatusInRangeAsync(QboSyncStatus.ToBeUpdated, _command,
                            _setting.ExportOrderFromDate, _setting.ExportOrderToDate);
                    pendingOrderHeaders.AddRange(ComlexTypeConvertExtension.DatatableToList<QboSalesOrder>(toBeUpdatedOrdersDtb));
                }

                QboSyncStatus transferInitiatedStatus = (transferType == OrderTransferType.CreateTransaction) ?
                    QboSyncStatus.SyncStarted : QboSyncStatus.PreprocessStarted;

                QboSyncStatus transferErrorStatus = (transferType == OrderTransferType.CreateTransaction) ?
                    QboSyncStatus.SyncedWithError : QboSyncStatus.PreprocessedWithError;

                int proceesedOrderCount = 0;
                int pendingOrderCount = pendingOrderHeaders.Count;

                foreach (QboSalesOrder qboOrderHeader in pendingOrderHeaders)
                {
                    // Prevent flag loss because instance get killed by execution time
                    if (_instanceWatch.ElapsedMilliseconds > 250000)
                    {
                        break;
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    var watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    ////////////////////////////////////////////////////////////////////////////////////////////////////

                    string curErrMsg = "";
                    int itemLinesCount = 0;
                    try
                    {
                        await _qboOrderDb.UpdateCentralOrderSyncStatus(qboOrderHeader.SalesOrderNum, transferInitiatedStatus);

                        // Get order lines 
                        DataTable unSyncedItemLineDtb = await _qboOrderItemLineDb.GetOrderItemLineByStatusAsync(
                            QboSyncStatus.UnSynced, qboOrderHeader.SalesOrderNum);

                        // For QboSyncStatus Updating, need to provide counts for RunInTransition
                        itemLinesCount = unSyncedItemLineDtb.Rows.Count;

                        List<QboOrderItemLine> unSyncedItemLines =
                            ComlexTypeConvertExtension.DatatableToList<QboOrderItemLine>(unSyncedItemLineDtb);

                        // There should be at least 1 item line per Order Header, the SyncStatus of the item has been changed by other instance.
                        if (itemLinesCount == 0)
                        {
                            curErrMsg = transferType + QboOrderExportErrorMsgs.OrderTransferErrorPrefix + 
                                qboOrderHeader.DigitbridgeOrderId + QboOrderExportErrorMsgs.OrderSyncStatusErrorPostfix;
                            errMsgs.Add(curErrMsg);

                            await _qboOrderDb.UpdateCentralOrderSyncStatus(qboOrderHeader.SalesOrderNum, transferErrorStatus);
                            continue;
                        }

                        // Mark order item lines status as sync initiated 
                        await _qboOrderItemLineDb.UpdateCentralOrderItemLinesSyncStatus(
                            qboOrderHeader.SalesOrderNum, transferInitiatedStatus, itemLinesCount);

                        // Handle Item Lines
                        List<Line> lines = new List<Line>();

                        bool skipDueToNoMatching = false;
                        // Can't find item with DefaultInventoryItemId in user setting
                        bool itemIdLookUpWithError = false;

                        foreach (QboOrderItemLine qboOrderItemLine in unSyncedItemLines)
                        {
                            // Handle Item
                            string itemId = await DbToQuickbooksUtility.GetQboItemId(_setting, qboOrderItemLine, _qboUniversal);
                            if (itemId.Equals(QboMappingConsts.NoMatchingItemReturnString))
                            {
                                skipDueToNoMatching = true;
                                break;
                            }
                            else if (itemId.Equals(QboMappingConsts.QboItemNullId))
                            {
                                itemIdLookUpWithError = true;
                                break;
                            }
                            else
                            {
                                switch (transferType)
                                {
                                    case OrderTransferType.CreateTransaction:
                                        lines.Add(DbToQuickbooksUtility.MapOrderItemLinesToQboLine(qboOrderItemLine, itemId));
                                        break;
                                    case OrderTransferType.PreprocessSummaryOrder:
                                        await _qboOrderItemLineDb.UpdateCentralOrderItemLinesQboItemId(
                                            qboOrderItemLine.ItemLineNum, itemId.ForceToInt());
                                        break;
                                }
                            }
                        }

                        if (skipDueToNoMatching || itemIdLookUpWithError)
                        {
                            QboSyncStatus targetItemCheckStatus = itemIdLookUpWithError ? transferErrorStatus : QboSyncStatus.Skipped;

                            if (itemIdLookUpWithError)
                            {
                                curErrMsg = transferType + QboOrderExportErrorMsgs.OrderTransferErrorPrefix
                                    + qboOrderHeader.DigitbridgeOrderId + QboOrderExportErrorMsgs.OrderDefaultItemIdNotFoundErrorPostfix;
                                errMsgs.Add(curErrMsg);
                            }
                            await _qboOrderDb.UpdateCentralOrderSyncStatus(qboOrderHeader.SalesOrderNum, targetItemCheckStatus);
                            await _qboOrderItemLineDb.UpdateCentralOrderItemLinesSyncStatus(qboOrderHeader.SalesOrderNum, targetItemCheckStatus, itemLinesCount);
                            
                            continue;
                        }

                        if (transferType == OrderTransferType.CreateTransaction)
                        {
                            lines.Add(DbToQuickbooksUtility.MapOrderShippingCostToQboLine(qboOrderHeader, _setting));
                            lines.Add(DbToQuickbooksUtility.MapOrderDiscountToQboLine(qboOrderHeader, _setting));

                            if (_setting.QboItemCreateRule == (int)QboItemCreateRule.DefaultItem &&
                                _setting.SalesTaxExportRule == (int)SalesTaxExportRule.ExportToDefaultSaleTaxItemAccount)
                            {
                                lines.Add(DbToQuickbooksUtility.MapOrderTaxToQboLine(qboOrderHeader, _setting));
                            }

                            // Handle customer
                            string customerId = await DbToQuickbooksUtility.GetQboCustomerId(_setting, qboOrderHeader, _msSql, _dbConfig, _qboUniversal);
                            if (customerId == "")
                            {
                                curErrMsg = transferType + QboOrderExportErrorMsgs.OrderTransferErrorPrefix + 
                                    qboOrderHeader.DigitbridgeOrderId + QboOrderExportErrorMsgs.OrderTransferCustomerErrorPostfix;
                                errMsgs.Add(curErrMsg);
                                await _qboOrderDb.UpdateCentralOrderSyncStatus(
                                    qboOrderHeader.SalesOrderNum, QboSyncStatus.SyncedWithError);
                                await _qboOrderItemLineDb.UpdateCentralOrderItemLinesSyncStatus(qboOrderHeader.SalesOrderNum,
                                    QboSyncStatus.SyncedWithError, itemLinesCount);
                                continue;
                            }

                            switch (_setting.ExportOrderAs)
                            {
                                case (int)OrderExportRule.Invoice:
                                    Invoice invoice = DbToQuickbooksUtility.MapSalesOrderToQboInvoice(qboOrderHeader, customerId, _setting);
                                    invoice.Line = lines.ToArray();
                                    await _qboUniversal.CreateInvoiceIfAbsent(invoice);

                                    break;

                                case (int)OrderExportRule.SalesReceipt:
                                    SalesReceipt salesReceipt = DbToQuickbooksUtility.MapSalesOrderToQboSalesReceipt(qboOrderHeader, customerId, _setting);
                                    salesReceipt.Line = lines.ToArray();
                                    await _qboUniversal.CreateSalesReceiptIfAbsent(salesReceipt);

                                    break;
                            }
                        }
                        
                        await _qboOrderDb.UpdateCentralOrderQboType(qboOrderHeader.SalesOrderNum,(SaleOrderQboType)_setting.ExportOrderAs);

                        QboSyncStatus targetSuccessStatus = (transferType == OrderTransferType.CreateTransaction) ?
                            QboSyncStatus.SyncedSuccess : QboSyncStatus.PendingSummary;

                        await _qboOrderDb.UpdateCentralOrderSyncStatus(qboOrderHeader.SalesOrderNum, targetSuccessStatus);
                        await _qboOrderItemLineDb.UpdateCentralOrderItemLinesSyncStatus(qboOrderHeader.SalesOrderNum, targetSuccessStatus, itemLinesCount);
                    }
                    catch (Exception ex)
                    {
                        noException = false;

                        excepMsg += TextUtility.NewLine + transferType + QboOrderExportErrorMsgs.OrderTransferExceptionPrefix + 
                            qboOrderHeader.DigitbridgeOrderId + " : " + ex.Message + TextUtility.NewLine;
                        try
                        {
                            await _qboOrderDb.UpdateCentralOrderSyncStatus(qboOrderHeader.SalesOrderNum, transferErrorStatus);
                            if(itemLinesCount > 0)
                            {
                                await _qboOrderItemLineDb.UpdateCentralOrderItemLinesSyncStatus(
                                    qboOrderHeader.SalesOrderNum, transferErrorStatus, itemLinesCount);
                            }
                        }
                        catch (Exception iex)
                        {
                            throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), iex);
                        }
                    }

                    proceesedOrderCount++;
                    pendingOrderCount--;

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    watch.Stop();
                    Console.WriteLine($"Execution Time for {transferType} {qboOrderHeader.DigitbridgeOrderId} : {watch.ElapsedMilliseconds} ms");
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                }

                if (noException == false)
                {
                    throw new Exception(excepMsg);
                }

                foreach (string str in errMsgs) returnMsg += str;

                if (errMsgs.Count > 0)
                {
                    status = errMsgs.Count == proceesedOrderCount ? DetailResultStatus.Fail : DetailResultStatus.SuccessPartial;
                }
                return new DetailTransferResult(status, returnMsg, pendingOrderCount, proceesedOrderCount - errMsgs.Count);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<DetailTransferResult> TransferOrdersAsDailySummaryAsync()
        {
            string returnMsg = "";
            DetailResultStatus status = DetailResultStatus.Success;

            List<string> errMsgs = new List<string>();

            bool noException = true;
            string excepMsg = "";

            try
            {
                DateTime startOfLastDay = DbToQuickbooksUtility.GetStartOfDay(DateTime.UtcNow.AddDays(-1));
                DateTime endOfLastDay = DbToQuickbooksUtility.GetEndOfDay(DateTime.UtcNow.AddDays(-1));
                DateTime orderFromDate = _setting.ExportOrderFromDate > startOfLastDay ? _setting.ExportOrderFromDate : startOfLastDay;
                DateTime orderEndDate = _setting.ExportOrderToDate < endOfLastDay ? _setting.ExportOrderToDate : endOfLastDay;

                if (orderEndDate > orderFromDate)
                {
                    QboChnlAccSettingDb qboChnlAccSettingDb = new QboChnlAccSettingDb(_dbConfig.QuickBooksChannelAccSettingTableName, _msSql);
                    // Get the Chnl Acc Settings that have not been exproted today
                    DataTable accSettingsDtb = await qboChnlAccSettingDb.GetChnlAccSettingsBeforeAsync(_command, endOfLastDay);
                    //DataTable accSettingsDtb = await qboChnlAccSettingDb.GetChnlAccSettingsAsync(_command);

                    List<QboChnlAccSetting> accSettings = ComlexTypeConvertExtension.DatatableToList<QboChnlAccSetting>(accSettingsDtb);

                    int proceesedChnlAccCount = 0;
                    int pendingChnlAccCount = accSettings.Count;

                    foreach (QboChnlAccSetting accSetting in accSettings)
                    {
                        // Prevent flag loss because instance get killed by execution time
                        if (_instanceWatch.ElapsedMilliseconds > 250000)
                        {
                            break;
                        }
                        try
                        {
                            ////////////////////////////////////////////////////////////////////////////////////////////////////
                            var watch = new System.Diagnostics.Stopwatch();
                            watch.Start();
                            ////////////////////////////////////////////////////////////////////////////////////////////////////

                            DataTable unSyncedOrdersDtb =
                                await _qboOrderDb.GetSummaryOrdersInRangeAsync(QboSyncStatus.PendingSummary,
                                    _command, orderFromDate, orderEndDate, accSetting.ChannelAccountNum);
                            List<QboSalesOrder> unSyncedOrderHeaders =
                                ComlexTypeConvertExtension.DatatableToList<QboSalesOrder>(unSyncedOrdersDtb);

                            DataTable unSyncedItemLineDtb = await _qboOrderItemLineDb.GetSummaryItemLinesInRangeAsync(QboSyncStatus.PendingSummary,
                                    _command, orderFromDate, orderEndDate, accSetting.ChannelAccountNum);

                            List<QboOrderItemLine> unSyncedItemLines =
                                        ComlexTypeConvertExtension.DatatableToList<QboOrderItemLine>(unSyncedItemLineDtb);
                            // Mark orders and order item lines status as sync initiated 
                            await UpdateOrdersStatusAsync(QboSyncStatus.PendingSummary, orderFromDate, orderEndDate,
                                QboSyncStatus.SyncStarted, accSetting.ChannelAccountNum);

                            List<Line> summaryLines = new List<Line>();
                            List<Line> curOrderLines = new List<Line>();

                            foreach (QboSalesOrder qboOrderHeader in unSyncedOrderHeaders)
                            {
                                
                                int itemLinesCount = 0;

                                try
                                {
                                    // Reset curLines for current Seles Order
                                    curOrderLines.Clear();

                                    List<QboOrderItemLine> pendingItemLines = unSyncedItemLines.Where(a => a.SalesOrderNum == qboOrderHeader.SalesOrderNum).ToList();

                                    itemLinesCount = pendingItemLines.Count;

                                    foreach (QboOrderItemLine qboOrderItemLine in pendingItemLines)
                                    {
                                        curOrderLines.Add(DbToQuickbooksUtility.MapOrderItemLinesToQboLine(qboOrderItemLine));
                                    }

                                    Line curShippingLine = DbToQuickbooksUtility.MapOrderShippingCostToQboLine(qboOrderHeader, _setting);
                                    if (curShippingLine.Amount != 0)
                                    {
                                        curOrderLines.Add(curShippingLine);
                                    }

                                    Line curDiscountLine = DbToQuickbooksUtility.MapOrderDiscountToQboLine(qboOrderHeader, _setting);
                                    if (curDiscountLine.Amount != 0)
                                    {
                                        curOrderLines.Add(curDiscountLine);
                                    }

                                    if (_setting.QboItemCreateRule == (int)QboItemCreateRule.DefaultItem &&
                                        _setting.SalesTaxExportRule == (int)SalesTaxExportRule.ExportToDefaultSaleTaxItemAccount)
                                    {
                                        Line curTaxLine = DbToQuickbooksUtility.MapOrderTaxToQboLine(qboOrderHeader, _setting);
                                        if(curTaxLine.Amount != 0)
                                        {
                                            curOrderLines.Add(curTaxLine);
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    noException = false;
                                    excepMsg += TextUtility.NewLine + QboOrderExportErrorMsgs.OrderTransferAsDailySummeryExceptionPrefix +
                                        qboOrderHeader.DigitbridgeOrderId + " : " + ex.Message + TextUtility.NewLine;

                                    await _qboOrderDb.UpdateCentralOrderSyncStatus(
                                        qboOrderHeader.SalesOrderNum, QboSyncStatus.SyncedWithError);
                                    await _qboOrderItemLineDb.UpdateCentralOrderItemLinesSyncStatus(qboOrderHeader.SalesOrderNum,
                                            QboSyncStatus.SyncedWithError, itemLinesCount);
                                }

                                // If no exception then append lines from current Sales Order to the Summery Lines 
                                foreach (Line curLine in curOrderLines)
                                {
                                    summaryLines.Add(curLine);
                                }
                            }

                            if (summaryLines.Count > 0)
                            {
                                switch (_setting.ExportOrderAs)
                                {
                                    case (int)OrderExportRule.DailySummarySalesReceipt:
                                        SalesReceipt salesReceipt = new SalesReceipt();
                                        salesReceipt.CustomerRef = new ReferenceType() { Value = accSetting.ChannelQboCustomerId.ForceToTrimString() };
                                        salesReceipt.DocNumber = $"{orderFromDate.ToString("yyyyMMdd")}-{accSetting.ChannelAccountNum}-Smry";
                                        salesReceipt.Line = summaryLines.ToArray();
                                        await _qboUniversal.CreateSalesReceiptIfAbsent(salesReceipt);

                                        break;

                                    case (int)OrderExportRule.DailySummaryInvoice:
                                        Invoice invoice = new Invoice();
                                        invoice.CustomerRef = new ReferenceType() { Value = accSetting.ChannelQboCustomerId.ForceToTrimString() };
                                        invoice.DocNumber = $"{orderFromDate.ToString("yyyyMMdd")}-{accSetting.ChannelAccountNum}-Smry";
                                        invoice.Line = summaryLines.ToArray();
                                        await _qboUniversal.CreateInvoiceIfAbsent(invoice);

                                        break;
                                }
                            }
                            // Mark Sync status as "SyncedSuccess" for Orders and Lines if the status is not "SyncedWithError" and is "SyncStart" 
                            await UpdateOrdersStatusAsync(QboSyncStatus.SyncStarted, orderFromDate, orderEndDate,
                                QboSyncStatus.SyncedSuccess, accSetting.ChannelAccountNum);
                            await qboChnlAccSettingDb.UpdateDailySummaryLastExportAsync(_command, accSetting.ChannelAccountNum);

                            ////////////////////////////////////////////////////////////////////////////////////////////////////
                            watch.Stop();
                            Console.WriteLine($"Execution Time for DailySummay for {accSetting.ChannelAccountName} : {watch.ElapsedMilliseconds} ms");
                            ////////////////////////////////////////////////////////////////////////////////////////////////////
                        }
                        catch (Exception ex)
                        {
                            noException = false;
                            excepMsg += TextUtility.NewLine + QboOrderExportErrorMsgs.DailySummeryTransferExceptionPrefix +
                                accSetting.ChannelAccountName + ex.Message + TextUtility.NewLine;
                            await UpdateOrdersStatusAsync(QboSyncStatus.SyncStarted, orderFromDate, orderEndDate,
                                QboSyncStatus.SyncedWithError, accSetting.ChannelAccountNum);
                        }
                        proceesedChnlAccCount++;
                        pendingChnlAccCount--;
                    }

                    if (noException == false)
                    {
                        throw new Exception(excepMsg);
                    }
                    foreach (string str in errMsgs) returnMsg += str;

                    if (errMsgs.Count > 0)
                    {
                        status = errMsgs.Count == proceesedChnlAccCount ? DetailResultStatus.Fail : DetailResultStatus.SuccessPartial;
                    }
                    return new DetailTransferResult(status, returnMsg, pendingChnlAccCount, proceesedChnlAccCount - errMsgs.Count);
                }
                return new DetailTransferResult(status, returnMsg);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Update QboSyncStatus for multiple Sales Orders and Order Item Lines filter by current QboSyncStatus (and channelAccNum)
        /// The Count function is for safety purpose required by CTO
        /// </summary>
        /// <param name="_setting"></param>
        /// <param name="filterStatus"></param>
        /// <param name="status"></param>
        /// <param name="channelAccNum"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task UpdateOrdersStatusAsync( QboSyncStatus filterStatus, 
            DateTime fromDate, DateTime endDate, QboSyncStatus status, int channelAccNum = -1)
        {
            try
            {
                // Update unsync Order Headers
                int countHeaders = await _qboOrderDb.GetSummaryOrdersCount(filterStatus, _command, fromDate, endDate, channelAccNum);
                await _qboOrderDb.UpdateSummaryOrdersSyncStatus(_command, fromDate, endDate, countHeaders, filterStatus, status, channelAccNum);
                // Update unsync Order Item Lines
                int countItemLines = await _qboOrderItemLineDb.GetSummaryOrderItemLinesCount(filterStatus, _command, fromDate, endDate, channelAccNum);
                await _qboOrderItemLineDb.UpdateSummaryOrderItemLinesSyncStatus(_command, fromDate, endDate, countItemLines, filterStatus, status, channelAccNum);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
