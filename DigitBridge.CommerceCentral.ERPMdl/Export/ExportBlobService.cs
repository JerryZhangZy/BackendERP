using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.AzureStorage;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ExportBlobService
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
        //public async Task<bool> SaveToBlobAsync(ImportExportFilesPayload payload)
        //{
        //    var blobContainer = await GetBlobContainerAsync(payload.ExportUuid);

        //    // save options Blob
        //    if (!(await SaveOptionsToBlobAsync(payload)))
        //        return false;

        //    // save options Blob
        //    if (!(await SaveFilesToBlobAsync(payload)))
        //        return false;
        //    return true;
        //}
        public async Task<bool> SaveOptionsToBlobAsync(ImportExportFilesPayload payload)
        {
            if (!ValidateOptions(payload))
            {
                return false;
            }

            try
            {
                var blobContainer = await GetBlobContainerAsync(payload.ImportUuid);
                await blobContainer.UploadBlobAsync(OPTIONS_NAME, payload.Options.ObjectToString());
                return true;
            }
            catch (Exception e)
            {
                payload.ReturnError($"Export Options save error. {e.Message}");
                return false;
            }
        }

        protected bool ValidateOptions(ImportExportFilesPayload payload)
        {
            if (!payload.HasOptions)
            {
                payload.ReturnError($"Export Options is required.");
                return false;
            }

            var validate = true;
            if (!payload.Options.HasFormatType)
            {
                payload.ReturnError($"Export Options FormatType is required.");
                validate = false;
            }
            if (!payload.Options.HasFormatNumber)
            {
                payload.ReturnError($"Export Options FormatNumber is required.");
                validate = false;
            }
            if (!payload.Options.HasMasterAccountNum)
            {
                payload.ReturnError($"Export Options MasterAccountNum is required.");
                validate = false;
            }
            if (!payload.Options.HasProfileNum)
            {
                payload.ReturnError($"Export Options ProfileNum is required.");
                validate = false;
            }
            if (!payload.Options.HasImportUuid)
            {
                payload.ReturnError($"Export Options exportUuid is required.");
                validate = false;
            }

            return validate;
        }

        protected async Task<bool> SaveFilesToBlobAsync(ImportExportFilesPayload payload)
        {
            if (payload.ImportFiles == null || payload.ImportFiles.Count == 0)
            {
                payload.ReturnError($"Export file is required.");
                return false;
            }

            foreach (var item in payload.ImportFiles)
            {
                if (item.Value == null) continue;
                if (!item.Key.EndsWith("csv") && !item.Key.EndsWith("txt")) continue;
                try
                {
                    var blobContainer = await GetBlobContainerAsync(payload.ImportUuid);
                    await blobContainer.UploadBlobAsync(item.Key, item.Value);
                }
                catch (Exception e)
                {
                    payload.ReturnError($"Export file {item.Key} save error. {e.Message}");
                    return false;
                }
            }
            return true;
        }

        #endregion save to blob


        #region load from blob
        public async Task<bool> LoadFromBlobAsync(ImportExportFilesPayload payload)
        {
            // load file names from blob
            if (!(await LoadFileNamesFromBlobAsync(payload)))
                return false;
            return true;
        }
        protected async Task<bool> LoadOptionsFromBlobAsync(ImportExportFilesPayload payload)
        {
            try
            {
                var blobContainer = await GetBlobContainerAsync(payload.ImportUuid);
                var optionJson = await blobContainer.DownloadBlobToStringAsync(OPTIONS_NAME);
                if (string.IsNullOrWhiteSpace(optionJson))
                {
                    payload.ReturnError($"Export Options is required.");
                    return false;
                }
                payload.Options = optionJson.JsonToObject<ImportExportOptions>();
                return true;
            }
            catch (Exception e)
            {
                payload.ReturnError($"Export Options save error. {e.Message}");
                return false;
            }
        }
        protected async Task<bool> LoadFileNamesFromBlobAsync(ImportExportFilesPayload payload)
        {
            var blobContainer = await GetBlobContainerAsync(payload.ImportUuid);
            var fileList = blobContainer.GetBlobList();
            if (fileList == null || fileList.Count == 0)
            {
                payload.ReturnError($"Export files not found.");
                return false;
            }

            payload.FileNames = new List<string>();
            foreach (var fileName in fileList)
            {
                if (string.IsNullOrWhiteSpace(fileName) ||
                    fileName.EqualsIgnoreSpace(OPTIONS_NAME) ||
                    fileName.EqualsIgnoreSpace(RESULT_NAME)
                    ) continue;
                payload.AddFileName(fileName);
            }

            if (payload.FileNames == null || payload.FileNames.Count == 0)
            {
                payload.ReturnError($"Export files not found.");
                return false;
            }
            return true;
        }

        public async Task<byte[]> LoadFileFromBlobAsync(string fileName, ImportExportFilesPayload payload)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            try
            {
                var blobContainer = await GetBlobContainerAsync(payload.ImportUuid);
                return await blobContainer.DownloadBlobAsync(fileName);
            }
            catch (Exception e)
            {
                payload.ReturnError($"Load file {fileName} error. {e.Message}");
                return null;
            }
        }

        public async Task<bool> LoadFileFromBlobAsync(string fileName, ImportExportFilesPayload payload, Stream stream)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            try
            {
                var blobContainer = await GetBlobContainerAsync(payload.ImportUuid);
                await blobContainer.DownloadBlobAsync(fileName, stream);
                stream.Position = 0;
                return true;
            }
            catch (Exception e)
            {
                payload.ReturnError($"Load file {fileName} error. {e.Message}");
                return false;
            }
        }

        #endregion load from blob 
    }
}
