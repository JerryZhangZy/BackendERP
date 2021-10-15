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
    public class EventServieHelper
    {
        public static async Task ToQueueAsync(Event_ERP data)
        {
            await ToQueueAsync(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode, data);
        }
        public static async Task ToQueueAsync(string url, string authcode, Event_ERP data)
        {
            try
            {
                var dicHeaders = new Dictionary<string, string>();
                dicHeaders.Add("masterAccountNum", data.MasterAccountNum.ToString());
                dicHeaders.Add("profileNum", data.ProfileNum.ToString());

                var responseStr = await HttpRequestUtil.CallAsync(url, authcode, data, dicHeaders);
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

        public static void ToQueue(Event_ERP message)
        {
            ToQueue(ConfigUtil.EventApi_BaseUrl, ConfigUtil.EventApi_AuthCode, message);
        }
        public static void ToQueue(string url, string authcode, Event_ERP message)
        {
            try
            {
                var dicHeaders = new Dictionary<string, string>();
                dicHeaders.Add("masterAccountNum", message.MasterAccountNum.ToString());
                dicHeaders.Add("profileNum", message.ProfileNum.ToString());

                var responseStr = HttpRequestUtil.Call(url, authcode, message, dicHeaders);
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
