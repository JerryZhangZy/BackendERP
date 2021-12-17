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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ImportBlobService
    {
        public const string CONTAINER_NAME = "erp-import";
        public const string OPTIONS_NAME = "erp-import-options";
        public const string RESULT_NAME = "erp-import-result";

        private BlobUniversal _blobUniversal;

        public string ContainerName(string importUuid)
        {
            var nm = MySingletonAppSetting.ERPImportContainerName;
            if (string.IsNullOrWhiteSpace(nm))
                nm = CONTAINER_NAME;
            return $"{nm.ToLower()}-{importUuid}";
        }
        public string ConnectionString
        {
            get => MySingletonAppSetting.ERPBlobConnectionString;
        }

        protected async Task<BlobUniversal> GetBlobContainerAsync(string importUuid)
        {
            try
            {
                if (_blobUniversal == null)
                    _blobUniversal = await BlobUniversal.CreateAsync(ContainerName(importUuid), ConnectionString);
                return _blobUniversal;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        #region save to blob
        public async Task<bool> SaveToBlobAsync(ImportExportFilesPayload payload)
        {
            var blobContainer = await GetBlobContainerAsync(payload.ImportUuid);

            // save options Blob
            if (!(await SaveOptionsToBlobAsync(payload)))
                return false;

            // save options Blob
            if (!(await SaveFilesToBlobAsync(payload)))
                return false;
            return true;
        }
        protected async Task<bool> SaveOptionsToBlobAsync(ImportExportFilesPayload payload)
        {
            if (!payload.HasOptions)
            {
                payload.ReturnError($"Import Options is required.");
                return false;
            }
            if (!payload.Options.HasFormatType)
            {
                payload.ReturnError($"Import Options FormatType is required.");
                return false;
            }
            //if (!payload.Options.HasFormatNumber)
            //{
            //    payload.ReturnError($"Import Options FormatNumber is required.");
            //    return false;
            //}

            try
            {
                var blobContainer = await GetBlobContainerAsync(payload.ImportUuid);
                await blobContainer.UploadBlobAsync(OPTIONS_NAME, payload.Options.ObjectToString());
                return true;
            }
            catch (Exception e)
            {
                payload.ReturnError($"Import Options save error. {e.Message}");
                return false;
            }
        }

        protected async Task<bool> SaveFilesToBlobAsync(ImportExportFilesPayload payload)
        {
            if (payload.ImportFiles == null || payload.ImportFiles.Count == 0)
            {
                payload.ReturnError($"Import file is required.");
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
                    payload.ReturnError($"Import file {item.Key} save error. {e.Message}");
                    return false;
                }
            }
            return true;
        }

        #endregion save to blob


        #region load from blob
        public async Task<bool> LoadFromBlobAsync(ImportExportFilesPayload payload)
        {
            var blobContainer = await GetBlobContainerAsync(payload.ImportUuid);

            // save options Blob
            if (!(await LoadOptionsFromBlobAsync(payload)))
                return false;

            // save options Blob
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
                    payload.ReturnError($"Import Options is required.");
                    return false;
                }
                payload.Options = optionJson.JsonToObject<ImportExportOptions>();
                return true;
            }
            catch (Exception e)
            {
                payload.ReturnError($"Import Options save error. {e.Message}");
                return false;
            }
        }
        protected async Task<bool> LoadFileNamesFromBlobAsync(ImportExportFilesPayload payload)
        {
            var blobContainer = await GetBlobContainerAsync(payload.ImportUuid);
            var fileList = blobContainer.GetBlobList();
            if (fileList == null || fileList.Count == 0)
            {
                payload.ReturnError($"Import files not found.");
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
                payload.ReturnError($"Import files not found.");
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
