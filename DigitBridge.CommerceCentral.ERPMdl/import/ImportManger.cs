﻿using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ImportManger : IMessage
    {
        /// <summary>
        /// Import file to blob and send message to queue
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="erpEventType"></param>
        /// <returns></returns>
        public async Task<bool> SendToBlobAndQueue(ImportExportFilesPayload payload, ErpEventType erpEventType)
        {
            var blobService = new ImportBlobService();
            var success = await blobService.SaveToBlobAsync(payload);

            if (!success)
            {
                //Add save to blob error to current message.
                return false;
            }

            var queueService = new QueueService();
            success = await queueService.InQueueAsync(payload, erpEventType);
            if (!success)
            {
                this.Messages.Add(queueService.Messages);
                return false;
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