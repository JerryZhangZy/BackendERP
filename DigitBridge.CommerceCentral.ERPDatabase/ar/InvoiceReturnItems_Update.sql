
-- 09/24/2021 By junxian
IF COL_LENGTH('InvoiceReturnItems', 'ReturnDiscountAmount') IS NULL					
BEGIN					
    ALTER TABLE InvoiceReturnItems ADD [ReturnDiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0 
END	 
