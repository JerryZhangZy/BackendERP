using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPApiSDK;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EventERPPayload = DigitBridge.CommerceCentral.ERPDb.EventERPPayload;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class IntegrationEventApi : IMessage
    {
        public IntegrationEventApi() { }
        public IntegrationEventApi(string baseUrl, string authCode)
        {
            _ErpEventResendClient = new ErpEventResendClient(baseUrl, authCode);
        }
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
        ///// <summary>
        ///// resend event by event uuid.
        ///// </summary>
        ///// <param name="payload"></param>
        ///// <param name="eventUuid"></param>
        ///// <returns></returns>
        //public virtual async Task<bool> ResendEventAsync(int masterAccountNum, int profileNum, string eventUuid)
        //{
        //    var success = await ErpEventResendClient.ResendEventAsync(masterAccountNum, profileNum, eventUuid);
        //    if (!success)
        //    {
        //        this.Messages.Add(ErpEventResendClient.Messages);
        //    }
        //    return success;
        //}

        /// <summary>
        /// resend event by array event uuid.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="eventUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> ResendEventAsync(EventERPPayload payload)
        {
            var success = await ErpEventResendClient.ResendEventAsync(payload.MasterAccountNum, payload.ProfileNum, payload.EventUuids);

            if (!success)
            {
                this.Messages.Add(ErpEventResendClient.Messages);
            }

            payload.SentEventUuids = ErpEventResendClient.ResopneData.SentEventUuids;

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
