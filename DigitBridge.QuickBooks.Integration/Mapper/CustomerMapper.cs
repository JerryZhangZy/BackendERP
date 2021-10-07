using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public class CustomerMapper
    {
        public static string GetMarketPlaceCustomer(QboIntegrationSetting setting)
        {
            //string customerId;
            //// case (int)OrderExportRule.DailySummaryInvoice: 
            ////invoice.CustomerRef = new ReferenceType() { Value = accSetting.ChannelQboCustomerId.ForceToTrimString() };
            ////_setting.ExportOrderAs== (int)OrderExportRule.Invoice:  	ChannelQboCustomerId int NULL, -- Use if select Create Customer Records per Marketplace

            //TODO get ChannelQboCustomerId
            return "3";
        }
    }
}
