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
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ShipmentTransfer : IMessage
    {
        private DateTime _dtNowUtc = DateTime.UtcNow;
        private DateTime _initNowUtc = new DateTime(1800, 12, 28);
        private string _userId;
        public ShipmentTransfer(IMessage serviceMessage, IDataBaseFactory dbFactory, string userId)
        {
            SetDataBaseFactory(dbFactory);
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

        #region DataBase
        [XmlIgnore, JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [XmlIgnore, JsonIgnore]
        public IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public void SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion DataBase

        #region load all shipment data from sales order
        public async Task<bool> FromSalesOrder(SalesOrderData soData, OrderShipmentService service)
        {
            if (soData == null) return false;

            _dtNowUtc = DateTime.UtcNow;
            //_soUuid = Guid.NewGuid().ToString();

            var shipmentData = service.Data;

            TransferOrderShipmentHeader(soData, shipmentData);
            TransferOrderShipmentPackage(soData, shipmentData);
            TransferOrderShipmentShippedItem(soData, shipmentData);
            TransferOrderShipmentCanceledItem(soData, shipmentData);

            return true;
        }

        protected void TransferOrderShipmentHeader(SalesOrderData soData, OrderShipmentData shipmentData)
        {
            var soHeader = soData.SalesOrderHeader;
            var soInfo = soData.SalesOrderHeaderInfo;
            if (shipmentData.OrderShipmentHeader == null)
                shipmentData.OrderShipmentHeader = shipmentData.NewOrderShipmentHeader();
            var shipHeader = shipmentData.OrderShipmentHeader;

            shipHeader.OrderShipmentUuid = Guid.NewGuid().ToString();
            shipHeader.DatabaseNum = soHeader.DatabaseNum;
            shipHeader.MasterAccountNum = soHeader.MasterAccountNum;
            shipHeader.ProfileNum = soHeader.ProfileNum;

            shipHeader.ChannelNum = soInfo.ChannelNum;
            shipHeader.ChannelAccountNum = soInfo.ChannelAccountNum;
            shipHeader.OrderDCAssignmentNum = soInfo.OrderDCAssignmentNum;
            shipHeader.DistributionCenterNum = soInfo.DistributionCenterNum;
            shipHeader.CentralOrderNum = soInfo.CentralOrderNum;
            shipHeader.ChannelOrderID = soInfo.ChannelOrderID;
            shipHeader.ShipmentID = string.Empty;
            shipHeader.WarehouseCode = soInfo.WarehouseCode;
            shipHeader.ShipmentType = 0;
            shipHeader.ShipmentReferenceID = string.Empty;
            shipHeader.ShipmentDateUtc = _dtNowUtc;
            shipHeader.ShippingCarrier = soInfo.ShippingCarrier;
            shipHeader.ShippingClass = soInfo.ShippingClass;
            shipHeader.ShippingCost = soHeader.ShippingAmount;
            shipHeader.MainTrackingNumber = string.Empty;
            shipHeader.MainReturnTrackingNumber = string.Empty;
            shipHeader.BillOfLadingID = string.Empty;
            shipHeader.TotalPackages = 1;
            shipHeader.TotalShippedQty = 1;
            shipHeader.TotalCanceledQty = 0;
            shipHeader.TotalWeight = 1;
            shipHeader.TotalVolume = 1;
            shipHeader.WeightUnit = 1;
            shipHeader.LengthUnit = 1;
            shipHeader.VolumeUnit = 1;
            shipHeader.ShipmentStatus = 0;
            shipHeader.DBChannelOrderHeaderRowID = soInfo.DBChannelOrderHeaderRowID;
            shipHeader.ProcessStatus = (int)OrderShipmentProcessStatusEnum.Pending;
            shipHeader.ProcessDateUtc = DateTime.MinValue;
            //shipHeader.EnterDateUtc =
            shipHeader.RowNum = 0;
            //shipHeader.DigitBridgeGuid = 
            shipHeader.InvoiceNumber = string.Empty;
            shipHeader.InvoiceUuid = string.Empty;
            shipHeader.SalesOrderUuid = soHeader.SalesOrderUuid;
            shipHeader.OrderNumber = soHeader.OrderNumber;

            return;
        }

        protected void TransferOrderShipmentPackage(SalesOrderData soData, OrderShipmentData shipmentData)
        {
            var soItemList = soData.SalesOrderItems;
            var soHeader = soData.SalesOrderHeader;
            var soInfo = soData.SalesOrderHeaderInfo;

            var shipHeader = shipmentData.OrderShipmentHeader;

            if (shipmentData.OrderShipmentPackage == null)
                shipmentData.OrderShipmentPackage = new List<OrderShipmentPackage>();
            shipmentData.OrderShipmentPackage.Clear();
            var obj = shipmentData.NewOrderShipmentPackage();
            shipmentData.OrderShipmentPackage.Add(obj);

            //obj.OrderShipmentPackageNum = 
            obj.DatabaseNum = shipHeader.DatabaseNum;
            obj.MasterAccountNum = shipHeader.MasterAccountNum;
            obj.ProfileNum = shipHeader.ProfileNum;
            obj.ChannelNum = shipHeader.ChannelNum;
            obj.ChannelAccountNum = shipHeader.ChannelAccountNum;
            obj.OrderShipmentNum = shipHeader.OrderShipmentNum;
            obj.PackageID = "Package-10001";
            obj.PackageType = 0;
            obj.PackagePatternNum = 0;
            obj.PackageTrackingNumber = string.Empty;
            obj.PackageReturnTrackingNumber = string.Empty;
            obj.PackageWeight = 1;
            obj.PackageLength = 1;
            obj.PackageWidth = 1;
            obj.PackageHeight = 1;
            obj.PackageVolume = 1;
            obj.PackageQty = 1;
            obj.ParentPackageNum = 0;
            obj.HasChildPackage = false;
            //obj.EnterDateUtc = 
            obj.OrderShipmentUuid = shipHeader.OrderShipmentUuid;
            obj.OrderShipmentPackageUuid = Guid.NewGuid().ToString();
        }

        protected void TransferOrderShipmentShippedItem(SalesOrderData soData, OrderShipmentData shipmentData)
        {
            var soItemList = soData.SalesOrderItems;
            var soHeader = soData.SalesOrderHeader;
            var soInfo = soData.SalesOrderHeaderInfo;

            var shipHeader = shipmentData.OrderShipmentHeader;
            var package = shipmentData.OrderShipmentPackage[0];

            if (package.OrderShipmentShippedItem == null)
                package.OrderShipmentShippedItem = new List<OrderShipmentShippedItem>();
            package.OrderShipmentShippedItem.Clear();

            foreach (var item in soItemList)
            {
                if (item == null || item.IsEmpty) continue;
                var obj = package.NewOrderShipmentShippedItem();
                package.OrderShipmentShippedItem.Add(obj);

                //obj.OrderShipmentShippedItemNum = 
                obj.DatabaseNum = shipHeader.DatabaseNum;
                obj.MasterAccountNum = shipHeader.MasterAccountNum;
                obj.ProfileNum = shipHeader.ProfileNum;
                obj.ChannelNum = shipHeader.ChannelNum;
                obj.ChannelAccountNum = shipHeader.ChannelAccountNum;
                obj.OrderShipmentNum = shipHeader.OrderShipmentNum;
                obj.OrderShipmentPackageNum = package.OrderShipmentPackageNum;
                obj.ChannelOrderID = shipHeader.ChannelOrderID;
                obj.OrderDCAssignmentLineNum = shipHeader.OrderDCAssignmentNum;
                obj.SKU = item.SKU;
                obj.ShippedQty = item.OpenQty;
                obj.DBChannelOrderLineRowID = item.DBChannelOrderLineRowID;
                //obj.EnterDateUtc = 
                obj.OrderShipmentUuid = shipHeader.OrderShipmentUuid;
                obj.OrderShipmentPackageUuid = package.OrderShipmentPackageUuid;
                obj.OrderShipmentShippedItemUuid = Guid.NewGuid().ToString();
                obj.RowNum = 0;
                //obj.DigitBridgeGuid = 
                obj.SalesOrderItemsUuid = item.SalesOrderItemsUuid;
            }
        }

        protected void TransferOrderShipmentCanceledItem(SalesOrderData soData, OrderShipmentData shipmentData)
        {
            shipmentData.OrderShipmentCanceledItem = new List<OrderShipmentCanceledItem>();
        }
        #endregion 

        // shipment data already created, load other data from sales order
        public async Task<bool> LoadOthersDataFromSalesOrder(SalesOrderData soData, OrderShipmentDataDto shipmentDto)
        {
            if (soData == null || shipmentDto == null) return false;

            _dtNowUtc = DateTime.UtcNow;
            //_soUuid = Guid.NewGuid().ToString();

            LoadOrderShipmentHeader(soData, shipmentDto);
            LoadOrderShipmentPackage(soData, shipmentDto);
            LoadOrderShipmentCanceledItem(soData, shipmentDto);

            return true;
        }

        protected void LoadOrderShipmentHeader(SalesOrderData soData, OrderShipmentDataDto shipmentDto)
        {
            var soHeader = soData.SalesOrderHeader;
            var soInfo = soData.SalesOrderHeaderInfo;
            if (shipmentDto.OrderShipmentHeader == null)
                shipmentDto.OrderShipmentHeader = new OrderShipmentHeaderDto();
            var shipHeader = shipmentDto.OrderShipmentHeader;

            shipHeader.OrderShipmentUuid = Guid.NewGuid().ToString();
            shipHeader.DatabaseNum = soHeader.DatabaseNum;
            shipHeader.MasterAccountNum = soHeader.MasterAccountNum;
            shipHeader.ProfileNum = soHeader.ProfileNum;

            shipHeader.ChannelNum = soInfo.ChannelNum;
            shipHeader.ChannelAccountNum = soInfo.ChannelAccountNum;
            //shipHeader.OrderDCAssignmentNum = soHeader.OrderSourceCode.IsZero() ? 0 : soHeader.OrderSourceCode.Replace(Consts.SalesOrderSourceCode_Prefix, "").ToInt();
            shipHeader.OrderDCAssignmentNum = soInfo.OrderDCAssignmentNum;
            shipHeader.DistributionCenterNum = soInfo.DistributionCenterNum;
            //shipHeader.CentralOrderNum = soInfo.CentralOrderNum;
            //shipHeader.ChannelOrderID = soInfo.ChannelOrderID;
            //shipHeader.ShipmentID = string.Empty;
            shipHeader.WarehouseCode = soInfo.WarehouseCode;
            //shipHeader.ShipmentType = 0;
            //shipHeader.ShipmentReferenceID = string.Empty;
            //shipHeader.ShipmentDateUtc = _dtNowUtc;
            //shipHeader.ShippingCarrier = soInfo.ShippingCarrier;
            //shipHeader.ShippingClass = soInfo.ShippingClass;
            //shipHeader.ShippingCost = soHeader.ShippingAmount;
            //shipHeader.MainTrackingNumber = string.Empty;
            //shipHeader.MainReturnTrackingNumber = string.Empty;
            //shipHeader.BillOfLadingID = string.Empty;
            //shipHeader.TotalPackages = 1;
            //shipHeader.TotalShippedQty = 1;
            //shipHeader.TotalCanceledQty = 0;
            //shipHeader.TotalWeight = 1;
            //shipHeader.TotalVolume = 1;
            //shipHeader.WeightUnit = 1;
            //shipHeader.LengthUnit = 1;
            //shipHeader.VolumeUnit = 1;
            //shipHeader.ShipmentStatus = 0;
            shipHeader.DBChannelOrderHeaderRowID = soInfo.DBChannelOrderHeaderRowID;
            shipHeader.ProcessStatus = (int)OrderShipmentProcessStatusEnum.Pending;
            shipHeader.ProcessDateUtc = DateTime.MinValue;
            //shipHeader.EnterDateUtc =
            shipHeader.RowNum = 0;
            //shipHeader.DigitBridgeGuid = 
            shipHeader.InvoiceNumber = string.Empty;
            shipHeader.InvoiceUuid = string.Empty;
            shipHeader.SalesOrderUuid = soHeader.SalesOrderUuid;
            shipHeader.OrderNumber = soHeader.OrderNumber;

            return;
        }

        protected void LoadOrderShipmentPackage(SalesOrderData soData, OrderShipmentDataDto shipmentDto)
        {
            var soItemList = soData.SalesOrderItems;
            var soHeader = soData.SalesOrderHeader;
            var soInfo = soData.SalesOrderHeaderInfo;

            var shipHeader = shipmentDto.OrderShipmentHeader;

            if (shipmentDto.OrderShipmentPackage == null)
                return;

            foreach (var package in shipmentDto.OrderShipmentPackage)
            {
                if (package == null) continue;

                //obj.OrderShipmentPackageNum = 
                package.DatabaseNum = shipHeader.DatabaseNum;
                package.MasterAccountNum = shipHeader.MasterAccountNum;
                package.ProfileNum = shipHeader.ProfileNum;
                package.ChannelNum = shipHeader.ChannelNum;
                package.ChannelAccountNum = shipHeader.ChannelAccountNum;
                package.OrderShipmentNum = shipHeader.OrderShipmentNum;
                //package.PackageID = "Package-10001";
                //package.PackageType = 0;
                //package.PackagePatternNum = 0;
                //package.PackageTrackingNumber = string.Empty;
                //package.PackageReturnTrackingNumber = string.Empty;
                //package.PackageWeight = 1;
                //package.PackageLength = 1;
                //package.PackageWidth = 1;
                //package.PackageHeight = 1;
                //package.PackageVolume = 1;
                //package.PackageQty = 1;
                //package.ParentPackageNum = 0;
                //package.HasChildPackage = false;
                package.OrderShipmentUuid = shipHeader.OrderShipmentUuid;
                package.OrderShipmentPackageUuid = Guid.NewGuid().ToString();

                foreach (var item in package.OrderShipmentShippedItem)
                {
                    if (item == null) continue;
                    var soItem = soItemList.FindById(item.SalesOrderItemsUuid);
                    if (soItem == null) continue;

                    //item.OrderShipmentShippedItemNum = 
                    item.DatabaseNum = shipHeader.DatabaseNum;
                    item.MasterAccountNum = shipHeader.MasterAccountNum;
                    item.ProfileNum = shipHeader.ProfileNum;
                    item.ChannelNum = shipHeader.ChannelNum;
                    item.ChannelAccountNum = shipHeader.ChannelAccountNum;
                    item.OrderShipmentNum = shipHeader.OrderShipmentNum;
                    item.OrderShipmentPackageNum = package.OrderShipmentPackageNum;
                    item.ChannelOrderID = shipHeader.ChannelOrderID;
                    //item.OrderDCAssignmentLineNum = shipHeader.OrderDCAssignmentNum;
                    //item.SKU = item.SKU;
                    //item.ShippedQty = item.OpenQty;
                    item.DBChannelOrderLineRowID = soItem.DBChannelOrderLineRowID;
                    item.OrderShipmentUuid = shipHeader.OrderShipmentUuid;
                    item.OrderShipmentPackageUuid = package.OrderShipmentPackageUuid;
                    item.OrderShipmentShippedItemUuid = Guid.NewGuid().ToString();
                    item.RowNum = 0;
                    //item.SalesOrderItemsUuid = item.SalesOrderItemsUuid;
                }
            }
        }

        protected void LoadOrderShipmentCanceledItem(SalesOrderData soData, OrderShipmentDataDto shipmentDto)
        {
            return;
        }

    }
}
