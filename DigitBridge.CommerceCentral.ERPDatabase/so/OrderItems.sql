CREATE TABLE [dbo].[OrderItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [OrderItemsUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Order Item Line

    [OrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for Order
    [Seq] INT NOT NULL DEFAULT 0, --Order Item Line sort sequence
    [OrderItemType] INT NOT NULL DEFAULT 0, --Order item type
    [OrderItemStatus] INT NOT NULL DEFAULT 0, --Order item status
	[ItemDate] DATE NOT NULL, --Order date
	[ItemTime] TIME NOT NULL, --Order time
	[ShipDate] DATE NULL, --Estimated vendor ship date
	[EtaArrivalDate] DATE NULL, --Estimated date when item arrival to buyer 

	[SKU] Varchar(100) NOT NULL DEFAULT '',--Product SKU 
	[ProductUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for Inventory Item Line
	[InventoryUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for Inventory Item Line
	[WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --Warehouse Guid
	[LotNum] Varchar(100) NOT NULL DEFAULT '',--Product SKU Lot Number 
	[Description] NVarchar(200) NOT NULL DEFAULT '',--Order item description 
	[Notes] NVarchar(500) NOT NULL DEFAULT '',--Order item notes 

	[Currency] VARCHAR(10) NOT NULL DEFAULT '',
	[UOM] Varchar(50) NOT NULL DEFAULT '',--Product SKU Qty unit of measure 
	[PackType] Varchar(50) NOT NULL DEFAULT '',--Product SKU Qty pack type, for example: Case, Box, Each 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty each per pack. 
	[OrderPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Order number of pack. 
	[ShipPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Shipped number of pack. 
	[CancelledPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Cancelled number of pack. 
	[OrderQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Order Qty. 
	[ShipQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Shipped Qty. 
	[CancelledQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Cancelled Qty. 
	[OpenQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Open Qty. 

	[PriceRule] VARCHAR(50) NOT NULL DEFAULT '', --Item Order price rule. 
	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Order price. 
	[DiscountRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order level discount amount, base on SubTotalAmount
	[DiscountPrice] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Order after discount price. 
	[ExtAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 
	[TaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should apply tax
	[NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should not apply tax
	[TaxRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default Tax rate for Order items. 
	[TaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Order tax amount (include shipping tax and misc tax) 
	[ShippingAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order total Charg Allowance Amount
	[ItemTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 
	[ShipAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 
	[CancelledAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 
	[OpenAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 

	[Stockable] TINYINT NOT NULL DEFAULT 1,--Order item will update inventory instock qty 
	[IsAr] TINYINT NOT NULL DEFAULT 1,--Order item will add to Order total amount
	[Taxable] TINYINT NOT NULL DEFAULT 0,--Order item will apply tax
	[Costable] TINYINT NOT NULL DEFAULT 1,--Order item will calculate total cost
	[IsProfit] TINYINT NOT NULL DEFAULT 1,--Invoice item will calculate profilt

	[UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Unit Cost. 
	[AvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Avg.Cost. 
	[LotCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Lot Cost. 
	[LotInDate] DATE NULL, --Lot receive Date
	[LotExpDate] DATE NULL, --Lot Expiration date

    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL DEFAULT '',
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '',
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderItems]') AND name = N'UK_OrderItems_OrderItemsId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderItems_OrderItemsUuid] ON [dbo].[OrderItems]
(
	[OrderItemsUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderItems]') AND name = N'FK_OrderItems_OrderId_Seq')
CREATE NONCLUSTERED INDEX [FK_OrderItems_OrderUuid_Seq] ON [dbo].[OrderItems]
(
	[OrderUuid] ASC,
	[Seq] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderItems]') AND name = N'BLK_OrderItems_OrderId_Seq')
CREATE NONCLUSTERED INDEX [BLK_OrderItems_OrderUuid_Seq] ON [dbo].[OrderItems]
(
	[SKU] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderItems]') AND name = N'IX_OrderItems_OrderId')
CREATE NONCLUSTERED INDEX [IX_OrderItems_OrderUuid] ON [dbo].[OrderItems]
(
	[OrderUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderItems]') AND name = N'IX_OrderItems_InventoryId')
CREATE NONCLUSTERED INDEX [IX_OrderItems_InventoryUuid] ON [dbo].[OrderItems]
(
	[InventoryUuid] ASC
) 
GO


