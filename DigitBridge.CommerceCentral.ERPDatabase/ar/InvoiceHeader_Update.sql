

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


-- 09/24/2021 By wenjian
IF COL_LENGTH('InvoiceHeader', 'QboDocNumber') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [QboDocNumber] VARCHAR(50) NOT NULL DEFAULT ''
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_ShipDate')
END	 


-- 10/24/2021 By junxian
----IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_QboDocNumber')
--CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_QboDocNumber] ON [dbo].[InvoiceHeader]
--(
--	[QboDocNumber] ASC
--) 
--GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_InvoiceDate')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_InvoiceDate] ON [dbo].[InvoiceHeader]
(
	[InvoiceDate] ASC
) 
GO
--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_DueDate')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_DueDate] ON [dbo].[InvoiceHeader]
(
	[DueDate] ASC
) 
--GO
----IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_CustomerCode')
--CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_CustomerCode] ON [dbo].[InvoiceHeader]
--(
--	[CustomerCode] ASC
--) 
--GO
----IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_CustomerName')
--CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_CustomerName] ON [dbo].[InvoiceHeader]
--(
--	[CustomerName] ASC
--) 
--GO
