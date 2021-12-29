using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.AzureStorage;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Linq;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ExportBlobService : IMessage
    {
        public const string CONTAINER_NAME = "erp-export";
        public const string OPTIONS_NAME = "erp-export-options";
        public const string RESULT_NAME = "erp-export-result";

        private BlobUniversal _blobUniversal;

        public string ContainerName(string exportUuid)
        {
            var nm = MySingletonAppSetting.ERPExportContainerName;
            if (string.IsNullOrWhiteSpace(nm))
                nm = CONTAINER_NAME;
            return $"{nm.ToLower()}-{exportUuid}";
        }
        public string ConnectionString
        {
            get => MySingletonAppSetting.ERPBlobConnectionString;
        }

        protected async Task<BlobUniversal> GetBlobContainerAsync(string exportUuid)
        {
            try
            {
                if (_blobUniversal == null)
                    _blobUniversal = await BlobUniversal.CreateAsync(ContainerName(exportUuid), ConnectionString);
                return _blobUniversal;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        #region save to blob

        public async Task<bool> SaveOptionsToBlobAsync(ImportExportFilesPayload payload)
        {
            if (!ValidateOptions(payload))
            {
                return false;
            }

            try
            {
                var blobContainer = await GetBlobContainerAsync(payload.ExportUuid);
                await blobContainer.UploadBlobAsync(OPTIONS_NAME, payload.Options.ObjectToString());
                return true;
            }
            catch (Exception e)
            {
                AddError($"Export Options save error. {e.Message}");
                return false;
            }
        }

        protected bool ValidateOptions(ImportExportFilesPayload payload)
        {
            if (!payload.HasOptions)
            {
                AddError($"Export Options is required.");
                return false;
            }

            var validate = true;
            if (!payload.Options.HasFormatType)
            {
                AddError($"Export Options FormatType is required.");
                validate = false;
            }
            if (!payload.Options.HasFormatNumber)
            {
                AddError($"Export Options FormatNumber is required.");
                validate = false;
            }
            if (!payload.Options.HasMasterAccountNum)
            {
                AddError($"Export Options MasterAccountNum is required.");
                validate = false;
            }
            if (!payload.Options.HasProfileNum)
            {
                AddError($"Export Options ProfileNum is required.");
                validate = false;
            }
            if (!payload.Options.HasExportUuid)
            {
                AddError($"Export Options exportUuid is required.");
                validate = false;
            }

            return validate;
        }

        public async Task<bool> SaveFilesToBlobAsync(ImportExportFilesPayload payload)
        {
            if (payload.ExportFiles == null || payload.ExportFiles.Count == 0)
            {
                AddError($"No export file to save to blob.");
                return false;
            }

            foreach (var item in payload.ExportFiles)
            {
                if (item.Value == null) continue;
                if (!item.Key.EndsWith("csv") && !item.Key.EndsWith("txt")) continue;
                try
                {
                    var blobContainer = await GetBlobContainerAsync(payload.ExportUuid);
                    await blobContainer.UploadBlobAsync(item.Key, item.Value);
                    await SqlQuery.ExecuteNonQueryAsync(
@$"INSERT INTO ExportFiles(DatabaseNum,MasterAccountNum,ProfileNum,ProcessUuid) VALUES
({payload.DatabaseNum},{payload.MasterAccountNum},{payload.ProfileNum},'{payload.ExportUuid}')");
                }
                catch (Exception e)
                {
                    AddError($"Export file {item.Key} save error. {e.Message}");
                    return false;
                }
            }
            return true;
        }

        #endregion save to blob


        #region load from blob

        public async Task<bool> LoadOptionsFromBlobAsync(ImportExportFilesPayload payload)
        {
            try
            {
                var blobContainer = await GetBlobContainerAsync(payload.ExportUuid);
                var optionJson = await blobContainer.DownloadBlobToStringAsync(OPTIONS_NAME);
                if (string.IsNullOrWhiteSpace(optionJson))
                {
                    AddError($"Export Options is empty.");
                    return false;
                }
                payload.Options = optionJson.JsonToObject<ImportExportOptions>();
                return true;
            }
            catch (Exception e)
            {
                AddError($"Export Options save error. {e.Message}");
                return false;
            }
        }

        public async Task<FileContentResult> DownloadFileFromBlobAsync(ImportExportFilesPayload payload)
        {
            if (!await LoadFilesFromBlobAsync(payload))
                return null;

            if (!payload.HasExportFiles)
                return null;

            var exportFileInfo = payload.ExportFiles.FirstOrDefault();
            var downfile = new FileContentResult(exportFileInfo.Value, "text/csv");
            downfile.FileDownloadName = exportFileInfo.Key;
            return downfile;
        }

        protected async Task<bool> LoadFilesFromBlobAsync(ImportExportFilesPayload payload)
        {
            if (!payload.HasExportUuid)
            {
                AddError($"ExportUuid cannot be empty.");
                return false;
            }

            var blobContainer = await GetBlobContainerAsync(payload.ExportUuid);
            var fileList = blobContainer.GetBlobList();
            if (fileList == null || fileList.Count == 0)
            {
                AddError($"Export files not found.");
                return false;
            }

            payload.ExportFiles = new Dictionary<string, byte[]>();
            foreach (var fileName in fileList)
            {
                if (string.IsNullOrWhiteSpace(fileName) ||
                    fileName.EqualsIgnoreSpace(OPTIONS_NAME) ||
                    fileName.EqualsIgnoreSpace(RESULT_NAME)
                    ) continue;

                try
                {
                    var exportData = await blobContainer.DownloadBlobAsync(fileName);
                    payload.ExportFiles.Add(fileName, exportData);
                }
                catch (Exception e)
                {
                    AddError($"Load file {fileName} error. {e.Message}");
                }
            }

            return true;
        }

        #endregion load from blob 


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
