IF COL_LENGTH('ApInvoiceTransaction', 'PaymentUuid') IS NULL					
BEGIN					
    ALTER TABLE ApInvoiceTransaction ADD [PaymentUuid] VARCHAR(50) NOT NULL DEFAULT ''
END	 

IF COL_LENGTH('ApInvoiceTransaction', 'PaymentNumber') IS NULL					
BEGIN					
    ALTER TABLE ApInvoiceTransaction ADD [PaymentNumber] BIGINT NOT NULL DEFAULT 0
END	 