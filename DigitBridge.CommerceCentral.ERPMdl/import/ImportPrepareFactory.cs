using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ImportPrepareFactory
    {
        public static IPrepare<SalesOrderService, SalesOrderData, SalesOrderDataDto> GetSalesOrderImportInstance(SalesOrderService service, int formatNum)
        {
            switch (formatNum)
            {
                case 10001:
                    return new SalesOrderImportSystem10001(service);
                default:
                    return new SalesOrderImportDefault(service);
            }
        }
    }
}
