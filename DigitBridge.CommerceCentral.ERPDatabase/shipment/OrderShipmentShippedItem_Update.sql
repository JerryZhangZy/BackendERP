
-- 07/10/20201 By Jerry Z 
IF COL_LENGTH('OrderShipmentShippedItem', 'OrderShipmentUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentShippedItem ADD [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT ''
    CREATE NONCLUSTERED INDEX [FK_OrderShipmentShippedItem_OrderShipmentUuid] ON [dbo].[OrderShipmentShippedItem]
    (
	    [OrderShipmentUuid] ASC,
	    [OrderDCAssignmentLineNum] ASC
    );
END					

IF COL_LENGTH('OrderShipmentShippedItem', 'OrderShipmentPackageUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentShippedItem ADD [OrderShipmentPackageUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('OrderShipmentShippedItem', 'OrderShipmentShippedItemUuid]') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentShippedItem ADD [OrderShipmentShippedItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
    CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderShipmentShippedItem_OrderShipmentShippedItemUuid] ON [dbo].[OrderShipmentShippedItem]
    (
        [OrderShipmentShippedItemUuid] ASC
    ) 
END					

IF COL_LENGTH('OrderShipmentShippedItem', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentShippedItem ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('OrderShipmentShippedItem', 'DigitBridgeGuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentShippedItem ADD [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid())
END					

/*
    UPDATE spi
    SET spi.OrderShipmentUuid = sph.OrderShipmentUuid
    FROM OrderShipmentShippedItem spi
    INNER JOIN OrderShipmentHeader sph ON (sph.OrderShipmentNum = spi.OrderShipmentNum);
*/


-- 11/22/20201 By Jerry Z 
IF COL_LENGTH('OrderShipmentShippedItem', 'SalesOrderItemsUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentShippedItem ADD [SalesOrderItemsUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

