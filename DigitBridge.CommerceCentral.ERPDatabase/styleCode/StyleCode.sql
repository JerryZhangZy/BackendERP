CREATE TABLE [dbo].[StyleCode]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [StyleCodeUuid] VARCHAR(50) NOT NULL, --(Readonly) Product StyleCode uuid. load from ProductBasic data. <br> Display: false, Editable: false
	[ProductStyleCode] Varchar(100) NOT NULL DEFAULT '', --Product style code use to group multiple SKU. load from ProductExt data. <br> Title: Style Code, Display: true, Editable: true

	[VariationSet] Varchar(50) NOT NULL DEFAULT '', --Product Default VariationSet. <br> Title: Variation Set, Display: true, Editable: true
	[VariationSetUuid] Varchar(50) NOT NULL DEFAULT '', --Product Default VariationSetUuid. <br> Display: false, Editable: false

	[ColorPatternCode] Varchar(2000) NOT NULL DEFAULT '', --Product included color and pattern codes. <br> Title: Colors, Display: true, Editable: true
	[SizeType] Varchar(1000) NOT NULL DEFAULT '', --Product included size types. <br> Title: Size Type, Display: true, Editable: true
	[SizeCode] Varchar(1000) NOT NULL DEFAULT '', --Product included size codes. <br> Title: Size, Display: true, Editable: true
	[WidthCode] Varchar(1000) NOT NULL DEFAULT '', --Product included width codes. <br> Title: Width, Display: true, Editable: true
	[LengthCode] Varchar(1000) NOT NULL DEFAULT '', --Product included length codes. <br> Title: Length, Display: true, Editable: true
    [SKU] VARCHAR(50) NOT NULL DEFAULT '', --SKU Prefix. <br> Title: SKU, Display: true, Editable: true

	[StyleStatus] INT NOT NULL DEFAULT 0, --Status. <br> Title: Status, Display: true, Editable: true
	[StyleType] INT NOT NULL DEFAULT 0, --Type. <br> Title: Type, Display: true, Editable: true

	[ClassCode] Varchar(50) NOT NULL DEFAULT '', --Product class code. <br> Title: Class, Display: true, Editable: true
	[SubClassCode] Varchar(50) NOT NULL DEFAULT '', --Product sub class code. <br> Title: Sub Class, Display: true, Editable: true
	[DepartmentCode] Varchar(50) NOT NULL DEFAULT '', --Product department code. <br> Title: Department, Display: true, Editable: true
	[DivisionCode] Varchar(50) NOT NULL DEFAULT '', --Product division code. <br> Title: Division, Display: true, Editable: true
    [OEMCode] VARCHAR(50) NOT NULL DEFAULT '', --Product OEM code. <br> Title: OEM, Display: true, Editable: true
    [AlternateCode] VARCHAR(50) NOT NULL DEFAULT '', --Product alternate number. <br> Title: Alt. Code, Display: true, Editable: true
    [Remark] VARCHAR(50) NOT NULL DEFAULT '', --Product remark. <br> Title: Remark, Display: true, Editable: true
    [Model] VARCHAR(50) NOT NULL DEFAULT '', --Product model. <br> Title: Model, Display: true, Editable: true
    [CatalogPage] VARCHAR(50) NOT NULL DEFAULT '', --Product in page of catalog. <br> Title: Catalog, Display: true, Editable: true
    [CategoryCode] VARCHAR(50) NOT NULL DEFAULT '', --Product Category. <br> Title: Category, Display: true, Editable: true
    [GroupCode] VARCHAR(50) NOT NULL DEFAULT '', --Product Group. <br> Title: Group, Display: true, Editable: true
    [SubGroupCode] VARCHAR(50) NOT NULL DEFAULT '', --Product Sub Group. <br> Title: Sub Group, Display: true, Editable: true

	[PriceRule] Varchar(50) NOT NULL DEFAULT '', --Product Default Price Rule. <br> Title: Price Rule, Display: true, Editable: true
    [Price] DECIMAL(10, 2) NOT NULL DEFAULT 0, --Product retail price. <br> Title: Default Price, Display: true, Editable: true
    [MAPPrice] DECIMAL(10, 2) NOT NULL DEFAULT 0, --Product MAP Price. <br> Title: MAP Price, Display: true, Editable: true
    [MSRP] MONEY NOT NULL DEFAULT 0, --Product MSRP Price. <br> Title: MSRP, Display: true, Editable: true

	[Stockable] TINYINT NOT NULL DEFAULT 1, --Product need calculate inventory instock qty. <br> Title: Stockable, Display: true, Editable: true
	[IsAr] TINYINT NOT NULL DEFAULT 1, --Product need add to Invoice sales amount amount. <br> Title: A/R, Display: true, Editable: true
	[IsAp] TINYINT NOT NULL DEFAULT 1, --Product need add to A/P Invoice payable amount. <br> Title: A/P, Display: true, Editable: true
	[Taxable] TINYINT NOT NULL DEFAULT 0, --Product need apply tax. <br> Title: Taxable, Display: true, Editable: true
	[Costable] TINYINT NOT NULL DEFAULT 1, --Product need calculate total cost. <br> Title: Apply Cost, Display: true, Editable: true
	[IsProfit] TINYINT NOT NULL DEFAULT 1, --Product need calculate profit. <br> Title: Apply Profit, Display: true, Editable: true
	[Release] TINYINT NOT NULL DEFAULT 1, --Product is release to sales

	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --(Ignore) Inventory price in currency. <br> Title: Currency, Display: false, Editable: false
	[UOM] Varchar(50) NOT NULL DEFAULT '',--Product SKU Qty unit of measure. <br> Title: UOM, Display: true, Editable: true
	[QtyPerPallot] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per Pallot. <br> Title: Qty/Pallot, Display: true, Editable: true
	[QtyPerCase] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per case. <br> Title: Qty/Case, Display: true, Editable: true
	[QtyPerBox] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per box. <br> Title: Qty/Box, Display: true, Editable: true
	[PackType] Varchar(50) NOT NULL DEFAULT '', --Product specified pack type name. <br> Title: Pack Type, Display: true, Editable: true
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Qty per each pack. <br> Title: Qty/Pack, Display: true, Editable: true
	[DefaultPackType] Varchar(50) NOT NULL DEFAULT '', --Default pack type in S/O or invoice. <br> Title: Default Pack, Display: true, Editable: true
	[DefaultWarehouseCode] Varchar(50) NOT NULL DEFAULT '', --Default Warehouse. <br> Title: Deafult Warehouse, Display: true, Editable: true
	[DefaultVendorCode] Varchar(50) NOT NULL DEFAULT '', --Default Vendor code when make P/O. <br> Title: Deafult Warehouse, Display: true, Editable: true

	[PoSize] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default P/O qty. <br> Title: Deafult P/O Qty, Display: true, Editable: true
	[MinStock] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Garantee minimal Instock in anytime. <br> Title: Min.Stock, Display: true, Editable: true
	[SalesCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --A display cost for sales. <br> Title: Sales Cost, Display: true, Editable: true

    [LeadTimeDay] INT NOT NULL DEFAULT 0, --Processing days before shipping. <br> Title: Leading Days, Display: true, Editable: true 
    [ProductYear] VARCHAR(50) NOT NULL DEFAULT '', --Product year. <br> Title: Year od Product, Display: true, Editable: true 

    [Brand] VARCHAR(150) NOT NULL DEFAULT '', --Product Brand. <br> Title: Brand, Display: true, Editable: true
    [Manufacturer] NVARCHAR(255) NOT NULL DEFAULT '', --Product Manufacturer. <br> Title: Manufacturer, Display: true, Editable: true
    [ProductTitle] NVARCHAR(500) NOT NULL DEFAULT '', --Product Title. <br> Title: Title, Display: true, Editable: true
    [LongDescription] NVARCHAR(2000) NOT NULL DEFAULT '', --Product Long Description. <br> Title: Long Description, Display: true, Editable: true
    [ShortDescription] NVARCHAR(100) NOT NULL DEFAULT '', --Product Short Description. <br> Title: Short Description, Display: true, Editable: true
    [Subtitle] NVARCHAR(50) NOT NULL DEFAULT '', --Product Subtitle. <br> Title: Subtitle, Display: true, Editable: true

	[Notes] NVarchar(500) NOT NULL DEFAULT '', --StyleCode notes. <br> Title: Notes, Display: true, Editable: true 
	[SourceCode] VARCHAR(100) NOT NULL DEFAULT '', --(Readonly) Style Code created from other entity number, use to prevent import duplicate order. <br> Title: Source Number, Display: false, Editable: false

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_StyleCode] PRIMARY KEY ([RowNum]), 
)  
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StyleCode]') AND name = N'UK_StyleCode_StyleCodeUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_StyleCode_StyleCodeUuid] ON [dbo].[StyleCode]
(
	[StyleCodeUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StyleCode]') AND name = N'UI_StyleCode')
CREATE NONCLUSTERED INDEX [UI_StyleCode] ON [dbo].[StyleCode]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[StyleCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StyleCode]') AND name = N'IX_StyleCode_VariationSet')
CREATE NONCLUSTERED INDEX [IX_StyleCode_VariationSet] ON [dbo].[StyleCode]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[VariationSet] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StyleCode]') AND name = N'IX_StyleCode_SKU')
CREATE NONCLUSTERED INDEX [IX_StyleCode_SKU] ON [dbo].[StyleCode]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[SKU] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StyleCode]') AND name = N'IX_StyleCode_ClassCode_SubClassCode')
CREATE NONCLUSTERED INDEX [IX_StyleCode_ClassCode_SubClassCode] ON [dbo].[StyleCode]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ClassCode] ASC,
	[SubClassCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StyleCode]') AND name = N'IX_StyleCode_GroupCode_SubGroupCode')
CREATE NONCLUSTERED INDEX [IX_StyleCode_GroupCode_SubGroupCode] ON [dbo].[StyleCode]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
    [GroupCode] ASC,
    [SubGroupCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StyleCode]') AND name = N'IX_StyleCode_DivisionCode_DepartmentCode')
CREATE NONCLUSTERED INDEX [IX_StyleCode_DivisionCode_DepartmentCode] ON [dbo].[StyleCode]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[DivisionCode] ASC,
	[DepartmentCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StyleCode]') AND name = N'IX_StyleCode_O_A_M_C')
CREATE NONCLUSTERED INDEX [IX_StyleCode_O_A_M_C] ON [dbo].[StyleCode]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
    [OEMCode] ASC,
    [AlternateCode] ASC,
    [Model] ASC,
    [CategoryCode] ASC
) 
GO
