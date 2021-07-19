CREATE TABLE [dbo].[InvoiceHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [InvoiceUuid] VARCHAR(50) NOT NULL, --Invoice uuid. <br> Display: false, Editable: false.
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --(Ignore) JSON string. 

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore) 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
    CONSTRAINT [PK_InvoiceHeaderAttributes] PRIMARY KEY ([RowNum]), 
)
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderAttributes]') AND name = N'UK_InvoiceHeaderAttributes_InvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceHeaderAttributes_InvoiceUuid] ON [dbo].[InvoiceHeaderAttributes]
(
	[InvoiceUuid] ASC
)
GO



