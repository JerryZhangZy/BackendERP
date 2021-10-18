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
        #region Instance

        private static ErpEventClient _erpEventClient;

        private static ErpEventClient erpEventClient
        {
            get
            {
                if (_erpEventClient is null)
                    _erpEventClient = new ErpEventClient();
                return _erpEventClient;
            }
        }
        #endregion

        public static async Task<bool> AddEventERPAsync(Event_ERPDto eventDto)
        {
            return await erpEventClient.AddEventERPAsync(eventDto);
        }
        public static async Task<bool> UpdateEventERPAsync(bool success, ERPQueueMessage message, string error)
        {
            var eventDto = new Event_ERPDto()
            {
                ActionStatus = success ? (int)ErpEventActionStatus.Success : (int)ErpEventActionStatus.Other,
                EventUuid = message.EventUuid,
                EventMessage = error,
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum
            };
            return await erpEventClient.UpdateEventERPAsync(eventDto);
        }
    }
}
