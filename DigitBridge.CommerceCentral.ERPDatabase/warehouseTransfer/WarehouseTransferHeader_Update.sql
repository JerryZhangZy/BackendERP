IF COL_LENGTH('WarehouseTransferHeader', 'TransferStatus') IS NULL					
BEGIN					
    ALTER TABLE WarehouseTransferHeader ADD [TransferStatus] int NOT NULL DEFAULT 0
 
END	 