CREATE TABLE [dbo].[InventoryUpdateItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [InventoryUpdateItemsUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) Order Item Line uuid. <br> Display: false, Editable: false

    [InventoryUpdateUuid] VARCHAR(50) NOT NULL, --Order uuid. <br> Display: false, Editable: false.
    [Seq] INT NOT NULL DEFAULT 0, --Order Item Line sequence number. <br> Title: Line#, Display: true, Editable: false
    [OrderItemType] INT NOT NULL DEFAULT 0, --Order item type. <br> Title: Type, Display: true, Editable: true
    [InventoryUpdateItemstatus] INT NOT NULL DEFAULT 0, --Order item status. <br> Title: Status, Display: true, Editable: true
	[ItemDate] DATE NOT NULL, --(Ignore) Order date
	[ItemTime] TIME NOT NULL, --(Ignore) Order time
	[ShipDate] DATE NULL, --Estimated vendor ship date. <br> Title: Ship Date, Display: true, Editable: true
	[EtaArrivalDate] DATE NULL, --Estimated date when item arrival to buyer. <br> Title: Delivery Date, Display: true, Editable: true

	[SKU] Varchar(100) NOT NULL DEFAULT '', --Product SKU. <br> Title: SKU, Display: true, Editable: true
	[ProductUuid] VARCHAR(50) NOT NULL, --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
	[InventoryUuid] VARCHAR(50) NOT NULL, --(Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
	[WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
	[WarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable warehouse code, load from inventory data. <br> Title: Warehouse Code, Display: true, Editable: true
	[LotNum] Varchar(100) NOT NULL DEFAULT '', --Lot Number. <br> Title: Lot Number, Display: true, Editable: true 
	[Description] NVarchar(200) NOT NULL DEFAULT '', --Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
	[Notes] NVarchar(500) NOT NULL DEFAULT '',--Order item line notes. <br> Title: Notes, Display: true, Editable: true

	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --(Ignore)  
	[UOM] Varchar(50) NOT NULL DEFAULT '', --(Readonly) Product unit of measure, load from ProductBasic data. <br> Title: UOM, Display: true, Editable: false
	[PackType] Varchar(50) NOT NULL DEFAULT '', --(Ignore) Product SKU Qty pack type, for example: Case, Box, Each 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --(Ignore) Item Qty each per pack. 
	[OrderPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item Order number of pack. 
	[ShipPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item Shipped number of pack. 
	[CancelledPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item Cancelled number of pack. 
	[OpenPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item Cancelled number of pack. 
	[OrderQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Order Qty. <br> Title: Order Qty, Display: true, Editable: true
	[ShipQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Shipped Qty. <br> Title: Shipped Qty, Display: true, Editable: false
	[CancelledQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Cancelled Qty. <br> Title: Cancelled Qty, Display: true, Editable: false
	[OpenQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Open Qty. 

	[PriceRule] VARCHAR(50) NOT NULL DEFAULT '', --Item price rule. <br> Title: Price Type, Display: true, Editable: true
	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item unit price. <br> Title: Unit Price, Display: true, Editable: true
	[DiscountRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level discount rate. <br> Title: Discount Rate, Display: true, Editable: true
	[DiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level discount amount. <br> Title: Discount Amount, Display: true, Editable: true
	[DiscountPrice] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item after discount price. <br> Title: Discount Price, Display: true, Editable: false
	[ExtAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. <br> Title: Ext.Amount, Display: true, Editable: false
	[TaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should apply tax. <br> Display: false, Editable: false
	[NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should not apply tax. <br> Display: false, Editable: false
	[TaxRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default Tax rate for item. <br> Display: false, Editable: false
	[TaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level tax amount (include shipping tax and misc tax). <br> Display: false, Editable: false
	[ShippingAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --shipping fee for this item. <br> Display: false, Editable: false
	[ShippingTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level tax amount of shipping fee. <br> Display: false, Editable: false
	[MiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level handling charge. <br> Display: false, Editable: false
	[MiscTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level tax amount of handling charge. <br> Display: false, Editable: false
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level Charge and Allowance Amount. <br> Display: false, Editable: false
	[ItemTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount include all. <br> Display: false, Editable: false
	[ShipAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item shipped amount. <br> Display: false, Editable: false
	[CancelledAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item cancelled amount. <br> Display: false, Editable: false
	[OpenAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item open amount. <br> Display: false, Editable: false

	[Stockable] TINYINT NOT NULL DEFAULT 1, --item will update inventory instock qty. <br> Title: Stockable, Display: true, Editable: true
	[IsAr] TINYINT NOT NULL DEFAULT 1,--item will add to Order total amount. <br> Title: A/R, Display: true, Editable: true
	[Taxable] TINYINT NOT NULL DEFAULT 0,--item will apply tax. <br> Title: Taxable, Display: true, Editable: true
	[Costable] TINYINT NOT NULL DEFAULT 1,--item will calculate total cost. <br> Title: Apply Cost, Display: true, Editable: true
	[IsProfit] TINYINT NOT NULL DEFAULT 1,--item will calculate profit. <br> Title: Apply Profit, Display: true, Editable: true

	[UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item Unit Cost. 
	[AvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item Avg.Cost. 
	[LotCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item Lot Cost. 
	[LotInDate] DATE NULL, --(Ignore) Lot receive Date
	[LotExpDate] DATE NULL, --(Ignore) Lot Expiration date

    [UpdateDateUtc] DATETIME NULL, --(Ignore) 
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore) 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
    CONSTRAINT [PK_InventoryUpdateItems] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateItems]') AND name = N'UK_InventoryUpdateItems_InventoryUpdateItemsId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InventoryUpdateItems_InventoryUpdateItemsUuid] ON [dbo].[InventoryUpdateItems]
(
	[InventoryUpdateItemsUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateItems]') AND name = N'FK_InventoryUpdateItems_OrderId_Seq')
CREATE NONCLUSTERED INDEX [FK_InventoryUpdateItems_InventoryUpdateUuid_Seq] ON [dbo].[InventoryUpdateItems]
(
	[InventoryUpdateUuid] ASC,
	[Seq] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateItems]') AND name = N'BLK_InventoryUpdateItems_OrderId_Seq')
CREATE NONCLUSTERED INDEX [BLK_InventoryUpdateItems_InventoryUpdateUuid_Seq] ON [dbo].[InventoryUpdateItems]
(
	[SKU] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateItems]') AND name = N'IX_InventoryUpdateItems_OrderId')
CREATE NONCLUSTERED INDEX [IX_InventoryUpdateItems_InventoryUpdateUuid] ON [dbo].[InventoryUpdateItems]
(
	[InventoryUpdateUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateItems]') AND name = N'IX_InventoryUpdateItems_InventoryId')
CREATE NONCLUSTERED INDEX [IX_InventoryUpdateItems_InventoryUuid] ON [dbo].[InventoryUpdateItems]
(
	[InventoryUuid] ASC
) 
GO


