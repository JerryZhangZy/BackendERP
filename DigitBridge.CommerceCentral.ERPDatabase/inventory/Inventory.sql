CREATE TABLE [dbo].[Inventory]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL DEFAULT 0, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL DEFAULT 0,
	[ProfileNum] INT NOT NULL DEFAULT 0,

    [ProductUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for product SKU
    [InventoryUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Inventory Item Line

	[StyleCode] Varchar(100) NOT NULL DEFAULT '',--Product SKU Item No 
	[Color] Varchar(50) NOT NULL DEFAULT '',--Product SKU Color Code 
	[SizeType] Varchar(50) NOT NULL DEFAULT '',--Product SKU. Ex: Regular, Plus 
	[SizeSystem] Varchar(50) NOT NULL DEFAULT '',--Product SKU size code
	[Size] Varchar(50) NOT NULL DEFAULT '',--Product SKU size code
	[Width] Varchar(50) NOT NULL DEFAULT '',--Product SKU width code
	[Length] Varchar(50) NOT NULL DEFAULT '',--Product SKU Length code

	[ClassCode] Varchar(50) NOT NULL DEFAULT '',--Product SKU Class  
	[Department] Varchar(50) NOT NULL DEFAULT '',--Product SKU department
	[Division] Varchar(50) NOT NULL DEFAULT '',--Product SKU department
	[Year] Varchar(20) NOT NULL DEFAULT '',

	[PriceRule] Varchar(50) NOT NULL DEFAULT '',--Product SKU
	[LeadDay] Int NOT NULL,--Product SKU processing days before ship

	[SKU] Varchar(100) NOT NULL DEFAULT '',--Product SKU 
	[Description] NVARCHAR(200) NOT NULL DEFAULT '', --Warehouse Guid
	[WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --Warehouse Guid
	[WhsDescription] NVarchar(200) NOT NULL DEFAULT '',--Invoice item description 
	[LotNum] Varchar(100) NOT NULL DEFAULT '',--Product SKU Lot Number 
	[LotInDate] DATE NULL, --Lot receive Date
	[LotExpDate] DATE NULL, --Lot Expiration date
	[LotDescription] NVarchar(200) NOT NULL DEFAULT '',--Invoice item description 
	[LpnNum] Varchar(100) NOT NULL,--Product SKU LPN Number 
	[LpnDescription] NVarchar(200) NOT NULL DEFAULT '',--Invoice item description 
	[Notes] NVarchar(500) NOT NULL DEFAULT '',--Invoice item notes 

	[Currency] VARCHAR(10) NOT NULL DEFAULT '',
	[UOM] Varchar(50) NOT NULL DEFAULT '',--Product SKU Qty unit of measure 
	[PackType] Varchar(50) NOT NULL DEFAULT '',--Product SKU default Qty pack type, for example: Case, Box, Each 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item default Qty each per pack. 
	[EachPerPallot] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per Pallot. 
	[EachPerCase] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per case. 
	[EachPerBox] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per box. 
	[QtyType] Varchar(50) NOT NULL DEFAULT 'EA', --Item storage type (case, box, pallot)

	[Instock] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item in stock Qty. 
	[OnHand] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item On hand. 
	[OpenSoQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Open S/O qty. 
	[OpenFulfillmentQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Open Fulfillment qty. 
	[AvaQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Availiable sales qty. 
	[OpenPoQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Open P/O qty. 
	[OpenInTransitQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Open InTransit qty. 
	[OpenWipQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Open Work in process qty. 
	[ProjectedQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Damage or not putback stock qty. 

	[BaseCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --P/O receive price. 
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for Invoice items. 
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total Invoice tax amount (include shipping tax and misc tax) 
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice level discount amount, base on [SubTotalAmount]
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice total Charg Allowance Amount
	[UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total receive cost. 
	[AvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item moving average cost. 
	[SalesCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item cost display for sales. 

	[Stockable] TINYINT NOT NULL DEFAULT 1,--Invoice item will update inventory instock qty 
	[IsAr] TINYINT NOT NULL DEFAULT 1,--Invoice item will add to A/R invoice total amount
	[IsAp] TINYINT NOT NULL DEFAULT 1,--Invoice item will add to A/P invoice total amount
	[Taxable] TINYINT NOT NULL DEFAULT 0,--Invoice item will apply tax
	[Costable] TINYINT NOT NULL DEFAULT 0,--Invoice item will apply to total sales cost

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL DEFAULT '',
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '',
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_Inventory] PRIMARY KEY ([RowNum]), 
)  
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'UK_Inventory_InventoryUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_Inventory_InventoryUuid] ON [dbo].[Inventory]
(
	[InventoryUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'FK_Inventory_ProductUuid_WarehouseUuid')
CREATE UNIQUE NONCLUSTERED INDEX [FK_Inventory_ProductUuid_WarehouseUuid] ON [dbo].[Inventory]
(
	[ProductUuid] ASC,
	[WarehouseUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_SKU')
CREATE NONCLUSTERED INDEX [IX_Inventory_SKU] ON [dbo].[Inventory]
(
	[SKU] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_S_W_L_L')
CREATE NONCLUSTERED INDEX [IX_Inventory_S_W_L_L] ON [dbo].[Inventory]
(
	[SKU],
	[WarehouseUuid],
	[LotNum],
	[LpnNum]
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_WarehouseID')
CREATE NONCLUSTERED INDEX [FK_Inventory_WarehouseID] ON [dbo].[Inventory]
(
	[WarehouseUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_LpnNum')
CREATE NONCLUSTERED INDEX [IX_Inventory_LpnNum] ON [dbo].[Inventory]
(
	[LpnNum] ASC
) 
GO



