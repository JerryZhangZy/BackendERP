CREATE TABLE [dbo].[ProductBasic] (
    [CentralProductNum]  BIGINT         IDENTITY (1000000, 1) NOT NULL,
    [DatabaseNum]        INT            NOT NULL,
    [MasterAccountNum]   INT            NOT NULL,
    [ProfileNum]         INT            NOT NULL,
    [SKU]                VARCHAR(100)   NOT NULL DEFAULT '',
    [FNSku]              NVARCHAR(10)   NOT NULL DEFAULT '',
    [Condition]          TINYINT        NOT NULL DEFAULT 0,
    [Brand]              VARCHAR(150)   NOT NULL DEFAULT '',
    [Manufacturer]       NVARCHAR(255)  NOT NULL DEFAULT '',
    [ProductTitle]       NVARCHAR(500)  NOT NULL DEFAULT '',
    [LongDescription]    NVARCHAR(2000) NOT NULL DEFAULT '',
    [ShortDescription]   NVARCHAR(100)  NOT NULL DEFAULT '',
    [Subtitle]           NVARCHAR(50)   NOT NULL DEFAULT '',
    [ASIN]               NVARCHAR(10)   NOT NULL DEFAULT '',
    [UPC]                VARCHAR(20)    NOT NULL DEFAULT '',
    [EAN]                VARCHAR(20)    NOT NULL DEFAULT '',
    [ISBN]               VARCHAR(20)    NOT NULL DEFAULT '',
    [MPN]                NVARCHAR(50)   NOT NULL DEFAULT '',
    [Price]              DECIMAL(10, 2) NOT NULL DEFAULT 0,
    [Cost]               MONEY           NOT NULL DEFAULT 0,
    [AvgCost]            DECIMAL(10, 2) NOT NULL DEFAULT 0,
    [MAPPrice]           DECIMAL(10, 2) NOT NULL DEFAULT 0,
    [MSRP]               MONEY           NOT NULL DEFAULT 0,
    [BundleType]         TINYINT         NOT NULL DEFAULT 0,      --None=0 ; BundleItem =1
    [ProductType]        TINYINT         NOT NULL DEFAULT 0,
    [VariationVaryBy]    NVARCHAR(80)   NOT NULL DEFAULT '',      --Item=0 ; Child =1; Parent =2       
    [CopyToChildren]     TINYINT         NOT NULL DEFAULT 0,
    [MultipackQuantity]  INT             NOT NULL DEFAULT 0,
    [VariationParentSKU] NVARCHAR(50)   NOT NULL DEFAULT '',
    [IsInRelationship]   TINYINT         NOT NULL DEFAULT 0,
    [NetWeight]          DECIMAL(10, 2) NOT NULL DEFAULT 0,
    [GrossWeight]        DECIMAL(6, 2)  NOT NULL DEFAULT 0,
    [WeightUnit]         TINYINT         NOT NULL DEFAULT 0,
    [ProductHeight]      DECIMAL(10, 2) NOT NULL DEFAULT 0, -- Change column name
    [ProductLength]      DECIMAL(10, 2) NOT NULL DEFAULT 0, -- Change column name
    [ProductWidth]       DECIMAL(10, 2) NOT NULL DEFAULT 0,  -- Change column name
    [BoxHeight]          DECIMAL(6, 2)  NOT NULL DEFAULT 0,
    [BoxLength]          DECIMAL(6, 2)  NOT NULL DEFAULT 0,
    [BoxWidth]           DECIMAL(6, 2)  NOT NULL DEFAULT 0,
    [Unit]               TINYINT         NOT NULL DEFAULT 0,
    [HarmonizedCode]     NVARCHAR(20)   NOT NULL DEFAULT '',
    [TaxProductCode]     NVARCHAR(25)   NOT NULL DEFAULT '',
    [IsBlocked]          TINYINT         NOT NULL DEFAULT 0,
    [Warranty]           VARCHAR(255)   NOT NULL DEFAULT '',
    [CreateBy]           NVARCHAR(100)  NOT NULL DEFAULT '',
    [UpdateBy]           NVARCHAR(100)  NOT NULL DEFAULT '',
    [CreateDate]         DATETIME        NOT NULL DEFAULT (getutcdate()),
    [UpdateDate]         DATETIME        NOT NULL DEFAULT (getutcdate()),
    [ClassificationNum]  BIGINT          NOT NULL DEFAULT 0,

    -- Add new Columns
    [OriginalUPC] VARCHAR(20) NOT NULL DEFAULT '',
    [ProductUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for product SKU
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_ProductBasic] PRIMARY KEY CLUSTERED ([RowNum] ASC)
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
 