 

IF COL_LENGTH('PoItems', 'WarehouseUuid') IS NULL					
BEGIN					
    ALTER TABLE PoItems ADD [WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('PoItems', 'WarehouseCode') IS NULL					
BEGIN					
    ALTER TABLE PoItems ADD [WarehouseCode] VARCHAR(50) NOT NULL DEFAULT ''
END					
