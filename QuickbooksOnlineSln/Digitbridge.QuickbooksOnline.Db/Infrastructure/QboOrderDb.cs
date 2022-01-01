using Digitbridge.QuickbooksOnline.Db.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;

namespace Digitbridge.QuickbooksOnline.Db.Infrastructure
{
    public class QboOrderDb
    {
        protected MsSqlUniversal _msSql;
        protected string _orderTblName { get; set; }
        public QboOrderDb(MsSqlUniversal msSql)
        {
            _msSql = msSql;
        }
        public QboOrderDb(string orderTblName, MsSqlUniversal msSql)
        {
            _orderTblName = orderTblName;
            _msSql = msSql;
        }
        /// <summary>
        /// Check if Order exist by unique DigitbridgeOrderId
        /// </summary>
        /// <param name="digitbridgeOrderId"></param>
        /// <returns></returns>
        public async Task<bool> ExistsOrderAsync(String digitbridgeOrderId)
        {
            string selectSql = $"Select * from {_orderTblName} where DigitbridgeOrderId = '{digitbridgeOrderId}'";
            try
            {
                return await _msSql.ExistsAsync(selectSql);

            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Check if the order header record already been created is the same as current one from Api
        /// </summary>
        /// <param name="digitbridgeOrderId"></param>
        /// <param name="updatedTimeInDb"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfOrderModifiedAsync(String digitbridgeOrderId, DateTime updatedTimeInDb)
        {
            bool isModifiedAfterCreated = false;
            try
            {
                string queryOrderHeaderSql = $"Select CentralUpdatedTime From {_orderTblName} " +
                $"where DigitbridgeOrderId ='{digitbridgeOrderId}'";
                object result = await _msSql.ExecuteScalarAsync(queryOrderHeaderSql);
                if (result != null)
                {
                    isModifiedAfterCreated = !updatedTimeInDb.Equals((DateTime)result);
                }
                return isModifiedAfterCreated;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<bool> IsOrderTrackingUpdated(String digitbridgeOrderId)
        {
            string trkNum = "";
            try
            {
                string sql = $"Select TOP 1 TrackingNum From {_orderTblName} " +
                $"Where DigitbridgeOrderId = '{digitbridgeOrderId}'";

                object result = await _msSql.ExecuteScalarAsync(sql);

                if (result != null)
                {
                    trkNum = result.ForceToTrimString();
                }
                return !String.IsNullOrEmpty(trkNum);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<DateTime> GetLatestOrderEnterDate(Command command,
            QboSyncStatus filterStatus = QboSyncStatus.Null)
        {
            DateTime dateTime = new DateTime();
            try
            {
                string sql = $"Select TOP 1 EnterDate From {_orderTblName} " +
                $"Where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

                if (filterStatus != QboSyncStatus.Null)
                {
                    sql += $" and QboSyncStatus = '{(int)filterStatus}'";
                }

                sql += "ORDER BY CentralUpdatedTime DESC";

                object result = await _msSql.ExecuteScalarAsync(sql);

                if (result != null)
                {
                    dateTime = result.ForceToDateTime();
                }
                return dateTime;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<QboSyncStatus> GetQboSyncStatus(String digitbridgeOrderId)
        {
            int qboSyncStatus = -1;
            try
            {
                string sql = $"Select TOP 1 QboSyncStatus From {_orderTblName} " +
                $"Where DigitbridgeOrderId = '{digitbridgeOrderId}'";

                object result = await _msSql.ExecuteScalarAsync(sql);

                if (result != null)
                {
                    qboSyncStatus = result.ForceToInt();
                }
                return (QboSyncStatus)qboSyncStatus;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<SaleOrderQboType> GetSaleOrderQboType(String digitbridgeOrderId)
        {
            int saleOrderQboType = -1;
            try
            {
                string sql = $"Select TOP 1 SaleOrderQboType From {_orderTblName} " +
                $"Where DigitbridgeOrderId = '{digitbridgeOrderId}'";

                object result = await _msSql.ExecuteScalarAsync(sql);

                if (result != null)
                {
                    saleOrderQboType = result.ForceToInt();
                }
                return (SaleOrderQboType)saleOrderQboType;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<DateTime> GetLatestCentralUpdatedTime(Command command, bool isFilterExclude = false,
            QboSyncStatus filterStatus = QboSyncStatus.Null)
        {
            DateTime dateTime = new DateTime();
            try
            {
                string sql = $"Select TOP 1 CentralUpdatedTime From {_orderTblName} " +
                $"Where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

                if (filterStatus != QboSyncStatus.Null)
                {
                    sql += $" and QboSyncStatus " + ( isFilterExclude ? "!=" : "=" )
                        + $" '{(int)filterStatus}'";
                }

                sql += "ORDER BY CentralUpdatedTime DESC";

                object result = await _msSql.ExecuteScalarAsync(sql);

                if (result != null)
                {
                    dateTime = result.ForceToDateTime();
                }
                return dateTime;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Get the latest Order Update Time which QboSyncStatus has been updated
        /// </summary>
        /// <param name="command"></param>
        /// <param name="filterStatus"></param>
        /// <returns></returns>
        public async Task<DateTime> GetLatestSyncStatusUpdateTime(Command command, bool isFilterExclude = false,
            QboSyncStatus filterStatus = QboSyncStatus.Null)
        {
            DateTime dateTime = new DateTime();
            try
            {
                string sql = $"Select TOP 1 LastUpdate From {_orderTblName} " +
                $"Where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

                if (filterStatus != QboSyncStatus.Null)
                {
                    sql += $" and QboSyncStatus " + (isFilterExclude ? "!=" : "=") + $" '{(int)filterStatus}'";
                }

                sql += "ORDER BY LastUpdate DESC";

                object result = await _msSql.ExecuteScalarAsync(sql);

                if (result != null)
                {
                    dateTime = result.ForceToDateTime();
                }
                return dateTime;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Get Order Number by unique DigitbridgeOrderId for foreign key refs
        /// </summary>
        /// <param name="digitbridgeOrderId"></param>
        /// <returns></returns>
        public async Task<long> GetOrderNumAsync(String digitbridgeOrderId)
        {
            long salesOrderNum = 0;
            try
            {
                string queryOrderHeaderSql = $"Select SalesOrderNum From {_orderTblName} " +
                $"where DigitbridgeOrderId ='{digitbridgeOrderId}'";
                object result = await _msSql.ExecuteScalarAsync(queryOrderHeaderSql);
                if (result != null) salesOrderNum = result.ForceToLong();
                return salesOrderNum;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Add Order Header( QboSalesOrder ) and return SalesOrderNum
        /// </summary>
        /// <param name="qboSalesOrder"></param>
        /// <param name="qboItemLines"></param>
        /// <returns></returns>
        public async Task<bool> AddOrderAsync(DataTable qboSalesOrder)
        {
            try
            {
                bool isOrderHeaderCreated = false;

                if (qboSalesOrder.Rows.Count > 0)
                {
                    DataRow orderHeaderRow = qboSalesOrder.Rows[0];

                    string createOrderHeaderSql = SqlStatementUtility.BuildInsertTableStatement(
                        qboSalesOrder
                        , orderHeaderRow);
                    // If affect row is 1 then created successfully
                    isOrderHeaderCreated = await _msSql.ExecuteNonQueryAsync(createOrderHeaderSql) == 1;
                }
                return isOrderHeaderCreated;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Update shipping information for Order header
        /// </summary>
        /// <param name="qboSalesOrder"></param>
        /// <returns></returns>
        public async Task<int> UpdateOrderAsync(QboSalesOrder qboSalesOrder)
        {
            string sql = "";
            try
            {
                sql = $"Update {_orderTblName} " +
                    $"Set TrackingNum ='{qboSalesOrder.TrackingNum}'" +
                    $", ShipMethodRef ='{qboSalesOrder.ShipMethodRef}'" +
                    $", ShipDate ='{qboSalesOrder.ShipDate}'" +
                    $", LastUpdate = " +
                    $"'{DateTime.Now.ToUniversalTime().ToString(SqlCommandConsts.DateTimeFormatStringForLastUpdate)}'" +
                    $", QboSyncStatus = {(int)QboSyncStatus.ToBeUpdated} " +
                    $"where DigitbridgeOrderId = '{qboSalesOrder.DigitbridgeOrderId}' " +
                    $"and SaleOrderQboType in ({(int)SaleOrderQboType.Invoice}, " +
                    $"{(int)SaleOrderQboType.SalesReceipt}) ";
                    //$"and QboSyncStatus in ({(int)QboSyncStatus.SyncedSuccess}," +
                    //$"{(int)QboSyncStatus.UnSynced})";
                return await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }
        }
        /// <summary>
        /// Get orders from Db for posting to Quickbooks
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> GetOrdersByStatusInRangeAsync(QboSyncStatus status, Command command, 
            DateTime start, DateTime end, int channelAccNum = -1)
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_orderTblName} "  + 
                    QboDbUtility.GetOrderAndLineWhereFilter(status, command, start, end, channelAccNum);
                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }
        public async Task<DataTable> GetSummaryOrdersInRangeAsync(QboSyncStatus status, Command command,
            DateTime start, DateTime end, int channelAccNum)
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_orderTblName} " +
                    QboDbUtility.GetSummaryOrderAndLineWhereFilter(status, command, start, end, channelAccNum);

                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }
        }
        /// <summary>
        /// Update SaleOrderQboType after order has been exported to QBO for later reference
        /// </summary>
        /// <param name="salesOrderNum"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task UpdateCentralOrderQboType(long salesOrderNum, SaleOrderQboType type)
        {
            string sql = "";
            try
            {
                sql = $"Update {_orderTblName} " +
                    $"Set SaleOrderQboType ='{(int)type}'" +
                    $", LastUpdate ='{DateTime.Now.ToUniversalTime()}'" +
                    $"where SalesOrderNum = {salesOrderNum}";
                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }
        }
        /// <summary>
        /// Mark single order header status filter by salesOrderNum ( and SyncStatus )
        /// </summary>
        /// <param name="salesOrderNum"></param>
        /// <param name="status"></param>
        /// <param name="filterStatus"></param>
        /// <returns></returns>
        public async Task UpdateCentralOrderSyncStatus(long salesOrderNum, QboSyncStatus status,
            QboSyncStatus filterStatus = QboSyncStatus.Null)
        {
            string sql = "";
            try
            {
                sql = $"Update {_orderTblName} " +
                    $"Set QboSyncStatus ='{(int)status}'" +
                    $", LastUpdate =" +
                    $"'{DateTime.Now.ToUniversalTime().ToString(SqlCommandConsts.DateTimeFormatStringForLastUpdate)}' " +
                    $"where SalesOrderNum = {salesOrderNum}";
                if(filterStatus != QboSyncStatus.Null)
                {
                    sql += $" and QboSyncStatus = '{(int)filterStatus}'";
                }
                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }
        }
        public async Task<int> GetSummaryOrdersCount(QboSyncStatus status, Command command,
            DateTime start, DateTime end, int chnlAccNum = -1)
        {
            int count = 0;
            string sql = "";
            try
            {
                sql = $"Select COUNT(*) From {_orderTblName} " + 
                    QboDbUtility.GetSummaryOrderAndLineWhereFilter(status, command, start, end, chnlAccNum);
                object result = await _msSql.ExecuteScalarAsync(sql);
                if (result != null) count = result.ForceToInt();
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }
            return count;
        }
        /// <summary>
        /// Update multiple sales order headers sync status filter by current status (and chnlAccNum)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="count"></param>
        /// <param name="filterStatus"></param>
        /// <param name="status"></param>
        /// <param name="chnlAccNum"></param>
        /// <returns></returns>
        public async Task UpdateSummaryOrdersSyncStatus(Command command, DateTime start, DateTime end,
            int count, QboSyncStatus filterStatus, QboSyncStatus status, int chnlAccNum = -1)
        {
            string updateSql = "";
            try
            {
                updateSql = $"Update {_orderTblName} " +
                    $"Set QboSyncStatus ='{(int)status}'" +
                    $", LastUpdate =" +
                    $"'{DateTime.Now.ToUniversalTime().ToString(SqlCommandConsts.DateTimeFormatStringForLastUpdate)}'" +
                    QboDbUtility.GetSummaryOrderAndLineWhereFilter(filterStatus, command, start, end, chnlAccNum);
                await QboDbUtility.RunInTransactionAsync(_msSql, updateSql, count);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, updateSql);
            }

        }

    }   
}
