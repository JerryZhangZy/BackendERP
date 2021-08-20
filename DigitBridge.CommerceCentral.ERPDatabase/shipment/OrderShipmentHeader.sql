CREATE TABLE [dbo].[OrderShipmentHeader](
	[OrderShipmentNum] [bigint] IDENTITY(1,1) NOT NULL, --(Readonly) Shipment Unique Number. Required, <br> Title: Shipment Number Display: true, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[ChannelNum] [int] NOT NULL DEFAULT 0, --(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] [int] NOT NULL DEFAULT 0, --(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
	[OrderDCAssignmentNum] [bigint] NULL DEFAULT 0, --(Readonly) The unique number of Order DC Assignment. <br> Title: Assignment Number: Display: true, Editable: false
	[DistributionCenterNum] [int] NULL DEFAULT 0, --(Readonly) DC number. <br> Title: DC Number: Display: true, Editable: false
	[CentralOrderNum] [bigint] NULL DEFAULT 0, --(Readonly) CentralOrderNum. <br> Title: Central Order: Display: true, Editable: false
	[ChannelOrderID] [varchar](130) NOT NULL DEFAULT '', --(Readonly) This usually is the marketplace order ID, or merchant PO Number. <br> Title: Channel Order: Display: true, Editable: false
	[ShipmentID] [nvarchar](50) NULL DEFAULT '', --(Readonly) Shipment ID. <br> Title: Shipment Id, Display: true, Editable: false
	[WarehouseCode] [nvarchar](50) NULL DEFAULT '', --Warehouse Code. <br> Title: Warehouse Code, Display: true, Editable: true
	[ShipmentType] [int] NULL DEFAULT 0, --Shipment Type. <br> Title: Shipment Type, Display: true, Editable: true
	[ShipmentReferenceID] [varchar](50) NULL DEFAULT '', --Ref Id. <br> Title: Reference, Display: true, Editable: true
	[ShipmentDateUtc] [datetime] NULL, --Ship Date. <br> Title: Ship Date, Display: true, Editable: true
	[ShippingCarrier] [varchar](50) NULL DEFAULT '', --Shipping Carrier. <br> Title: Shipping Carrier: Display: true, Editable: true
	[ShippingClass] [varchar](50) NULL DEFAULT '', --Shipping Method. <br> Title: Shipping Method: Display: true, Editable: true
	[ShippingCost] [decimal](24, 6) NULL DEFAULT 0, --Shipping fee. <br> Title: Shipping Fee, Display: true, Editable: true
	[MainTrackingNumber] [varchar](50) NULL DEFAULT '', --Master TrackingNumber. <br> Title: Tracking Number, Display: true, Editable: true
	[MainReturnTrackingNumber] [varchar](50) NULL DEFAULT '', --Master Return TrackingNumber. <br> Title: Return Tracking Number, Display: true, Editable: true
	[BillOfLadingID] [nvarchar](50) NULL DEFAULT '', --Bill Of Lading ID. <br> Title: BOL Id, Display: true, Editable: true
	[TotalPackages] [int] NULL DEFAULT 0, --Total Packages. <br> Title: Number of Package, Display: true, Editable: true
	[TotalShippedQty] [decimal](24, 6) NULL DEFAULT 0, --Total Shipped Qty. <br> Title: Shipped Qty, Display: true, Editable: true
	[TotalCanceledQty] [decimal](24, 6) NULL DEFAULT 0, --Total Cancelled Qty. <br> Title: Cancelled Qty, Display: true, Editable: true
	[TotalWeight] [decimal](24, 6) NULL DEFAULT 0, --Total Weight. <br> Title: Weight, Display: true, Editable: true
	[TotalVolume] [decimal](24, 6) NULL DEFAULT 0, --Total Volume. <br> Title: Volume, Display: true, Editable: true
	[WeightUnit] [int] NULL DEFAULT 0, --Weight Unit. <br> Title: Weight Unit, Display: true, Editable: true
	[LengthUnit] [int] NULL DEFAULT 0, --Length Unit. <br> Title: Length Unit, Display: true, Editable: true
	[VolumeUnit] [int] NULL DEFAULT 0, --Volume Unit. <br> Title: Volume Unit, Display: true, Editable: true
	[ShipmentStatus] [int] NULL DEFAULT 0, --Shipment Status. <br> Title: Shipment Status, Display: true, Editable: true
	[DBChannelOrderHeaderRowID] [varchar](50) NULL DEFAULT '', --(Ignore) DBChannelOrderHeaderRowID. <br> Display: false, Editable: false
	[ProcessStatus] [int] NULL DEFAULT 0, --Process Status. <br> Title: Process Status, Display: true, Editable: false
	[ProcessDateUtc] [datetime] NULL, --Process Date. <br> Title: Process Date, Display: true, Editable: false
	[EnterDateUtc] [datetime] NOT NULL DEFAULT (getutcdate()), --(Ignore) 

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


