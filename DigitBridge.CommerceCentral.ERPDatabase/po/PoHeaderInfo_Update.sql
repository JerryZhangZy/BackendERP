 
 -- 11/19/2021 added by junxian
IF COL_LENGTH('PoHeaderInfo', 'WarehouseCode') IS NULL					
BEGIN					
    ALTER TABLE PoItems ADD [WarehouseCode] VARCHAR(50) NOT NULL DEFAULT ''
END					
