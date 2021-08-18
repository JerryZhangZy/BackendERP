CREATE TABLE [dbo].[OrderDCAssignmentLine](
	[OrderDCAssignmentLineNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
	[DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.
	[ChannelNum] INT NOT NULL DEFAULT 0, --(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] INT NOT NULL DEFAULT 0, --(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
	[OrderDCAssignmentNum] BIGINT NOT NULL DEFAULT 0, --(Readonly) Assignment header number. <br> Title: Assignment Number: Display: false, Editable: false
	[CentralOrderNum] BIGINT NOT NULL DEFAULT 0, --(Readonly) CentralOrderNum. <br> Title: Central Order: Display: true, Editable: false
	[CentralOrderLineNum] BIGINT NOT NULL DEFAULT 0, --(Readonly) CentralOrder Line Number. <br> Title: Central Order Line#: Display: true, Editable: false
	[ChannelOrderID] VARCHAR(130) NOT NULL DEFAULT '', --(Readonly) This usually is the marketplace order ID, or merchant PO Number. <br> Title: Channel Order: Display: true, Editable: false
	[CentralProductNum] BIGINT NOT NULL DEFAULT 0, --(Readonly) Product Unique Number. <br> Title: Product Number: Display: true, Editable: false
	[DistributionProductNum] BIGINT NOT NULL DEFAULT 0, --(Readonly) Product DC link Unique Number. <br> Title: Product DC Number: Display: true, Editable: false
	[SKU] VARCHAR(100) NOT NULL DEFAULT '', --Product SKU. <br> Title: SKU, Display: true, Editable: true
	[ChannelItemID] VARCHAR(50) NOT NULL DEFAULT '', --Channel Item ID. <br> Title: Channel Item ID, Display: true, Editable: true
	[OrderQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Order Qty. <br> Title: Order Qty, Display: true, Editable: true
	[DBChannelOrderLineRowID] VARCHAR(50) NOT NULL DEFAULT '', --DB Channel Order Line RowID. <br> Title: Channel Order Line RowID, Display: true, Editable: true

	[EnterDateUtc] DATETIME NULL, --(Ignore) 

    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderLineUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [OrderDCAssignmentUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [OrderDCAssignmentLineUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [RowNum]      BIGINT NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
	CONSTRAINT [PK_OrderDCAssignmentLine] PRIMARY KEY CLUSTERED ([OrderDCAssignmentLineNum] ASC)
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderDCAssignmentLine]') AND name = N'UK_OrderDCAssignmentLine_OrderDCAssignmentLineUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderDCAssignmentLine_OrderDCAssignmentLineUuid] ON [dbo].[OrderDCAssignmentLine]
(
	[OrderDCAssignmentLineUuid] ASC
);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderDCAssignmentLine]') AND name = N'FK_OrderDCAssignmentLine_OrderDCAssignmentUuid')
CREATE UNIQUE NONCLUSTERED INDEX [FK_OrderDCAssignmentLine_OrderDCAssignmentUuid] ON [dbo].[OrderDCAssignmentLine]
(
	[OrderDCAssignmentUuid] ASC,
	[OrderDCAssignmentLineNum] ASC
);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderDCAssignmentLine]') AND name = N'FK_OrderDCAssignmentLine_OrderDCAssignmentNum')
CREATE UNIQUE NONCLUSTERED INDEX [FK_OrderDCAssignmentLine_OrderDCAssignmentNum] ON [dbo].[OrderDCAssignmentLine]
(
	[OrderDCAssignmentNum] ASC,
	[OrderDCAssignmentLineNum] ASC
);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderDCAssignmentLine]') AND name = N'BLK_OrderDCAssignmentLine_sku')
CREATE NONCLUSTERED INDEX [BLK_OrderDCAssignmentLine_sku] ON [dbo].[OrderDCAssignmentLine]
(
	[SKU] ASC
) 
GO

CREATE NONCLUSTERED INDEX [DI_OrderDCAssignmentLine_CentralOrderNum]
    ON [dbo].[OrderDCAssignmentLine]([CentralOrderNum] ASC);
GO
