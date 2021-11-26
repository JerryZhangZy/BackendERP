using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPApiSDK;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class IntegrationEventApi : IMessage
    {
        public IntegrationEventApi() { }
        public IntegrationEventApi(string baseUrl, string authCode)
        {
            _erpEventClient = new ErpEventClient(baseUrl, authCode);
        }
        private ErpEventClient _erpEventClient;

        protected ErpEventClient erpEventClient
        {
            get
            {
                if (_erpEventClient is null)
                    _erpEventClient = new ErpEventClient();
                return _erpEventClient;
            }
        }
        /// <summary>
        /// resend event by event uuid.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="eventUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> ResendEventAsync(int masterAccountNum, int profileNum, string eventUuid)
        {
            var success = await erpEventClient.ResendEventAsync(masterAccountNum, profileNum, eventUuid);
            if (!success)
            {
                this.Messages.Add(erpEventClient.Messages);
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
