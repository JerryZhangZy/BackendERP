using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.AzureStorage;
using DigitBridge.CommerceCentral.ERPDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl.ImportExport
{
  public  class ImportExportMemoryTableService
    {
        private TableUniversal<ImportExportResult> summaryTableUniversal;

        protected async Task<TableUniversal<ImportExportResult>> GetTableUniversalAsync()
        {
            if (summaryTableUniversal == null)
            {
                summaryTableUniversal = await TableUniversal<ImportExportResult>.CreateAsync(MySingletonAppSetting.ERPImportExportTableName, MySingletonAppSetting.ERPImportExportTableConnectionString);
            }
            return summaryTableUniversal;
        }


        public async Task UpdateImportExportRecordAsync(ImportExportFilesPayload payload)
        {

            string rowKey = string.Empty;
            if (string.IsNullOrWhiteSpace(payload.ExportUuid))
                rowKey = payload.ImportUuid;
            else
                rowKey = payload.ExportUuid;
            var universal = await GetTableUniversalAsync();

            await universal.UpSertEntityAsync(payload.Result, "ImportExport", rowKey);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowKey">import Or ExportUuid</param>
        /// <returns></returns>
        public async Task<ImportExportResult> GetImportExportRecordAsync(string rowKey)
        {
            var universal = await GetTableUniversalAsync();
            return await universal.GetEntityAsync(rowKey, "ImportExport");
        }
    }
}
