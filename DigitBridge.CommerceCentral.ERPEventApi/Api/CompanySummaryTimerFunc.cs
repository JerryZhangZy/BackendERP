using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DigitBridge.CommerceCentral.ERPEventApi.Api
{
    public static class CompanySummaryTimerFunc
    {
        [FunctionName("CompanySummaryTimerFunc")]
        public static async Task Run([TimerTrigger("0 */30 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
            var payloadList =await GenerateAllDefaultCompanySummaryPayload();
            var srv = new Mdl.CompanySummaryService();
            foreach(var payload in payloadList)
            {
                await srv.UpdateCompanySummaryAsync(payload);
            }
        }

        public static async Task<IList<CompanySummaryPayload>> GenerateAllDefaultCompanySummaryPayload()
        {
            var allpayloads =await MyAppHelper.GetAllPayloadAsync();
            var payloadList = new List<CompanySummaryPayload>();
            foreach(var payload in allpayloads)
            {
                payloadList.Add(CompanySummaryPayload.GeneratedDefaultPayload(payload));
            }
            return payloadList;
        }
    }
}
