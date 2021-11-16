CREATE TABLE [dbo].[ProductExt] (
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [ProductUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
    [CentralProductNum] BIGINT NOT NULL DEFAULT 0, --(Readonly) Product Number. load from ProductBasic data. <br> Display: false, Editable: false
    [SKU] VARCHAR(100) NOT NULL DEFAULT '', --Product SKU. load from ProductBasic data. <br> Display: false, Editable: false

	[StyleCode] Varchar(100) NOT NULL DEFAULT '', --Product style code use to group multiple SKU. load from ProductExt data. <br> Title: Style Code, Display: true, Editable: true
	[ColorPatternCode] Varchar(50) NOT NULL DEFAULT '', --Product color and pattern code. <br> Title: Color, Display: true, Editable: true
	[SizeType] Varchar(50) NOT NULL DEFAULT '', --Product size type. <br> Title: Size Type, Display: true, Editable: true
	[SizeCode] Varchar(50) NOT NULL DEFAULT '', --Product size code. <br> Title: Size, Display: true, Editable: true
	[WidthCode] Varchar(30) NOT NULL DEFAULT '', --Product width code. <br> Title: Width, Display: true, Editable: true
	[LengthCode] Varchar(30) NOT NULL DEFAULT '', --Product length code. <br> Title: Length, Display: true, Editable: true

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

	[ProductStatus] INT NOT NULL DEFAULT 0, --Product status. <br> Title: Status, Display: true, Editable: true
	[PriceRule] Varchar(50) NOT NULL DEFAULT '', --Product Default Price Rule. <br> Title: Price Rule, Display: true, Editable: true
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

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_ProductExt] PRIMARY KEY CLUSTERED ([RowNum])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_ProductExt_ProductUuid] ON [dbo].[ProductExt]
(
	[ProductUuid] ASC
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_ProductExt_MasterAccountNum_ProfileNum_SKU] ON [dbo].[ProductExt]
(
    [MasterAccountNum] ASC, 
    [ProfileNum] ASC, 
    [SKU] ASC
);
GO

CREATE NONCLUSTERED INDEX [UI_ProductExt_CentralProductNum] ON [dbo].[ProductExt]
(
	[CentralProductNum] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProductExt]') AND name = N'IX_ProductExt_C_S_D_D_O_A_R_M_C_G_S')
CREATE NONCLUSTERED INDEX [IX_IProductExt_S_C_S_W_L_W_L_L] ON [dbo].[ProductExt]
(
	[ClassCode] ASC, 
	[SubClassCode] ASC,
	[DepartmentCode] ASC,
	[DivisionCode] ASC,
	[OEMCode] ASC,
	[AlternateCode] ASC,
	[Remark] ASC,
	[Model] ASC,
	[CategoryCode] ASC,
	[GroupCode] ASC,
	[SubGroupCode] ASC
) 
GO

CREATE NONCLUSTERED INDEX [IX_IProductExt_Status] ON [dbo].[ProductExt]
(
    [MasterAccountNum] ASC, 
    [ProfileNum] ASC, 
    [ProductStatus] ASC
) 
