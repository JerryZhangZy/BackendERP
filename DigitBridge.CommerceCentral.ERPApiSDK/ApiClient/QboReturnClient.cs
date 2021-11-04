﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class QboReturnClient : ErpEventClient
    {

        public QboReturnClient() : base()
        { }
        public QboReturnClient(string baseUrl, string authCode) : base(baseUrl, authCode)
        { }


        public async Task<bool> SendAddQboReturnAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, FunctionUrl.QuickBooksReturn);
        }

        public async Task<bool> SendDeleteQboReturnAsync(AddErpEventDto eventDto)
        {
            return await AddEventERPAsync(eventDto, FunctionUrl.QuickBooksReturnDelete);
        }
    }
}