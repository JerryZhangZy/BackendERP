using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPEventSDK
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

        public static async Task<bool> AddEventERPAsync(AddErpEventDto eventDto, string functionUrl)
        {
            return await erpEventClient.AddEventERPAsync(eventDto, functionUrl);
        }

        public static async Task<bool> UpdateEventERPAsync(bool success, ERPQueueMessage message, string error)
        {
            var eventDto = new UpdateErpEventDto()
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
