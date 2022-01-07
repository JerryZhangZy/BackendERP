
-- 07/10/20201 By Jerry Z 
IF COL_LENGTH('OrderShipmentCanceledItem', 'OrderShipmentUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentCanceledItem ADD [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT ''
    CREATE NONCLUSTERED INDEX [FK_OrderShipmentCanceledItem_OrderShipmentUuid] ON [dbo].[OrderShipmentCanceledItem]
    (
	    [OrderShipmentUuid] ASC,
	    [OrderDCAssignmentLineNum] ASC
    );
END					

IF COL_LENGTH('OrderShipmentCanceledItem', 'OrderShipmentCanceledItemUuid]') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentCanceledItem ADD [OrderShipmentCanceledItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
    CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderShipmentCanceledItem_OrderShipmentCanceledItemUuid] ON [dbo].[OrderShipmentCanceledItem]
    (
        [OrderShipmentCanceledItemUuid] ASC
    ) 
END					

IF COL_LENGTH('OrderShipmentCanceledItem', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentCanceledItem ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('OrderShipmentCanceledItem', 'DigitBridgeGuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentCanceledItem ADD [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid())
END					

/* Update script

    UPDATE spc
    SET spc.OrderShipmentUuid = sph.OrderShipmentUuid
    FROM OrderShipmentCanceledItem spc
    INNER JOIN OrderShipmentHeader sph ON (sph.OrderShipmentNum = spc.OrderShipmentNum);

*/

-- 11/22/20201 By Jerry Z 
IF COL_LENGTH('OrderShipmentCanceledItem', 'SalesOrderItemsUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentCanceledItem ADD [SalesOrderItemsUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

