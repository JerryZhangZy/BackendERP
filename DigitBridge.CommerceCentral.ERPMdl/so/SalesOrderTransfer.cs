using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderTransfer : IMessage
    {
        private DateTime _dtNowUtc = DateTime.UtcNow;

        private string _coHeaderJson;
        
        public SalesOrderTransfer(IMessage serviceMessage)
        {
            this.ServiceMessage = serviceMessage;
        }

        #region message
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (ServiceMessage != null)
                    return ServiceMessage.Messages;

                if (_Messages == null)
                    _Messages = new List<MessageClass>();
                return _Messages;
            }
            set
            {
                if (ServiceMessage != null)
                    ServiceMessage.Messages = value;
                else
                    _Messages = value;
            }
        }

        protected IList<MessageClass> _Messages;
        public IMessage ServiceMessage { get; set; }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddInfo(message, code) : Messages.AddInfo(message, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddWarning(message, code) : Messages.AddWarning(message, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddError(message, code) : Messages.AddError(message, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddFatal(message, code) : Messages.AddFatal(message, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddDebug(message, code) : Messages.AddDebug(message, code);

        #endregion message

        public SalesOrderData FromChannelOrder(DCAssignmentData dcData, ChannelOrderData coData)
        {
            SalesOrderData soData = new SalesOrderData();

            _coHeaderJson = coData.OrderHeader.ObjectToString();

            soData.SalesOrderHeader = ChannelOrderToSalesOrderHeader(dcData.OrderDCAssignmentHeader, coData.OrderHeader);

            soData.SalesOrderHeaderAttributes = ChannelOrderToSalesOrderHeaderAttributes();

            soData.SalesOrderHeaderInfo = ChannelOrderToSalesOrderHeaderInfo(dcData.OrderDCAssignmentHeader, coData.OrderHeader);

            soData.SalesOrderItems = ChannelorderLineToSalesOrderLines(dcData, coData);

            return soData;
        }

        private SalesOrderHeader ChannelOrderToSalesOrderHeader(OrderDCAssignmentHeader dcHeader, OrderHeader coHeader)
        {
            var soHeader = JsonConvert.DeserializeObject<SalesOrderHeader>(_coHeaderJson);

            soHeader.OrderNumber = coHeader.DatabaseNum + "-" + coHeader.CentralOrderNum;
            soHeader.OrderDate = coHeader.OriginalOrderDateUtc;
            soHeader.OrderTime = coHeader.OriginalOrderDateUtc.TimeOfDay;
            soHeader.OrderSourceCode = "OrderDCAssignmentUuid:" + dcHeader.OrderDCAssignmentUuid;
            soHeader.UpdateDateUtc = _dtNowUtc;

            return soHeader;
        }

        private SalesOrderHeaderAttributes ChannelOrderToSalesOrderHeaderAttributes()
        {
            SalesOrderHeaderAttributes soHeaderAttributes = new SalesOrderHeaderAttributes();

            return soHeaderAttributes;
        }

        private SalesOrderHeaderInfo ChannelOrderToSalesOrderHeaderInfo(OrderDCAssignmentHeader dcHeader, OrderHeader coHeader)
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

        private List<SalesOrderItems> ChannelorderLineToSalesOrderLines(DCAssignmentData dcData, ChannelOrderData coData)
        {
            List<SalesOrderItems> soItemList = new List<SalesOrderItems>();

            int itemSeq = 1;
            foreach (var dcLine in dcData.OrderDCAssignmentLine)
            {
                var coLine = coData.OrderLine.FirstOrDefault(p => p.CentralOrderLineUuid == dcLine.CentralOrderLineUuid);
                if (coLine == null)
                {
                    AddError($"Data not found.");

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
                    TaxableAmount = coLine.LineItemTaxAmount ?? 0,
                    DiscountAmount = coLine.LinePromotionAmount ?? 0,
                    ShippingAmount = coLine.LineShippingAmount ?? 0,
                    ShippingTaxAmount = coLine.LineShippingTaxAmount ?? 0,
                    MiscAmount = coLine.LineGiftAmount ?? 0,
                    MiscTaxAmount = coLine.LineGiftTaxAmount ?? 0,
                    UpdateDateUtc = _dtNowUtc
                };

                soItemList.Add(soItem);
            }

            return soItemList;
        }

    }
}
