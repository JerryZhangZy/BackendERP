IF COL_LENGTH('PoHeader', 'TaxableAmount') IS NULL					
BEGIN					
    ALTER TABLE PoHeader ADD [TaxableAmount]  DECIMAL(24, 6) NOT NULL DEFAULT 0
END

IF COL_LENGTH('PoHeader', 'NonTaxableAmount') IS NULL					
BEGIN					
    ALTER TABLE PoHeader ADD [NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END