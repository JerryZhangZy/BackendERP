using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ErpEventClientHelper
    {
        public static async Task ToQueueAsync(Event_ERPDto eventDto)
        {
            await ToQueueAsync(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode, eventDto);
        }
        public static async Task ToQueueAsync(string url, string authcode, Event_ERPDto eventDto)
        {
            try
            {
                var dicHeaders = new Dictionary<string, string>();
                dicHeaders.Add("masterAccountNum", eventDto.MasterAccountNum.ToString());
                dicHeaders.Add("profileNum", eventDto.ProfileNum.ToString());

                var eventERPPayload = new EventERPPayload()
                {
                    EventERP = new EventERPDataDto()
                    {
                        Event_ERP = eventDto
                    }
                };

                var responseStr = await HttpRequestUtil.CallAsync(url, authcode, eventERPPayload, dicHeaders);
                var responseObj = JsonConvert.DeserializeObject<EventERPPayload>(responseStr);

                if (responseObj == null)
                {
                    //TODO do something or write log.
                    //AddError("Call event api has no resopne.");
                    return;
                }

                if (!responseObj.Success)
                {
                    //TODO do something or write log.
                    //this.Messages = this.Messages.Concat(responseObj.Messages).ToList();
                }
            }
            catch (Exception e)
            {
                //TODO write log.
            }

        }

        public static void ToQueue(Event_ERPDto eventDto)
        {
            ToQueue(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode, eventDto);
        }
        public static void ToQueue(string url, string authcode, Event_ERPDto eventDto)
        {
            try
            {
                var dicHeaders = new Dictionary<string, string>();
                dicHeaders.Add("masterAccountNum", eventDto.MasterAccountNum.ToString());
                dicHeaders.Add("profileNum", eventDto.ProfileNum.ToString());

                var eventERPPayload = new EventERPPayload()
                {
                    EventERP = new EventERPDataDto()
                    {
                        Event_ERP = eventDto
                    }
                };

                var responseStr = HttpRequestUtil.Call(url, authcode, eventERPPayload, dicHeaders);
                var responseObj = JsonConvert.DeserializeObject<EventERPPayload>(responseStr);

                if (responseObj == null)
                {
                    //TODO do something or write log.
                    //AddError("Call event api has no resopne.");
                    return;
                }

                if (!responseObj.Success)
                {
                    //TODO do something or write log.
                    //this.Messages = this.Messages.Concat(responseObj.Messages).ToList();
                }
            }
            catch (Exception e)
            {
                //TODO write log.
            }

        }
    }
}
