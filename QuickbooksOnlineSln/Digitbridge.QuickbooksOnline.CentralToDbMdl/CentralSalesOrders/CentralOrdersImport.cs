using Digitbridge.QuickbooksOnline.CentralToDbMdl;
using Digitbridge.QuickbooksOnline.Db.Infrastructure;
using Digitbridge.QuickbooksOnline.Db.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql ;

namespace Digitbridge.QuickbooksOnline.CentralToDbMdl
{
    public class CentralOrdersImport
    {
        private QboDbConfig _dbConfig;
        public MsSqlUniversal _msSql;
        private QboOrderDb _qboOrderDb;
        private QboOrderItemLineDb _qboOrderItemLineDb;
        private string _extensionFlagsApiEndPoint;
        private string _extensionFlagsApiCode;

        private CentralOrdersImport(MsSqlUniversal msSql, QboDbConfig dbConfig, 
            string extensionFlagsApiEndPoint, string extensionFlagsApiCode)
        {
            _msSql = msSql;
            _dbConfig = dbConfig;
            _qboOrderDb = new QboOrderDb(dbConfig.QuickBooksDbOrderTableName, msSql);
            _qboOrderItemLineDb = new QboOrderItemLineDb(dbConfig.QuickBooksDbItemLineTableName, msSql);
            _extensionFlagsApiEndPoint = extensionFlagsApiEndPoint;
            _extensionFlagsApiCode = extensionFlagsApiCode;
        }

