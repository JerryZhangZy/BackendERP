

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

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_OrderType_OrderStatus')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_OrderType_OrderStatus] ON [dbo].[SalesOrderHeader]
(
	[OrderType] ASC,
	[OrderStatus] ASC
)  
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_CustomerUuid_CustomerCode')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_CustomerUuid_CustomerCode] ON [dbo].[SalesOrderHeader]
(
	[CustomerUuid] ASC,
	[CustomerCode] ASC
) 
GO


-- 11/16/20201 By Jerry
IF COL_LENGTH('SalesOrderHeader', 'EtaArrivalDate') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [EtaArrivalDate] DATE NULL
END	

IF COL_LENGTH('SalesOrderHeader', 'EarliestShipDate') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [EarliestShipDate] DATE NULL
END

-- 11/17/20201 By junxian
IF COL_LENGTH('SalesOrderHeader', 'LatestShipDate') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [LatestShipDate] DATE NULL
END	

IF COL_LENGTH('SalesOrderHeader', 'SignatureFlag') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [SignatureFlag] TINYINT NOT NULL DEFAULT 0
END	

IF COL_LENGTH('SalesOrderHeader', 'ChannelAmount') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [ChannelAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END	


-- 12/11/20201 By Jerry Z
IF COL_LENGTH('SalesOrderHeader', 'SalesRep') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [SalesRep] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('SalesOrderHeader', 'SalesRep2') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [SalesRep2] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('SalesOrderHeader', 'SalesRep3') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [SalesRep3] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('SalesOrderHeader', 'SalesRep4') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [SalesRep4] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('SalesOrderHeader', 'CommissionRate') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [CommissionRate] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('SalesOrderHeader', 'CommissionRate2') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [CommissionRate2] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('SalesOrderHeader', 'CommissionRate3') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [CommissionRate3] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('SalesOrderHeader', 'CommissionRate4') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [CommissionRate4] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('SalesOrderHeader', 'CommissionAmount') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [CommissionAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('SalesOrderHeader', 'CommissionAmount2') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [CommissionAmount2] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('SalesOrderHeader', 'CommissionAmount3') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [CommissionAmount3] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('SalesOrderHeader', 'CommissionAmount4') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeader ADD [CommissionAmount4] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_SalesRep1234')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_SalesRep1234] ON [dbo].[SalesOrderHeader]
(
	[SalesRep] ASC,
	[SalesRep2] ASC,
	[SalesRep3] ASC,
	[SalesRep4] ASC
) 
GO
