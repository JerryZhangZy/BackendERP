CREATE TABLE [dbo].[OrderShipmentHeader](
	[OrderShipmentNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Shipment Unique Number. Required, <br> Title: Shipment Number Display: true, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[ChannelNum] INT NOT NULL DEFAULT 0, --(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] INT NOT NULL DEFAULT 0, --(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
	[OrderDCAssignmentNum] BIGINT NULL DEFAULT 0, --(Readonly) The unique number of Order DC Assignment. <br> Title: Assignment Number: Display: true, Editable: false
	[DistributionCenterNum] INT NULL DEFAULT 0, --(Readonly) DC number. <br> Title: DC Number: Display: true, Editable: false
	[CentralOrderNum] BIGINT NULL DEFAULT 0, --(Readonly) CentralOrderNum. <br> Title: Central Order: Display: true, Editable: false
	[ChannelOrderID] VARCHAR(130) NOT NULL DEFAULT '', --(Readonly) This usually is the marketplace order ID, or merchant PO Number. <br> Title: Channel Order: Display: true, Editable: false
	[ShipmentID] NVARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Shipment ID. <br> Title: Shipment Id, Display: true, Editable: false
	[WarehouseCode] NVARCHAR(50) NOT NULL DEFAULT '', --Warehouse Code. <br> Title: Warehouse Code, Display: true, Editable: true
	[ShipmentType] INT NOT NULL DEFAULT 0, --Shipment Type. <br> Title: Shipment Type, Display: true, Editable: true
	[ShipmentReferenceID] VARCHAR(50) NOT NULL DEFAULT '', --Ref Id. <br> Title: Reference, Display: true, Editable: true
	[ShipmentDateUtc] DATETIME NULL, --Ship Date. <br> Title: Ship Date, Display: true, Editable: true
	[ShippingCarrier] VARCHAR(50) NOT NULL DEFAULT '', --Shipping Carrier. <br> Title: Shipping Carrier: Display: true, Editable: true
	[ShippingClass] VARCHAR(50) NOT NULL DEFAULT '', --Shipping Method. <br> Title: Shipping Method: Display: true, Editable: true
	[ShippingCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Shipping fee. <br> Title: Shipping Fee, Display: true, Editable: true
	[MainTrackingNumber] VARCHAR(50) NOT NULL DEFAULT '', --Master TrackingNumber. <br> Title: Tracking Number, Display: true, Editable: true
	[MainReturnTrackingNumber] VARCHAR(50) NOT NULL DEFAULT '', --Master Return TrackingNumber. <br> Title: Return Tracking Number, Display: true, Editable: true
	[BillOfLadingID] NVARCHAR(50) NOT NULL DEFAULT '', --Bill Of Lading ID. <br> Title: BOL Id, Display: true, Editable: true
	[TotalPackages] INT NOT NULL DEFAULT 0, --Total Packages. <br> Title: Number of Package, Display: true, Editable: true
	[TotalShippedQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Shipped Qty. <br> Title: Shipped Qty, Display: true, Editable: true
	[TotalCanceledQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Cancelled Qty. <br> Title: Cancelled Qty, Display: true, Editable: true
	[TotalWeight] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Weight. <br> Title: Weight, Display: true, Editable: true
	[TotalVolume] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Volume. <br> Title: Volume, Display: true, Editable: true
	[WeightUnit] INT NOT NULL DEFAULT 0, --Weight Unit. <br> Title: Weight Unit, Display: true, Editable: true
	[LengthUnit] INT NOT NULL DEFAULT 0, --Length Unit. <br> Title: Length Unit, Display: true, Editable: true
	[VolumeUnit] INT NOT NULL DEFAULT 0, --Volume Unit. <br> Title: Volume Unit, Display: true, Editable: true
	[ShipmentStatus] INT NOT NULL DEFAULT 0, --Shipment Status. <br> Title: Shipment Status, Display: true, Editable: true
	[DBChannelOrderHeaderRowID] VARCHAR(50) NOT NULL DEFAULT '', --(Ignore) DBChannelOrderHeaderRowID. <br> Display: false, Editable: false
	[ProcessStatus] INT NOT NULL DEFAULT 0, --Process Status. <br> Title: Process Status, Display: true, Editable: false
	[ProcessDateUtc] DATETIME NOT NULL, --Process Date. <br> Title: Process Date, Display: true, Editable: false
	[EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore) 

    [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Shipment uuid. <br> Display: false, Editable: false.
    [RowNum] BIGINT NOT NULL DEFAULT 0,	--(Ignore) dummy field for T4 template 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 

	CONSTRAINT [PK_OrderShipmentHeader] PRIMARY KEY CLUSTERED ([OrderShipmentNum] ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderShipmentHeader_OrderShipmentUuid] ON [dbo].[OrderShipmentHeader]
(
    [OrderShipmentUuid] ASC
) 

--ALTER TABLE [dbo].[OrderShipmentHeader] ADD  CONSTRAINT [DF_OrderShipmentHeader_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
--GO


