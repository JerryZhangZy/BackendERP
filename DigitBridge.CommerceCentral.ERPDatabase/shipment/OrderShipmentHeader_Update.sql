
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

--Add by junxian 10/30/2021
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderShipmentHeader]') AND name = N'UI_OrderShipmentHeader_MainTrackingNumber')
CREATE UNIQUE NONCLUSTERED INDEX [UI_OrderShipmentHeader_MainTrackingNumber] ON [dbo].[OrderShipmentHeader]
(
	[ProfileNum] ASC,
	[MainTrackingNumber] ASC
) 
GO

