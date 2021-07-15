CREATE TABLE [dbo].[OrderShipmentPackage](
	[OrderShipmentPackageNum] [bigint] IDENTITY(1,1) NOT NULL,
	[DatabaseNum] [int] NOT NULL,
	[MasterAccountNum] [int] NULL,
	[ProfileNum] [int] NULL,
	[ChannelNum] [int] NOT NULL,
	[ChannelAccountNum] [int] NOT NULL,
	[OrderShipmentNum] [bigint] NULL,
	[PackageID] [nvarchar](50) NULL,
	[PackageType] [int] NULL,
	[PackagePatternNum] [int] NULL,
	[PackageTrackingNumber] [varchar](50) NULL,
	[PackageReturnTrackingNumber] [varchar](50) NULL,
	[PackageWeight] [decimal](24, 6) NULL,
	[PackageLength] [decimal](24, 6) NULL,
	[PackageWidth] [decimal](24, 6) NULL,
	[PackageHeight] [decimal](24, 6) NULL,
	[PackageVolume] [decimal](24, 6) NULL,
	[PackageQty] [decimal](24, 6) NULL,
	[ParentPackageNum] [bigint] NULL,
	[HasChildPackage] [bit] NULL,
	[EnterDateUtc] [datetime] NOT NULL DEFAULT (getutcdate()),

    [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for one OrderShipment
    [OrderShipmentPackageUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for one OrderShipment Package
    [RowNum] BIGINT NOT NULL DEFAULT 0,		-- dummy field for T4 template 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),

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

--ALTER TABLE [dbo].[OrderShipmentPackage] ADD  CONSTRAINT [DF_OrderShipmentPackage_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
--GO
