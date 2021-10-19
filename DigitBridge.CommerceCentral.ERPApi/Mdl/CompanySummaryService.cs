using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.AzureStorage;
using DigitBridge.CommerceCentral.ERPMdl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi
{
    public class CompanySummaryService
    {
        private TableUniversal<SummaryInquiryTableEntity> summaryTableUniversal;

        protected async Task<TableUniversal<SummaryInquiryTableEntity>> GetSummaryTableUniversalAsync()
        {
            if (summaryTableUniversal == null)
            {
                summaryTableUniversal = await TableUniversal<SummaryInquiryTableEntity>.CreateAsync(MySingletonAppSetting.ERPSummaryTable, MySingletonAppSetting.ERPSummaryTableStorage);
            }
            return summaryTableUniversal;
        }

        public async Task GetCompanySummaryAsync(CompanySummaryPayload payload)
        {
            var universal = await GetSummaryTableUniversalAsync();
            var summary =await universal.GetEntityAsync("ErpSummaryCache", payload.Filters.GenerateFilterKey);
            if (summary == null)
            {
                payload.Success = false;
                payload.Messages.Add(new MessageClass("Summary is not initializatied"));

                //TODO:
            }
            else
            {
                payload.Summary = summary.Summary;
            }
        }

        //public async Task UpdateCompanySummaryAsync(CompanySummaryPayload payload)
        //{
        //    var db =await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new CompanySummaryInquiry(db);
        //    await service.GetCompaySummaryAsync(payload);

        //    var universal = await GetSummaryTableUniversalAsync();
        //    var summary = universal.GetEntityByRowKey(payload.Filters.GenerateFilterKey);
        //    if (summary == null)
        //    {
        //        summary = new SummaryInquiryTableEntity
        //        {
        //            RowKey = payload.Filters.GenerateFilterKey,
        //            ProfileNum = payload.ProfileNum,
        //            MasterAccountNum = payload.MasterAccountNum,
        //            CreateInquiryTimeUtc = DateTime.UtcNow,
        //            SummaryInquiryInfo = JsonConvert.SerializeObject(payload.Summary)
        //        };
        //    }
        //    else
        //    {
        //        summary.CreateInquiryTimeUtc = DateTime.UtcNow;
        //        summary.SummaryInquiryInfo = JsonConvert.SerializeObject(payload.Summary);
        //    }
        //    await universal.UpSertEntityAsync(summary, summary.PartitionKey, summary.RowKey);
        //}
    }
}
