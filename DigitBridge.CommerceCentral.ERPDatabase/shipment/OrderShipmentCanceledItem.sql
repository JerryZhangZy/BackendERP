CREATE TABLE [dbo].[OrderShipmentCanceledItem](
	[OrderShipmentCanceledItemNum] [bigint] IDENTITY(1,1) NOT NULL,
	[DatabaseNum] [int] NOT NULL,
	[MasterAccountNum] [int] NULL,
	[ProfileNum] [int] NULL,
	[ChannelNum] [int] NOT NULL,
	[ChannelAccountNum] [int] NOT NULL,
	[OrderShipmentNum] [bigint] NULL,
	[ChannelOrderID] [varchar](130) NOT NULL,
	[OrderDCAssignmentLineNum] [bigint] NULL,
	[SKU] [varchar](100) NULL,
	[CanceledQty] [decimal](24, 6) NOT NULL,
	[CancelCode] [varchar](50) NOT NULL,
	[CancelOtherReason] [nvarchar](200) NULL,
	[DBChannelOrderLineRowID] [varchar](50) NOT NULL,
	[EnterDateUtc] [datetime] NOT NULL DEFAULT (getutcdate()),

    [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for one OrderShipment
    [OrderShipmentCanceledItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for one OrderShipment cancell Item
    [RowNum] BIGINT NOT NULL DEFAULT 0,		-- dummy field for T4 template 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),

	CONSTRAINT [PK_OrderShipmentCanceledItem] PRIMARY KEY CLUSTERED ([OrderShipmentCanceledItemNum] ASC)
	
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderShipmentCanceledItem_OrderShipmentCanceledItemUuid] ON [dbo].[OrderShipmentCanceledItem]
(
    [OrderShipmentCanceledItemUuid] ASC
) 
GO

CREATE NONCLUSTERED INDEX [FK_OrderShipmentCanceledItem_OrderShipmentUuid] ON [dbo].[OrderShipmentCanceledItem]
(
	[OrderShipmentUuid] ASC,
	[OrderShipmentCanceledItemNum] ASC
) 
GO

--ALTER TABLE [dbo].[OrderShipmentCanceledItem] ADD  CONSTRAINT [DF_OrderShipmentCanceledItem_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
--GO
