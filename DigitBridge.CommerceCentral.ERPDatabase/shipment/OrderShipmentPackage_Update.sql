
-- 07/10/20201 By Jerry Z 
IF COL_LENGTH('OrderShipmentPackage', 'OrderShipmentPackageUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentPackage ADD [OrderShipmentPackageUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
    CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderShipmentPackage_OrderShipmentPackageUuid] ON [dbo].[OrderShipmentPackage]
    (
        [OrderShipmentPackageUuid] ASC
    ) 
END					

IF COL_LENGTH('OrderShipmentPackage', 'OrderShipmentUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentPackage ADD [OrderShipmentUuid] VARCHAR(50) NOT NULL DEFAULT ''
    CREATE NONCLUSTERED INDEX [FK_OrderShipmentPackage_OrderShipmentUuid] ON [dbo].[OrderShipmentPackage]
    (
	    [OrderShipmentUuid] ASC,
	    [PackageID] ASC
    );
END					

IF COL_LENGTH('OrderShipmentPackage', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentPackage ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('OrderShipmentPackage', 'DigitBridgeGuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentPackage ADD [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid())
END					

/*
    UPDATE spp
    SET spp.OrderShipmentUuid = sph.OrderShipmentUuid
    FROM OrderShipmentPackage spp
    INNER JOIN OrderShipmentHeader sph ON (sph.OrderShipmentNum = spp.OrderShipmentNum);
*/

-- 12/26/20201 By cuijunxian 
IF COL_LENGTH('OrderShipmentShippedItem', 'CentralOrderNum') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentShippedItem ADD [CentralOrderNum] [bigint] NULL DEFAULT 0
END
