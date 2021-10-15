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
        public static async Task UpdateEventAsync(Event_ERP data)
        {
            await UpdateEventAsync(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode, data);
        }
        public static async Task UpdateEventAsync(string url, string authcode, Event_ERP data)
        {
            try
            {
                var dicHeaders = new Dictionary<string, string>();
                dicHeaders.Add("masterAccountNum", data.MasterAccountNum.ToString());
                dicHeaders.Add("profileNum", data.ProfileNum.ToString());

                var responseStr = await HttpRequestUtil.CallAsync(url, authcode, data, dicHeaders, "PATCH");
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


        public static void UpdateEvent(ERPQueueMessage message)
        {
            UpdateEvent(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode, message);
        }
        public static void UpdateEvent(string url, string authcode, ERPQueueMessage message)
        {
            try
            {
                var dicHeaders = new Dictionary<string, string>();
                dicHeaders.Add("masterAccountNum", message.MasterAccountNum.ToString());
                dicHeaders.Add("profileNum", message.ProfileNum.ToString());

                var responseStr = HttpRequestUtil.Call(url, authcode, message, dicHeaders, "PATCH");
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
