using DigitBridge.CommerceCentral.ERPDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl.so
{
    public partial class SalesOrderMapper
    {
        public static SalesOrderData MapChannelOrderToSalesOrder(ChannelOrderData co)
        {
            SalesOrderData soData = new SalesOrderData();

            soData.SalesOrderHeader = MapChannelOrderToSalesOrderHeader(co.OrderHeader);

            soData.SalesOrderHeaderAttributes = MapChannelOrderToSalesOrderHeaderAttributes(co.OrderHeader);

            soData.SalesOrderHeaderInfo = MapChannelOrderToSalesOrderHeaderInfo(co.OrderHeader);

            return soData;
        }

        private static SalesOrderHeader MapChannelOrderToSalesOrderHeader(OrderHeader coHeader)
        {
            DateTime dtNow = DateTime.UtcNow;

            SalesOrderHeader soHeader = new SalesOrderHeader()
            {
                DatabaseNum = coHeader.DatabaseNum,
                MasterAccountNum = coHeader.MasterAccountNum,
                ProfileNum = coHeader.ProfileNum,
                OrderNumber = coHeader.DatabaseNum + "-" + coHeader.CentralOrderNum,
                //OrderType = coHeader,
                //OrderStatus = coHeader,
                OrderDate = dtNow,
                OrderTime = dtNow.TimeOfDay,
                //DueDate = coHeader,
                //BillDate = coHeader,
                //CustomerUuid = coHeader,
                //CustomerCode = coHeader,
                //CustomerName = coHeader,
                //Terms = coHeader,
                //TermsDays = coHeader,
                Currency = coHeader.Currency,
                //SubTotalAmount = coHeader,
                //SalesAmount = coHeader,
                //TotalAmount = coHeader,
                //TaxableAmount = coHeader,
                //NonTaxableAmount = coHeader,
                //TaxRate = coHeader,
                //TaxAmount = coHeader,
                //DiscountRate = coHeader,
                //DiscountAmount = coHeader,
                //ShippingAmount = coHeader,
                //ShippingTaxAmount = coHeader,
                //MiscAmount = coHeader,
                //MiscTaxAmount = coHeader,
                //ChargeAndAllowanceAmount = coHeader,
                //PaidAmount = coHeader,
                //CreditAmount = coHeader,
                //Balance = coHeader,
                //UnitCost = coHeader,
                //AvgCost = coHeader,
                //LotCost = coHeader,
                //OrderSourceCode = coHeader,
                UpdateDateUtc = dtNow,
                //EnterBy = coHeader,
                //UpdateBy = coHeader,
                //ShipDate
            };

            return soHeader;
        }

        private static SalesOrderHeaderAttributes MapChannelOrderToSalesOrderHeaderAttributes(OrderHeader coHeader)
        {
            SalesOrderHeaderAttributes soHeaderAttributes = new SalesOrderHeaderAttributes();
    
            return soHeaderAttributes;
        }

        private static SalesOrderHeaderInfo MapChannelOrderToSalesOrderHeaderInfo(OrderHeader orderHeader)
        {
            SalesOrderHeaderInfo soHeaderInfo = new SalesOrderHeaderInfo()
            {

            };

            return soHeaderInfo;
        }
    }
}
