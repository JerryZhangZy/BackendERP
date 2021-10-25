﻿using DigitBridge.Base.Common;
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
        public ErpEventClient(string baseUrl,string authCode) :  base(baseUrl, authCode)
        { }

        protected async Task<bool> AddEventERPAsync(AddErpEventDto eventDto, string functionUrl)
        {

            var success = await PostAsync(eventDto, functionUrl);
            if (!success)
            {
                //todo write this.Messages to log or do something. 
            }
            return success;
        }

        public async Task<bool> SendActionResultAsync(UpdateErpEventDto eventDto)
        {
            var success = await PatchAsync(eventDto);
            if (!success)
            {
                //todo write this.Messages to log or do something. 
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