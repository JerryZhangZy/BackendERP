using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPBroker
{
    public class EventServieHelper
    {
        public static async Task UpdateEventAsync(bool success, ERPQueueMessage message, string error)
        {
            var eventDto = new Event_ERPDto()
            {
                ActionStatus = success ? (int)ErpEventActionStatus.Success : (int)ErpEventActionStatus.Other,
                EventUuid = message.EventUuid,
                EventMessage = error,
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum
            };

            await UpdateEventAsync(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode, eventDto);
        }

        public static async Task UpdateEventAsync(Event_ERPDto eventDto)
        {
            await UpdateEventAsync(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode, eventDto);
        }
        public static async Task UpdateEventAsync(string url, string authcode, Event_ERPDto eventDto)
        {
            try
            {
                if (eventDto.EventUuid.IsZero())
                {
                    // this means the message is not deserialized.
                    //todo write log.
                    return;
                }

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

                var responseStr = await HttpRequestUtil.CallAsync(url, authcode, eventERPPayload, dicHeaders, "PATCH");
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


        public static void UpdateEvent(Event_ERPDto message)
        {
            UpdateEvent(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode, message);
        }
        public static void UpdateEvent(string url, string authcode, Event_ERPDto eventDto)
        {
            try
            {
                if (eventDto.EventUuid.IsZero())
                {
                    // this means the message is not deserialized.
                    //todo write log.
                    return;
                }

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


                var responseStr = HttpRequestUtil.Call(url, authcode, eventERPPayload, dicHeaders, "PATCH");
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
