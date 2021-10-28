
using System;
using System.Collections.Generic;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public class InputOrderShipmentMapper
    {
        public static OrderShipmentDataDto MapperToErpShipment(InputOrderShipmentType inputShipment)
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

        protected static OrderShipmentHeaderDto Write(InputOrderShipmentHeaderType inputShipmentHeader)
        {
            if (inputShipmentHeader is null)
                return null;

            var header = new OrderShipmentHeaderDto();

            header.ShipmentID = inputShipmentHeader.ShipmentID;
            header.OrderDCAssignmentNum = inputShipmentHeader.OrderDCAssignmentNum;
            header.CentralOrderNum = inputShipmentHeader.CentralOrderNum;
            //publicntistributionCenterNum;
            header.ChannelOrderID = inputShipmentHeader.ChannelOrderID;
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
            //header.TotalHandlingFee = inputShipmentHeader.TotalHandlingFee;//TODO this col not exist in table
            header.TotalShippedQty = inputShipmentHeader.TotalShippedQty;
            header.TotalCanceledQty = inputShipmentHeader.TotalCanceledQty;
            header.TotalWeight = inputShipmentHeader.TotalWeight;
            header.TotalVolume = inputShipmentHeader.TotalVolume;
            header.WeightUnit = (int)inputShipmentHeader.WeightUnit; //= WeightUnitEnum.Pound;
            header.LengthUnit = (int)inputShipmentHeader.LengthUnit;//= LengthUnitEnum.Inch;
            header.VolumeUnit = (int)inputShipmentHeader.VolumeUnit;//= VolumeUnitEnum.CubicInch;
            header.ShipmentStatus = (int)inputShipmentHeader.ShipmentStatus;//OrderStatus: Shipped = 1,PartiallyShipped = 2, Canceled = 16

            return header;
        }
        protected static IList<OrderShipmentPackageDto> Write(List<InputOrderShipmentPackageItemsType> inputPackageItems)
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
        protected static OrderShipmentPackageDto Write(InputOrderShipmentPackageItemsType inputPackageItem)
        {
            if (inputPackageItem is null)
                return null;
            var packageItem = Write(inputPackageItem.ShipmentPackage);

            if (inputPackageItem.ShippedItems is null || inputPackageItem.ShippedItems.Count == 0)
                return packageItem;

            packageItem.OrderShipmentShippedItem = new List<OrderShipmentShippedItemDto>();
            foreach (var item in inputPackageItem.ShippedItems)
            {
                packageItem.OrderShipmentShippedItem.Add(Write(item));
            }
            return packageItem;
        }
        protected static OrderShipmentShippedItemDto Write(InputOrderShipmentShippedItemType inputOrderShippedPackageItem)
        {
            if (inputOrderShippedPackageItem is null)
                return null;
            var orderShippedPackageItem = new OrderShipmentShippedItemDto();

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
            return orderShippedPackageItem;
        }
        protected static OrderShipmentPackageDto Write(InputOrderShipmentPackageType inputShipmentPackage)
        {
            if (inputShipmentPackage is null)
                return null;
            var packageItem = new OrderShipmentPackageDto();


            packageItem.PackageID = inputShipmentPackage.PackageID;
            packageItem.PackageType = (int)inputShipmentPackage.PackageType;
            packageItem.PackagePatternNum = inputShipmentPackage.PackagePatternNum;

            packageItem.PackageTrackingNumber = inputShipmentPackage.PackageTrackingNumber;
            packageItem.PackageReturnTrackingNumber = inputShipmentPackage.PackageReturnTrackingNumber;
            packageItem.PackageWeight = inputShipmentPackage.PackageWeight;
            packageItem.PackageLength = inputShipmentPackage.PackageLength;
            packageItem.PackageWidth = inputShipmentPackage.PackageWidth;
            packageItem.PackageHeight = inputShipmentPackage.PackageHeight;
            packageItem.PackageVolume = inputShipmentPackage.PackageVolume;

            packageItem.PackageQty = inputShipmentPackage.PackageQty;
            packageItem.ParentPackageNum = inputShipmentPackage.ParentPackageNum;
            packageItem.HasChildPackage = inputShipmentPackage.HasChildPackage = false;

            return packageItem;
        }

        protected static IList<OrderShipmentCanceledItemDto> Write(List<InputOrderShipmentCanceledItemType> inputCanceledItems)
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

        protected static OrderShipmentCanceledItemDto Write(InputOrderShipmentCanceledItemType inputCanceledItem)
        {
            if (inputCanceledItem is null)
                return null;

            var canceledItem = new OrderShipmentCanceledItemDto();
            //TODO column not exist in table 
            //canceledItem.CentralOrderLineNum = inputCanceledItem.CentralOrderLineNum ;

            canceledItem.OrderDCAssignmentLineNum = inputCanceledItem.OrderDCAssignmentLineNum;
            canceledItem.SKU = inputCanceledItem.SKU;
            canceledItem.CanceledQty = inputCanceledItem.CanceledQty;
            canceledItem.CancelCode = inputCanceledItem.CancelCode; //see CancelCodeType
            canceledItem.CancelOtherReason = inputCanceledItem.CancelOtherReason;
            return canceledItem;
        }
    }
}

