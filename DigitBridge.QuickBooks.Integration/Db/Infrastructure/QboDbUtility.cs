using DigitBridge.QuickBooks.Integration.Model;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;

namespace DigitBridge.QuickBooks.Integration.Db.Infrastructure
{
    public class QboDbUtility
    {
        /// <summary>
        /// Add the sql statement, usually Update or Delete into a transaction.
        /// If the affected row count does't match the expectedRowRow, roll back the
        /// transaction, and throw exception.
        /// This function needs to be re-written to handle isolation level
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <param name="expectedRowChange"></param>
        public static async Task<int> RunInTransactionAsync(MsSqlUniversal msSql, string sqlStatement,
                int expectedRowCount = 1,
                IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted
                )
        {
            try
            {
                int affectedRowCount = await msSql.TransactionExecuteNonQueryAsync(sqlStatement, expectedRowCount,
                    false);
                return affectedRowCount;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static string GetSummaryOrderAndLineWhereFilter(QboSyncStatus status, Command command, DateTime start,
            DateTime end, int chnlAccNum = -1)
        {
            try
            {
                // If the user didn't specify the ExportOrderTo date then set NOW to ExportOrderTo
                end = end.Equals(DateTime.MinValue) ? DateTime.UtcNow : end;

                string chnlAccFilterStr = chnlAccNum != -1 ? $"and ChannelAccountNum = {chnlAccNum}" : "";
                return $"Where QboSyncStatus = {(int)status} " +
                $"and MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} " +
                $"and EnterDate BETWEEN '{start}' and '{end}' " + chnlAccFilterStr;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static string GetOrderAndLineWhereFilter(QboSyncStatus status, Command command, DateTime start, 
            DateTime end, int chnlAccNum = -1)
        {
            try
            {
                // If the user didn't specify the ExportOrderTo date then set NOW to ExportOrderTo
                end = end.Equals(DateTime.MinValue) ? DateTime.UtcNow : end;

                string chnlAccFilterStr = chnlAccNum != -1 ? $"and ChannelAccountNum = {chnlAccNum}" : "";
                return $"Where QboSyncStatus = {(int)status} " +
                $"and MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} " +
                $"and CentralCreateTime BETWEEN '{start}' and '{end}' " + chnlAccFilterStr;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static string BuildUpdateTableStatement(DataTable colDt)
        {
            string tableName = colDt.TableName;

            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception("Empty table name to insert.");
            }
            
            DataColumn[] tableColumns = SqlStatementUtility.GetDataTableColumns(colDt);
            DataRowCollection dataRows = colDt.Rows;

            StringBuilder sqlStatement =
                new StringBuilder("Update " + tableName + " Set ");
            try
            {
                if (dataRows.Count > 1)
                {
                    throw new ArgumentException("More than one Row is passed to update, can only take one at a time.");
                }

                int length = tableColumns.Length;
                DataRow dataRow = dataRows[0];

                for (int index = 0; index < length; index++)
                {
                    DataColumn dc = tableColumns[index];
                    if (dc.AutoIncrement)
                    {
                        continue;
                    }
                    sqlStatement.Append( dc.ColumnName + " = ");

                    if (dataRow[index] == DBNull.Value)
                    {
                        sqlStatement.Append("NULL");
                    }
                    else
                    {
                        string fieldValue = ConvertUtility.Trim(dataRow[index]);
                        string fieldType = dc.DataType.Name;
                        //sqlStatement.Append(BuildSqlValue(fieldValue, fieldType));
                        sqlStatement.Append(SqlStatementUtility.BuildSqlValue(fieldValue, fieldType));
                    }

                    if (index != (length - 1))
                    {
                        sqlStatement.Append(", ");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }

            return sqlStatement.ToString();
        }

        /// <summary>
        /// Build Insert table statement for one or multiple rows.
        /// </summary>
        /// <param name="colDt"></param>
        /// <param name="dataRows"></param>
        /// <returns></returns>
        public static string BuildInsertTableStatement(DataTable colDt, DataRow dataRow = null)
        {
            DataColumn[] cols = SqlStatementUtility.GetDataTableColumns(colDt);

            if (dataRow != null)
            {
                return BuildInsertTableStatement(colDt.TableName, cols, new[] { dataRow }.CopyToDataTable().Rows);
            }
            return BuildInsertTableStatement(colDt.TableName, cols, colDt.Rows);
        }

        public static string BuildInsertTableStatement(string tableName, DataColumn[] tableColumns, DataRowCollection dataRows)
        {
            try
            {
                if (string.IsNullOrEmpty(tableName))
                {
                    throw new Exception("Empty table name to insert.");
                }

                StringBuilder sqlStatement =
                   new StringBuilder("Insert Into " + tableName + " (");

                int length = tableColumns.Length;

                for (int index = 0; index < length; index++)
                {
                    DataColumn dc = tableColumns[index];
                    if (dc.AutoIncrement)
                    {
                        continue;
                    }
                    sqlStatement.Append("[" + dc.ColumnName + "]");
                    if (index != (length - 1))
                    {
                        sqlStatement.Append(", ");

                    }
                    else
                    {
                        sqlStatement.Append(") ");
                    }
                }

                sqlStatement.Append(" Values");

                foreach (var rowIndexPair in dataRows.Cast<DataRow>()
                                       .Select((r, i) => new { Row = r, Index = i }))
                {
                    DataRow dataRow = rowIndexPair.Row;

                    sqlStatement.Append(rowIndexPair.Index == 0 ? "(" : ",(");

                    for (int index = 0; index < length; index++)
                    {
                        DataColumn dc = tableColumns[index];
                        if (dc.AutoIncrement)
                        {
                            continue;
                        }
                        if (dataRow[index] == DBNull.Value)
                        {
                            sqlStatement.Append("NULL");
                        }
                        else
                        {
                            string fieldValue = ConvertUtility.Trim(dataRow[index]);
                            string fieldType = dc.DataType.Name;
                            //sqlStatement.Append(BuildSqlValue(fieldValue, fieldType));
                            sqlStatement.Append(SqlStatementUtility.BuildSqlValue(fieldValue, fieldType));
                        }

                        if (index != (length - 1))
                        {
                            sqlStatement.Append(", ");

                        }
                        else
                        {
                            sqlStatement.Append(") ");
                        }
                    }
                }

                return sqlStatement.ToString();
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }

        }
    }
}
