
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceTransaction]') AND name = N'IX_InvoiceTransaction_Composite1')
CREATE NONCLUSTERED INDEX [IX_InvoiceTransaction_Composite1] ON [dbo].[InvoiceTransaction]
( 
    [ProfileNum] ASC,
    [TransDate] ASC,
	[PaidBy] ASC
) 
GO
