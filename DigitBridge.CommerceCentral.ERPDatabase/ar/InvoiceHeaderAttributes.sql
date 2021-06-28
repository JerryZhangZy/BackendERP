﻿CREATE TABLE [dbo].[InvoiceHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [InvoiceId] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[Fields] VARCHAR(max) NULL, --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_InvoiceHeaderAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderAttributes]') AND name = N'UK_InvoiceHeaderAttributes_InvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceHeaderAttributes_InvoiceId] ON [dbo].[InvoiceHeaderAttributes]
(
	[InvoiceId] ASC
) ON [PRIMARY]
GO



