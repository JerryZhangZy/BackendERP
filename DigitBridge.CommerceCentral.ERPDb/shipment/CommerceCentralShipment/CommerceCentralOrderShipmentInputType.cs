using DigitBridge.Base.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigitBridge.CommerceCentral.ERPDb
{

    public class InputOrderShipmentHeaderType
    {
        public string SalesOrderUuid { get; set; }
        public string WarehouseCode { get; set; }

        public int ChannelNum { get; set; }

        public int ChannelAccountNum { get; set; }

        [Required]
        public string ShipmentID = "";

        [Required]
        public long OrderDCAssignmentNum { get; set; }

        [Required]
        public long CentralOrderNum { get; set; }
        //public int DistributionCenterNum { get; set; }
        public string ChannelOrderID { get; set; }
        //public string WarehouseID = "";
        public OrderShipmentTypeEnum ShipmentType { get; set; }
        public string ShipmentReferenceID { get; set; }

        [Required]
        public DateTime ShipmentDateUtc { get; set; }

        [Required]
        public string ShippingCarrier = "";

        [Required]
        public string ShippingClass = "";
        public decimal ShippingCost = 0;

        [Required]
        public string MainTrackingNumber = "";
        public string MainReturnTrackingNumber = "";
        public string BillOfLadingID = "";
        public int TotalPackages = 0;
        public decimal TotalHandlingFee = 0;
        public decimal TotalShippedQty = 0;
        public decimal TotalCanceledQty = 0;
        public decimal TotalWeight = 0;
        public decimal TotalVolume = 0;
        public WeightUnitEnum WeightUnit = WeightUnitEnum.Pound;
        public LengthUnitEnum LengthUnit = LengthUnitEnum.Inch;
        public VolumeUnitEnum VolumeUnit = VolumeUnitEnum.CubicInch;
        public OrderShipmentStatusEnum ShipmentStatus { get; set; }//OrderStatus: Shipped = 1,PartiallyShipped = 2, Canceled = 16

        public string InvoiceNumber = "";
    }

    public class InputOrderShipmentPackageType
    {
        [Required]
        public string PackageID = "";
        public OrderShipmentPackageTypeEnum PackageType = OrderShipmentPackageTypeEnum.Solid; //OrderShipmentPackageTypeEnum: Solid = 0, Mix = 1, PrePack = 2,
        public int PackagePatternNum = 0; //0: NoPattern

        [Required]
        public string PackageTrackingNumber = "";
        public string PackageReturnTrackingNumber = "";
        public decimal PackageWeight = 0;
        public decimal PackageLength = 0;
        public decimal PackageWidth = 0;
        public decimal PackageHeight = 0;
        public decimal PackageVolume = 0;

        [Required]
        public decimal PackageQty = 0;
        [Required]
        public long ParentPackageNum = 0;
        [Required]
        public bool HasChildPackage = false;

    }
    public class InputOrderShipmentShippedItemType
    {
        [Required]
        public string SalesOrderItemsUuid = "";

        [Required]
        public long CentralOrderLineNum = 0;

        public long OrderDCAssignmentLineNum = 0;
        [Required]
        public string SKU = "";
        public decimal UnitHandlingFee = 0;
        [Required]
        public decimal ShippedQty = 0;

        public decimal LineHandlingFee = 0;

        public DateTime EnterDateUtc = DateTime.UtcNow;
    }
    public class InputOrderShipmentCanceledItemType
    {
        [Required]
        public string SalesOrderItemsUuid = "";

        [Required]
        public long CentralOrderLineNum = 0;

        public long OrderDCAssignmentLineNum = 0;
        [Required]
        public string SKU = "";
        [Required]
        public decimal CanceledQty = 0;
        [Required]
        public string CancelCode = ""; //see CancelCodeType
        public string CancelOtherReason = "";
    }

    public class InputOrderShipmentPackageItemsType
    {
        public InputOrderShipmentPackageType ShipmentPackage { get; set; }
        public List<InputOrderShipmentShippedItemType> ShippedItems { get; set; }
    }
    public class InputOrderShipmentType
    {
        public InputOrderShipmentHeaderType ShipmentHeader { get; set; }
        public List<InputOrderShipmentPackageItemsType> PackageItems { get; set; }
        public List<InputOrderShipmentCanceledItemType> CanceledItems { get; set; }
    }
}
