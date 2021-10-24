 
--TODO put frequently used filter columns in this index.
--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_Complex')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_Complex] ON [dbo].[InvoiceHeader]
( 
	[InvoiceNumber] ASC,
	[TransDate] ASC, 
	[TransNum]ASC,
	[MasterAccountNum] ASC,
	[ProfileNum] ASC
) 
GO
