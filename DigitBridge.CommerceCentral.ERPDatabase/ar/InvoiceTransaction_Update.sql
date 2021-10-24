 
--TODO put frequently used filter columns in this index.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceTransaction]') AND name = N'IX_InvoiceTransaction_Complex')
CREATE NONCLUSTERED INDEX [IX_InvoiceTransaction_Complex] ON [dbo].[InvoiceTransaction]
( 
	[InvoiceNumber] ASC,
	[TransDate] ASC, 
	[TransNum]ASC,
	[MasterAccountNum] ASC,
	[ProfileNum] ASC
) 
GO