 
 -- 11/19/2021 added by junxian
IF COL_LENGTH('PoHeaderInfo', 'WarehouseCode') IS NULL					
BEGIN					
    ALTER TABLE PoHeaderInfo ADD [WarehouseCode] VARCHAR(50) NOT NULL DEFAULT ''
END		

IF COL_LENGTH('PoHeaderInfo', 'Notes') IS NULL					
BEGIN					
    ALTER TABLE PoHeaderInfo ADD [Notes] NVarchar(1000) NOT NULL DEFAULT ''
END	

