CREATE TABLE [dbo].[ProductBasic] (
    [CentralProductNum]  BIGINT          IDENTITY (1000000, 1) NOT NULL,
    [DatabaseNum]        INT             NOT NULL,
    [MasterAccountNum]   INT             NOT NULL,
    [ProfileNum]         INT             NOT NULL,
    [SKU]                VARCHAR(100)   NOT NULL,
    [FNSku]              NVARCHAR(10)   CONSTRAINT [DF_ProductBasic_FNSku] DEFAULT ('') NULL,
    [Condition]          TINYINT         CONSTRAINT [DF_ProductBasic_Condition] DEFAULT ((0)) NULL,
    [Brand]              VARCHAR(150)   CONSTRAINT [DF_ProductBasic_Brand] DEFAULT ('') NULL,
    [Manufacturer]       NVARCHAR(255)  CONSTRAINT [DF_ProductBasic_Manufacturer] DEFAULT ('') NULL,
    [ProductTitle]       NVARCHAR(500)  CONSTRAINT [DF_ProductBasic_ProductTitle] DEFAULT ('') NULL,
    [LongDescription]    NVARCHAR(2000) CONSTRAINT [DF_ProductBasic_LongDescription] DEFAULT ('') NULL,
    [ShortDescription]   NVARCHAR(100)  CONSTRAINT [DF_ProductBasic_ShortDescription] DEFAULT ('') NULL,
    [Subtitle]           NVARCHAR(50)   CONSTRAINT [DF_ProductBasic_Subtitle] DEFAULT ('') NULL,
    [ASIN]               NVARCHAR(10)   CONSTRAINT [DF_ProductBasic_ASIN] DEFAULT ('') NULL,
    [UPC]                VARCHAR(20)    CONSTRAINT [DF_ProductBasic_UPC] DEFAULT ('') NULL,
    [EAN]                VARCHAR(20)    CONSTRAINT [DF_ProductBasic_EAN] DEFAULT ('') NULL,
    [ISBN]               VARCHAR(20)    CONSTRAINT [DF_ProductBasic_ISBN] DEFAULT ('') NULL,
    [MPN]                NVARCHAR(50)   CONSTRAINT [DF_ProductBasic_MPN] DEFAULT ('') NULL,
    [Price]              DECIMAL (10, 2) CONSTRAINT [DF_ProductBasic_Price] DEFAULT ((0.00)) NULL,
    [Cost]               MONEY           CONSTRAINT [DF_ProductBasic_Cost] DEFAULT ((0.00)) NOT NULL,
    [AvgCost]            DECIMAL (10, 2) CONSTRAINT [DF_ProductBasic_AvgCost] DEFAULT ((0.00)) NULL,
    [MAPPrice]           DECIMAL (10, 2) CONSTRAINT [DF_ProductBasic_MAPPrice] DEFAULT ((0.00)) NULL,
    [MSRP]               MONEY           CONSTRAINT [DF_ProductBasic_MSRP] DEFAULT ((0.00)) NOT NULL,
    [BundleType]         TINYINT         CONSTRAINT [DF_ProductBasic_BundleType] DEFAULT ((0)) NULL,                --None=0 ; BundleItem =1
    [ProductType]        TINYINT         CONSTRAINT [DF_ProductBasic_ProductType] DEFAULT ((0)) NULL,
    [VariationVaryBy]    NVARCHAR(80)   CONSTRAINT [DF_ProductBasic_VariationVaryBy] DEFAULT ('') NULL,      --Item=0 ; Child =1; Parent =2       
    [CopyToChildren]     TINYINT         CONSTRAINT [DF_ProductBasic_CopyToChildren] DEFAULT ((0)) NULL,
    [MultipackQuantity]  INT             CONSTRAINT [DF_ProductBasic_MultipackQuantity] DEFAULT ((0)) NULL,
    [VariationParentSKU] NVARCHAR(50)   CONSTRAINT [DF_ProductBasic_VariationParentSKU] DEFAULT ('') NULL,
    [IsInRelationship]   TINYINT         CONSTRAINT [DF_ProductBasic_IsInRelationship] DEFAULT ((0)) NULL,
    [NetWeight]          DECIMAL (10, 2) CONSTRAINT [DF_ProductBasic_NetWeight] DEFAULT ((0.00)) NOT NULL,
    [GrossWeight]        DECIMAL (6, 2)  CONSTRAINT [DF_ProductBasic_GrossWeight] DEFAULT ((0.00)) NOT NULL,
    [WeightUnit]         TINYINT         CONSTRAINT [DF_ProductBasic_WeightUnit] DEFAULT ((0)) NULL,
    [ProductHeight]    DECIMAL (10, 2) CONSTRAINT [DF_ProductBasic_ProductHeight] DEFAULT ((0.00)) NOT NULL, -- Change column name
    [ProductLength]    DECIMAL (10, 2) CONSTRAINT [DF_ProductBasic_ProductLength] DEFAULT ((0.00)) NOT NULL, -- Change column name
    [ProductWidth]     DECIMAL (10, 2) CONSTRAINT [DF_ProductBasic_ProductWidth] DEFAULT ((0.00)) NOT NULL,  -- Change column name
    [BoxHeight]          DECIMAL (6, 2)  CONSTRAINT [DF_ProductBasic_BoxHeight] DEFAULT ((0.00)) NOT NULL,
    [BoxLength]          DECIMAL (6, 2)  CONSTRAINT [DF_ProductBasic_BoxLength] DEFAULT ((0.00)) NOT NULL,
    [BoxWidth]           DECIMAL (6, 2)  CONSTRAINT [DF_ProductBasic_BoxWidth] DEFAULT ((0.00)) NOT NULL,
    [DimensionUnit]      TINYINT         CONSTRAINT [DF_ProductBasic_DimensionUnit] DEFAULT ((0)) NULL,
    [HarmonizedCode]     NVARCHAR(20)   CONSTRAINT [DF_ProductBasic_HarmonizedCode] DEFAULT ('') NULL,
    [TaxProductCode]     NVARCHAR(25)   CONSTRAINT [DF_ProductBasic_TaxProductCode] DEFAULT ('') NULL,
    [IsBlocked]          TINYINT         CONSTRAINT [DF_ProductBasic_IsBlocked] DEFAULT ((0)) NULL,
    [Warranty]           VARCHAR(255)   CONSTRAINT [DF_ProductBasic_Warranty] DEFAULT ('') NULL,
    [CreateBy]           NVARCHAR(100)  CONSTRAINT [DF_ProductBasic_CreateBy] DEFAULT ('') NULL,
    [UpdateBy]           NVARCHAR(100)  CONSTRAINT [DF_ProductBasic_UpdateBy] DEFAULT ('') NULL,
    [CreateDate]         DATETIME        CONSTRAINT [DF_ProductBasic_CreateDate] DEFAULT (getutcdate()) NULL,
    [UpdateDate]         DATETIME        CONSTRAINT [DF_ProductBasic_UpdateDate] DEFAULT (getutcdate()) NULL,
    [ClassificationNum]  BIGINT          CONSTRAINT [DF_ProductBasic_ClassificationNum] DEFAULT ((0)) NULL,

    -- Add new Columns
    [OriginalUPC] VARCHAR(20) NOT NULL DEFAULT '',
    [ProductUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for product SKU
    CONSTRAINT [PK_ProductBasic] PRIMARY KEY CLUSTERED ([CentralProductNum] ASC)
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
) ON [PRIMARY]
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'None =0 ; BundleItem =1 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProductBasic', @level2type = N'COLUMN', @level2name = N'BundleType';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Item=0 ; Child =1 ; Parent =2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProductBasic', @level2type = N'COLUMN', @level2name = N'ProductType';
GO



ALTER TABLE ProductBasic ADD [StyleCode] Varchar(100) NOT NULL DEFAULT '' 
GO
ALTER TABLE ProductBasic ADD [ColorPatternCode] Varchar(50) NOT NULL DEFAULT ''
GO
ALTER TABLE ProductBasic ADD [SizeType] Varchar(50) NOT NULL DEFAULT ''
GO
ALTER TABLE ProductBasic ADD [SizeCode] Varchar(50) NOT NULL DEFAULT ''
GO
ALTER TABLE ProductBasic ADD [WidthCode] Varchar(30) NOT NULL DEFAULT ''
GO
ALTER TABLE ProductBasic ADD [LengthCode] Varchar(30) NOT NULL DEFAULT ''
GO
ALTER TABLE ProductBasic ADD [ClassCode] Varchar(50) NOT NULL DEFAULT ''
GO
ALTER TABLE ProductBasic ADD [DepartmentCode] Varchar(50) NOT NULL DEFAULT ''
GO
ALTER TABLE ProductBasic ADD [DivisionCode] Varchar(50) NOT NULL DEFAULT ''
GO
ALTER TABLE ProductBasic ADD [PriceRule] Varchar(50) NOT NULL DEFAULT ''
GO
ALTER TABLE ProductBasic ADD [Stockable] TINYINT NOT NULL DEFAULT 0
GO
ALTER TABLE ProductBasic ADD [OriginalUPC] VARCHAR(20) NOT NULL DEFAULT ''
GO

ALTER TABLE ProductBasic ADD [ProductUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
GO
