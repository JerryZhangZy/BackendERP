CREATE TABLE [dbo].[ProductExtAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [ProductUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_ProductExtAttributes] PRIMARY KEY ([RowNum]), 
)
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProductExtAttributes]') AND name = N'UK_ProductExtAttributes_InvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_ProductExtAttributes_ProductUuid] ON [dbo].[ProductExtAttributes]
(
	[ProductUuid] ASC
)
GO



