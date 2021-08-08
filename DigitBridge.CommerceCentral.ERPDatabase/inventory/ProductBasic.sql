CREATE TABLE [dbo].[ProductBasic] (
    [CentralProductNum] BIGINT IDENTITY (1000000, 1) NOT NULL, --(Readonly) Product Unique Number. Required, <br> Title: Product Number, Display: true, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [SKU] VARCHAR(100) NOT NULL DEFAULT '', --Product SKU. Required. <br> Title: Sku, Display: true, Editable: true
    [FNSku] NVARCHAR(10) NOT NULL DEFAULT '', --Product FN SKU. <br> Title: FNSku, Display: true, Editable: true
    [Condition] TINYINT NOT NULL DEFAULT 0, --Product FN SKU. <br> Title: Condition, Display: true, Editable: true
    [Brand] VARCHAR(150) NOT NULL DEFAULT '', --Product Brand. <br> Title: Brand, Display: true, Editable: true
    [Manufacturer] NVARCHAR(255) NOT NULL DEFAULT '', --Product Manufacturer. <br> Title: Manufacturer, Display: true, Editable: true
    [ProductTitle] NVARCHAR(500) NOT NULL DEFAULT '', --Product Title. <br> Title: Title, Display: true, Editable: true
    [LongDescription] NVARCHAR(2000) NOT NULL DEFAULT '', --Product Long Description. <br> Title: Long Description, Display: true, Editable: true
    [ShortDescription] NVARCHAR(100) NOT NULL DEFAULT '', --Product Short Description. <br> Title: Short Description, Display: true, Editable: true
    [Subtitle] NVARCHAR(50) NOT NULL DEFAULT '', --Product Subtitle. <br> Title: Subtitle, Display: true, Editable: true
    [ASIN] NVARCHAR(10) NOT NULL DEFAULT '', --Product ASIN. <br> Title: ASIN, Display: true, Editable: true
    [UPC] VARCHAR(20) NOT NULL DEFAULT '', --Product UPC. <br> Title: UPC, Display: true, Editable: true
    [EAN] VARCHAR(20) NOT NULL DEFAULT '', --Product EAN. <br> Title: EAN, Display: true, Editable: true
    [ISBN] VARCHAR(20) NOT NULL DEFAULT '', --Product UPC. <br> Title: ISBN, Display: true, Editable: true
    [MPN] NVARCHAR(50) NOT NULL DEFAULT '', --Product UPC. <br> Title: MPN, Display: true, Editable: true
    [Price] DECIMAL(10, 2) NOT NULL DEFAULT 0, --Product retail price. <br> Title: Default Price, Display: true, Editable: true
    [Cost] MONEY NOT NULL DEFAULT 0, --Product display sales cost. <br> Title: Sales Cost, Display: true, Editable: true
    [AvgCost] DECIMAL(10, 2) NOT NULL DEFAULT 0, --(Ignore) Product display avg. cost. <br> Title: Sales Cost, Display: true, Editable: true
    [MAPPrice] DECIMAL(10, 2) NOT NULL DEFAULT 0, --Product MAP Price. <br> Title: MAP Price, Display: true, Editable: true
    [MSRP] MONEY NOT NULL DEFAULT 0, --Product MSRP Price. <br> Title: MSRP, Display: true, Editable: true
    [BundleType] TINYINT NOT NULL DEFAULT 0, --Product is Bundle. None=0 ; BundleItem =1. <br> Title: Bundle, Display: true, Editable: true     
    [ProductType] TINYINT NOT NULL DEFAULT 0, --Product Type. <br> Title: Type, Display: true, Editable: true     
    [VariationVaryBy] NVARCHAR(80) NOT NULL DEFAULT '', --Product Variation By Item=0 ; Child =1; Parent =2. <br> Title: VariationBy, Display: true, Editable: true       
    [CopyToChildren] TINYINT NOT NULL DEFAULT 0, --Product info need CopyToChildren. <br> Title: CopyToChildren, Display: true, Editable: true       
    [MultipackQuantity] INT NOT NULL DEFAULT 0, --Product include Multiple Quantity. <br> Title: MultipackQuantity, Display: true, Editable: true       
    [VariationParentSKU] NVARCHAR(50) NOT NULL DEFAULT '', --Variation Parent SKU. <br> Title: Parent SKU, Display: true, Editable: true       
    [IsInRelationship] TINYINT NOT NULL DEFAULT 0, --IsInRelationship. <br> Title: In Relationship, Display: true, Editable: true       
    [NetWeight] DECIMAL(10, 2) NOT NULL DEFAULT 0, --Net Weight. <br> Title: Net Weight, Display: true, Editable: true       
    [GrossWeight] DECIMAL(6, 2) NOT NULL DEFAULT 0, --Gross Weight. <br> Title: Gross Weight, Display: true, Editable: true
    [WeightUnit] TINYINT NOT NULL DEFAULT 0, --Unit measure of Weight. <br> Title: Weight Unit, Display: true, Editable: true
    [ProductHeight] DECIMAL(10, 2) NOT NULL DEFAULT 0, --Height. <br> Title: Height, Display: true, Editable: true
    [ProductLength] DECIMAL(10, 2) NOT NULL DEFAULT 0, --Length. <br> Title: Length, Display: true, Editable: true
    [ProductWidth] DECIMAL(10, 2) NOT NULL DEFAULT 0, --Width. <br> Title: Width, Display: true, Editable: true
    [BoxHeight] DECIMAL(6, 2) NOT NULL DEFAULT 0, --Box Height. <br> Title: Box Height, Display: true, Editable: true
    [BoxLength] DECIMAL(6, 2) NOT NULL DEFAULT 0, --Box Length. <br> Title: Box Length, Display: true, Editable: true
    [BoxWidth] DECIMAL(6, 2) NOT NULL DEFAULT 0, --Box Width. <br> Title: Box Width, Display: true, Editable: true
    [DimensionUnit] TINYINT NOT NULL DEFAULT 0, --Dimension measure unit. <br> Title: Dimension Unit, Display: true, Editable: true
    [HarmonizedCode] NVARCHAR(20) NOT NULL DEFAULT '', --HarmonizedCode. <br> Title: Harmonized, Display: true, Editable: true
    [TaxProductCode] NVARCHAR(25) NOT NULL DEFAULT '', --TaxProductCode. <br> Title: Tax Code, Display: true, Editable: true
    [IsBlocked] TINYINT NOT NULL DEFAULT 0, --Product Is Blocked. <br> Title: Blocked, Display: true, Editable: true
    [Warranty] VARCHAR(255) NOT NULL DEFAULT '', --Product Warranty. <br> Title: Warranty, Display: true, Editable: true

    [CreateBy] NVARCHAR(100) NOT NULL DEFAULT '', --(Readonly) User who created this product. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] NVARCHAR(100) NOT NULL DEFAULT '', --(Readonly) User who updated this product. <br> Title: Updated By, Display: true, Editable: false
    [CreateDate] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [UpdateDate] DATETIME NOT NULL DEFAULT (getutcdate()), --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [ClassificationNum] BIGINT NOT NULL DEFAULT 0, -- ClassificationNum. <br> Title: Classification, Display: true, Editable: true

    [RowNum]      BIGINT NOT NULL DEFAULT 0, --(Ignore)
    [OriginalUPC] VARCHAR(20) NOT NULL DEFAULT '', --Product Original UPC. <br> Title: Original UPC, Display: true, Editable: true
    [ProductUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_ProductBasic] PRIMARY KEY CLUSTERED ([CentralProductNum])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_ProductBasic_MasterAccountNum_ProfileNum_SKU] ON [dbo].[ProductBasic]
(
    [MasterAccountNum] ASC, 
    [ProfileNum] ASC, 
    [SKU] ASC
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_ProductBasic_ProductUuid] ON [dbo].[ProductBasic]
(
	[ProductUuid] ASC
) 
GO 

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'None =0 ; BundleItem =1 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProductBasic', @level2type = N'COLUMN', @level2name = N'BundleType';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Item=0 ; Child =1 ; Parent =2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProductBasic', @level2type = N'COLUMN', @level2name = N'ProductType';
GO 
 