        public static async Task<CentralOrdersImport> CreatAsync(QboDbConfig dbConfig, string extensionFlagsApiEndPoint, string extensionFlagsApiCode)
        {
            try
            {
                MsSqlUniversal msSql = await MsSqlUniversal.CreateAsync(
                 dbConfig.QuickBooksDbConnectionString
                 , dbConfig.UseAzureManagedIdentity
                 , dbConfig.TokenProviderConnectionString
                 , dbConfig.AzureTenantId
                 );

                var centralOrdersImport = new CentralOrdersImport(msSql, dbConfig, extensionFlagsApiEndPoint, extensionFlagsApiCode);
                return centralOrdersImport;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Import Orders from Central to Qbo Database
        /// </summary>
        /// <param name="qboOrdersWrappers"></param>
        /// <returns></returns>
        public async Task<DetailTransferResult> ImportOrdersAsync(List<QboSalesOrderWrapper> qboOrdersWrappers)
        {
            DateTime lastOrderEnterDate =
                    await _qboOrderDb.GetLatestOrderEnterDate(
                        new Command(
                           qboOrdersWrappers.First().qboSalesOrder.MasterAccountNum,
                           qboOrdersWrappers.First().qboSalesOrder.ProfileNum
                           )
                    );

            // Execute only if time difference is more than 20 sec to prevent 2 instances running in same time.
            if ((DateTime.UtcNow - lastOrderEnterDate).TotalSeconds < 2)
            {
                return new DetailTransferResult(DetailResultStatus.SkipForTimeGap, "Skip due to Short Time Gap,");
            }

            string returnMsg = "";
            DetailResultStatus status = DetailResultStatus.Success;

            int processedOrderCount = 0;
            int pendingOrderCount = qboOrdersWrappers.Count;

            OrderExtensionFlagsPostType initialDownloadedFlags =
                new OrderExtensionFlagsPostType() {
                    ExtensionFlags = new List<int> { (int)OrderExtensionFlag.Qbo_Initial_Downloaded },
                    DigitBridgeOrderid = new List<string>()
                };
            OrderExtensionFlagsPostType trackingDownloadedFlags =
                new OrderExtensionFlagsPostType()
                {
                    ExtensionFlags = new List<int> { (int)OrderExtensionFlag.Qbo_Tracking_Downloaded },
                    DigitBridgeOrderid = new List<string>()
                };
            OrderExtensionFlagsPostType errorFlags = 
                new OrderExtensionFlagsPostType()
                {
                    ExtensionFlags = new List<int> { (int)OrderExtensionFlag.Synced_Wtih_Error },
                    DigitBridgeOrderid = new List<string>()
                };

            List<string> errMsgs = new List<string>();

            bool noException = true;
            string excepMsg = "";

            try
            {
                foreach(QboSalesOrderWrapper salesOrderWrapper in qboOrdersWrappers)
                {
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    var watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    ////////////////////////////////////////////////////////////////////////////////////////////////////

                    processedOrderCount++;
                    pendingOrderCount--;

                    DataTable orderDt = ComlexTypeConvertExtension.ObjectToDataTable(salesOrderWrapper.qboSalesOrder);
                    orderDt.TableName = _dbConfig.QuickBooksDbOrderTableName;
                    orderDt.Columns["SalesOrderNum"].AutoIncrement = true;

                    if( orderDt.Rows.Count > 0)
                    {
                        string curErrMsg = "";

                        long salesOrderNum = 0;
                        string curDigitbridgeOrderId = salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId;
                        try
                        {
                            bool isExisit = await _qboOrderDb.ExistsOrderAsync(curDigitbridgeOrderId);
                            // Order header has already exist, check if current record and the one in Db matches.
                            if (isExisit)
                            {
                                bool isModified = 
                                    await _qboOrderDb.CheckIfOrderModifiedAsync(
                                        curDigitbridgeOrderId, 
                                        salesOrderWrapper.qboSalesOrder.CentralUpdatedTime
                                        );
                                if (isModified)
                                {
                                    curErrMsg += QboOrderImportErrorMsgs.OrderImportErrorPrefix + curDigitbridgeOrderId +
                                        QboOrderImportErrorMsgs.OrderImportLineExistError;
                                    errMsgs.Add(curErrMsg);
                                    errorFlags.DigitBridgeOrderid.Add(
                                        salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId.ForceToTrimString());
                                    //////////////////////
                                    Console.WriteLine(curErrMsg);
                                    //////////////////////
                                    continue;
                                }
                            }
                            // Order header has not been imported: add order header.
                            if (!isExisit)
                            {
                                isExisit = await _qboOrderDb.AddOrderAsync(orderDt);
                            }
                            // SalesOrderNum is the FK of Sales Order Item Lines in DB so we need to get it before creating lines in DB.
                            if (isExisit)
                            {
                                salesOrderNum = await _qboOrderDb.GetOrderNumAsync(curDigitbridgeOrderId);
                            }
                            // Create sales order item lines if order header is Created or Queried successfully.
                            if (salesOrderNum != 0)
                            {
                                // Check if there is any order item line with that salesOrderNum in database already.
                                bool isLineExist = await _qboOrderItemLineDb.ExistsAnyOrderItemLineAsync(salesOrderNum);
                                if (isLineExist)
                                {
                                    curErrMsg += QboOrderImportErrorMsgs.OrderImportErrorPrefix + curDigitbridgeOrderId +
                                        QboOrderImportErrorMsgs.OrderImportLineExistError;
                                    errMsgs.Add(curErrMsg);
                                    errorFlags.DigitBridgeOrderid.Add(salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId.ForceToTrimString());
                                    continue;
                                }
                                DataTable linesDt = ComlexTypeConvertExtension.ListToDataTable(salesOrderWrapper.qboItemLines);
                                linesDt.TableName = _dbConfig.QuickBooksDbItemLineTableName;
                                linesDt.Columns["ItemLineNum"].AutoIncrement = true;

                                await _qboOrderItemLineDb.AddOrderItemLinesAsync(linesDt, salesOrderNum);
                            }
                            // Add flag for this order into extension flag list
                            if (!String.IsNullOrEmpty(salesOrderWrapper.qboSalesOrder.TrackingNum))
                            {
                                trackingDownloadedFlags.DigitBridgeOrderid.Add(salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId.ForceToTrimString());
                            }
                            else
                            {
                                initialDownloadedFlags.DigitBridgeOrderid.Add(salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId.ForceToTrimString());
                            }


                        }
                        catch (Exception ex)
                        {
                            errorFlags.DigitBridgeOrderid.Add(
                                    salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId.ForceToTrimString());
                            noException = false;
                            excepMsg += QboOrderImportErrorMsgs.OrderImportExceptionPrefix + curDigitbridgeOrderId +
                                " : " + ex.Message + TextUtility.NewLine;
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                        watch.Stop();
                        Console.WriteLine($"Execution Time for create {salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId} : {watch.ElapsedMilliseconds} ms");
                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                }

                // Post Order Extension Flags to Central
                if (initialDownloadedFlags.DigitBridgeOrderid.Count > 0)
                {
                    await QuickBooksDbUtility.PostExtensionFlagsAsync(_extensionFlagsApiEndPoint, 
                        _extensionFlagsApiCode, initialDownloadedFlags);
                }

                if(trackingDownloadedFlags.DigitBridgeOrderid.Count > 0)
                {
                    await QuickBooksDbUtility.PostExtensionFlagsAsync(_extensionFlagsApiEndPoint, 
                        _extensionFlagsApiCode, trackingDownloadedFlags);
                }

                if(errorFlags.DigitBridgeOrderid.Count > 0)
                {
                    await QuickBooksDbUtility.PostExtensionFlagsAsync(_extensionFlagsApiEndPoint, 
                        _extensionFlagsApiCode, errorFlags);
                }
                
                if (noException == false)
                {
                    throw new Exception(excepMsg);
                }
                foreach (string str in errMsgs) returnMsg += str;
                if( errMsgs.Count > 0 )
                {
                    status = errMsgs.Count == processedOrderCount ? DetailResultStatus.Fail : DetailResultStatus.SuccessPartial;
                }
                return new DetailTransferResult(status, returnMsg, pendingOrderCount, processedOrderCount - errMsgs.Count);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Update the Orders after the shipping info has been upadated in Central
        /// </summary>
        /// <param name="qboOrdersWrappers"></param>
        /// <returns></returns>
        public async Task<DetailTransferResult> UpdateOrdersAsync(List<QboSalesOrderWrapper> qboOrdersWrappers)
        {
            DateTime lastOrderToBeUpdatedDate =
                    await _qboOrderDb.GetLatestSyncStatusUpdateTime(
                        new Command(
                           qboOrdersWrappers.First().qboSalesOrder.MasterAccountNum,
                           qboOrdersWrappers.First().qboSalesOrder.ProfileNum
                           ),
                        false,
                        QboSyncStatus.ToBeUpdated
                    );

            // Execute only if time difference is more than 20 sec to prevent 2 instances running in same time.
            if ((DateTime.UtcNow - lastOrderToBeUpdatedDate).TotalSeconds < 20)
            {
                return new DetailTransferResult(DetailResultStatus.SkipForTimeGap, "Skip due to Short Time Gap,");
            }

            string returnMsg = "";
            DetailResultStatus status = DetailResultStatus.Success;

            OrderExtensionFlagsPostType trackingDownloadedFlags =
                new OrderExtensionFlagsPostType()
                {
                    ExtensionFlags = new List<int> { (int)OrderExtensionFlag.Qbo_Tracking_Downloaded },
                    DigitBridgeOrderid = new List<string>()
                };
            OrderExtensionFlagsPostType errorFlags =
                new OrderExtensionFlagsPostType()
                {
                    ExtensionFlags = new List<int> { (int)OrderExtensionFlag.Synced_Wtih_Error },
                    DigitBridgeOrderid = new List<string>()
                };

            List<QboSalesOrderWrapper> toBeUpdatedOrders = 
                qboOrdersWrappers.Where(
                    wrapper => !String.IsNullOrEmpty(wrapper.qboSalesOrder.TrackingNum)
                ).ToList();

            int processedOrderCount = 0;
            int pendingOrderCount = toBeUpdatedOrders.Count;

            List<string> errMsgs = new List<string>();

            bool noException = true;
            string excepMsg = "";

            try
            {
                // Filter out the Orders without tracking Info
                foreach (QboSalesOrderWrapper salesOrderWrapper in toBeUpdatedOrders)
                {
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    var watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    processedOrderCount++;
                    pendingOrderCount--;

                    string curErrMsg = "";

                    try
                    {
                        bool isExisit = await _qboOrderDb.ExistsOrderAsync(salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId);

                        QboSyncStatus orderStatus = await _qboOrderDb.GetQboSyncStatus(salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId);
                        bool isValidQboSyncStatus = !orderStatus.Equals(QboSyncStatus.PendingSummary);
                        
                        SaleOrderQboType orderQboType = await _qboOrderDb.GetSaleOrderQboType(salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId);
                        bool isValidSaleOrderQboType = !(
                            orderQboType.Equals(SaleOrderQboType.DailySummaryInvoice) || 
                            orderQboType.Equals(SaleOrderQboType.DailySummarySalesReceipt)
                            );

                        if (isExisit && isValidQboSyncStatus && isValidSaleOrderQboType)
                        {
                            // Update Order Header with shipping info
                            await _qboOrderDb.UpdateOrderAsync(salesOrderWrapper.qboSalesOrder);
                        }
                        else if(!isExisit)
                        {
                            curErrMsg += QboOrderImportErrorMsgs.OrderUpdateErrorPrefix + salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId +
                                " " + QboOrderImportErrorMsgs.OrderUpdateNotExistInDbError;
                            errMsgs.Add(curErrMsg);
                            errorFlags.DigitBridgeOrderid.Add(salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId.ForceToTrimString());
                            continue;
                        }

                        // Add flag for this order into extension flag list
                        trackingDownloadedFlags.DigitBridgeOrderid.Add(salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId.ForceToTrimString());
                    }
                    catch (Exception ex)
                    {
                        errorFlags.DigitBridgeOrderid.Add(salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId.ForceToTrimString());
                        noException = false;
                        excepMsg += QboOrderImportErrorMsgs.OrderUpdateExceptionPrefix + 
                            salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId +
                            " : " + ex.Message + TextUtility.NewLine;
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    watch.Stop();
                    Console.WriteLine($"Execution Time for update {salesOrderWrapper.qboSalesOrder.DigitbridgeOrderId} : {watch.ElapsedMilliseconds} ms");
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                }

                if (trackingDownloadedFlags.DigitBridgeOrderid.Count > 0)
                {
                    await QuickBooksDbUtility.PostExtensionFlagsAsync(_extensionFlagsApiEndPoint, 
                        _extensionFlagsApiCode, trackingDownloadedFlags);
                }

                if (errorFlags.DigitBridgeOrderid.Count > 0)
                {
                    await QuickBooksDbUtility.PostExtensionFlagsAsync(_extensionFlagsApiEndPoint,
                        _extensionFlagsApiCode, errorFlags);
                }

                if (noException == false)
                {
                    throw new Exception(excepMsg);
                }

                foreach (string str in errMsgs) returnMsg += str;
                if (errMsgs.Count > 0)
                {
                    status = errMsgs.Count == processedOrderCount ? DetailResultStatus.Fail : DetailResultStatus.SuccessPartial;
                }
                return new DetailTransferResult(status, returnMsg, pendingOrderCount, processedOrderCount - errMsgs.Count);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }


}
