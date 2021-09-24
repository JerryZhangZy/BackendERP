

-- 08/10/20201 By Jerry Z 
IF COL_LENGTH('InvoiceHeader', 'SalesOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [SalesOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_SalesOrderUuid')
	CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_SalesOrderUuid] ON [dbo].[InvoiceHeader]
	(
		[SalesOrderUuid] ASC
	) 
END					

IF COL_LENGTH('InvoiceHeader', 'OrderNumber') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [OrderNumber] VARCHAR(50) NOT NULL DEFAULT ''
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_OrderNumber')
	CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_OrderNumber] ON [dbo].[InvoiceHeader]
	(
		[OrderNumber] ASC
	) 
END	

-- 09/24/2021 By junxian
IF COL_LENGTH('InvoiceHeader', 'ShipDate') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [ShipDate] DATE NULL
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_ShipDate')
	CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_ShipDate] ON [dbo].[InvoiceHeader]
	(
		[ShipDate] ASC
	) 
END	 
