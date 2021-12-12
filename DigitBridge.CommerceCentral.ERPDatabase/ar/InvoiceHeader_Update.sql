

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
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_DueDate')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_DueDate] ON [dbo].[InvoiceHeader]
(
	[DueDate] ASC
) 
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_CustomerCode')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_CustomerCode] ON [dbo].[InvoiceHeader]
(
	[CustomerCode] ASC
) 
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_CustomerName')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_CustomerName] ON [dbo].[InvoiceHeader]
(
	[CustomerName] ASC
) 
GO


-- 11/19/2021 By jerry
IF COL_LENGTH('InvoiceHeader', 'ChannelAmount') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [ChannelAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END	 

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_CustomerCode_InvoiceStatus')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_CustomerCode_InvoiceStatus] ON [dbo].[InvoiceHeader]
(
	[ProfileNum] ASC,
	[CustomerCode] ASC,
	[InvoiceStatus] ASC
) 
GO


IF COL_LENGTH('InvoiceHeader', 'SalesRep') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [SalesRep] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('InvoiceHeader', 'SalesRep2') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [SalesRep2] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('InvoiceHeader', 'SalesRep3') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [SalesRep3] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('InvoiceHeader', 'SalesRep4') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [SalesRep4] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('InvoiceHeader', 'CommissionRate') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [CommissionRate] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('InvoiceHeader', 'CommissionRate2') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [CommissionRate2] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('InvoiceHeader', 'CommissionRate3') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [CommissionRate3] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('InvoiceHeader', 'CommissionRate4') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [CommissionRate4] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('InvoiceHeader', 'CommissionAmount') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [CommissionAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('InvoiceHeader', 'CommissionAmount2') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [CommissionAmount2] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('InvoiceHeader', 'CommissionAmount3') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [CommissionAmount3] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('InvoiceHeader', 'CommissionAmount4') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeader ADD [CommissionAmount4] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_SalesRep1234')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_SalesRep1234] ON [dbo].[InvoiceHeader]
(
	[SalesRep] ASC,
	[SalesRep2] ASC,
	[SalesRep3] ASC,
	[SalesRep4] ASC
) 
GO
