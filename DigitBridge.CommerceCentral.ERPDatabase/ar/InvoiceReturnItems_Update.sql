
-- 10/13/2021 By junxian
IF COL_LENGTH('InvoiceReturnItems', 'ReturnDiscountAmount') IS NULL					
BEGIN					
    ALTER TABLE InvoiceReturnItems ADD [ReturnDiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0 
END	 

-- 10/14/2021 By junxian
IF COL_LENGTH('InvoiceReturnItems', 'InvoiceDiscountAmount') IS NULL					
BEGIN					
    ALTER TABLE InvoiceReturnItems ADD [InvoiceDiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0 
END	 

