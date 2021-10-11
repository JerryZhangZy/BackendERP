using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.QuickBooks.Integration.Connection.Model;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.OAuth2PlatformClient;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UneedgoHelper.DotNet.Common;
using Task = System.Threading.Tasks.Task;

namespace DigitBridge.QuickBooks.Integration.Mdl.Qbo
{
    public class QboServiceBase : IMessage
    {
        protected OAuth2Client _auth2Client;
        private DataService _dataService;
        private ServiceContext _serviceContext;
        protected OAuth2RequestValidator _oauthValidator;
        protected QuickBooksConnectionInfo _qboConnectionInfo;
        private QuickBooksConnectionInfoService quickBooksConnectionInfoService;
        private QboConnectionTokenStatus _qboConnectionTokenStatus;
        private ConnectionTokenStatus _connectionStatus = ConnectionTokenStatus.UnInitalized;
        protected IDataBaseFactory dbFactory;
        protected IPayload payload;
        protected QuickBooksExportLog _exportLog;

        private QboIntegrationSetting _qboIntegrationSetting;

        protected async Task<QboIntegrationSetting> IntegrationSetting()
        {
            if (_qboIntegrationSetting == null)
            {
                if (await QuickBooksSettingInfoService.GetByPayloadAsync(payload))
                {
                    _qboIntegrationSetting = QuickBooksSettingInfoService.Data.QuickBooksSettingInfo.SettingInfo;
                }
            }
            return _qboIntegrationSetting;

        }

        public QboServiceBase() { }

        public QboServiceBase(IPayload pl, IDataBaseFactory databaseFactory)
        {
            this.payload = pl;
            this.dbFactory = databaseFactory;
            quickBooksConnectionInfoService = new QuickBooksConnectionInfoService(dbFactory);
            GetQuickBooksConnectionInfo();
            if (_qboConnectionInfo == null)
            {
                AddError($"Initialize Error,MasterAccountNum:{payload.MasterAccountNum} with ProfileNum:{payload.ProfileNum} is uninitialized");
                //throw new Exception($"Initialize Error,MasterAccountNum:{payload.MasterAccountNum} with ProfileNum:{payload.ProfileNum} is uninitialized");
            }
            ConnectToDataServiceAsync();
        }

        private async Task CheckInitialed()
        {
            if (_connectionStatus == ConnectionTokenStatus.UnInitalized)
            {
                await HandleConnectivityAsync();
            }
        }

        public async Task<T> AddDataAsync<T>(T entity) where T : IEntity
        {
            await CheckInitialed();
            return _dataService.Add(entity);
        }

        public async Task<T> UpdateDataAsync<T>(T entity) where T : IEntity
        {
            await CheckInitialed();
            return _dataService.Update(entity);
        }

        public async Task<T> DeleteDataAsync<T>(T entity) where T : IEntity
        {
            await CheckInitialed();
            return _dataService.Delete(entity);
        }

        public async Task<T> VoidDataAsync<T>(T entity) where T : IEntity
        {
            await CheckInitialed();
            return _dataService.Void(entity);
        }

        public async Task<QueryService<T>> GetQueryServiceAsync<T>()
        {
            await CheckInitialed();
            return new QueryService<T>(_serviceContext);
        }
        public async Task<T> FindByIdAsync<T>(T entity) where T : IEntity
        {
            await CheckInitialed();
            return _dataService.FindById(entity);
        }
        #region Messages

        public bool HasMessages => Messages.Count > 0;

