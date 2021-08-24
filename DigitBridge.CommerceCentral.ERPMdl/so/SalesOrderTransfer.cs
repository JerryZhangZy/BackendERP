using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.YoPoco;
using System.Xml.Serialization;
using System.Linq;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderTransfer : IMessage
    {
        private DateTime _dtNowUtc = DateTime.UtcNow;
        private DateTime _initNowUtc = new DateTime(1800, 12, 28);
        private string _userId;
        public SalesOrderTransfer(IMessage serviceMessage, string userId)
        {
            this.ServiceMessage = serviceMessage;

            _userId = userId;
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

        //private string _soUuid;
        public SalesOrderData FromChannelOrder(DCAssignmentData dcData, ChannelOrderData coData)
        {
            _dtNowUtc = DateTime.UtcNow;
            //_soUuid = Guid.NewGuid().ToString();

            SalesOrderData soData = new SalesOrderData();
            decimal dcQty = dcData.OrderDCAssignmentLine.Sum(p => p.OrderQty);
            decimal coQty = coData.OrderLine.Sum(p => p.OrderQty ?? 0);
            if(coQty <= 0)
            {
                AddError($"No ChannelOrder Qty {coQty}.");
                return null;
            }
            decimal qtyRatio = dcQty / coQty;

            soData.SalesOrderHeader = ChannelOrderToSalesOrderHeader(dcData.OrderDCAssignmentHeader, coData.OrderHeader, qtyRatio);

            soData.SalesOrderHeaderAttributes = ChannelOrderToSalesOrderHeaderAttributes();

            soData.SalesOrderHeaderInfo = ChannelOrderToSalesOrderHeaderInfo(dcData.OrderDCAssignmentHeader, coData.OrderHeader);

            soData.SalesOrderItems = ChannelorderLineToSalesOrderLines(dcData, coData);

            return soData;
        }

        private SalesOrderHeader ChannelOrderToSalesOrderHeader(OrderDCAssignmentHeader dcHeader
            , OrderHeader coHeader
            , decimal qtyRatio)
        {
            var soHeader = new SalesOrderHeader();
            //soHeader.SalesOrderUuid = _soUuid;
            soHeader.DatabaseNum = coHeader.DatabaseNum;
            soHeader.MasterAccountNum = coHeader.MasterAccountNum;
            soHeader.ProfileNum = coHeader.ProfileNum;
            soHeader.ProfileNum = coHeader.ProfileNum;
            //SalesOrderUuid
            soHeader.OrderNumber = coHeader.DatabaseNum + "-" + coHeader.CentralOrderNum + "-" + dcHeader.OrderDCAssignmentNum;
            //OrderType
            //OrderStatus
            soHeader.OrderDate = coHeader.OriginalOrderDateUtc;
            soHeader.OrderTime = coHeader.OriginalOrderDateUtc.TimeOfDay;
            soHeader.DueDate = _initNowUtc;
            soHeader.BillDate = _initNowUtc;
            //CustomerUuid
            //CustomerCode
            //CustomerName
            //Terms
            //TermsDays
            soHeader.Currency = coHeader.Currency;
            //SubTotalAmount
            //SalesAmount
            //TotalAmount
            //TaxableAmount
            //NonTaxableAmount
            //TaxRate
            //TaxAmount
            //DiscountRate
            soHeader.DiscountAmount = coHeader.PromotionAmount ?? 0;
            soHeader.ShippingAmount = (coHeader.TotalShippingAmount ?? 0) * qtyRatio;
            soHeader.ShippingTaxAmount = (coHeader.TotalShippingTaxAmount ?? 0) * qtyRatio;
            //MiscAmount
            //MiscTaxAmount
            //ChargeAndAllowanceAmount
            //PaidAmount
            //CreditAmount
            //Balance
            //UnitCost
            //AvgCost
            //LotCost
            soHeader.OrderSourceCode = "OrderDCAssignmentUuid:" + dcHeader.OrderDCAssignmentUuid;
            soHeader.UpdateDateUtc = _dtNowUtc;
            soHeader.EnterBy = _userId;
            //UpdateBy
            //EnterDateUtc
            //DigitBridgeGuid
            soHeader.ShipDate = _initNowUtc;
            return soHeader;
        }

        private SalesOrderHeaderAttributes ChannelOrderToSalesOrderHeaderAttributes()
        {
            SalesOrderHeaderAttributes soHeaderAttributes = new SalesOrderHeaderAttributes();
            soHeaderAttributes.RowNum = string.IsNullOrEmpty(soHeaderAttributes.JsonFields) ? 1 : 0; //1: not add, 0: add
            return soHeaderAttributes;
        }

        private SalesOrderHeaderInfo ChannelOrderToSalesOrderHeaderInfo(OrderDCAssignmentHeader dcHeader, OrderHeader coHeader)
        {
            string coHeaderJson = coHeader.ObjectToString();
            var soHeaderInfo = JsonConvert.DeserializeObject<SalesOrderHeaderInfo>(coHeaderJson);
            //soHeaderInfo.SalesOrderUuid = _soUuid;
            soHeaderInfo.RowNum = 0;
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

            var coHeader = coData.OrderHeader;

            int itemSeq = 1;
            //string soLnUuid = Guid.NewGuid().ToString();
            foreach (var dcLine in dcData.OrderDCAssignmentLine)
            {
                var coLine = coData.OrderLine.FirstOrDefault(p => p.CentralOrderLineUuid == dcLine.CentralOrderLineUuid);
                if (coLine == null)
                {
                    AddError($"Data not found.");

                    return (null);
                }

                SalesOrderItems soItem = new SalesOrderItems()
                {
                    //SalesOrderItemsUuid = soLnUuid,
                    //SalesOrderUuid = _soUuid,
                    Seq = itemSeq++,
                    //OrderItemType
                    //SalesOrderItemstatus
                    ItemDate = coHeader.OriginalOrderDateUtc,
                    ItemTime = coHeader.OriginalOrderDateUtc.TimeOfDay,
                    ShipDate = _initNowUtc,
                    EtaArrivalDate = _initNowUtc,
                    SKU = coLine.SKU,
                    //ProductUuid
                    //InventoryUuid
                    //WarehouseUuid
                    WarehouseCode = dcData.OrderDCAssignmentHeader.SellerWarehouseID,
                    //LotNum
                    Description = coLine.ItemTitle,
                    //Notes
                    Currency = coData.OrderHeader.Currency,
                    //UOM
                    //PackType
                    //PackQty
                    //OrderPack
                    //ShipPack
                    //CancelledPack
                    //OpenPack
                    OrderQty = dcLine.OrderQty,
                    //ShipQty
                    //CancelledQty
                    OpenQty = dcLine.OrderQty,
                    //PriceRule
                    Price = coLine.UnitPrice ?? 0,
                    //DiscountRate
                    DiscountAmount = coLine.LinePromotionAmount ?? 0,
                    //DiscountPrice
                    //ExtAmount
                    //TaxableAmount,
                    //NonTaxableAmount
                    //TaxRate
                    TaxAmount = coLine.LineItemTaxAmount ?? 0,
                    ShippingAmount = coLine.LineShippingAmount ?? 0,
                    ShippingTaxAmount = coLine.LineShippingTaxAmount ?? 0,
                    MiscAmount = coLine.LineGiftAmount ?? 0,
                    MiscTaxAmount = coLine.LineGiftTaxAmount ?? 0,
                    //ChargeAndAllowanceAmount
                    //ItemTotalAmount
                    //ShipAmount
                    //CancelledAmount
                    //OpenAmount
                    //Stockable
                    //IsAr
                    Taxable = coLine.LineItemTaxAmount.Value > 0 ,
                    //Costable
                    //IsProfit
                    //UnitCost
                    //AvgCost
                    //LotCost
                    LotInDate = _initNowUtc,
                    LotExpDate = _initNowUtc,
                    UpdateDateUtc = _dtNowUtc,
                    EnterBy = _userId,
                    //UpdateBy
                    //EnterDateUtc
                    //DigitBridgeGuid
                    CentralOrderLineUuid = dcLine.CentralOrderLineUuid,
                    DBChannelOrderLineRowID = dcLine.DBChannelOrderLineRowID,
                    OrderDCAssignmentLineUuid = dcLine.OrderDCAssignmentLineUuid
                };

                soItem.SalesOrderItemsAttributes = new SalesOrderItemsAttributes();
                soItem.SalesOrderItemsAttributes.RowNum = string.IsNullOrEmpty(soItem.SalesOrderItemsAttributes.JsonFields) ? 1 : 0; //1: not add, 0: add

                soItemList.Add(soItem);

            }

            return soItemList;
        }

    }
}
