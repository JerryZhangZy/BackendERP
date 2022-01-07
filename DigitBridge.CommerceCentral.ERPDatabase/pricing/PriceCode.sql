CREATE TABLE [dbo].[PriceCode]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [PriceCodeUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Order Item Line uuid. <br> Display: false, Editable: false
	[PriceCode] Varchar(50) NOT NULL DEFAULT '', --VariationSet Code. <br> Title: Variation Set, Display: true, Editable: true

	[Description] NVarchar(200) NOT NULL DEFAULT '', --Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
	[Inactive] TINYINT NOT NULL DEFAULT 0, --item will update inventory instock qty. <br> Title: Stockable, Display: true, Editable: true
	[StartDate] DATE NULL, --start active date. <br> Title: Start Date, Display: true, Editable: true
	[EndDate] DATE NULL, --start active date. <br> Title: End Date, Display: true, Editable: true

    [UpdateDateUtc] DATETIME NULL, --(Ignore) 
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore) 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
   CONSTRAINT [PK_PriceCode] PRIMARY KEY ([RowNum]), 
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceCode]') AND name = N'UK_PriceCode_PriceCodeUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PriceCode_PriceCodeUuid] ON [dbo].[PriceCode]
(
	[PriceCodeUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceCode]') AND name = N'UI_PriceCode')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PriceCode] ON [dbo].[PriceCode]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[PriceCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceCode]') AND name = N'IX_PriceCode_Inactive')
CREATE NONCLUSTERED INDEX [IX_PriceCode_Inactive] ON [dbo].[PriceCode]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[Inactive] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceCode]') AND name = N'IX_PriceCode_StartDate_EndDate')
CREATE NONCLUSTERED INDEX [IX_PriceCode_StartDate_EndDate] ON [dbo].[PriceCode]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[StartDate] ASC,
	[EndDate] ASC
) 
GO
