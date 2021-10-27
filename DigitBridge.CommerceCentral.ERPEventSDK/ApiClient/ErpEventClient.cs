using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPEventSDK
{
    public class ErpEventClient : ApiClientBase<EventERPPayload>
    {
        public ErpEventClient() : base(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode)
        { }
        public ErpEventClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        public EventERP Data { get; set; }

        protected async Task<bool> AddEventERPAsync(AddErpEventDto eventDto, string functionUrl)
        {

            return await PostAsync(eventDto, functionUrl);
        }

        public async Task<bool> SendActionResultAsync(UpdateErpEventDto eventDto)
        {
            return await PatchAsync(eventDto);
        }

        public async Task<bool> SendActionResultAsync(ERPQueueMessage message, string error, bool success = false)
        {
            var eventDto = new UpdateErpEventDto()
            {
                EventMessage = error,
                ActionStatus = success ? (int)ErpEventActionStatus.Success : (int)ErpEventActionStatus.Other,
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                EventUuid = message.EventUuid,
            };
            return await PatchAsync(eventDto);
        }

        protected override async Task<bool> AnalysisResponseAsync(string responseData)
        {
            if (ResopneData == null)
            {
                AddError("Call event api has no resopne.");
                return false;
            }
            if (ResopneData.EventERP == null)
            {
                //Maybe the api throw exception.
                var exception = JsonConvert.DeserializeObject<Exception>(responseData, jsonSerializerSettings);
                if (exception != null)
                    AddError(exception.ObjectToString());
                return false;
            }
            Data = ResopneData.EventERP.Event_ERP;

            var success = ResopneData.Success;
            if (!success)
            {
                this.Messages = this.Messages.Concat(ResopneData.Messages).ToList();
            }
            return success;
        }
    }
}
