using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPApiSDK;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class IntegrationCentralOrderApi : IMessage
    {
        public IntegrationCentralOrderApi() { }
        public IntegrationCentralOrderApi(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
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
        public IntegrationCentralOrderApi(string baseUrl, string authCode)
        {
            _centralOrderClient = new CentralOrderClient(baseUrl, authCode);
        }

        #region Service
        private CentralOrderClient _centralOrderClient;
        protected CentralOrderClient centralOrderClient
        {
            get
            {
                if (_centralOrderClient is null)
                    _centralOrderClient = new CentralOrderClient();
                return _centralOrderClient;
            }
        }
        #endregion

        /// <summary>
        /// send ChannelOrder to erp.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="centralOrderUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> ReSendCentralOrderToErpAsync(ChannelOrderPayload payload, string centralOrderUuid)
        {
            if (centralOrderUuid.IsZero())
            {
                AddInfo("CentralOrderUuid cann't be empty.");
                return false;
            }
            if (payload.SentCentralOrderUuids is null)
            {
                payload.SentCentralOrderUuids = new List<string>();
            }
            var success = await centralOrderClient.CentralOrderToErpAsync(payload.MasterAccountNum, payload.ProfileNum, centralOrderUuid);
            if (success)
            {
                payload.SentCentralOrderUuids.Add(centralOrderUuid);
            }
            else
            {
                this.Messages.Add(centralOrderClient.Messages);
            }
            return success;
        }

        /// <summary>
        /// send ChannelOrder to erp.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="centralOrderUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> ReSendAllCentralOrderToErp(ChannelOrderPayload payload)
        { 
            var srv = new CentralOrderList(dbFactory, new CentralOrderQuery());
            await srv.GetChannelOrderListAsync(payload);
            if (!payload.Success)
            {
                this.Messages.Add(srv.Messages);
                return false;
            }
            if (payload.ChannelOrderListCount == 0)
            {
                AddInfo("ChannelOrderListCount is 0");
                return true;
            }

            var jsonData = payload.ChannelOrderList.ToString();

            if (jsonData.IsZero())
            {
                AddInfo("ChannelOrderList is empty.");
                return true;
            }

            var queryResult = JArray.Parse(jsonData);
            payload.MatchedCentralOrderUuids = queryResult.Select(i => i.Value<string>("centralOrderUuid")).Distinct().ToList();

            foreach (var centralOrderUuid in payload.MatchedCentralOrderUuids)
            {
                await ReSendCentralOrderToErpAsync(payload, centralOrderUuid);
            }
            return true;
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
