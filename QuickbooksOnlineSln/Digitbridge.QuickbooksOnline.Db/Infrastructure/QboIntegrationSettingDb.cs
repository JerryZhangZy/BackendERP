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
    public class QboIntegrationSettingDb
    {
        private MsSqlUniversal _msSql;
        private string _intgSettingTblName { get; set; }

        public QboIntegrationSettingDb(MsSqlUniversal msSql)
        {
            _msSql = msSql;
        }
        public QboIntegrationSettingDb(string intgSettingTblName, MsSqlUniversal msSql)
        {
            _intgSettingTblName = intgSettingTblName;
            _msSql = msSql;
        }

        public async Task<bool> ExistsIntegrationSettingAsync(Command command)
        {
            string selectSql = $"Select * from {_intgSettingTblName} " +
                $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";
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
        /// Full update an qboIntegrationSetting
        /// </summary>
        /// <param name="intgSettingDtb"></param>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns></returns>
        public async Task UpdateIntegrationSettingAsync(DataTable intgSettingDtb, Command command)
        {
            try
            {
                if (intgSettingDtb.Rows.Count > 0)
                {
                    intgSettingDtb.TableName = _intgSettingTblName;


                    intgSettingDtb.Columns.Remove("MasterAccountNum");
                    intgSettingDtb.Columns.Remove("ProfileNum");

                    string sql = QboDbUtility.BuildUpdateTableStatement(intgSettingDtb);

                    sql += $", LastUpdate = '{DateTime.UtcNow}'";

                    sql += $" where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

                    await QboDbUtility.RunInTransactionAsync(_msSql, sql);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task UpdateQboSettingStatus(Command command, QboSettingStatus settingStatus)
        {
            string sql = "";
            try
            {
                sql = $"Update {_intgSettingTblName} " +
                    $"Set QboSettingStatus = {(int)settingStatus}" +
                    $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";
                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        public async Task<bool> AddIntegrationSettingAsync(DataTable intgSettingDtb)
        {
            bool isSuccessful = false;

            try
            {
                if (intgSettingDtb.Rows.Count > 0)
                {
                    intgSettingDtb.TableName = _intgSettingTblName;

                    intgSettingDtb.Columns.Add("QboSettingStatus", typeof(int));

                    DataRow row = intgSettingDtb.Rows[0];

                    row["QboSettingStatus"] = (int)QboSettingStatus.Active;

                    string sql = QboDbUtility.BuildInsertTableStatement(intgSettingDtb);

                    isSuccessful = await _msSql.ExecuteNonQueryAsync(sql) == 1;
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }

            return isSuccessful;
        }
        public async Task<DataTable> GetIntegrationSettingAsync(Command command)
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_intgSettingTblName} " +
                $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";
                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        public async Task DeleteIntegrationSettingAsync(Command command)
        {
            string sql = "";
            try
            {
                sql = $"Delete From {_intgSettingTblName} " +
                $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        /// <summary>
        /// Get Integration Settings filter by [ExportOrderAs]
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> GetSummarySettingsAsync()
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_intgSettingTblName} " +
                $"where ExportOrderAs = {(int)SaleOrderQboType.DailySummaryInvoice} " +
                $"or ExportOrderAs = {(int)SaleOrderQboType.DailySummarySalesReceipt}";
                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        public async Task<DateTime> GetExportOrderFromDate(Command command)
        {
            DateTime dateTime = new DateTime();
            try
            {
                string sql = $"Select ExportOrderFromDate From {_intgSettingTblName} " +
                $"Where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

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

        public async Task<DateTime> GetExportOrderToDate(Command command)
        {
            DateTime dateTime = new DateTime();
            try
            {
                string sql = $"Select ExportOrderToDate From {_intgSettingTblName} " +
                $"Where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

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

        public async Task<OrderExportRule> GetOrderExportRule(Command command)
        {
            OrderExportRule rule = OrderExportRule.Null;
            try
            {
                string sql = $"Select ExportOrderAs From {_intgSettingTblName} " +
                $"Where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

                object result = await _msSql.ExecuteScalarAsync(sql);

                if (result != null && Enum.IsDefined(typeof(OrderExportRule), result.ForceToInt()))
                {
                    rule = (OrderExportRule)(int)result;
                }
                return rule;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
