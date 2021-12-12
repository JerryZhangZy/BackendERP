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
    public class IntegrationWMSShipmentApi : IMessage
    {
        public IntegrationWMSShipmentApi() { }
        public IntegrationWMSShipmentApi(IDataBaseFactory _dbFactory)
        {
            SetDataBaseFactory(_dbFactory);
        }
        public IntegrationWMSShipmentApi(string baseUrl, string authCode)
        {
            _WMSShipmentResendClient = new WMSShipmentResendClient(baseUrl, authCode);
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


        #region Service
        private WMSShipmentResendClient _WMSShipmentResendClient;
        protected WMSShipmentResendClient WMSShipmentResendClient
        {
            get
            {
                if (_WMSShipmentResendClient is null)
                    _WMSShipmentResendClient = new WMSShipmentResendClient();
                return _WMSShipmentResendClient;
            }
        }
        #endregion

        /// <summary>
        /// send ChannelOrder to erp.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="WMSShipmentID"></param>
        /// <returns></returns>
        public virtual async Task<bool> ReSendWMSShipmentToErpAsync(OrderShipmentPayload payload)
        {
            if (!payload.HasWMSShipmentIDs)
            {
                AddInfo("WMSShipmentIDs cann't be empty.");
                return false;
            }

            payload.Success = await WMSShipmentResendClient.ResendWMSShipmentToErpAsync(payload.MasterAccountNum, payload.ProfileNum, payload.WMSShipmentIDs);

            payload.Messages = WMSShipmentResendClient.Messages;

            payload.SentWMSShipmentIDs = WMSShipmentResendClient.ResopneData.SentWMSShipmentIDs;

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
