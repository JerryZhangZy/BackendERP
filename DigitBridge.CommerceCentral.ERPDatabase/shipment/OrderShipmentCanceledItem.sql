CREATE TABLE [dbo].[OrderShipmentCanceledItem](
	[OrderShipmentCanceledItemNum] [bigint] IDENTITY(1,1) NOT NULL, --(Readonly) Shipment Canceled Item Unique Number. Required, <br> Title: Canceled Item Number, Display: true, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[ChannelNum] [int] NOT NULL DEFAULT 0, --(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] [int] NOT NULL DEFAULT 0, --(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
	[OrderShipmentNum] [bigint] NULL DEFAULT 0, --(Readonly) Shipment Unique Number. Required, <br> Title: Shipment Number Display: true, Editable: false.
	[ChannelOrderID] [varchar](130) NOT NULL DEFAULT '', --(Readonly) This usually is the marketplace order ID, or merchant PO Number. <br> Title: Channel Order: Display: true, Editable: false
	[OrderDCAssignmentLineNum] [bigint] NULL DEFAULT 0, --(Readonly) The unique number of Order DC Assignment. <br> Title: Assignment Number: Display: true, Editable: false
	[SKU] [varchar](100) NULL DEFAULT '', --Product SKU. <br> Title: Sku, Display: true, Editable: false
	[CanceledQty] [decimal](24, 6) NOT NULL, --Canceled Qty. <br> Title: Canceled Qty, Display: true, Editable: true
	[CancelCode] [varchar](50) NOT NULL, --Cancel code. <br> Title: Cancel Code, Display: true, Editable: true
	[CancelOtherReason] [nvarchar](200) NULL, --Cancel Reason. <br> Title: Cancel Reason, Display: true, Editable: true
	[DBChannelOrderLineRowID] [varchar](50) NOT NULL,	--(Ignore)
	[EnterDateUtc] [datetime] NOT NULL DEFAULT (getutcdate()), --(Ignore)

    [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT '', --Shipment uuid. <br> Display: false, Editable: false.
    [OrderShipmentCanceledItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Shipment Canceled Item uuid. <br> Display: false, Editable: false.
    [RowNum] BIGINT NOT NULL DEFAULT 0, --(Ignore) 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)

    [SalesOrderItemsUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Order Item Line uuid. <br> Display: false, Editable: false

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


CREATE NONCLUSTERED INDEX [FK_OrderShipmentCanceledItem_OrderShipmentNum] ON [dbo].[OrderShipmentCanceledItem]
(
	[OrderShipmentNum] ASC
) 
GO

