
using System;
using System.Collections.Generic;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Mapper wms shipment to erp shipment
    /// </summary>
    public class WMSOrderShipmentMapper
    {
        /// <summary>
        /// login MasterAccountNum
        /// </summary>
        protected int MasterAccountNum;
        /// <summary>
        /// login profile num
        /// </summary>
        protected int ProfileNum;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        public WMSOrderShipmentMapper(int masterAccountNum, int profileNum)
        {
            MasterAccountNum = masterAccountNum;
            ProfileNum = profileNum;
        }
        /// <summary>
        /// Mapper wms shipment to erp shipment
        /// </summary>
        /// <param name="inputShipment"></param>
        /// <returns></returns>
        public OrderShipmentDataDto MapperToErpShipment(InputOrderShipmentType inputShipment)
        {
            if (inputShipment is null)
                return null;

            var dataDto = new OrderShipmentDataDto()
            {
                OrderShipmentHeader = Write(inputShipment.ShipmentHeader),
                OrderShipmentPackage = Write(inputShipment.PackageItems),
                OrderShipmentCanceledItem = Write(inputShipment.CanceledItems),
            };
            return dataDto;
        }

        /// <summary>
        /// write header
        /// </summary>
        /// <param name="inputShipmentHeader"></param>
        /// <returns></returns>
        protected OrderShipmentHeaderDto Write(InputOrderShipmentHeaderType inputShipmentHeader)
        {
            if (inputShipmentHeader is null)
                return null;

            var header = new OrderShipmentHeaderDto();
            header.MasterAccountNum = MasterAccountNum;
            header.ProfileNum = ProfileNum;
            header.ShipmentID = inputShipmentHeader.ShipmentID;
            header.OrderDCAssignmentNum = inputShipmentHeader.OrderDCAssignmentNum;
            header.CentralOrderNum = inputShipmentHeader.CentralOrderNum;
            //publicntistributionCenterNum;
            header.ChannelOrderID = inputShipmentHeader.ChannelOrderID;
            header.WarehouseCode = inputShipmentHeader.WarehouseCode;
            //header = inputShipmentHeader.WarehouseID ;
            header.ShipmentType = (int)inputShipmentHeader.ShipmentType;
            header.ShipmentReferenceID = inputShipmentHeader.ShipmentReferenceID;
            header.ShipmentDateUtc = inputShipmentHeader.ShipmentDateUtc;
            header.ShippingCarrier = inputShipmentHeader.ShippingCarrier;
            header.ShippingClass = inputShipmentHeader.ShippingClass;
            header.ShippingCost = inputShipmentHeader.ShippingCost;//= 0; 
            header.MainTrackingNumber = inputShipmentHeader.MainTrackingNumber;
            header.MainReturnTrackingNumber = inputShipmentHeader.MainReturnTrackingNumber;
            header.BillOfLadingID = inputShipmentHeader.BillOfLadingID;
            header.TotalPackages = inputShipmentHeader.TotalPackages;
            header.TotalHandlingFee = inputShipmentHeader.TotalHandlingFee;
            header.TotalShippedQty = inputShipmentHeader.TotalShippedQty;
            header.TotalCanceledQty = inputShipmentHeader.TotalCanceledQty;
            header.TotalWeight = inputShipmentHeader.TotalWeight;
            header.TotalVolume = inputShipmentHeader.TotalVolume;
            header.WeightUnit = (int)inputShipmentHeader.WeightUnit; //= WeightUnitEnum.Pound;
            header.LengthUnit = (int)inputShipmentHeader.LengthUnit;//= LengthUnitEnum.Inch;
            header.VolumeUnit = (int)inputShipmentHeader.VolumeUnit;//= VolumeUnitEnum.CubicInch;
            header.ShipmentStatus = (int)inputShipmentHeader.ShipmentStatus;//OrderStatus: Shipped = 1,PartiallyShipped = 2, Canceled = 16

            header.SalesOrderUuid = inputShipmentHeader.SalesOrderUuid;

            return header;
        }
        /// <summary>
        /// write package.
        /// </summary>
        /// <param name="inputPackageItems"></param>
        /// <returns></returns>
        protected IList<OrderShipmentPackageDto> Write(List<InputOrderShipmentPackageItemsType> inputPackageItems)
        {
            if (inputPackageItems is null || inputPackageItems.Count == 0)
                return null;
            var packageItems = new List<OrderShipmentPackageDto>();
            foreach (var item in inputPackageItems)
            {
                packageItems.Add(Write(item));
            }
            return packageItems;
        }
        /// <summary>
        /// write package item
        /// </summary>
        /// <param name="inputPackageItem"></param>
        /// <returns></returns>
        protected OrderShipmentPackageDto Write(InputOrderShipmentPackageItemsType inputPackageItem)
        {
            if (inputPackageItem is null)
                return null;
            var package = Write(inputPackageItem.ShipmentPackage);

            if (inputPackageItem.ShippedItems is null || inputPackageItem.ShippedItems.Count == 0)
                return package;

            package.OrderShipmentShippedItem = new List<OrderShipmentShippedItemDto>();
            foreach (var packageItem in inputPackageItem.ShippedItems)
            {
                package.OrderShipmentShippedItem.Add(Write(packageItem, package.OrderShipmentPackageNum));
            }
            return package;
        }
        /// <summary>
        /// write package item.
        /// </summary>
        /// <param name="inputOrderShippedPackageItem"></param>
        /// <returns></returns>
        protected OrderShipmentShippedItemDto Write(InputOrderShipmentShippedItemType inputOrderShippedPackageItem, long? orderShipmentPackageNum)
        {
            if (inputOrderShippedPackageItem is null)
                return null;
            var orderShippedPackageItem = new OrderShipmentShippedItemDto();
            orderShippedPackageItem.OrderShipmentNum = 0;
            orderShippedPackageItem.OrderShipmentPackageNum = orderShipmentPackageNum.HasValue ? orderShipmentPackageNum : 0;
            orderShippedPackageItem.MasterAccountNum = MasterAccountNum;
            orderShippedPackageItem.ProfileNum = ProfileNum;
            //TODO column not exist in table 
            //orderShippedPackageItem.CentralOrderLineNum = inputOrderShippedPackageItem.CentralOrderLineNum;

            orderShippedPackageItem.OrderDCAssignmentLineNum = inputOrderShippedPackageItem.OrderDCAssignmentLineNum;

            orderShippedPackageItem.SKU = inputOrderShippedPackageItem.SKU;
            //TODO column not exist in table 
            //orderShippedPackageItem.UnitHandlingFee = inputOrderShippedPackageItem.UnitHandlingFee;
            orderShippedPackageItem.ShippedQty = inputOrderShippedPackageItem.ShippedQty;

            //TODO column not exist in table 
            //orderShippedPackageItem.LineHandlingFee = inputOrderShippedPackageItem.LineHandlingFee;

            orderShippedPackageItem.EnterDateUtc = inputOrderShippedPackageItem.EnterDateUtc;
            orderShippedPackageItem.SalesOrderItemsUuid = inputOrderShippedPackageItem.SalesOrderItemsUuid;
            return orderShippedPackageItem;
        }
        /// <summary>
        /// write package item
        /// </summary>
        /// <param name="inputShipmentPackage"></param>
        /// <returns></returns>
        protected OrderShipmentPackageDto Write(InputOrderShipmentPackageType inputShipmentPackage)
        {
            if (inputShipmentPackage is null)
                return null;
            var package = new OrderShipmentPackageDto();
            package.OrderShipmentNum = 0;
            package.MasterAccountNum = MasterAccountNum;
            package.ProfileNum = ProfileNum;

            package.PackageID = inputShipmentPackage.PackageID;
            package.PackageType = (int)inputShipmentPackage.PackageType;
            package.PackagePatternNum = inputShipmentPackage.PackagePatternNum;

            package.PackageTrackingNumber = inputShipmentPackage.PackageTrackingNumber;
            package.PackageReturnTrackingNumber = inputShipmentPackage.PackageReturnTrackingNumber;
            package.PackageWeight = inputShipmentPackage.PackageWeight;
            package.PackageLength = inputShipmentPackage.PackageLength;
            package.PackageWidth = inputShipmentPackage.PackageWidth;
            package.PackageHeight = inputShipmentPackage.PackageHeight;
            package.PackageVolume = inputShipmentPackage.PackageVolume;

            package.PackageQty = inputShipmentPackage.PackageQty;
            package.ParentPackageNum = inputShipmentPackage.ParentPackageNum;
            package.HasChildPackage = inputShipmentPackage.HasChildPackage = false;

            return package;
        }
        /// <summary>
        /// write canceled item.
        /// </summary>
        /// <param name="inputCanceledItems"></param>
        /// <returns></returns>
        protected IList<OrderShipmentCanceledItemDto> Write(List<InputOrderShipmentCanceledItemType> inputCanceledItems)
        {
            if (inputCanceledItems is null || inputCanceledItems.Count == 0)
                return null;

            var canceledItems = new List<OrderShipmentCanceledItemDto>();
            foreach (var item in inputCanceledItems)
            {
                canceledItems.Add(Write(item));
            }
            return canceledItems;
        }

        /// <summary>
        /// write canceled item
        /// </summary>
        /// <param name="inputCanceledItem"></param>
        /// <returns></returns>
        protected OrderShipmentCanceledItemDto Write(InputOrderShipmentCanceledItemType inputCanceledItem)
        {
            if (inputCanceledItem is null)
                return null;

            var canceledItem = new OrderShipmentCanceledItemDto();
            canceledItem.OrderShipmentNum = 0;
            canceledItem.MasterAccountNum = MasterAccountNum;
            canceledItem.ProfileNum = ProfileNum;
            //TODO column not exist in table 
            //canceledItem.CentralOrderLineNum = inputCanceledItem.CentralOrderLineNum ;

            canceledItem.OrderDCAssignmentLineNum = inputCanceledItem.OrderDCAssignmentLineNum;
            canceledItem.SKU = inputCanceledItem.SKU;
            canceledItem.CanceledQty = inputCanceledItem.CanceledQty;
            canceledItem.CancelCode = inputCanceledItem.CancelCode; //see CancelCodeType
            canceledItem.CancelOtherReason = inputCanceledItem.CancelOtherReason;
            canceledItem.SalesOrderItemsUuid = inputCanceledItem.SalesOrderItemsUuid;
            return canceledItem;
        }
    }
}

