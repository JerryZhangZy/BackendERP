CREATE TABLE [dbo].[ApInvoiceHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [ApInvoiceId] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[Fields] VARCHAR(max) NULL, --JSON string, store any document fields

    CONSTRAINT [PK_ApInvoiceHeaderAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceHeaderAttributes]') AND name = N'UI_ApInvoiceHeaderAttributes_ApInvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ApInvoiceHeaderAttributes_ApInvoiceId] ON [dbo].[ApInvoiceHeaderAttributes]
(
	[ApInvoiceId] ASC
) ON [PRIMARY]
GO



