
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

IF COL_LENGTH('OrderShipmentHeader', 'InvoiceNumber') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentHeader ADD [InvoiceNumber] VARCHAR(50) NOT NULL DEFAULT ''
END	



-- 11/22/20201 By Jerry Z 
IF COL_LENGTH('OrderShipmentHeader', 'InvoiceUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentHeader ADD [InvoiceUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('OrderShipmentHeader', 'SalesOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentHeader ADD [SalesOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('OrderShipmentHeader', 'OrderNumber') IS NULL					
BEGIN					
    ALTER TABLE OrderShipmentHeader ADD [OrderNumber] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderShipmentHeader]') AND name = N'IX_OrderShipmentHeader_InvoiceNumber')
CREATE NONCLUSTERED INDEX [IX_OrderShipmentHeader_InvoiceNumber] ON [dbo].[OrderShipmentHeader]
(
	[ProfileNum] ASC,
	[InvoiceNumber] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderShipmentHeader]') AND name = N'IX_OrderShipmentHeader_OrderNumber')
CREATE NONCLUSTERED INDEX [IX_OrderShipmentHeader_OrderNumber] ON [dbo].[OrderShipmentHeader]
(
	[ProfileNum] ASC,
	[OrderNumber] ASC
) 
GO

--Add by junxian 11/30/2021
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderShipmentHeader]') AND name = N'UI_OrderShipmentHeader_MainTrackingNumber')
drop INDEX [UI_OrderShipmentHeader_MainTrackingNumber] ON [dbo].[OrderShipmentHeader]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderShipmentHeader]') AND name = N'IX_OrderShipmentHeader_ShipmentID')
CREATE unique INDEX [IX_OrderShipmentHeader_ShipmentID] ON [dbo].[OrderShipmentHeader]
(
	[ProfileNum] ASC,
	[ShipmentID] ASC
) 
GO
