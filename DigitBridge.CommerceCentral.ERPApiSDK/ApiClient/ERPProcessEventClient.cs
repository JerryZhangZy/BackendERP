using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class ERPProcessEventClient : ApiClientBase<EventProcessERPPayload>
    {
        public ERPProcessEventClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        { }
        public ERPProcessEventClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        public EventProcessERP Data { get; set; }

        protected async Task<bool> AddEventERPAsync(AddErpEventDto eventDto, string functionUrl)
        {
            if (!SetAccount(eventDto.MasterAccountNum, eventDto.ProfileNum))
            {
                return false;
            }
            return await PostAsync(eventDto, functionUrl);
        }

        public async Task<bool> SendActionResultAsync(UpdateErpEventDto eventDto)
        {
            if (!SetAccount(eventDto.MasterAccountNum, eventDto.ProfileNum))
            {
                return false;
            }
            return await PatchAsync(eventDto);
        }

        public async Task<bool> UpdateActionStatusAsync(ERPQueueMessage message, string error, bool downloaded = false)
        {
            var eventDto = new UpdateErpEventDto()
            {
                EventMessage = error,
                ActionStatus = downloaded ? (int)EventProcessActionStatusEnum.Downloaded : (int)EventProcessActionStatusEnum.Pending,
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                EventUuid = message.EventUuid,
            };
            if (!SetAccount(eventDto.MasterAccountNum, eventDto.ProfileNum))
            {
                return false;
            }
            return await PatchAsync(eventDto);
        }

        protected override async Task<bool> AnalysisResponseAsync(string responseData)
        {
            if (ResopneData == null)
            {
                AddError("Call event api has no resopne.");
                return false;
            }
            if (ResopneData.EventProcessERP == null)
            {
                //Maybe the api throw exception.
                var exception = JsonConvert.DeserializeObject<Exception>(responseData, jsonSerializerSettings);
                if (exception != null)
                    AddError(exception.ObjectToString());
                return false;
            }
            Data = ResopneData.EventProcessERP.EventProcessERP;

            var success = ResopneData.Success;
            if (!success)
            {
                this.Messages = this.Messages.Concat(ResopneData.Messages).ToList();
            }
            return success;
        }
    }
}
