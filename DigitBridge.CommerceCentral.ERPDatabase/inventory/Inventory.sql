CREATE TABLE [dbo].[Inventory]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [ProductUuid] VARCHAR(50) NOT NULL, --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
    [InventoryUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) Inventory uuid. <br> Display: false, Editable: false

	[StyleCode] Varchar(100) NOT NULL DEFAULT '', --Product style code use to group multiple SKU. load from ProductExt data. <br> Title: Style Code, Display: true, Editable: true
	[ColorPatternCode] Varchar(50) NOT NULL DEFAULT '', --Product color and pattern code. <br> Title: Color, Display: true, Editable: true
	[SizeType] Varchar(50) NOT NULL DEFAULT '', --Product size type. <br> Title: Size Type, Display: true, Editable: true
	[SizeCode] Varchar(50) NOT NULL DEFAULT '', --Product size code. <br> Title: Size, Display: true, Editable: true
	[WidthCode] Varchar(30) NOT NULL DEFAULT '', --Product width code. <br> Title: Width, Display: true, Editable: true
	[LengthCode] Varchar(30) NOT NULL DEFAULT '', --Product length code. <br> Title: Length, Display: true, Editable: true

	[PriceRule] Varchar(50) NOT NULL DEFAULT '', --Product Default Price Rule. <br> Title: Price Rule, Display: true, Editable: true
    [LeadTimeDay] INT NOT NULL DEFAULT 0, --Processing days before shipping. <br> Title: Leading Days, Display: true, Editable: true 
	[PoSize] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default P/O qty. <br> Title: Deafult P/O Qty, Display: true, Editable: true
	[MinStock] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Garantee minimal Instock in anytime. <br> Title: Min.Stock, Display: true, Editable: true

    [SKU] VARCHAR(100) NOT NULL DEFAULT '', --Product SKU. load from ProductBasic data. <br> Display: false, Editable: false
	[WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Warehouse uuid, load from warehouse data. <br> Display: false, Editable: false
	[WarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable warehouse code, load from warehouse data. <br> Title: Warehouse Code, Display: true, Editable: true
	[WarehouseName] NVarchar(200) NOT NULL, --(Readonly) Warehouse name, load from warehouse data. <br> Title: Warehouse Name, Display: true, Editable: false
	[LotNum] Varchar(100) NOT NULL, --Lot Number. <br> Title: Lot Number, Display: true, Editable: true
	[LotInDate] DATE NULL, --Lot receive Date. <br> Title: Receive Date, Display: true, Editable: true
	[LotExpDate] DATE NULL, --Lot Expiration date. <br> Title: Expiration Date, Display: true, Editable: true
	[LotDescription] NVarchar(200) NOT NULL DEFAULT '', --Lot description. <br> Title: Lot Description, Display: true, Editable: true
	[LpnNum] Varchar(100) NOT NULL, --LPN Number. <br> Title: LPN, Display: true, Editable: true 
	[LpnDescription] NVarchar(200) NOT NULL DEFAULT '', --LPN description. <br> Title: LPN Description, Display: true, Editable: true 
	[Notes] NVarchar(500) NOT NULL DEFAULT '', --Inventory notes. <br> Title: Notes, Display: true, Editable: true 

	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --(Ignore) Inventory price in currency. <br> Title: Currency, Display: false, Editable: false
	[UOM] Varchar(50) NOT NULL DEFAULT '', --(Readonly) Product unit of measure, load from ProductBasic data. <br> Title: UOM, Display: true, Editable: false
	[QtyPerPallot] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per Pallot. <br> Title: Qty/Pallot, Display: true, Editable: true
	[QtyPerCase] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per case. <br> Title: Qty/Case, Display: true, Editable: true
	[QtyPerBox] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per box. <br> Title: Qty/Box, Display: true, Editable: true
	[PackType] Varchar(50) NOT NULL DEFAULT '', --Product specified pack type name. <br> Title: Pack Type, Display: true, Editable: true
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Qty per each pack. <br> Title: Qty/Pack, Display: true, Editable: true
	[DefaultPackType] Varchar(50) NOT NULL DEFAULT '', --Default pack type in S/O or invoice. <br> Title: Default Pack, Display: true, Editable: true

	[Instock] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item in stock Qty. <br> Title: Instock, Display: true, Editable: false
	[OnHand] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item On hand. <br> Title: Onhand, Display: false, Editable: false
	[OpenSoQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Open S/O qty. <br> Title: Open S/O, Display: true, Editable: false
	[OpenFulfillmentQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Open Fulfillment qty. <br> Title: Open Fulfillment, Display: true, Editable: false
	[AvaQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Availiable sales qty. <br> Title: Available, Display: true, Editable: false
	[OpenPoQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Open P/O qty. <br> Title: Open P/O, Display: true, Editable: false
	[OpenInTransitQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Open InTransit qty. <br> Title: In transit, Display: true, Editable: false
	[OpenWipQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Open Work in process qty. <br> Title: WIP, Display: true, Editable: false
	[ProjectedQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Forcasting projected qty. <br> Title: Projected, Display: true, Editable: false

	[BaseCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --P/O receive price. <br> Title: Base Cost, Display: true, Editable: true
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Duty Tax rate. <br> Title: Duty Rate, Display: true, Editable: true
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Duty tax amount. <br> Title: Duty Amount, Display: true, Editable: true
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Shipping fee. <br> Title: Shipping Fee, Display: true, Editable: true
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Handling charge. <br> Title: Handling Fee, Display: true, Editable: true
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Other Charg or Allowance Amount. <br> Title: Charg&Allowance, Display: true, Editable: true
	[UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Unit cost include duty,and charge. <br> Title: Unit Cost, Display: true, Editable: false
	[AvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Moving average cost. <br> Title: Avg.Cost, Display: true, Editable: false
	[SalesCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item cost display for sales. <br> Title: Sales Cost, Display: true, Editable: false

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_Inventory] PRIMARY KEY ([RowNum]), 
)  
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'UK_Inventory_InventoryUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_Inventory_InventoryUuid] ON [dbo].[Inventory]
(
	[InventoryUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_ProductUuid')
CREATE NONCLUSTERED INDEX [IX_Inventory_ProductUuid] ON [dbo].[Inventory]
(
	[ProductUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'FK_Inventory_ProductUuid_WarehouseUuid')
CREATE NONCLUSTERED INDEX [FK_Inventory_ProductUuid_WarehouseUuid] ON [dbo].[Inventory]
(
	[ProductUuid] ASC,
	[WarehouseUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'BLK_Inventory_SKU_WarehouseCode')
CREATE NONCLUSTERED INDEX [BLK_Inventory_SKU_WarehouseCode] ON [dbo].[Inventory]
(
	[SKU] ASC,
	[WarehouseCode] ASC
)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_S_C_S_W_L_W')
CREATE NONCLUSTERED INDEX [IX_Inventory_S_C_S_W_L_W] ON [dbo].[Inventory]
(
	[SKU] ASC, 
	[ColorPatternCode] ASC,
	[SizeCode] ASC,
	[WidthCode] ASC,
	[LengthCode] ASC,
	[WarehouseCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_S_W_L_L')
CREATE NONCLUSTERED INDEX [IX_Inventory_S_W_L_L] ON [dbo].[Inventory]
(
	[SKU] ASC, 
	[WarehouseCode] ASC, 
	[LotNum] ASC, 
	[LpnNum] ASC
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

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_LotNum')
CREATE NONCLUSTERED INDEX [IX_Inventory_LotNum] ON [dbo].[Inventory]
(
	[LotNum] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_S_C_S_W_L_W_L_L')
CREATE NONCLUSTERED INDEX [IX_Inventory_S_C_S_W_L_W_L_L] ON [dbo].[Inventory]
(
	[SKU] ASC, 
	[ColorPatternCode] ASC,
	[SizeCode] ASC,
	[WidthCode] ASC,
	[LengthCode] ASC,
	[WarehouseCode] ASC,
	[LpnNum] ASC,
	[LotNum] ASC
) 
GO



