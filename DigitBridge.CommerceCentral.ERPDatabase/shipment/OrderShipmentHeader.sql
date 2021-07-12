CREATE TABLE [dbo].[OrderShipmentHeader](
	[OrderShipmentNum] [bigint] IDENTITY(1,1) NOT NULL,
	[DatabaseNum] [int] NOT NULL,
	[MasterAccountNum] [int] NULL,
	[ProfileNum] [int] NULL,
	[ChannelNum] [int] NOT NULL,
	[ChannelAccountNum] [int] NOT NULL,
	[OrderDCAssignmentNum] [bigint] NULL,
	[DistributionCenterNum] [int] NULL,
	[CentralOrderNum] [bigint] NULL,
	[ChannelOrderID] [varchar](130) NOT NULL,
	[ShipmentID] [nvarchar](50) NULL,
	[WarehouseID] [nvarchar](50) NULL,
	[ShipmentType] [int] NULL,
	[ShipmentReferenceID] [varchar](50) NULL,
	[ShipmentDateUtc] [datetime] NULL,
	[ShippingCarrier] [varchar](50) NULL,
	[ShippingClass] [varchar](50) NULL,
	[ShippingCost] [decimal](24, 6) NULL,
	[MainTrackingNumber] [varchar](50) NULL,
	[MainReturnTrackingNumber] [varchar](50) NULL,
	[BillOfLadingID] [nvarchar](50) NULL,
	[TotalPackages] [int] NULL,
	[TotalShippedQty] [decimal](24, 6) NULL,
	[TotalCanceledQty] [decimal](24, 6) NULL,
	[TotalWeight] [decimal](24, 6) NULL,
	[TotalVolume] [decimal](24, 6) NULL,
	[WeightUnit] [int] NULL,
	[LengthUnit] [int] NULL,
	[VolumeUnit] [int] NULL,
	[ShipmentStatus] [int] NULL,
	[DBChannelOrderHeaderRowID] [varchar](50) NULL,
	[ProcessStatus] [int] NULL,
	[ProcessDateUtc] [datetime] NULL,
	[EnterDateUtc] [datetime] NOT NULL DEFAULT (getutcdate()),

    [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for one OrderShipment
    [RowNum] BIGINT NOT NULL DEFAULT 0,		-- dummy field for T4 template 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),

	CONSTRAINT [PK_OrderShipmentHeader] PRIMARY KEY CLUSTERED ([OrderShipmentNum] ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderShipmentHeader_OrderShipmentUuid] ON [dbo].[OrderShipmentHeader]
(
    [OrderShipmentUuid] ASC
) 

--ALTER TABLE [dbo].[OrderShipmentHeader] ADD  CONSTRAINT [DF_OrderShipmentHeader_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
--GO


