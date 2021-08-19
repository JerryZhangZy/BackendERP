using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl.so
{
    public class SalesOrderMapper
    {
        private static DateTime _dtNowUtc = DateTime.UtcNow;

        private static string _coHeaderJson;

        public static SalesOrderData ChannelOrderToSalesOrder(DCAssignmentData dcData, ChannelOrderData coData)
        {
            SalesOrderData soData = new SalesOrderData();

            _coHeaderJson = coData.OrderHeader.ObjectToString();

            soData.SalesOrderHeader = ChannelOrderToSalesOrderHeader(coData.OrderHeader);

            soData.SalesOrderHeaderAttributes = ChannelOrderToSalesOrderHeaderAttributes();

            soData.SalesOrderHeaderInfo = ChannelOrderToSalesOrderHeaderInfo(dcData.OrderDCAssignmentHeader, coData.OrderHeader);

            soData.SalesOrderItems = ChannelorderLineToSalesOrderLines(dcData, coData);

            return soData;
        }

        private static SalesOrderHeader ChannelOrderToSalesOrderHeader(OrderHeader coHeader)
        {
            var soHeader = JsonConvert.DeserializeObject<SalesOrderHeader>(_coHeaderJson);

            soHeader.OrderNumber = coHeader.DatabaseNum + "-" + coHeader.CentralOrderNum;
            soHeader.OrderDate = coHeader.OriginalOrderDateUtc;
            soHeader.OrderTime = coHeader.OriginalOrderDateUtc.TimeOfDay;
            soHeader.UpdateDateUtc = _dtNowUtc;

            return soHeader;
        }

        private static SalesOrderHeaderAttributes ChannelOrderToSalesOrderHeaderAttributes()
        {
            SalesOrderHeaderAttributes soHeaderAttributes = new SalesOrderHeaderAttributes();

            return soHeaderAttributes;
        }

        private static SalesOrderHeaderInfo ChannelOrderToSalesOrderHeaderInfo(OrderDCAssignmentHeader dcHeader, OrderHeader coHeader)
        {
            var soHeaderInfo = JsonConvert.DeserializeObject<SalesOrderHeaderInfo>(_coHeaderJson);

            soHeaderInfo.CentralFulfillmentNum = dcHeader.OrderDCAssignmentNum;
            soHeaderInfo.DistributionCenterNum = dcHeader.DistributionCenterNum;
            soHeaderInfo.WarehouseCode = dcHeader.SellerWarehouseID;

            soHeaderInfo.ShippingCarrier = coHeader.RequestedShippingCarrier;
            soHeaderInfo.ShippingClass = coHeader.RequestedShippingClass;
            soHeaderInfo.EndBuyerName = coHeader.BillToName;

            soHeaderInfo.UpdateDateUtc = _dtNowUtc;

            return soHeaderInfo;
        }

        private static List<SalesOrderItems> ChannelorderLineToSalesOrderLines(DCAssignmentData dcData, ChannelOrderData coData)
        {
            List<SalesOrderItems> soItemList = new List<SalesOrderItems>();

            int itemSeq = 1;
            foreach (var dcLine in dcData.OrderDCAssignmentLine)
            {
                var coLine = coData.OrderLine.FirstOrDefault(p => p.CentralOrderLineUuid == dcLine.CentralOrderLineUuid);
                if (coLine == null)
                {
                    return null;
                }

                SalesOrderItems soItem = new SalesOrderItems()
                {
                    Seq = itemSeq++,
                    SKU = coLine.SKU,
                    WarehouseCode = dcData.OrderDCAssignmentHeader.SellerWarehouseID,
                    Description = coLine.ItemTitle,
                    Currency = coData.OrderHeader.Currency,
                    OrderQty = dcLine.OrderQty,
                    OpenQty = dcLine.OrderQty,
                    Price = coLine.UnitPrice ?? 0,
                    UpdateDateUtc = _dtNowUtc
                };

                soItemList.Add(soItem);
            }

            return soItemList;
        }

    }
}
