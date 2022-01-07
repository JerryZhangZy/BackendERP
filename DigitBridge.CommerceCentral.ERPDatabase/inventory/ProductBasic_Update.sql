IF COL_LENGTH('ProductBasic', 'ProductUuid') IS NULL					
BEGIN					
    ALTER TABLE ProductBasic ADD [ProductUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
    CREATE UNIQUE NONCLUSTERED INDEX [UK_ProductBasic_ProductUuid] ON [dbo].[ProductBasic]
    (
        [ProductUuid] ASC
    ) 
END					

IF COL_LENGTH('ProductBasic', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE ProductBasic ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('ProductBasic', 'OriginalUPC') IS NULL					
BEGIN					
    ALTER TABLE ProductBasic ADD [OriginalUPC] VARCHAR(20) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('ProductBasic', 'EnterDateUtc') IS NULL					
BEGIN					
    ALTER TABLE ProductBasic ADD [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate())
END					

IF COL_LENGTH('ProductBasic', 'DigitBridgeGuid') IS NULL					
BEGIN					
    ALTER TABLE ProductBasic ADD [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid())
END					


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProductBasic]') AND name = N'UI_ProductBasic_MasterAccountNum_ProfileNum_SKU')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ProductBasic_MasterAccountNum_ProfileNum_SKU] ON [dbo].[ProductBasic]
(
    [MasterAccountNum] ASC, 
    [ProfileNum] ASC, 
    [SKU] ASC
)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProductBasic]') AND name = N'UK_ProductBasic_ProductUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_ProductBasic_ProductUuid] ON [dbo].[ProductBasic]
(
	[ProductUuid] ASC
) 
GO 

 
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProductBasic]') AND name = N'IX_ProductBasic_S_B_M_P_U')
CREATE NONCLUSTERED INDEX [IX_ProductBasic_S_B_M_P_U] ON [dbo].[ProductBasic]
(
	[SKU] ASC, 
	[Brand] ASC,
	[Manufacturer] ASC,
	[ProductTitle] ASC,
	[UPC] ASC
) 
GO
