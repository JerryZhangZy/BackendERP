CREATE TABLE [dbo].[ApInvoiceHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,--(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [ApInvoiceUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[JsonFields] NVARCHAR(max) NULL, --JSON string, store any document fields
	[EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore) 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
    CONSTRAINT [PK_ApInvoiceHeaderAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceHeaderAttributes]') AND name = N'UK_ApInvoiceHeaderAttributes_ApInvoiceUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_ApInvoiceHeaderAttributes_ApInvoiceUuid] ON [dbo].[ApInvoiceHeaderAttributes]
(
	[ApInvoiceUuid] ASC
) ON [PRIMARY]
GO



