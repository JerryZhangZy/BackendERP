CREATE TABLE [dbo].[InventoryLog]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [InventoryLogUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) Inventory log Line uuid. <br> Display: false, Editable: false
    [ProductUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
    [InventoryUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Inventory uuid. load from Inventory data. <br> Display: false, Editable: false

    [BatchNum] BIGINT NULL DEFAULT 0, --Batch number for log update. <br> Title: Batch Number, Display: true, Editable: false
    [LogType] VARCHAR(50) NOT NULL DEFAULT '', --Log type. Which transaction to update inventory. For Example: Shippment, P/O Receive, Adjust. <br> Title: Type, Display: true, Editable: false
    [LogUuid] VARCHAR(50) NOT NULL DEFAULT '', --Transaction ID (for example: PO receive, Shhipment). <br> Display: false, Editable: false
    [LogNumber] VARCHAR(100) NOT NULL DEFAULT '', --Transaction Number (for example: PO receive number, Shhipment number). <br> Title: Number, Display: true, Editable: false
    [LogItemUuid] VARCHAR(50) NOT NULL DEFAULT '', --Transaction Item ID (for example: PO receive item Id, Fulfillment item Id). <br> Display: false, Editable: false
    [LogStatus] INT NULL DEFAULT 0, --Log status. <br> Title: Status, Display: true, Editable: false
	[LogDate] DATE NOT NULL, --Log date. <br> Title: Date, Display: true, Editable: false
	[LogTime] TIME NOT NULL, --Log time. <br> Title: Time, Display: true, Editable: false
	[LogBy] Varchar(100) NOT NULL DEFAULT '', --Log create by. <br> Title: By, Display: true, Editable: false

	[SKU] Varchar(100) NOT NULL DEFAULT '', --Product SKU. <br> Title: SKU, Display: true, Editable: false
	[Description] NVARCHAR(200) NOT NULL DEFAULT '', --Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: false
	[WarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable warehouse code, load from inventory data. <br> Title: Warehouse Code, Display: true, Editable: false
	[LotNum] Varchar(100) NOT NULL DEFAULT '', --Lot Number. <br> Title: Lot Number, Display: true, Editable: false
	[LotInDate] DATE NULL, --Lot receive Date. <br> Title: Lot Receive Date, Display: true, Editable: false
	[LotExpDate] DATE NULL, --Lot Expiration date. <br> Title: Lot Expiration Date, Display: true, Editable: false
	[LpnNum] Varchar(100) NOT NULL DEFAULT '', --LPN Number. <br> Title: LPN, Display: true, Editable: false
	[StyleCode] Varchar(100) NOT NULL DEFAULT '', --Product style code use to group multiple SKU. load from ProductExt data. <br> Title: Style Code, Display: true, Editable: false
	[ColorPatternCode] Varchar(50) NOT NULL DEFAULT '', --Product color and pattern code. <br> Title: Color, Display: true, Editable: false
	[SizeCode] Varchar(50) NOT NULL DEFAULT '', --Product size code. <br> Title: Size, Display: true, Editable: false
	[WidthCode] Varchar(30) NOT NULL DEFAULT '', --Product width code. <br> Title: Width, Display: true, Editable: false
	[LengthCode] Varchar(30) NOT NULL DEFAULT '', --Product length code. <br> Title: Length, Display: true, Editable: false

	[UOM] Varchar(50) NOT NULL DEFAULT '', --(Readonly) Product unit of measure, load from ProductBasic data. <br> Title: UOM, Display: true, Editable: false
	[LogQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Log Transaction Qty (>0: in, <0: out ). <br> Title: Qty, Display: true, Editable: false

	[BeforeInstock] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Before in stock Qty. <br> Title: Instock, Display: true, Editable: false
	[BeforeBaseCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Before base cost. <br> Display: false, Editable: false
	[BeforeUnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Before unit cost. <br> Display: false, Editable: false
	[BeforeAvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Before avg. cost. <br> Display: false, Editable: false

    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_InventoryLog] PRIMARY KEY ([RowNum]), 
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'FK_InventoryLog_InventoryUuid')
CREATE NONCLUSTERED INDEX [FK_InventoryLog_InventoryUuid] ON [dbo].[InventoryLog]
(
	[InventoryUuid] ASC
) 
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'UK_InventoryLog_InventoryLogUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InventoryLog_InventoryLogUuid] ON [dbo].[InventoryLog]
(
	[InventoryLogUuid] ASC
) 
GO 

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'BLK_InventoryLog_SKU_WarehouseCode')
CREATE NONCLUSTERED INDEX [BLK_InventoryLog_SKU_WarehouseCode] ON [dbo].[InventoryLog]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[SKU] ASC,
	[WarehouseCode] ASC
)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_Inventory_S_C_S_W_L_W')
CREATE NONCLUSTERED INDEX [IX_Inventory_S_C_S_W_L_W] ON [dbo].[InventoryLog]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[SKU] ASC, 
	[ColorPatternCode] ASC,
	[SizeCode] ASC,
	[WidthCode] ASC,
	[LengthCode] ASC,
	[WarehouseCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_S_W_L_L')
CREATE NONCLUSTERED INDEX [IX_InventoryLog_S_W_L_L] ON [dbo].[InventoryLog]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[SKU] ASC,
	[WarehouseCode] ASC,
	[LotNum] ASC,
	[LpnNum] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_LpnNum')
CREATE NONCLUSTERED INDEX [IX_InventoryLog_LpnNum] ON [dbo].[InventoryLog]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[LpnNum] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_LogType')
CREATE NONCLUSTERED INDEX [IX_InventoryLog_LogType] ON [dbo].[InventoryLog]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[LogType] ASC
)
GO


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_LogDate')
CREATE NONCLUSTERED INDEX [IX_InventoryLog_LogDate] ON [dbo].[InventoryLog]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[LogDate] ASC
)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_LogNumber')
CREATE NONCLUSTERED INDEX [IX_InventoryLog_LogNumber] ON [dbo].[InventoryLog]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[LogNumber] ASC
)
GO

