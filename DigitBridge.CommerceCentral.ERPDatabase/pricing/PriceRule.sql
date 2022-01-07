CREATE TABLE [dbo].[PriceRule]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [PriceRuleUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Order Item Line uuid. <br> Display: false, Editable: false
	[PriceRule] Varchar(50) NOT NULL DEFAULT '', --PriceRule Number. <br> Title: PriceRule #, Display: true, Editable: true
	[Description] NVarchar(200) NOT NULL DEFAULT '', --Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
	[PriceCode] Varchar(1000) NOT NULL DEFAULT '', --Price Code. <br> Title: Price Code, Display: true, Editable: true

    [UpdateDateUtc] DATETIME NULL, --(Ignore) 
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore) 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
   CONSTRAINT [PK_PriceRule] PRIMARY KEY ([RowNum]), 
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceRule]') AND name = N'UK_PriceRule_PriceRuleUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PriceRule_PriceRuleUuid] ON [dbo].[PriceRule]
(
	[PriceRuleUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceRule]') AND name = N'UI_PriceRule')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PriceRule] ON [dbo].[PriceRule]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[PriceRule] ASC
) 
GO
