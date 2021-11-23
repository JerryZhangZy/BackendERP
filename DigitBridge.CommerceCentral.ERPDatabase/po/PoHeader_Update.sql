IF COL_LENGTH('PoHeader', 'TaxableAmount') IS NULL					
BEGIN					
    ALTER TABLE PoHeader ADD [TaxableAmount]  DECIMAL(24, 6) NOT NULL DEFAULT 0
END

IF COL_LENGTH('PoHeader', 'NonTaxableAmount') IS NULL					
BEGIN					
    ALTER TABLE PoHeader ADD [NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END

IF COL_LENGTH('PoHeader', 'VendorNum') IS NOT NULL					
BEGIN
    exec sp_rename 'PoHeader.VendorNum', 'VendorCode', 'COLUMN'
END

-- 11/19/2021 added by junxian
IF COL_LENGTH('PoHeader', 'Terms') IS NULL					
BEGIN					
    ALTER TABLE PoHeader ADD [Terms] VARCHAR(50) NOT NULL DEFAULT ''
END
