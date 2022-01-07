CREATE TABLE [dbo].[ProductExtAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [ProductUuid] VARCHAR(50) NOT NULL, --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --(Ignore) JSON string

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_ProductExtAttributes] PRIMARY KEY ([RowNum]), 
)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProductExtAttributes]') AND name = N'UK_ProductExtAttributes_ProductUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_ProductExtAttributes_ProductUuid] ON [dbo].[ProductExtAttributes]
(
	[ProductUuid] ASC
)
GO



