﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPEventSDK.ApiClient
{
    public class SalesOrderClient:ErpEventClient
    {

        public SalesOrderClient() : base()
        { }
        public SalesOrderClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> SendCreateSalesOrderByCentralOrderAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, ERPEventFunctionUrl.CreateSalesOrderByCentralOrder);
        }
    }
}
