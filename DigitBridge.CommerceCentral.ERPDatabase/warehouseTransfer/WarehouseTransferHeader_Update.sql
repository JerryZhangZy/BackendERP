IF COL_LENGTH('WarehouseTransferHeader', 'InTransitToWarehouseCode') IS NULL					
BEGIN					
    ALTER TABLE WarehouseTransferHeader ADD [InTransitToWarehouseCode] VARCHAR(50) NOT NULL DEFAULT ''
 
END	 