CREATE TABLE [dbo].[ProductInfo] (
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum]        INT             NOT NULL,
    [MasterAccountNum]   INT             NOT NULL,
    [ProfileNum]         INT             NOT NULL,

    [ProductUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for product SKU
    [CentralProductNum] BIGINT NOT NULL,
    [SKU] VARCHAR(100) NOT NULL,

	[StyleCode] Varchar(100) NOT NULL DEFAULT '', --Product SKU Item No 
	[ColorPatternCode] Varchar(50) NOT NULL DEFAULT '',--Product SKU Color Code 
	[SizeType] Varchar(50) NOT NULL DEFAULT '',--Product SKU. Ex: Regular, Plus 
	[SizeCode] Varchar(50) NOT NULL DEFAULT '',--Product SKU size code
	[WidthCode] Varchar(30) NOT NULL DEFAULT '',--Product SKU width code
	[LengthCode] Varchar(30) NOT NULL DEFAULT '',--Product SKU Length code

	[ClassCode] Varchar(50) NOT NULL DEFAULT '',--Class Code
	[SubClassCode] Varchar(50) NOT NULL DEFAULT '',--Sub Class code
	[DepartmentCode] Varchar(50) NOT NULL DEFAULT '',--Department code
	[DivisionCode] Varchar(50) NOT NULL DEFAULT '',--Division code
    [OEMCode] VARCHAR(50) NOT NULL DEFAULT '',
    [AlternateCode] VARCHAR(50) NOT NULL DEFAULT '',
    [Remark] VARCHAR(50) NOT NULL DEFAULT '',
    [Model] VARCHAR(50) NOT NULL DEFAULT '',
    [CatalogPage] VARCHAR(50) NOT NULL DEFAULT '',
    [Category] VARCHAR(50) NOT NULL DEFAULT '',
    [GroupCode] VARCHAR(50) NOT NULL DEFAULT '',
    [SubGroupCode] VARCHAR(50) NOT NULL DEFAULT '',
    [CatalogPage] VARCHAR(50) NOT NULL DEFAULT '',

	[PriceRule] Varchar(50) NOT NULL DEFAULT '',--Product Default Price Rule 
	[Stockable] TINYINT NOT NULL DEFAULT 0,--Product has InStock 
	[IsAr] TINYINT NOT NULL DEFAULT 1,--item will add to A/R invoice total amount
	[IsAp] TINYINT NOT NULL DEFAULT 1,--item will add to A/P invoice total amount
	[Taxable] TINYINT NOT NULL DEFAULT 0,--item will apply tax
	[Costable] TINYINT NOT NULL DEFAULT 1,--item will apply to total sales cost
	[IsProfit] TINYINT NOT NULL DEFAULT 1,--item will apply to profit calculation
	[Release] TINYINT NOT NULL DEFAULT 1,--Release flag 

	[UOM] Varchar(50) NOT NULL DEFAULT '',--Product SKU Qty unit of measure 
	[QtyPerPallot] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per Pallot. 
	[QtyPerCase] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per case. 
	[QtyPerBox] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty per box. 
	[PackType] Varchar(50) NOT NULL DEFAULT '',--Product specified package type 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Qty per each package. 
	[DefaultPackType] Varchar(50) NOT NULL DEFAULT '',--Default package type in S/O or invoice
	[DefaultWarehouseNum] Varchar(50) NOT NULL DEFAULT '',--Default Warehouse
	[DefaultVendorNum] Varchar(50) NOT NULL DEFAULT '',--Default package type in S/O or invoice

	[OrderSize] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default P/O qty. 
	[MinStock] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Garantee Instock in anytime. 
	[SalesCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --A fake cost for sales 

    [LeadDays] INT NOT NULL DEFAULT 0,	--Processing days before shipping 
    [ProductYear] VARCHAR(50) NOT NULL DEFAULT '',	--Product year 
    [YearFrom] DECIMAL(24, 6) NOT NULL DEFAULT 0,	--Product availiable from year
    [YearTo] DECIMAL(24, 6) NOT NULL DEFAULT 0,		--Product availiable to year

    CONSTRAINT [PK_ProductInfo] PRIMARY KEY CLUSTERED ([RowNum] ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_ProductInfo_MasterAccountNum_ProfileNum_SKU] ON [dbo].[ProductInfo]
(
    [MasterAccountNum] ASC, 
    [ProfileNum] ASC, 
    [SKU] ASC
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_ProductInfo_ProductUuid] ON [dbo].[ProductInfo]
(
	[ProductUuid] ASC
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_ProductInfo_CentralProductNum] ON [dbo].[ProductInfo]
(
	[CentralProductNum] ASC
) ON [PRIMARY]
GO

