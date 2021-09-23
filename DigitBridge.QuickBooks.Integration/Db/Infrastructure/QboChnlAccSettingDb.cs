using DigitBridge.QuickBooks.Integration.Model;
using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;

namespace DigitBridge.QuickBooks.Integration.Db.Infrastructure
{
    public class QboChnlAccSettingDb
    {
        private MsSqlUniversal _msSql;
        private string _accSettingTblName { get; set; }

        public QboChnlAccSettingDb(MsSqlUniversal msSql)
        {
            _msSql = msSql;
        }
        public QboChnlAccSettingDb(string accSettingTblName, MsSqlUniversal msSql)
        {
            _accSettingTblName = accSettingTblName;
            _msSql = msSql;
        }

        public async Task<bool> ExistsChnlAccSettingAsync(Command command, int chnlAccNum)
        {
            string selectSql = $"Select * from {_accSettingTblName}"+
                $" where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum}" +
                $" and ChannelAccountNum = {chnlAccNum}";
            try
            {
                return await _msSql.ExistsAsync(selectSql);

            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task UpdateDailySummaryLastExportAsync(Command command, int chnlAccNum)
        {
            try
            {
                string sql = $"Update {_accSettingTblName} Set DailySummaryLastExport = '{DateTime.UtcNow}'" +
                    $", LastUpdate = '{DateTime.UtcNow}' " +
                    $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum}" +
                    $" and ChannelAccountNum = {chnlAccNum}";

                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task UpdateChnlAccSettingAsync(DataTable chnlAccSettingDtb, int masterAccNum, int profileNum, int chnlAccNum)
        {

            try
            {
                if (chnlAccSettingDtb.Rows.Count > 0)
                {
                    chnlAccSettingDtb.TableName = _accSettingTblName;

                    chnlAccSettingDtb.Columns.Remove("MasterAccountNum");
                    chnlAccSettingDtb.Columns.Remove("ProfileNum");
                    chnlAccSettingDtb.Columns.Remove("ChannelAccountNum");

                    string sql = QboDbUtility.BuildUpdateTableStatement(chnlAccSettingDtb);

                    sql += $" ,LastUpdate = '{DateTime.UtcNow}' ";

                    sql += $"where MasterAccountNum = {masterAccNum} and ProfileNum = {profileNum}" +
                        $" and ChannelAccountNum = {chnlAccNum}";

                    await QboDbUtility.RunInTransactionAsync(_msSql, sql); ;
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }


        public async Task<bool> AddChnlAccSettingAsync(DataTable chnlAccSettingDtb)
        {
            bool isSuccessful = false;

            try
            {
                if (chnlAccSettingDtb.Rows.Count > 0)
                {
                    chnlAccSettingDtb.TableName = _accSettingTblName;

                    string sql = QboDbUtility.BuildInsertTableStatement(chnlAccSettingDtb);

                    isSuccessful = await _msSql.ExecuteNonQueryAsync(sql) == 1;
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }

            return isSuccessful;
        }

        public async Task<DataTable> GetChnlAccSettingsAsync(Command command)
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_accSettingTblName} " +
                $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";
                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        public async Task<DataTable> GetChnlAccSettingsBeforeAsync(Command command, DateTime endOfLastDay)
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_accSettingTblName} " +
                $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} " + 
                $"and ( DailySummaryLastExport < '{endOfLastDay}' or DailySummaryLastExport IS NULL ) ";
                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        public async Task DeleteChnlAccSettingsAsync(Command command, int count)
        {
            string sql = "";
            try
            {
                sql = $"Delete {_accSettingTblName} " +
                $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";
                await QboDbUtility.RunInTransactionAsync(_msSql, sql, count);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        /// <summary>
        /// Get Channel Account Customer Id using ChannelAccountNum ,MasterAccountNum and ProfileNum
        /// </summary>
        /// <param name="command"></param>
        /// <param name="channelAccNum"></param>
        /// <returns></returns>
        public async Task<string> GetChnlCustomerIdAsync(Command command, long channelAccNum)
        {
            string customerId = "";
            try
            {
                string queryCustomerIdSql = $"Select ChannelQboCustomerId From {_accSettingTblName} " +
                $"where ChannelAccountNum ='{channelAccNum}'" +
                $"and MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";
                object result = await _msSql.ExecuteScalarAsync(queryCustomerIdSql);
                if (result != null) customerId = result.ForceToTrimString();
                return customerId;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
