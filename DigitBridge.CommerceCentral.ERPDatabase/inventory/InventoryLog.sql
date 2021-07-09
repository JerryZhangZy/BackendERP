CREATE TABLE [dbo].[InventoryLog]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [InventoryLogUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Inventory Log Line
    [ProductUuid] VARCHAR(50) NOT NULL,
    [InventoryUuid] VARCHAR(50) NOT NULL DEFAULT '',

    [BatchNum] BIGINT NULL DEFAULT 0, --Batch number for log update
    [LogType] VARCHAR(50) NOT NULL DEFAULT '', --Log type
    [LogUuid] VARCHAR(50) NOT NULL DEFAULT '', --Transaction ID (for example: PO receive, Fulfillment)
    [LogItemUuid] VARCHAR(50) NOT NULL DEFAULT '', --Transaction Item ID (for example: PO receive item Id, Fulfillment item Id)
    [LogStatus] INT NULL DEFAULT 0, --Log status
	[LogDate] DATE NOT NULL, --Invoice date
	[LogTime] TIME NOT NULL, --Invoice time
	[LogBy] Varchar(100) NOT NULL DEFAULT '',

	[SKU] Varchar(100) NOT NULL DEFAULT '',--Product SKU 
	[Description] NVARCHAR(200) NOT NULL DEFAULT '', --Warehouse Guid
	[WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --Warehouse Guid
	[WhsDescription] NVarchar(200) NOT NULL DEFAULT '',--Invoice item description 
	[LotNum] Varchar(100) NOT NULL DEFAULT '',--Product SKU Lot Number 
	[LotInDate] DATE NULL, --Lot receive Date
	[LotExpDate] DATE NULL, --Lot Expiration date
	[LotDescription] NVarchar(200) NOT NULL DEFAULT '',--Invoice item description 
	[LpnNum] Varchar(100) NOT NULL DEFAULT '',--Product SKU LPN Number 
	[LpnDescription] NVarchar(200) NOT NULL DEFAULT '',--Invoice item description 
	[Notes] NVarchar(500) NOT NULL,--Invoice item notes 

	[UOM] Varchar(50) NOT NULL DEFAULT '',--Product SKU Qty unit of measure 
	[LogQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Log Transaction Qty (>0: in, <0: out ).

	[BeforeInstock] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item in stock Qty. 
	[BeforeBaseCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --P/O receive price. 
	[BeforeUnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total receive cost. 
	[BeforeAvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item moving average cost. 

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL DEFAULT '',
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '',
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
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

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_SKU')
CREATE NONCLUSTERED INDEX [IX_InventoryLog_SKU] ON [dbo].[InventoryLog]
(
	[SKU] ASC
) 
GO


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_S_W_L_L')
CREATE NONCLUSTERED INDEX [IX_InventoryLog_S_W_L_L] ON [dbo].[InventoryLog]
(
	[SKU],
	[WarehouseUuid],
	[LotNum],
	[LpnNum]
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_WarehouseUuid')
CREATE NONCLUSTERED INDEX [FK_InventoryLog_WarehouseUuid] ON [dbo].[InventoryLog]
(
	[WarehouseUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_LpnNum')
CREATE NONCLUSTERED INDEX [IX_InventoryLog_LpnNum] ON [dbo].[InventoryLog]
(
	[LpnNum] ASC
) 
GO



