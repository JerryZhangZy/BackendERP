
-- 07/10/20201 By Jerry Z 
IF COL_LENGTH('OrderShipmentHeader', 'OrderShipmentUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentHeader ADD [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderShipmentHeader_OrderShipmentUuid] ON [dbo].[OrderShipmentHeader]
	(
		[OrderShipmentUuid] ASC
	) 
END					

IF COL_LENGTH('OrderShipmentHeader', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentHeader ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('OrderShipmentHeader', 'DigitBridgeGuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentHeader ADD [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid())
END					
