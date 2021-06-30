CREATE TABLE [dbo].[InventoryLog]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [InventoryLogUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Inventory Log Line
    [ProductUuid] VARCHAR(50) NOT NULL DEFAULT '',
    [InventoryUuid] VARCHAR(50) NOT NULL DEFAULT '',

    [BatchNum] BIGINT NULL DEFAULT 0, --Batch number for log update
    [LogType] VARCHAR(50) NOT NULL DEFAULT '', --Log type
    [LogUuid] VARCHAR(50) NOT NULL DEFAULT '', --Transaction ID (for example: PO receive, Fulfillment)
    [LogItemUuid] VARCHAR(50) NOT NULL DEFAULT '', --Transaction Item ID (for example: PO receive item Id, Fulfillment item Id)
    [LogStatus] INT NULL DEFAULT 0, --Log status
	[LogDate] DATE NOT NULL, --Invoice date
	[LogTime] TIME NOT NULL, --Invoice time
	[LogBy] Varchar(100) NOT NULL,

	[SKU] Varchar(100) NOT NULL,--Product SKU 
	[Description] NVARCHAR(200) NULL, --Warehouse Guid
	[WarehouseUuid] VARCHAR(50) NULL, --Warehouse Guid
	[WhsDescription] NVarchar(200) NOT NULL,--Invoice item description 
	[LotNum] Varchar(100) NOT NULL,--Product SKU Lot Number 
	[LotInDate] DATE NULL, --Lot receive Date
	[LotExpDate] DATE NULL, --Lot Expiration date
	[LotDescription] NVarchar(200) NOT NULL,--Invoice item description 
	[LpnNum] Varchar(100) NOT NULL,--Product SKU LPN Number 
	[LpnDescription] NVarchar(200) NOT NULL,--Invoice item description 
	[Notes] NVarchar(500) NOT NULL,--Invoice item notes 

	[UOM] Varchar(50) NULL,--Product SKU Qty unit of measure 
	[LogQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Log Transaction Qty (>0: in, <0: out ).

	[BeforeInstock] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item in stock Qty. 
	[BeforeBaseCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --P/O receive price. 
	[BeforeUnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total receive cost. 
	[BeforeAvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item moving average cost. 

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_Inventory] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'UI_Inventory_InventoryId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_Inventory_InventoryUuid] ON [dbo].[Inventory]
(
	[InventoryUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_SKU')
CREATE NONCLUSTERED INDEX [IX_Inventory_SKU] ON [dbo].[Inventory]
(
	[SKU] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_S_W_L_L')
CREATE NONCLUSTERED INDEX [IX_Inventory_S_W_L_L] ON [dbo].[Inventory]
(
	[SKU],
	[WarehouseID],
	[LotNum],
	[LpnNum]
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_WarehouseID')
CREATE NONCLUSTERED INDEX [IX_Inventory_WarehouseUuid] ON [dbo].[Inventory]
(
	[WarehouseUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_LpnNum')
CREATE NONCLUSTERED INDEX [IX_Inventory_LpnNum] ON [dbo].[Inventory]
(
	[LpnNum] ASC
) ON [PRIMARY]
GO



