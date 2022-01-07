CREATE TABLE [dbo].[InvoiceItemsAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [InvoiceItemsUuid] VARCHAR(50) NOT NULL, --Invoice item line uuid. <br> Display: false, Editable: false.
    [InvoiceUuid] VARCHAR(50) NOT NULL, --Invoice uuid. <br> Display: false, Editable: false.
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --(Ignore) JSON string

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_InvoiceItemsAttributes] PRIMARY KEY ([RowNum]), 
)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItemsAttributes]') AND name = N'UK_InvoiceItemsAttributes_InvoiceItemsUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceItemsAttributes_InvoiceItemsUuid] ON [dbo].[InvoiceItemsAttributes]
(
	[InvoiceItemsUuid] ASC
)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItemsAttributes]') AND name = N'FK_InvoiceItemsAttributes_InvoiceUuid')
CREATE NONCLUSTERED INDEX [FK_InvoiceItemsAttributes_InvoiceUuid] ON [dbo].[InvoiceItemsAttributes]
(
	[InvoiceUuid] ASC
) 
GO


