﻿using DigitBridge.Base.Common;
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
    public class ErpEventClient : ApiClientBase<EventERPPayload>
    {
        public ErpEventClient() : base(ConfigUtil.ERP_Integration_Api_BaseUrl, ConfigUtil.ERP_Integration_Api_AuthCode)
        { }
        public ErpEventClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

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
            return await PatchAsync(eventDto, FunctionUrl.UpdateEvent);
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
            if (!SetAccount(eventDto.MasterAccountNum, eventDto.ProfileNum))
            {
                return false;
            }
            return await PatchAsync(eventDto, FunctionUrl.UpdateEvent);
        }

        protected override async Task<bool> AnalysisResponseAsync(string responseData)
        {
            if (ResopneData == null)
            {
                //Maybe the api throw exception.
                var exception = JsonConvert.DeserializeObject<Exception>(responseData, jsonSerializerSettings);
                if (exception != null)
                    AddError(exception.ObjectToString());
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
