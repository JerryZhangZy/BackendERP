CREATE TABLE [dbo].[WarehouseTransferItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [WarehouseTransferItemsUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) Item Line uuid. <br> Display: false, Editable: false
    [ReferWarehouseTransferItemsUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) Original Item Line uuid. <br> Display: false, Editable: false

    [WarehouseTransferUuid] VARCHAR(50) NOT NULL, --Order uuid. <br> Display: false, Editable: false.
    [Seq] INT NOT NULL DEFAULT 0, --Item Line sequence number. <br> Title: Line#, Display: true, Editable: false
	[ItemDate] DATE NOT NULL, --Update item date
	[ItemTime] TIME NOT NULL, --Update item time

	[SKU] Varchar(100) NOT NULL DEFAULT '', --Product SKU. <br> Title: SKU, Display: true, Editable: true
	[ProductUuid] VARCHAR(50) NOT NULL, --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
	[FromInventoryUuid] VARCHAR(50) NOT NULL, --(Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
	[FromWarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Warehouse uuid, transfer from warehouse. <br> Display: false, Editable: false
	[FromWarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable warehouse code, transfer from warehouse. <br> Title: Warehouse Code, Display: true, Editable: true
	[LotNum] Varchar(100) NOT NULL DEFAULT '', --Lot Number. <br> Title: Lot Number, Display: true, Editable: true 
	[ToInventoryUuid] VARCHAR(50) NOT NULL, --(Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
	[ToWarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Warehouse uuid, transfer to warehouse. <br> Display: false, Editable: false
	[ToWarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable warehouse code, transfer to warehouse. <br> Title: Warehouse Code, Display: true, Editable: true
	[ToLotNum] Varchar(100) NOT NULL DEFAULT '', --To Lot Number. <br> Title: To Lot Number, Display: true, Editable: true 
	
	[Description] NVarchar(200) NOT NULL DEFAULT '', --Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
	[Notes] NVarchar(500) NOT NULL DEFAULT '',--Order item line notes. <br> Title: Notes, Display: true, Editable: true

	[UOM] Varchar(50) NOT NULL DEFAULT '', --(Readonly) Product unit of measure, load from ProductBasic data. <br> Title: UOM, Display: true, Editable: false
	[PackType] Varchar(50) NOT NULL DEFAULT '', --Product SKU Qty pack type, for example: Case, Box, Each. <br> Title: Pack, Display: true, Editable: true 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty each per pack. <br> Title: Qty/Pack, Display: true, Editable: true
	[TransferPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item transfered pack (positive/negative). <br> Title: Transfer Pack, Display: true, Editable: true
	[TransferQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item transfer qty (positive). <br> Title: Transfer Qty, Display: true, Editable: true
	[FromBeforeInstockPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) from warehouse Instock pack before transfer. <br> Title: Instock Pack, Display: true, Editable: false
	[FromBeforeInstockQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) from warehouse Instock before transfer. <br> Title: Instock, Display: true, Editable: false
	[ToBeforeInstockPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) to warehouse Instock pack before transfer. <br> Title: Instock Pack, Display: true, Editable: false
	[ToBeforeInstockQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) to warehouse Instock before transfer. <br> Title: Instock, Display: true, Editable: false

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
    CONSTRAINT [PK_WarehouseTransferItems] PRIMARY KEY ([RowNum]), 
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseTransferItems]') AND name = N'UK_WarehouseTransferItems_WarehouseTransferItemsUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_WarehouseTransferItems_WarehouseTransferItemsUuid] ON [dbo].[WarehouseTransferItems]
(
	[WarehouseTransferItemsUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseTransferItems]') AND name = N'FK_WarehouseTransferItems_WarehouseTransferUuid_Seq')
CREATE NONCLUSTERED INDEX [FK_WarehouseTransferItems_WarehouseTransferUuid_Seq] ON [dbo].[WarehouseTransferItems]
(
	[WarehouseTransferUuid] ASC,
	[Seq] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseTransferItems]') AND name = N'IX_WarehouseTransferItems_WarehouseTransferUuid_Sku')
CREATE NONCLUSTERED INDEX [IX_WarehouseTransferItems_WarehouseTransferUuid_Sku] ON [dbo].[WarehouseTransferItems]
(
	[WarehouseTransferUuid] ASC,
	[SKU] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseTransferItems]') AND name = N'BLK_WarehouseTransferItems_SKU')
CREATE NONCLUSTERED INDEX [BLK_WarehouseTransferItems_SKU] ON [dbo].[WarehouseTransferItems]
(
	[SKU] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseTransferItems]') AND name = N'IX_WarehouseTransferItems_ProductUuid')
CREATE NONCLUSTERED INDEX [IX_WarehouseTransferItems_ProductUuid] ON [dbo].[WarehouseTransferItems]
(
	[ProductUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseTransferItems]') AND name = N'IX_WarehouseTransferItems_FromInventoryUuid')
CREATE NONCLUSTERED INDEX [IX_WarehouseTransferItems_FromInventoryUuid] ON [dbo].[WarehouseTransferItems]
(
	[FromInventoryUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseTransferItems]') AND name = N'IX_WarehouseTransferItems_ToInventoryUuid')
CREATE NONCLUSTERED INDEX [IX_WarehouseTransferItems_ToInventoryUuid] ON [dbo].[WarehouseTransferItems]
(
	[ToInventoryUuid] ASC
) 
GO

