using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApi
{
    public class MyAppHelper
    {
        public static DataBaseFactory GetDatabase()
        {
            return new DataBaseFactory("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DigitBridgeERP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
