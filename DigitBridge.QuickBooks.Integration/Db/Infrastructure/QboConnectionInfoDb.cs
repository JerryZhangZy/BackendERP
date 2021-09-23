using DigitBridge.QuickBooks.Integration.Model;
using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;


namespace DigitBridge.QuickBooks.Integration.Db.Infrastructure
{
    public class QboConnectionInfoDb
    {
        private MsSqlUniversal _msSql;
        private string _conInfoTblName { get; set; }
        private string _cryptKey { get; set; }

        public QboConnectionInfoDb(MsSqlUniversal msSql)
        {
            _msSql = msSql;
        }
        public QboConnectionInfoDb(string conInfoTblName, MsSqlUniversal msSql, string cryptKey)
        {
            _conInfoTblName = conInfoTblName;
            _msSql = msSql;
        }

        public async Task<bool> ExistsConnectionInfoAsync(Command command)
        {
            string selectSql = $"Select * from {_conInfoTblName} " +
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

        public async Task<bool> AddConnectionInfoAsync(Command command, string clientId, string clientSecret, string state)
        {
            try
            {
                bool isConnectionInfoCreated = false;

                string sql = $"INSERT INTO {_conInfoTblName} (" +
                    $"MasterAccountNum, ProfileNum, ClientId, ClientSecret, RequestState ) VALUES (" +
                    $"{command.MasterAccountNum}, {command.ProfileNum}, '{clientId}', '{clientSecret}', '{state}' )";

                // If affect row is 1 then created successfully
                isConnectionInfoCreated = await _msSql.ExecuteNonQueryAsync(sql) == 1;

                return isConnectionInfoCreated;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task UpdateClientCredentialAsync(Command command, string clientId, string clientSecret, string state)
        {
            string sql = "";
            try
            {
                sql = $"Update {_conInfoTblName} " +
                    $"Set ClientId ='{clientId}'" +
                    $", ClientSecret ='{clientSecret}'" +
                    $", RequestState = '{state}'" +
                    $", LastUpdate = '{DateTime.Now.ToUniversalTime()}'" +
                    $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task UpdateQboOAuthTokenStatusAsync(Command command, QboOAuthTokenStatus qboOAuthTokenStatus)
        {
            string sql = "";
            try
            {
                sql = $"Update {_conInfoTblName} " +
                    $"Set QboOAuthTokenStatus ='{(int)qboOAuthTokenStatus}'" +
                    $", LastUpdate = '{DateTime.Now.ToUniversalTime()}'" +
                    $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<DataTable> GetConnectionInfoByCommandAsync(Command command)
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_conInfoTblName} " +
                $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";
                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        public async Task DeleteConnectionInfoByCommandAsync(Command command)
        {
            string sql = "";
            try
            {
                sql = $"Delete From {_conInfoTblName} " +
                $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";

                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }
        }

        public async Task<DataTable> GetConnectionInfoByRequestStateAsync(string state)
        {
            string sql = "";
            try
            {
                sql = $"Select * From {_conInfoTblName} where RequestState= '{state}'";
                DataTable dt = await _msSql.GetDataTableAsync(sql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        public async Task InitiateToken(string accToken, string refreshToken, string realmId, string authCode, Command command)
        {
            string sql = "";
            try
            {
                sql = $"Update {_conInfoTblName} " +
                    $"Set AccessToken ='{accToken}'" +
                    $", LastAccessTokUpdate ='{DateTime.Now.ToUniversalTime()}'" +
                    $", RefreshToken ='{refreshToken}'" +
                    $", LastRefreshTokUpdate ='{DateTime.Now.ToUniversalTime()}'" +
                    $", RealmId ='{realmId}'" +
                    $", AuthCode ='{authCode}'" +
                    $", LastUpdate = '{DateTime.Now.ToUniversalTime()}'" +
                    $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";
                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        public async Task UpdateQboAccessTokenAsync(string accToken, DateTime updateTime, Command command)
        {
            string sql = "";
            try
            {
                sql = $"Update {_conInfoTblName} " +
                    $"Set AccessToken ='{accToken}'" +
                    $", LastAccessTokUpdate ='{updateTime}'" +
                    $", LastUpdate = '{DateTime.Now.ToUniversalTime()}'" +
                    $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";
                await QboDbUtility.RunInTransactionAsync(_msSql ,sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }

        public async Task UpdateQboRefreshTokenAsync(string refreshToken, DateTime updateTime, Command command)
        {
            string sql = "";
            try
            {
                sql = $"Update {_conInfoTblName} " +
                    $"Set RefreshToken ='{refreshToken}'" +
                    $", LastRefreshTokUpdate ='{updateTime}'" +
                    $", LastUpdate = '{DateTime.Now.ToUniversalTime()}'" +
                    $"where MasterAccountNum = {command.MasterAccountNum} and ProfileNum = {command.ProfileNum} ";
                await QboDbUtility.RunInTransactionAsync(_msSql, sql);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, sql);
            }

        }
    }
}
