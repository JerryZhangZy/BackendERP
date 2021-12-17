using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.AzureStorage;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class QueueService : IMessage
    {
        public string ConnectionString
        {
            get => MySingletonAppSetting.AzureWebJobsStorage;
        }

        public async Task<bool> InQueueAsync(ImportExportFilesPayload payload, ErpEventType erpEventType)
        {
            try
            {
                var message = new ERPQueueMessage()
                {
                    MasterAccountNum = payload.MasterAccountNum,
                    ProfileNum = payload.ProfileNum,
                    DatabaseNum = payload.DatabaseNum,
                    ProcessUuid = payload.ImportUuid,
                    ERPEventType = erpEventType,
                };
                var queueName = message.ERPEventType.GetErpEventQueueName();
                await QueueUniversal<ERPQueueMessage>.SendMessageAsync(queueName, ConnectionString, message);
                return true;
            }
            catch (Exception e)
            {
                AddError(e.ObjectToString());
                return false;
            }
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
