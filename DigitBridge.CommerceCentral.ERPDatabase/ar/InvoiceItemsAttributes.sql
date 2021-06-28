CREATE TABLE [dbo].[InvoiceItemsAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [InvoiceItemsId] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
    [InvoiceId] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Invoice
	[Fields] VARCHAR(max) NULL, --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_InvoiceItemsAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItemsAttributes]') AND name = N'UK_InvoiceItemsAttributes_InvoiceItemsId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceItemsAttributes_InvoiceItemsId] ON [dbo].[InvoiceItemsAttributes]
(
	[InvoiceItemsId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItemsAttributes]') AND name = N'IX_InvoiceItemsAttributes_InvoiceId')
CREATE NONCLUSTERED INDEX [FK_InvoiceItemsAttributes_InvoiceId] ON [dbo].[InvoiceItemsAttributes]
(
	[InvoiceId] ASC
) ON [PRIMARY]
GO


