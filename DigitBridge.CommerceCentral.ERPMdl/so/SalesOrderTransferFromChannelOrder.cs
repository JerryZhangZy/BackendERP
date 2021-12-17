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
    public class SalesOrderTransferFromChannelOrder : IMessage
    {
        private DateTime _dtNowUtc = DateTime.UtcNow;
        private DateTime _initNowUtc = new DateTime(1800, 12, 28);
        private string _userId;
        public SalesOrderTransferFromChannelOrder(IMessage serviceMessage, string userId)
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
        public SalesOrderData FromChannelOrder(DCAssignmentData dcData, ChannelOrderData coData, SalesOrderService service)
        {
            _dtNowUtc = DateTime.UtcNow;
            //_soUuid = Guid.NewGuid().ToString();

            //SalesOrderData soData = new SalesOrderData();
            SalesOrderData soData = service.Data;
            decimal dcQty = dcData.OrderDCAssignmentLine.Sum(p => p.OrderQty);
            decimal coQty = coData.OrderLine.Sum(p => p.OrderQty ?? 0);
            if (coQty <= 0)
            {
                AddError($"No ChannelOrder Qty {coQty}.");
                return null;
            }
            decimal qtyRatio = dcQty / coQty;

            soData.SalesOrderHeader = ChannelOrderToSalesOrderHeader(dcData, coData, qtyRatio);

            soData.SalesOrderHeaderAttributes = ChannelOrderToSalesOrderHeaderAttributes();

            soData.SalesOrderHeaderInfo = ChannelOrderToSalesOrderHeaderInfo(dcData, coData);

            soData.SalesOrderItems = ChannelOrderToSalesOrderLines(dcData, coData, soData);

            return soData;
        }

        private SalesOrderHeader ChannelOrderToSalesOrderHeader(DCAssignmentData dcData, ChannelOrderData coData, decimal qtyRatio)
        {
            var dcHeader = dcData.OrderDCAssignmentHeader;
            var coHeader = coData.OrderHeader;

            var soHeader = new SalesOrderHeader();
            soHeader.DatabaseNum = coHeader.DatabaseNum;
            soHeader.MasterAccountNum = coHeader.MasterAccountNum;
            soHeader.ProfileNum = coHeader.ProfileNum;
            //SalesOrderUuid
            //soHeader.OrderNumber = coHeader.DatabaseNum + "-" + coHeader.CentralOrderNum + "-" + dcHeader.OrderDCAssignmentNum;
            soHeader.OrderNumber = coHeader.CentralOrderNum + "-" + dcHeader.OrderDCAssignmentNum;
            soHeader.OrderType = (int)SalesOrderType.EcommerceOrder;
            soHeader.OrderStatus = (int)SalesOrderStatus.New;
            soHeader.OrderDate = coHeader.OriginalOrderDateUtc;
            soHeader.OrderTime = coHeader.OriginalOrderDateUtc.TimeOfDay;
            soHeader.ShipDate = coHeader.EstimatedShipDateUtc.IsZero() ? _dtNowUtc : coHeader.EstimatedShipDateUtc;
            soHeader.EtaArrivalDate = coHeader.DeliverByDateUtc;
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
            soHeader.DiscountAmount = Math.Abs(coHeader.PromotionAmount.ToDecimal());
            soHeader.ShippingAmount = (coHeader.TotalShippingAmount ?? 0) * qtyRatio;
            soHeader.ShippingTaxAmount = (coHeader.TotalShippingTaxAmount ?? 0) * qtyRatio;
            //MiscAmount
            //MiscTaxAmount
            //ChargeAndAllowanceAmount
            soHeader.ChannelAmount = coHeader.TotalDueSellerAmount;
            soHeader.PaidAmount = (coHeader.PaymentStatus) ? coHeader.TotalDueSellerAmount : 0;
            //CreditAmount
            //Balance
            //UnitCost
            //AvgCost
            //LotCost
            //soHeader.OrderSourceCode = "OrderDCAssignmentUuid:" + dcHeader.OrderDCAssignmentUuid;
            soHeader.OrderSourceCode = Consts.SalesOrderSourceCode_Prefix + dcHeader.OrderDCAssignmentNum;
            soHeader.UpdateDateUtc = _dtNowUtc;
            soHeader.EnterBy = _userId;
            soHeader.SignatureFlag = !string.IsNullOrEmpty(coHeader.SignatureFlag);
            //UpdateBy
            //EnterDateUtc
            //DigitBridgeGuid
            return soHeader;
        }

        private SalesOrderHeaderAttributes ChannelOrderToSalesOrderHeaderAttributes()
        {
            SalesOrderHeaderAttributes soHeaderAttributes = new SalesOrderHeaderAttributes();
            soHeaderAttributes.RowNum = string.IsNullOrEmpty(soHeaderAttributes.JsonFields) ? 1 : 0; //1: not add, 0: add
            return soHeaderAttributes;
        }

        private SalesOrderHeaderInfo ChannelOrderToSalesOrderHeaderInfo(DCAssignmentData dcData, ChannelOrderData coData)
        {
            var dcHeader = dcData.OrderDCAssignmentHeader;
            var coHeader = coData.OrderHeader;
            string coHeaderJson = coHeader.ObjectToString();
            var soHeaderInfo = JsonConvert.DeserializeObject<SalesOrderHeaderInfo>(coHeaderJson);

            //soHeaderInfo.SalesOrderUuid = _soUuid;
            soHeaderInfo.RowNum = 0;
            soHeaderInfo.CentralOrderNum = dcHeader.CentralOrderNum;
            soHeaderInfo.CentralOrderUuid = dcHeader.CentralOrderUuid;
            soHeaderInfo.ChannelNum = dcHeader.ChannelNum;
            soHeaderInfo.ChannelAccountNum = dcHeader.ChannelAccountNum;
            soHeaderInfo.ChannelOrderID = dcHeader.ChannelOrderID;
            soHeaderInfo.SecondaryChannelOrderID = coHeader.SecondaryChannelOrderID;
            soHeaderInfo.ChannelOrderID = dcHeader.ChannelOrderID;
            soHeaderInfo.OrderDCAssignmentNum = dcHeader.OrderDCAssignmentNum;
            soHeaderInfo.DBChannelOrderHeaderRowID = dcHeader.DBChannelOrderHeaderRowID;

            soHeaderInfo.CentralFulfillmentNum = dcHeader.OrderDCAssignmentNum;//todo check this.
            soHeaderInfo.DistributionCenterNum = dcHeader.DistributionCenterNum;
            soHeaderInfo.WarehouseCode = dcData.WarehouseCode;

            soHeaderInfo.ShippingCarrier = coHeader.RequestedShippingCarrier;
            soHeaderInfo.ShippingClass = coHeader.RequestedShippingClass;
            soHeaderInfo.EndBuyerName = coHeader.BillToName;

            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(coHeader.SellerPublicNote))
                sb.AppendLine(coHeader.SellerPublicNote);
            if (string.IsNullOrEmpty(coHeader.SellerPrivateNote))
                sb.AppendLine(coHeader.SellerPrivateNote);
            soHeaderInfo.Notes = sb.ToString();
            
            soHeaderInfo.UpdateDateUtc = _dtNowUtc;

            return soHeaderInfo;
        }

        private IList<SalesOrderItems> ChannelOrderToSalesOrderLines(DCAssignmentData dcData, ChannelOrderData coData, SalesOrderData soData)
        {
            var soItemList = soData.SalesOrderItems;

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

                var soItem = soData.GetNewSalesOrderItems();
                //soItem.SalesOrderItemsUuid = soLnUuid;
                //soItem.SalesOrderUuid = _soUuid;
                soItem.Seq = itemSeq++;
                soItem.OrderItemType = (int)SalesOrderType.EcommerceOrder;
                soItem.SalesOrderItemstatus = (int)SalesOrderStatus.New;
                soItem.ItemDate = coHeader.OriginalOrderDateUtc;
                soItem.ItemTime = coHeader.OriginalOrderDateUtc.TimeOfDay;
                soItem.ShipDate = _initNowUtc;
                soItem.EtaArrivalDate = _initNowUtc;
                soItem.SKU = coLine.SKU;
                //soItem.ProductUuid
                //soItem.InventoryUuid
                //soItem.WarehouseUuid
                //soItem.WarehouseCode = dcData.OrderDCAssignmentHeader.SellerWarehouseID,
                soItem.WarehouseCode = dcData.WarehouseCode;
                //soItem.LotNum
                soItem.Description = coLine.ItemTitle;
                //soItem.Notes
                soItem.Currency = coData.OrderHeader.Currency;
                //soItem.UOM
                //soItem.PackType
                //soItem.PackQty
                //soItem.OrderPack
                //soItem.ShipPack
                //soItem.CancelledPack
                //soItem.OpenPack
                soItem.OrderQty = dcLine.OrderQty;
                soItem.ShipQty = 0;
                soItem.CancelledQty = 0;
                soItem.OpenQty = dcLine.OrderQty;
                //soItem.PriceRule
                soItem.Price = coLine.UnitDueSellerAmount; // coLine.UnitPrice ?? 0,
                //soItem.DiscountRate
                soItem.DiscountAmount = Math.Abs(coLine.LinePromotionAmount.ToDecimal());
                //soItem.DiscountPrice
                //soItem.ExtAmount
                //soItem.TaxableAmount,
                //soItem.NonTaxableAmount
                //soItem.TaxRate
                soItem.TaxAmount = coLine.LineItemTaxAmount ?? 0;
                soItem.ShippingAmount = coLine.LineShippingAmount ?? 0;
                soItem.ShippingTaxAmount = coLine.LineShippingTaxAmount ?? 0;
                soItem.MiscAmount = coLine.LineGiftAmount ?? 0;
                soItem.MiscTaxAmount = coLine.LineGiftTaxAmount ?? 0;
                //soItem.ChargeAndAllowanceAmount
                //soItem.ItemTotalAmount
                //soItem.ShipAmount
                //soItem.CancelledAmount
                //soItem.OpenAmount
                //soItem.Stockable = true;
                //soItem.IsAr = true;
                soItem.Taxable = coLine.LineItemTaxAmount.Value > 0;
                //soItem.Costable = true;
                //soItem.IsProfit = true;
                //soItem.UnitCost
                //soItem.AvgCost
                //soItem.LotCost
                soItem.LotInDate = _initNowUtc;
                soItem.LotExpDate = _initNowUtc;
                soItem.UpdateDateUtc = _dtNowUtc;
                soItem.EnterBy = _userId;
                //soItem.UpdateBy
                //soItem.EnterDateUtc
                //soItem.DigitBridgeGuid
                soItem.CentralOrderLineUuid = dcLine.CentralOrderLineUuid;
                soItem.DBChannelOrderLineRowID = dcLine.DBChannelOrderLineRowID;
                soItem.OrderDCAssignmentLineUuid = dcLine.OrderDCAssignmentLineUuid;
                soItem.OrderDCAssignmentLineNum = dcLine.OrderDCAssignmentLineNum;

                soItem.SalesOrderItemsAttributes = new SalesOrderItemsAttributes();
                soItem.SalesOrderItemsAttributes.RowNum = string.IsNullOrEmpty(soItem.SalesOrderItemsAttributes.JsonFields) ? 1 : 0; //1: not add, 0: add

                //soItemList.Add(soItem);

            }

            return soItemList.ToList();
        }

    }
}
