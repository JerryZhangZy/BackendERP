using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPApiSDK;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EventERPPayload = DigitBridge.CommerceCentral.ERPDb.EventERPPayload;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class IntegrationEventApi : IMessage
    {
        public IntegrationEventApi() { }
        public IntegrationEventApi(IDataBaseFactory _dbFactory) { SetDataBaseFactory(_dbFactory); }
        public IntegrationEventApi(string baseUrl, string authCode)
        {
            _ErpEventResendClient = new ErpEventResendClient(baseUrl, authCode);
        }

        #region DataBase
        [XmlIgnore, JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [XmlIgnore, JsonIgnore]
        public IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public void SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion DataBase 

        private ErpEventResendClient _ErpEventResendClient;

        protected ErpEventResendClient ErpEventResendClient
        {
            get
            {
                if (_ErpEventResendClient is null)
                    _ErpEventResendClient = new ErpEventResendClient();
                return _ErpEventResendClient;
            }
        }

        /// <summary>
        /// resend event by array event uuid.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="eventUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> ResendEventAsync(EventERPPayload payload)
        {
            if (!payload.HasEventUuids)
            {
                AddError("EventUuids cann't be empty.");
                return false;
            }

            var success = await ErpEventResendClient.ResendEventAsync(payload.MasterAccountNum, payload.ProfileNum, payload.EventUuids);

            if (!success)
            {
                this.Messages.Add(ErpEventResendClient.Messages);
                return false;
            }

            payload.SentEventUuids = ErpEventResendClient.ResopneData?.SentEventUuids;

            return success;
        }

        /// <summary>
        /// resend event by search criteria
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="eventUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> ResendAllEventAsync(EventERPPayload payload)
        {
            var srv = new EventERPList(dbFactory, new EventERPQuery());
            payload.LoadAll = true;
            await srv.GetEventERPListAsync(payload);

            if (!payload.Success)
            {
                this.Messages.Add(srv.Messages);
                return false;
            }
            if (payload.EventERPListCount == 0)
            {
                AddInfo("EventERPListCount is 0");
                return true;
            }

            var jsonData = payload.EventERPList.ToString();

            if (jsonData.IsZero())
            {
                AddInfo("EventERPList is empty.");
                return true;
            }

            var queryResult = JArray.Parse(jsonData);
            payload.EventUuids = queryResult.Select(i => i.Value<string>("eventUuid")).Distinct().ToList();

            payload.EventERPList = null;

            if (!payload.HasEventUuids)
            {
                AddInfo("No event uuid found for search criteria.");
                return true;
            }

            return await ResendEventAsync(payload);
        }


        #region Messages
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
    }
}
