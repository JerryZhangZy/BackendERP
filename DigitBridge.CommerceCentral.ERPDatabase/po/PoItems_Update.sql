 

IF COL_LENGTH('PoItems', 'WarehouseUuid') IS NULL					
BEGIN					
    ALTER TABLE PoItems ADD [WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('PoItems', 'WarehouseCode') IS NULL					
BEGIN					
    ALTER TABLE PoItems ADD [WarehouseCode] VARCHAR(50) NOT NULL DEFAULT ''
END	
IF COL_LENGTH('PoItems', 'DiscountPrice') IS NULL					
BEGIN					
    ALTER TABLE PoItems ADD [DiscountPrice] DECIMAL(24, 6) NOT NULL DEFAULT 0 
END	

IF COL_LENGTH('PoItems', 'TaxableAmount') IS NULL					
BEGIN					
    ALTER TABLE PoItems ADD [TaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END	

IF COL_LENGTH('PoItems', 'NonTaxableAmount') IS NULL					
BEGIN					
    ALTER TABLE PoItems ADD [NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END	

IF COL_LENGTH('PoItems', 'ItemTotalAmount') IS NULL					
BEGIN					
    ALTER TABLE PoItems ADD [ItemTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END	 