using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.AzureStorage;
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
            payload.Options.IsImport = true;
            var blobService = new ImportBlobService();
            var success = await blobService.SaveToBlobAsync(payload);

            if (!success)
            {
                this.Messages.Add(payload.Messages);
                return false;
            }

            var queueService = new QueueService();
            success = await queueService.ImportInfoInQueueAsync(payload, erpEventType);
            if (!success)
            {
                this.Messages.Add(queueService.Messages);
                return false;
            }

            //var srv = new ImportExport.ImportExportMemoryTableService();
            //await srv.UpdateImportExportRecordAsync(payload);
            await UpdateImportRecordAsync(payload);
            return true;
        }

        private TableUniversal<ImportExportResult> tableUniversal;

        protected async Task<TableUniversal<ImportExportResult>> GetTableUniversalAsync()
        {
            if (tableUniversal == null)
            {
                tableUniversal = await TableUniversal<ImportExportResult>.CreateAsync(MySingletonAppSetting.ERPImportExportTableName, MySingletonAppSetting.ERPImportExportTableConnectionString);
            }
            return tableUniversal;
        }


        public async Task UpdateImportRecordAsync(ImportExportFilesPayload payload)
        {

            string rowKey = string.Empty;
            if (string.IsNullOrWhiteSpace(payload.ExportUuid))
                rowKey = payload.ImportUuid;
            else
                rowKey = payload.ExportUuid;
            var universal = await GetTableUniversalAsync();

            await universal.UpSertEntityAsync(payload.Result, "Import", rowKey);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowKey">import Or ExportUuid</param>
        /// <returns></returns>
        public async Task<ImportExportResult> GetExportRecordAsync(string rowKey)
        {
            var universal = await GetTableUniversalAsync();
            return await universal.GetEntityAsync(rowKey, "Import");
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
