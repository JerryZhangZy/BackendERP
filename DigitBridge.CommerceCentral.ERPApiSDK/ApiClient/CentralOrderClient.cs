using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// create sales order by centralOrderUuid
    /// </summary>
    public class CentralOrderClient : ErpEventClient
    {

        public CentralOrderClient() : base()
        { }
        public CentralOrderClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }

        /// <summary>
        /// Transfer channel order to erp
        /// </summary>
        /// <param name="MasterAccountNum"></param>
        /// <param name="ProfileNum"></param>
        /// <param name="processUuid"></param>
        /// <returns></returns>
        public async Task<bool> CentralOrderToErpAsync(int masterAccountNum, int profileNum, string centralOrderUuid)
        {
            var data = new AddErpEventDto
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                ProcessUuid = centralOrderUuid,
            };
            return await AddEventERPAsync(data, FunctionUrl.CreateSalesOrderByCentralOrder);
        }

        /// <summary>
        /// Transfer channel order to erp
        /// </summary>
        /// <param name="eventDto"></param>
        /// <returns></returns>
        public async Task<bool> CentralOrderToErpAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, FunctionUrl.CreateSalesOrderByCentralOrder);
        }
    }
}
