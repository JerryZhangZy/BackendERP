CREATE TABLE [dbo].[StyleCodeAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [StyleCodeUuid] VARCHAR(50) NOT NULL, --(Readonly) StyleCode uuid. load from StyleCodeBasic data. <br> Display: false, Editable: false
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --(Ignore) JSON string

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_StyleCodeAttributes] PRIMARY KEY ([RowNum]), 
)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StyleCodeAttributes]') AND name = N'UK_StyleCodeAttributes_StyleCodeUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_StyleCodeAttributes_StyleCodeUuid] ON [dbo].[StyleCodeAttributes]
(
	[StyleCodeUuid] ASC
)
GO



