using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.AzureStorage;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ImportBlobService
    {
        public const string CONTAINER_NAME = "erp_impot_files";
        public const string OPTIONS_NAME = "erp_impot_options";

        private BlobUniversal _blobUniversal;

        public string ContainerName 
        { 
            get => string.IsNullOrWhiteSpace(MySingletonAppSetting.ERPSummaryTableName) ? "erp_impot_files" : MySingletonAppSetting.ERPSummaryTableName; 
        }
        public string ConnectionString
        {
            get => MySingletonAppSetting.ERPBlobConnectionString;
        }

        protected async Task<BlobUniversal> GetBlobContainerAsync()
        {
            if (_blobUniversal == null)
                _blobUniversal = await BlobUniversal.CreateAsync(ContainerName, ConnectionString);
            return _blobUniversal;
        }

        #region save to blob
        public async Task<bool> SaveToBlob(ImportExportFilesPayload payload)
        {
            var blobContainer = await GetBlobContainerAsync();

            var blobList = blobContainer.GetBlobList();

            // save options Blob
            if (!(await SaveOptionsToBlob(payload))) 
                return false;

            // save options Blob
            if (!(await SaveFilesToBlob(payload)))
                return false;
            return true;
        }
        protected async Task<bool> SaveOptionsToBlob(ImportExportFilesPayload payload)
        {
            if (!payload.HasOptions)
            {
                payload.ReturnError($"Import Options is required.");
                return false;
            }

            try
            {
                var blobContainer = await GetBlobContainerAsync();
                await blobContainer.UploadBlobAsync(OPTIONS_NAME, payload.Options.ObjectToString());
                return true;
            }
            catch (Exception e)
            {
                payload.ReturnError($"Import Options save error. {e.Message}");
                return false;
            }
        }

        protected async Task<bool> SaveFilesToBlob(ImportExportFilesPayload payload)
        {
            if (!payload.HasImportFiles)
            {
                payload.ReturnError($"Import file is required.");
                return false;
            }

            foreach (var file in payload.ImportFiles)
            {
                if (file == null) continue;
                if (!(await SaveFileToBlob(file, payload)))
                    return false;
            }
            return true;
        }

        protected async Task<bool> SaveFileToBlob(IFormFile file, ImportExportFilesPayload payload)
        {
            if (file == null)
                return false;

            try
            {
                var blobContainer = await GetBlobContainerAsync();
                using (var ms = file.OpenReadStream())
                {
                    await blobContainer.UploadBlobAsync(file.Name, ms);
                }
                return true;
            }
            catch (Exception e)
            {
                payload.ReturnError($"Import file {file.Name} save error. {e.Message}");
                return false;
            }
        }
        #endregion save to blob


        #region load from blob
        protected async Task<bool> LoadOptionsFromBlob(ImportExportFilesPayload payload)
        {
            try
            {
                var blobContainer = await GetBlobContainerAsync();
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
        protected async Task<bool> LoadFileNamesFromBlob(ImportExportFilesPayload payload)
        {
            if (!payload.HasImportFiles)
            {
                payload.ReturnError($"Import file is required.");
                return false;
            }

            foreach (var file in payload.ImportFiles)
            {
                if (file == null) continue;
                if (!(await SaveFileToBlob(file, payload)))
                    return false;
            }
            return true;
        }

        protected async Task<byte[]> LoadFileFromBlob(string fileName, ImportExportFilesPayload payload)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            try
            {
                var blobContainer = await GetBlobContainerAsync();
                return await blobContainer.DownloadBlobAsync(fileName);
            }
            catch (Exception e)
            {
                payload.ReturnError($"Load file {fileName} error. {e.Message}");
                return null;
            }
        }

        #endregion load from blob

    }
}