        protected IList<MessageClass> _messages;
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (_messages is null)
                    _messages = new List<MessageClass>();
                return _messages;
            }
            set { _messages = value; }
        }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             Messages.Add(message, MessageLevel.Info, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Warning, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Error, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Fatal, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Debug, code);

        #endregion Messages

        protected async Task<QuickBooksConnectionInfo> GetQuickBooksConnectionInfoAsync()
        {
            if (_qboConnectionInfo == null)
            {
                if (await quickBooksConnectionInfoService.GetByPayloadAsync(payload))
                {
                    _qboConnectionInfo = quickBooksConnectionInfoService.Data.QuickBooksConnectionInfo;
                }
            }
            return _qboConnectionInfo;
        }

        protected QuickBooksConnectionInfo GetQuickBooksConnectionInfo()
        {
            if (_qboConnectionInfo == null)
            {
                if (quickBooksConnectionInfoService.GetByPayload(payload))
                {
                    _qboConnectionInfo = quickBooksConnectionInfoService.Data.QuickBooksConnectionInfo;
                }
            }
            return _qboConnectionInfo;
        }

        //private async Task<QboServiceBase> InitializeAsync(
        //    QuickBooksConnectionInfo qboConnectionInfo, IDataBaseFactory databaseFactory)
        //{
        //    try
        //    {
        //        _qboConnectionInfo = qboConnectionInfo;
        //        dbFactory = databaseFactory;
        //        ConnectToDataServiceAsync();
        //        await HandleConnectivityAsync();
        //        return this;
        //    }
        //    catch (Exception ex)
        //    {
        //        string additionalMsg = "Qbo Universal Initialize Error" + CommonConst.NewLine;
        //        throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
        //    }
        //}

        //public static Task<QboServiceBase> CreateAsync(
        //    QuickBooksConnectionInfo qboConnectionInfo, IDataBaseFactory databaseFactory)
        //{
        //    try
        //    {
        //        var newInstance = new QboServiceBase();
        //        return newInstance.InitializeAsync(qboConnectionInfo, databaseFactory);
        //    }
        //    catch (Exception ex)
        //    {
        //        string additionalMsg = "Qbo Universal CreateAsync Error" + CommonConst.NewLine;
        //        throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
        //    }

        //}

        private void ConnectToDataServiceAsync()
        {
            try
            {
                _auth2Client = new OAuth2Client(_qboConnectionInfo.ClientId, _qboConnectionInfo.ClientSecret
                    , MyAppSetting.RedirectUrl, MyAppSetting.Environment);
                _oauthValidator = new OAuth2RequestValidator(_qboConnectionInfo.AccessToken);
                _serviceContext = new ServiceContext(_qboConnectionInfo.RealmId, IntuitServicesType.QBO, _oauthValidator);
                _serviceContext.IppConfiguration.MinorVersion.Qbo = MyAppSetting.MinorVersion;
                _serviceContext.IppConfiguration.BaseUrl.Qbo = MyAppSetting.BaseUrl;
                _dataService = new DataService(_serviceContext);
                _qboConnectionTokenStatus = new QboConnectionTokenStatus();
            }
            catch (Exception ex)
            {
                AddError("Qbo Connection GetServiceContext Error");
                string additionalMsg = "Qbo Connection GetServiceContext Error" + CommonConst.NewLine;
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
            }
        }

        private async Task HandleConnectivityAsync()
        {
            try
            {
                // Check if the refresh token is too old, offical set life = 100 days
                if ((DateTime.Now.ToUniversalTime() - _qboConnectionInfo.LastRefreshTokUpdate.ForceToDateTime()).TotalDays > 80)
                {
                    await UpdateRefreshTokenAsync();
                }
                // Check if the access token is too old, offical set life = 1 hr
                if ((DateTime.Now.ToUniversalTime() - _qboConnectionInfo.LastAccessTokUpdate.ForceToDateTime()).TotalHours >= 0.75)
                {
                    await RefreshAccessTokenAsync();
                }
                // Use a simple API call to see if the access token is valid ( officially recommended method )
                CompanyInfo companyInfo = await GetCompanyInfoAsync();
                _connectionStatus = ConnectionTokenStatus.Initalized;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message == QboUniversalConsts.Connection401Error)
                {
                    await RefreshAccessTokenAsync();
                }
                else
                {
                    string additionalMsg = "Qbo Handle Connectivity Error" + CommonConst.NewLine;
                    throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
                }
            }
        }

        /// <summary>
        /// Use the Authorization Code to get the refresh token which will expire after 100 days after created
        /// </summary>
        /// <returns></returns>
        private async Task UpdateRefreshTokenAsync()
        {
            try
            {
                TokenResponse tokenResp = await _auth2Client.GetBearerTokenAsync(_qboConnectionInfo.AuthCode);
                _qboConnectionInfo.RefreshToken = tokenResp.RefreshToken;
                await RefreshAccessTokenAsync(true);
            }
            catch (Exception ex)
            {
                _qboConnectionTokenStatus.RefreshTokenStatus = ConnectionTokenStatus.UpdatedWithError;
                string additionalMsg = "Qbo Connection Update Refresh Token Error" + CommonConst.NewLine;
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
            }
        }

        private async Task RefreshAccessTokenAsync(bool runAfterRefreshUpdated = false)
        {
            try
            {
                TokenResponse tokenResp = await _auth2Client.RefreshTokenAsync(_qboConnectionInfo.RefreshToken);
                if (tokenResp.AccessToken != null)
                {
                    _qboConnectionInfo.AccessToken = tokenResp.AccessToken;
                    ConnectToDataServiceAsync(); // reconnect to data service after token refreshing
                    // Update time to be insert into DB table (UTC)
                    _qboConnectionInfo.LastAccessTokUpdate = DateTime.Now.ToUniversalTime();
                    // Flag tokens as updated
                    _qboConnectionTokenStatus.AccessTokenStatus = ConnectionTokenStatus.Updated;

                    if (!_qboConnectionInfo.RefreshToken.Equals(tokenResp.RefreshToken))
                    {
                        _qboConnectionInfo.RefreshToken = tokenResp.RefreshToken;
                        // Update time to be inserted into DB table(UTC)
                        _qboConnectionInfo.LastRefreshTokUpdate = DateTime.Now.ToUniversalTime();
                        // Flag tokens as updated
                        _qboConnectionTokenStatus.RefreshTokenStatus = ConnectionTokenStatus.Updated;
                    }
                    await _qboConnectionInfo.SetDataBaseFactory(dbFactory).SaveAsync();
                }
                else if (runAfterRefreshUpdated == false)
                {
                    await UpdateRefreshTokenAsync();
                }
                else
                {
                    throw new Exception("Refresh Access Token use the Updated Refresh Token Failed, " +
                        $"Check Auth Code For User: {_qboConnectionInfo.MasterAccountNum} {_qboConnectionInfo.ProfileNum}, " +
                        $"Error Msg: {tokenResp.Error}");
                }
            }
            catch (Exception ex)
            {
                _qboConnectionTokenStatus.AccessTokenStatus = ConnectionTokenStatus.UpdatedWithError;
                string additionalMsg = "Qbo Connection Refresh Access Token Error" + CommonConst.NewLine;
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
            }
        }

        public async Task<CompanyInfo> GetCompanyInfoAsync()
        {
            try
            {
                QueryService<CompanyInfo> queryService = new QueryService<CompanyInfo>(_serviceContext);
                return queryService.ExecuteIdsQuery("select * from CompanyInfo").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Return ( isTransactionExist, isTransactioniUpToDate )
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<(bool, bool)> IsSalesTransactionExistAndUpToDateAsync(SalesTransaction transaction)
        {
            var queryService = new QueryService<SalesTransaction>(_serviceContext);
            var qboQureyStr = $"select * from {transaction.GetType().Name} Where DocNumber = '{transaction.DocNumber}'";
            var latestTransaction = queryService.ExecuteIdsQuery(qboQureyStr).FirstOrDefault();

            return (latestTransaction != null,
                latestTransaction != null && latestTransaction.SyncToken.Equals(transaction.SyncToken));
        }

        private QuickBooksExportLogService _quickBooksExportLogService;
        protected QuickBooksExportLogService QuickBooksExportLogService
        {
            get
            {
                if (_quickBooksExportLogService == null)
                    _quickBooksExportLogService = new QuickBooksExportLogService(dbFactory);
                return _quickBooksExportLogService;
            }
        }

        private QuickBooksSettingInfoService _quickBooksSettingInfoService;
        public QuickBooksSettingInfoService QuickBooksSettingInfoService
        {
            get
            {
                if (_quickBooksSettingInfoService == null)
                    _quickBooksSettingInfoService = new QuickBooksSettingInfoService(dbFactory);
                return _quickBooksSettingInfoService;
            }
        }

        public virtual async Task<bool> SaveExportLogAsync()
        {
            _exportLog.DatabaseNum = payload.DatabaseNum;
            _exportLog.MasterAccountNum = payload.MasterAccountNum;
            _exportLog.ProfileNum = payload.ProfileNum;
            _exportLog.LogDate = DateTime.UtcNow.Date;
            _exportLog.LogTime = DateTime.UtcNow.TimeOfDay;
            if (_exportLog.RowNum.IsZero())
            {
                _exportLog.QuickBooksExportLogUuid = Guid.NewGuid().ToString();
                _exportLog.BatchNum = 0;
                return await QuickBooksExportLogService.AddExportLogAsync(_exportLog);
            }
            else
            {
                return await QuickBooksExportLogService.UpdateExportLogAsync(_exportLog);
            }
        }

        protected async Task<bool> LoadExportLog(string uuid)
        {
            var list = await QuickBooksExportLogService.QueryExportLogByLogUuidAsync(uuid);
            if (list != null)
                _exportLog = list.FirstOrDefault();
            if (_exportLog == null)
                _exportLog = new QuickBooksExportLog();
            return true;
        }
    }
}
