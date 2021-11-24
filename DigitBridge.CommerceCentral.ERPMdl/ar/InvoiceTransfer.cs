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
    public class InvoiceTransfer : IMessage
    {
        private DateTime _dtNowUtc = DateTime.UtcNow;
        private string _userId;
        public InvoiceTransfer(IMessage serviceMessage, string userId)
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

        public InvoiceData FromOrderShipmentAndSalesOrder(OrderShipmentData osData, SalesOrderData soData, InvoiceData invoiceData)
        {
            if (invoiceData == null)
                invoiceData = new InvoiceData();

            OrderShipmentAndSalesOrderToInvoiceHeader(osData.OrderShipmentHeader, soData.SalesOrderHeader, invoiceData);

            invoiceData.InvoiceHeaderAttributes = OrderShipmentAndSalesOrderToInvoiceHeaderAttributes();

            OrderShipmentAndSalesOrderToInvoiceHeaderInfo(osData.OrderShipmentHeader, soData.SalesOrderHeaderInfo, invoiceData);

            OrderShipmentAndSalesOrderToInvoiceLines(osData, soData, invoiceData);

            return invoiceData;
        }

        private InvoiceHeader OrderShipmentAndSalesOrderToInvoiceHeader(OrderShipmentHeader osHeader, SalesOrderHeader soHeader, InvoiceData invoiceData)
        {
            if (invoiceData.InvoiceHeader == null)
                invoiceData.InvoiceHeader = invoiceData.NewInvoiceHeader();
            var invoiceHeader = invoiceData.InvoiceHeader;

            invoiceHeader.DatabaseNum = soHeader.DatabaseNum;
            invoiceHeader.MasterAccountNum = soHeader.MasterAccountNum;
            invoiceHeader.ProfileNum = soHeader.ProfileNum;
            //InvoiceUuid
            invoiceHeader.InvoiceUuid = Guid.NewGuid().ToString();
            invoiceHeader.InvoiceNumber = string.IsNullOrEmpty(osHeader.InvoiceNumber) ? 
                soHeader.OrderNumber + "-" + osHeader.OrderShipmentNum : osHeader.InvoiceNumber;

            invoiceHeader.SalesOrderUuid = soHeader.SalesOrderUuid;
            invoiceHeader.OrderNumber = soHeader.OrderNumber;
            invoiceHeader.InvoiceType = soHeader.OrderType; //(int)InvoiceType.Sales;
            invoiceHeader.InvoiceStatus = (int)InvoiceStatusEnum.New;
            invoiceHeader.InvoiceDate = _dtNowUtc;
            invoiceHeader.InvoiceTime = _dtNowUtc.TimeOfDay;
#if DEBUG
            invoiceHeader.CustomerUuid = Guid.NewGuid().ToString();
            invoiceHeader.CustomerCode = "Test-YM";
            invoiceHeader.CustomerName = "Test-YM";
            invoiceHeader.Terms = "Basic";
            invoiceHeader.TermsDays = 30;
#else
            invoiceHeader.CustomerUuid = soHeader.CustomerUuid;
            invoiceHeader.CustomerCode = soHeader.CustomerCode;
            invoiceHeader.CustomerName = soHeader.CustomerName;
            invoiceHeader.Terms = soHeader.Terms;
            invoiceHeader.TermsDays = soHeader.TermsDays;
#endif
            invoiceHeader.DueDate = invoiceHeader.InvoiceDate.AddDays(invoiceHeader.TermsDays);
            invoiceHeader.BillDate = invoiceHeader.InvoiceDate;
            invoiceHeader.Currency = soHeader.Currency;
            //SubTotalAmount
            //SalesAmount
            //TotalAmount
            //TaxableAmount
            //NonTaxableAmount
            //TaxRate
            //TaxAmount
            //DiscountRate
            //DiscountAmount
            //ShippingAmount
            //ShippingTaxAmount
            //MiscAmount
            //MiscTaxAmount
            //ChargeAndAllowanceAmount
            //PaidAmount
            //CreditAmount
            //Balance
            //UnitCost
            //AvgCost
            //LotCost
            invoiceHeader.InvoiceSourceCode = "OrderShipmentUuid:" + osHeader.OrderShipmentUuid;
             
            invoiceHeader.EnterBy = _userId;
            //UpdateBy
            //EnterDateUtc
            //DigitBridgeGuid
            invoiceHeader.ShipDate = soHeader.ShipDate;
            return invoiceHeader;
        }

        private InvoiceHeaderAttributes OrderShipmentAndSalesOrderToInvoiceHeaderAttributes()
        {
            InvoiceHeaderAttributes invoieHeaderAttributes = new InvoiceHeaderAttributes();
            invoieHeaderAttributes.RowNum = string.IsNullOrEmpty(invoieHeaderAttributes.JsonFields) ? 1 : 0; //1: not add, 0: add
            return invoieHeaderAttributes;
        }

        private InvoiceHeaderInfo OrderShipmentAndSalesOrderToInvoiceHeaderInfo(OrderShipmentHeader osHeader, SalesOrderHeaderInfo soHeaderInfo, InvoiceData invoiceData)
        {
            string soHeaderInfoJson = soHeaderInfo.ObjectToString();
            var invoiceHeaderInfo = JsonConvert.DeserializeObject<InvoiceHeaderInfo>(soHeaderInfoJson);

            invoiceData.InvoiceHeaderInfo = invoiceHeaderInfo;

            invoiceHeaderInfo.RowNum = 0;

            invoiceHeaderInfo.OrderShipmentNum = osHeader.OrderShipmentNum;
            invoiceHeaderInfo.OrderShipmentUuid = osHeader.OrderShipmentUuid;
            invoiceHeaderInfo.ShippingCarrier = osHeader.ShippingCarrier;
            invoiceHeaderInfo.ShippingClass = osHeader.ShippingClass;
            invoiceHeaderInfo.WarehouseCode = osHeader.WarehouseCode;

            invoiceHeaderInfo.RefNum = osHeader.ShipmentReferenceID;

            return invoiceHeaderInfo;
        }

        private IList<InvoiceItems> OrderShipmentAndSalesOrderToInvoiceLines(OrderShipmentData osData, SalesOrderData soData, InvoiceData invoiceData)
        {
            if (invoiceData.InvoiceItems == null)
                invoiceData.InvoiceItems = new List<InvoiceItems>();
            var invoiceItemList = invoiceData.InvoiceItems;

            var osHeader = osData.OrderShipmentHeader;
            var soHeader = soData.SalesOrderHeader;

            int itemSeq = 1;
            foreach (var osPkg in osData.OrderShipmentPackage)
            {
                foreach (var osItem in osPkg.OrderShipmentShippedItem)
                {
                    var soLine = soData.SalesOrderItems.FirstOrDefault(p => p.OrderDCAssignmentLineNum == osItem.OrderDCAssignmentLineNum);
                    if (soLine == null)
                        soLine = soData.SalesOrderItems.FirstOrDefault(p => p.SalesOrderItemsUuid.EqualsIgnoreSpace(osItem.SalesOrderItemsUuid));
                    if (soLine == null)
                        {
                        AddError($"SKU {osItem.SKU} not found in sales order items.");
                        return null;
                    }

                    InvoiceItems invoiceItem = new InvoiceItems()
                    {
                        Seq = itemSeq++,
                        InvoiceItemType = soLine.OrderItemType,
                        InvoiceItemStatus = soLine.SalesOrderItemstatus,
                        ItemDate = _dtNowUtc,
                        ItemTime = _dtNowUtc.TimeOfDay,
                        ShipDate = osHeader.ShipmentDateUtc,
                        EtaArrivalDate = soLine.EtaArrivalDate,
                        SKU = soLine.SKU,
                        ProductUuid = soLine.ProductUuid,
                        InventoryUuid = soLine.InventoryUuid,
                        WarehouseCode = invoiceData.InvoiceHeaderInfo.WarehouseCode,
                        WarehouseUuid = invoiceData.InvoiceHeaderInfo.WarehouseUuid,

                        LotNum = soLine.LotNum,
                        Description = soLine.Description,
                        Notes = soLine.Notes,
                        Currency = soLine.Currency,
                        UOM = soLine.Currency,
                        PackType = soLine.PackType,
                        PackQty = soLine.PackQty,
                        //PackQty = osPkg.PackageQty ?? 0,
                        OrderPack = soLine.OrderPack - soLine.ShipPack - soLine.CancelledPack,
                        //ShipPack
                        OrderQty = soLine.OrderQty - soLine.ShipQty - soLine.CancelledQty,
                        ShipQty = osItem.ShippedQty,
                        //PriceRule
                        Price = soLine.Price,
                        DiscountRate = soLine.DiscountRate,
                        DiscountAmount = soLine.DiscountAmount,
                        DiscountPrice = soLine.DiscountPrice,
                        //ExtAmount
                        //TaxableAmount,
                        //NonTaxableAmount
                        //TaxRate
                        //TaxAmount = 0,
                        //ShippingAmount = 0,
                        //ShippingTaxAmount = 0,
                        //MiscAmount = 0,
                        //MiscTaxAmount = 0,
                        //ChargeAndAllowanceAmount
                        //ItemTotalAmount
                        //ShipAmount
                        //CancelledAmount
                        //OpenAmount
                        Stockable = soLine.Stockable,
                        IsAr = soLine.IsAr,
                        Taxable = soLine.Taxable,
                        Costable = soLine.Costable,
                        IsProfit = soLine.IsProfit,
                        //UnitCost
                        //AvgCost
                        //LotCost
                        //LotInDate
                        //LotExpDate 
                        EnterBy = _userId,
                        //UpdateBy
                        //EnterDateUtc
                        //DigitBridgeGuid
                    };

                    invoiceItem.InvoiceItemsAttributes = new InvoiceItemsAttributes();
                    invoiceItem.InvoiceItemsAttributes.RowNum = string.IsNullOrEmpty(invoiceItem.InvoiceItemsAttributes.JsonFields) ? 1 : 0; //1: not add, 0: add

                    invoiceItemList.Add(invoiceItem);
                }
            }

            return invoiceItemList;
        }

    }
}
