CREATE TABLE [dbo].[ApInvoiceHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [ApInvoiceUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[JsonFields] VARCHAR(max) NULL, --JSON string, store any document fields

    CONSTRAINT [PK_ApInvoiceHeaderAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceHeaderAttributes]') AND name = N'UI_ApInvoiceHeaderAttributes_ApInvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ApInvoiceHeaderAttributes_ApInvoiceUuid] ON [dbo].[ApInvoiceHeaderAttributes]
(
	[ApInvoiceUuid] ASC
) ON [PRIMARY]
GO



