
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceTransaction]') AND name = N'IX_InvoiceTransaction_Composite1')
CREATE NONCLUSTERED INDEX [IX_InvoiceTransaction_Composite1] ON [dbo].[InvoiceTransaction]
( 
    [ProfileNum] ASC,
    [TransDate] ASC,
	[PaidBy] ASC
) 
GO


-- 11/19/2021 By jerry
IF COL_LENGTH('InvoiceTransaction', 'PaymentUuid') IS NULL					
BEGIN					
    ALTER TABLE InvoiceTransaction ADD [PaymentUuid] VARCHAR(50) NOT NULL DEFAULT ''
END	 

IF COL_LENGTH('InvoiceTransaction', 'PaymentNumber') IS NULL					
BEGIN					
    ALTER TABLE InvoiceTransaction ADD [PaymentNumber] BIGINT NOT NULL DEFAULT 0
END	 
