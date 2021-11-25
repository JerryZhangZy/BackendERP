using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPApiSDK;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class IntegrationCentralOrderApi : IMessage
    {
        public IntegrationCentralOrderApi() { }
        public IntegrationCentralOrderApi(string baseUrl, string authCode)
        {
            _centralOrderClient = new CentralOrderClient(baseUrl, authCode);
        }
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
        /// <summary>
        /// send central order to erp.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="centralOrderUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> SendCentralOrderToErpAsync(int masterAccountNum, int profileNum, string centralOrderUuid)
        {
            var success = await centralOrderClient.CentralOrderToErpAsync(masterAccountNum, profileNum, centralOrderUuid);
            if (!success)
            {
                this.Messages.Add(centralOrderClient.Messages);
            }
            return success;
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
