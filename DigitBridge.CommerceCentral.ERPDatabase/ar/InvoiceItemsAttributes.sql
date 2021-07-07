CREATE TABLE [dbo].[InvoiceItemsAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [InvoiceItemsUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
    [InvoiceUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Invoice
	[JsonFields] VARCHAR(max) NOT NULL DEFAULT '', --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_InvoiceItemsAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItemsAttributes]') AND name = N'UK_InvoiceItemsAttributes_InvoiceItemsId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceItemsAttributes_InvoiceItemsUuid] ON [dbo].[InvoiceItemsAttributes]
(
	[InvoiceItemsUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItemsAttributes]') AND name = N'IX_InvoiceItemsAttributes_InvoiceId')
CREATE NONCLUSTERED INDEX [FK_InvoiceItemsAttributes_InvoiceUuid] ON [dbo].[InvoiceItemsAttributes]
(
	[InvoiceUuid] ASC
) ON [PRIMARY]
GO


