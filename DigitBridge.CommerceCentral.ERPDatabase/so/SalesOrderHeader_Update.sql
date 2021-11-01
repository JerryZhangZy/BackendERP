

-- 07/29/20201 By Jerry Z 
IF COL_LENGTH('SalesOrderHeader', 'ShipDate') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [ShipDate] DATE NULL
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_OrderSourceCode')
	CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_ShipDate] ON [dbo].[SalesOrderHeader]
	(
		[ShipDate] ASC
	)  
END			

-- 11/2/2021 By junxian
IF COL_LENGTH('SalesOrderHeader', 'DepositAmount') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [DepositAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END	

-- 11/2/2021 By junxian
IF COL_LENGTH('SalesOrderHeader', 'MiscInvoiceUuid') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [MiscInvoiceUuid] VARCHAR(50) NOT NULL DEFAULT ''
END


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_MasterAccountNum_ProfileNum')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_MasterAccountNum_ProfileNum] ON [dbo].[SalesOrderHeader]
(
	[MasterAccountNum] ASC, 
	[ProfileNum] ASC
)  
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_OrderSourceCode')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_OrderType_OrderStatus] ON [dbo].[SalesOrderHeader]
(
	[OrderType] ASC,
	[OrderStatus] ASC
)  
GO

CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_CustomerUuid_CustomerCode] ON [dbo].[SalesOrderHeader]
(
	[CustomerUuid] ASC,
	[CustomerCode] ASC
) 
GO