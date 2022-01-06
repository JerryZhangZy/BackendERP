using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.AzureStorage;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ExportManger : IMessage
    {
        /// <summary>
        /// Send export request to blob and queue 
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="erpEventType"></param>
        /// <returns></returns>
        public async Task<bool> SendToBlobAndQueue(ImportExportFilesPayload payload, ErpEventType erpEventType)
        {
            payload.Options.IsImport = false;
            var blobService = new ExportBlobService();
            var success = await blobService.SaveOptionsToBlobAsync(payload);

            if (!success)
            {
                this.Messages.Add(blobService.Messages);
                return false;
            }

            var queueService = new QueueService();
            success = await queueService.ExportInfoInQueueAsync(payload, erpEventType);
            if (!success)
            {
                this.Messages.Add(queueService.Messages);
                return false;
            }
            //var srv = new ImportExport.ImportExportMemoryTableService();
            //await srv.UpdateImportExportRecordAsync(payload);
            await UpdateExportRecordAsync(payload);
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




        public async Task<bool> FindExportProcessUuidsAsync(IDataBaseFactory dataBaseFactory, ImportExportFilesPayload payload)
        {
            try
            {
                payload.ExportUuids = await dataBaseFactory.Db.FetchAsync<string>(
                    $"SELECT ProcessUuid FROM ExportFiles WHERE MasterAccountNum={payload.MasterAccountNum} AND ProfileNum={payload.ProfileNum} AND Status=0");
                return true;
            }
            catch(Exception ex)
            {
                AddError(ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateExportStatus(IDataBaseFactory dataBaseFactory, ImportExportFilesPayload payload)
        {
            try
            {
                return await dataBaseFactory.Db.ExecuteAsync(
                    $"UPDATE ExportFiles SET Status=1, UpdateDateUtc=GETUTCDATE() WHERE MasterAccountNum={payload.MasterAccountNum} AND ProfileNum={payload.ProfileNum} AND ProcessUuid='{payload.ExportUuid}' AND Status=0") == 1;
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                return false;
            }
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


        public async Task UpdateExportRecordAsync(ImportExportFilesPayload payload)
        {

            string rowKey = string.Empty;
            if (string.IsNullOrWhiteSpace(payload.ExportUuid))
                rowKey = payload.ImportUuid;
            else
                rowKey = payload.ExportUuid;
            var universal = await GetTableUniversalAsync();

            await universal.UpSertEntityAsync(payload.Result, "Export", rowKey);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowKey">import Or ExportUuid</param>
        /// <returns></returns>
        public async Task<ImportExportResult> GetImportExportRecordAsync(string rowKey)
        {
            var universal = await GetTableUniversalAsync();
            return await universal.GetEntityAsync(rowKey, "Export");
        }
    }
}
