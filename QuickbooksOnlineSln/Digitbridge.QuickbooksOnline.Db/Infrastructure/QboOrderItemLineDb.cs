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
    public class QboOrderItemLineDb
    {
        protected MsSqlUniversal _msSql;
        protected string _orderItemLineTblName { get; set; }
        public QboOrderItemLineDb(MsSqlUniversal msSql)
        {
            _msSql = msSql;
        }
        public QboOrderItemLineDb(string orderItemLineTblName, MsSqlUniversal msSql)
        {
            _orderItemLineTblName = orderItemLineTblName;
            _msSql = msSql;
        }

        /// <summary>
        /// Get Order Item Lines As Datatable by SalesOrderNum(if Specified) and Sync Status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="salesOrderNum"></param>
        /// <returns></returns>
        public async Task<DataTable> GetOrderItemLineByStatusAsync(QboSyncStatus status, long salesOrderNum = 0)
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_orderItemLineTblName} " + $"Where QboSyncStatus = {(int)status} ";

                if(salesOrderNum != 0)
                {
                    sql += $"and SalesOrderNum = {salesOrderNum}";
                }
                
                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }
        }

        public async Task<DataTable> GetSummaryItemLinesInRangeAsync(QboSyncStatus status, Command command,
            DateTime start, DateTime end, int channelAccNum)
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_orderItemLineTblName} " +
                    QboDbUtility.GetSummaryOrderAndLineWhereFilter(status, command, start, end, channelAccNum);

                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }
        }

        public async Task<DataTable> GetItemLinesByStatusInRangeAsync(QboSyncStatus status, Command command,
            DateTime start, DateTime end, int channelAccNum = -1)
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_orderItemLineTblName} " +
                    QboDbUtility.GetOrderAndLineWhereFilter(status, command, start, end, channelAccNum);
                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        /// <summary>
        /// Check if there is any Line Item exists in database for specific salesOrderNum
        /// </summary>
        /// <param name="salesOrderNum"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAnyOrderItemLineAsync(long salesOrderNum)
        {
            string selectSql = $"Select * from {_orderItemLineTblName} where SalesOrderNum = '{salesOrderNum}'";
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
        /// Add Order Item Line s with salesOrderNum provided from Sales Order( header ) assigned 
        /// </summary>
        /// <param name="qboSalesOrderItemLines"></param>
        /// <param name="salesOrderNum"></param>
        /// <returns></returns>
        public async Task AddOrderItemLinesAsync(DataTable qboSalesOrderItemLines, long salesOrderNum)
        {
            try
            {
                Dictionary<string, int> sqlDic = new Dictionary<string, int>();
                foreach( DataRow curSalesItemLineRow in qboSalesOrderItemLines.Rows )
                {
                    // assign foreign key 
                    curSalesItemLineRow["SalesOrderNum"] = salesOrderNum; 
                    
                    string createItemLineSql = SqlStatementUtility.BuildInsertTableStatement(
                        qboSalesOrderItemLines
                        , curSalesItemLineRow);
                    // add sql insert command string for transaction at once later
                    sqlDic.Add(createItemLineSql, 1);
                }
                await _msSql.TransactionExecuteNonQueryAsync(sqlDic);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<int> GetSummaryOrderItemLinesCount(QboSyncStatus filterStatus, Command command,
            DateTime start, DateTime end, int chnlAccNum = -1)
        {
            int count = 0;
            string sql = "";
            try
            {
                sql = $"Select COUNT(*) From {_orderItemLineTblName} " +
                    QboDbUtility.GetSummaryOrderAndLineWhereFilter(filterStatus, command, start, end, chnlAccNum);
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
        /// Update item lines from multiple sales order sync status filter by current status
        /// </summary>
        /// <param name="command"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="count"></param>
        /// <param name="curStatus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task UpdateSummaryOrderItemLinesSyncStatus(Command command, DateTime start, DateTime end,
            int count, QboSyncStatus filterStatus, QboSyncStatus status, int chnlAccNum = -1)
        {
            string updateSql = "";
            try
            {
                updateSql = $"Update {_orderItemLineTblName} " +
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

        /// <summary>
        /// Update item lines from sindgle sales order sync status filter by salesOrderNum (and current status)
        /// </summary>
        /// <param name="salesOrderNum"></param>
        /// <param name="status"></param>
        /// <param name="filterStatus"></param>
        /// <returns></returns>
        public async Task UpdateCentralOrderItemLinesSyncStatus(long salesOrderNum, QboSyncStatus status,
            int rowCount = 1, QboSyncStatus filterStatus = QboSyncStatus.Null)
        {
            string sql = "";
            try
            {
                sql = $"Update {_orderItemLineTblName} " +
                    $"Set QboSyncStatus ='{(int)status}'" +
                    $", LastUpdate =" +
                    $"'{DateTime.Now.ToUniversalTime().ToString(SqlCommandConsts.DateTimeFormatStringForLastUpdate)}'" +
                    $"where SalesOrderNum = {salesOrderNum}";
                if (filterStatus != QboSyncStatus.Null)
                {
                    sql += $" and QboSyncStatus = '{(int)filterStatus}'";
                }
                await QboDbUtility.RunInTransactionAsync(_msSql, sql, rowCount);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }
        /// <summary>
        /// Update an item line from sindgle sales order sync status filter by salesOrderNum (and current status)
        /// </summary>
        /// <param name="salesOrderNum"></param>
        /// <param name="status"></param>
        /// <param name="filterStatus"></param>
        /// <returns></returns>
        public async Task UpdateCentralOrderItemLineSyncStatus(long itemLineNum, QboSyncStatus status
            , QboSyncStatus filterStatus = QboSyncStatus.Null)
        {
            string sql = "";
            try
            {
                sql = $"Update {_orderItemLineTblName} " +
                    $"Set QboSyncStatus ='{(int)status}'" +
                    $", LastUpdate =" +
                    $"'{DateTime.Now.ToUniversalTime().ToString(SqlCommandConsts.DateTimeFormatStringForLastUpdate)}'" +
                    $"where ItemLineNum = {itemLineNum}";
                if (filterStatus != QboSyncStatus.Null)
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

        public async Task UpdateCentralOrderItemLinesQboItemId(long itemLineNum, int itemId)
        {
            string sql = "";
            try
            {
                sql = $"Update {_orderItemLineTblName} " +
                    $"Set ItemRef ='{itemId}'" +
                    $", LastUpdate =" +
                    $"'{DateTime.Now.ToUniversalTime().ToString(SqlCommandConsts.DateTimeFormatStringForLastUpdate)}'" +
                    $"where ItemLineNum = {itemLineNum}";

                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }
        }


    }
}
