CREATE TABLE [dbo].[InventoryUpdateItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [InventoryUpdateItemsUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) Order Item Line uuid. <br> Display: false, Editable: false

    [InventoryUpdateUuid] VARCHAR(50) NOT NULL, --Order uuid. <br> Display: false, Editable: false.
    [Seq] INT NOT NULL DEFAULT 0, --Order Item Line sequence number. <br> Title: Line#, Display: true, Editable: false
	[ItemDate] DATE NOT NULL, --(Ignore) Update item date
	[ItemTime] TIME NOT NULL, --(Ignore) Update item time

	[SKU] Varchar(100) NOT NULL DEFAULT '', --Product SKU. <br> Title: SKU, Display: true, Editable: true
	[ProductUuid] VARCHAR(50) NOT NULL, --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
	[InventoryUuid] VARCHAR(50) NOT NULL, --(Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
	[WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
	[WarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable warehouse code, load from inventory data. <br> Title: Warehouse Code, Display: true, Editable: true
	[LotNum] Varchar(100) NOT NULL DEFAULT '', --Lot Number. <br> Title: Lot Number, Display: true, Editable: true 
	[Description] NVarchar(200) NOT NULL DEFAULT '', --Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
	[Notes] NVarchar(500) NOT NULL DEFAULT '',--Order item line notes. <br> Title: Notes, Display: true, Editable: true

	[UOM] Varchar(50) NOT NULL DEFAULT '', --(Readonly) Product unit of measure, load from ProductBasic data. <br> Title: UOM, Display: true, Editable: false
	[PackType] Varchar(50) NOT NULL DEFAULT '', --Product SKU Qty pack type, for example: Case, Box, Each. <br> Title: Pack, Display: true, Editable: true 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty each per pack. <br> Title: Qty/Pack, Display: true, Editable: true
	[UpdatePack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item updated pack (positive/negative). <br> Title: Update Pack, Display: true, Editable: true
	[CountPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item count pack (use for Count only). <br> Title: Count Pack, Display: true, Editable: true
	[BeforeInstockPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Instock pack before update. <br> Title: Instock Pack, Display: true, Editable: false
	[UpdateQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item updated qty (positive/negative). <br> Title: Update Qty, Display: true, Editable: true
	[CountQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item count qty (use for Count only). <br> Title: Count Qty, Display: true, Editable: true
	[BeforeInstockQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Instock before update. <br> Title: Instock, Display: true, Editable: false

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

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateItems]') AND name = N'UK_InventoryUpdateItems_InventoryUpdateItemsUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InventoryUpdateItems_InventoryUpdateItemsUuid] ON [dbo].[InventoryUpdateItems]
(
	[InventoryUpdateItemsUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateItems]') AND name = N'FK_InventoryUpdateItems_InventoryUpdateUuid_Seq')
CREATE NONCLUSTERED INDEX [FK_InventoryUpdateItems_InventoryUpdateUuid_Seq] ON [dbo].[InventoryUpdateItems]
(
	[InventoryUpdateUuid] ASC,
	[Seq] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateItems]') AND name = N'BLK_InventoryUpdateItems_InventoryUpdateUuid_Seq')
CREATE NONCLUSTERED INDEX [BLK_InventoryUpdateItems_InventoryUpdateUuid_Seq] ON [dbo].[InventoryUpdateItems]
(
	[SKU] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateItems]') AND name = N'IX_InventoryUpdateItems_ProductUuid')
CREATE NONCLUSTERED INDEX [IX_InventoryUpdateItems_ProductUuid] ON [dbo].[InventoryUpdateItems]
(
	[ProductUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateItems]') AND name = N'IX_InventoryUpdateItems_InventoryUuid')
CREATE NONCLUSTERED INDEX [IX_InventoryUpdateItems_InventoryUuid] ON [dbo].[InventoryUpdateItems]
(
	[InventoryUuid] ASC
) 
GO


