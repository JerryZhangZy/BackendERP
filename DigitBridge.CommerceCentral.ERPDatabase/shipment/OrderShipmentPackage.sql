CREATE TABLE [dbo].[OrderShipmentPackage](
	[OrderShipmentPackageNum] [bigint] IDENTITY(1,1) NOT NULL, --(Readonly) Shipment Package Unique Number. Required, <br> Title: Package Number, Display: true, Editable: false.
    [DatabaseNum] INT NOT NULL DEFAULT 0, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL DEFAULT 0, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL DEFAULT 0, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[ChannelNum] [int] NOT NULL DEFAULT 0, --(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] [int] NOT NULL DEFAULT 0, --(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
	[OrderShipmentNum] [bigint] NOT NULL DEFAULT 0, --(Readonly) Shipment Unique Number. Required, <br> Title: Shipment Number Display: true, Editable: false.
	[PackageID] [nvarchar](50) NOT NULL DEFAULT '', --(Readonly) Package ID. <br> Title: Package Id, Display: true, Editable: false
	[PackageType] [int] NOT NULL DEFAULT 0, --Package Type. <br> Title: Package Type, Display: true, Editable: true
	[PackagePatternNum] [int] NOT NULL DEFAULT 0, --Package Pattern. <br> Title: Package Pattern, Display: true, Editable: true
	[PackageTrackingNumber] [varchar](50) NOT NULL DEFAULT '', --Package TrackingNumber. <br> Title: Tracking Number, Display: true, Editable: true
	[PackageReturnTrackingNumber] [varchar](50) NOT NULL DEFAULT '', --Return TrackingNumber. <br> Title: Return Tracking Number, Display: true, Editable: true
	[PackageWeight] [decimal](24, 6) NOT NULL DEFAULT 0, --Weight. <br> Title: Weight, Display: true, Editable: true
	[PackageLength] [decimal](24, 6) NOT NULL DEFAULT 0, --Length. <br> Title: Length, Display: true, Editable: true
	[PackageWidth] [decimal](24, 6) NOT NULL DEFAULT 0, --Width. <br> Title: Width, Display: true, Editable: true
	[PackageHeight] [decimal](24, 6) NOT NULL DEFAULT 0, --Height. <br> Title: Height, Display: true, Editable: true
	[PackageVolume] [decimal](24, 6) NOT NULL DEFAULT 0, --Volume. <br> Title: Volume, Display: true, Editable: true
	[PackageQty] [decimal](24, 6) NOT NULL DEFAULT 0, --Qty. <br> Title: Qty, Display: true, Editable: true
	[ParentPackageNum] [bigint] NOT NULL DEFAULT 0, --Parent Package Num. <br> Title: Parent Package, Display: true, Editable: true
	[HasChildPackage] [bit] NOT NULL DEFAULT 0, --Has Child Package. <br> Title: Has Child, Display: true, Editable: true
	[EnterDateUtc] [datetime] NOT NULL DEFAULT (getutcdate()), --(Ignore) 

    [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT '', --Shipment uuid. <br> Display: false, Editable: false.
    [OrderShipmentPackageUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Shipment Pachage uuid. <br> Display: false, Editable: false.
    [RowNum] BIGINT NOT NULL DEFAULT 0, --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)

	CONSTRAINT [PK_OrderShipmentPackage] PRIMARY KEY CLUSTERED ([OrderShipmentPackageNum] ASC)
	
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderShipmentPackage_OrderShipmentPackageUuid] ON [dbo].[OrderShipmentPackage]
(
    [OrderShipmentPackageUuid] ASC
) 
GO

CREATE NONCLUSTERED INDEX [FK_OrderShipmentPackage_OrderShipmentUuid] ON [dbo].[OrderShipmentPackage]
(
	[OrderShipmentUuid] ASC,
	[OrderShipmentPackageNum] ASC
) 
GO

