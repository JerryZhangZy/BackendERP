using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ErpEventClient : ApiClientBase<EventERPPayload>
    {
        public ErpEventClient() : base(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode)
        { }

        public async Task<bool> AddEventERPAsync(Event_ERPDto eventDto)
        {

            var eventERPPayload = new EventERPPayload()
            {
                EventERP = new EventERPDataDto()
                {
                    Event_ERP = eventDto
                }
            };
            var success = await PostAsync(eventERPPayload, eventDto.MasterAccountNum.ToInt(), eventDto.ProfileNum.ToInt());
            if (!success)
            {
                //todo write this.Messages to log or do something. 
            }
            return success;
        }

        public async Task<bool> UpdateEventERPAsync(Event_ERPDto eventDto)
        {
            var eventERPPayload = new EventERPPayload()
            {
                EventERP = new EventERPDataDto()
                {
                    Event_ERP = eventDto
                }
            };
            var success = await PatchAsync(eventERPPayload, eventDto.MasterAccountNum.ToInt(), eventDto.ProfileNum.ToInt());
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
