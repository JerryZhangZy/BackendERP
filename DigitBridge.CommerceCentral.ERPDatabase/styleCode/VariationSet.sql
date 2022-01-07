CREATE TABLE [dbo].[VariationSet]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[VariationSetUuid] Varchar(50) NOT NULL DEFAULT '', --VariationSetUuid. <br> Display: false, Editable: false
	[VariationSet] Varchar(50) NOT NULL DEFAULT '', --VariationSet Code. <br> Title: Variation Set, Display: true, Editable: true
    [Description] NVARCHAR(100) NOT NULL DEFAULT '', --VariationSet Description. <br> Title: Description, Display: true, Editable: true

	[ColorPatternCode] Varchar(2000) NOT NULL DEFAULT '', --Product included color and pattern codes. <br> Title: Colors, Display: true, Editable: true
	[SizeType] Varchar(1000) NOT NULL DEFAULT '', --Product included size types. <br> Title: Size Type, Display: true, Editable: true
	[SizeCode] Varchar(1000) NOT NULL DEFAULT '', --Product included size codes. <br> Title: Size, Display: true, Editable: true
	[WidthCode] Varchar(1000) NOT NULL DEFAULT '', --Product included width codes. <br> Title: Width, Display: true, Editable: true
	[LengthCode] Varchar(1000) NOT NULL DEFAULT '', --Product included length codes. <br> Title: Length, Display: true, Editable: true

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_VariationSet] PRIMARY KEY ([RowNum]), 
)  
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VariationSet]') AND name = N'UK_VariationSet_VariationSetUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_VariationSet_VariationSetUuid] ON [dbo].[VariationSet]
(
	[VariationSetUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VariationSet]') AND name = N'UI_VariationSet')
CREATE NONCLUSTERED INDEX [UI_VariationSet] ON [dbo].[VariationSet]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[VariationSet] ASC
) 
GO

