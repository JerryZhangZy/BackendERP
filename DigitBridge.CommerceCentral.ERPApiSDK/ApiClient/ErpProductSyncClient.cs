using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPApiSDK.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class ErpProductSyncClient : ApiClientBase<ResponsePayloadBase>
    {

        public ErpProductSyncClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        { }
        public ErpProductSyncClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        public async Task<bool> SyncFromProductBasicAsync(int masterAccountNum, int profileNum)
        {
            if (!SetAccount(masterAccountNum, profileNum))
            {
                return false;
            }
            var success = true;
            try
            {
                using var httpClient = await CreateHttpClientAsync();

                var response = await httpClient.GetAsync(BuildRequestUri("syncProducts"));
                var responseData = await response.Content.ReadAsStringAsync();
                success = await AnalysisResponseAsync(responseData);
            }
            catch (Exception ex)
            {
                success = false;
                AddError($"SyncFromProductBasicAsync error: {ex.Message}");
            }

            return success;
        }

        protected override async Task<bool> AnalysisResponseAsync(string responseData)
        {
            if (ResopneData == null)
            {
                AddError("Call event api has no resopne.");
                return false;
            }

            var success = ResopneData.Success;
            if (!success)
            {
                this.Messages = this.Messages.Concat(ResopneData.Messages).ToList();
            }
            return success;
        }

    }
}
