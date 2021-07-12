CREATE TABLE [dbo].[OrderShipmentShippedItem](
	[OrderShipmentShippedItemNum] [bigint] IDENTITY(1,1) NOT NULL,
	[DatabaseNum] [int] NOT NULL,
	[MasterAccountNum] [int] NULL,
	[ProfileNum] [int] NULL,
	[ChannelNum] [int] NOT NULL,
	[ChannelAccountNum] [int] NOT NULL,
	[OrderShipmentNum] [bigint] NULL,
	[OrderShipmentPackageNum] [bigint] NULL,
	[ChannelOrderID] [varchar](130) NOT NULL,
	[OrderDCAssignmentLineNum] [bigint] NULL,
	[SKU] [varchar](100) NULL,
	[ShippedQty] [decimal](24, 6) NOT NULL,
	[DBChannelOrderLineRowID] [varchar](50) NOT NULL,
	[EnterDateUtc] [datetime] NOT NULL DEFAULT (getutcdate()),

    [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for one OrderShipment
    [OrderShipmentPackageUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for one OrderShipment Package
    [OrderShipmentShippedItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for one OrderShipment Item
    [RowNum] BIGINT NOT NULL DEFAULT 0,		-- dummy field for T4 template 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),

	CONSTRAINT [PK_OrderShipmentShippedItem] PRIMARY KEY CLUSTERED ([OrderShipmentShippedItemNum] ASC)
	
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderShipmentShippedItem_OrderShipmentShippedItemUuid] ON [dbo].[OrderShipmentShippedItem]
(
    [OrderShipmentShippedItemUuid] ASC
) 
GO

CREATE NONCLUSTERED INDEX [FK_OrderShipmentShippedItem_OrderShipmentUuid] ON [dbo].[OrderShipmentShippedItem]
(
	[OrderShipmentUuid] ASC,
	[OrderDCAssignmentLineNum] ASC
) 
GO

--ALTER TABLE [dbo].[OrderShipmentShippedItem] ADD  CONSTRAINT [DF_OrderShipmentShippedItem_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
--GO