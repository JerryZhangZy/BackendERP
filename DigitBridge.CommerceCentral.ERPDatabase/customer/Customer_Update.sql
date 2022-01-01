
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_C_C_C_P_E_W')
CREATE NONCLUSTERED INDEX [IX_Customer_C_C_C_P_E_W] ON [dbo].[Customer]
(
	[CustomerCode] ASC, 
	[CustomerName] ASC,
	[Contact] ASC,
	[Phone1] ASC,
	[Email] ASC,
	[WebSite] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_C_C_B_F_P')
CREATE NONCLUSTERED INDEX [IX_Customer_C_C_B_F_P] ON [dbo].[Customer]
(
	[CustomerType] ASC, 
	[CustomerStatus] ASC,
	[BusinessType] ASC,
	[FirstDate] ASC,
	[Priority] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_A_R_D_Z')
CREATE NONCLUSTERED INDEX [IX_Customer_A_R_D_Z] ON [dbo].[Customer]
(
	[Area] ASC, 
	[Region] ASC,
	[Districtn] ASC,
	[Zone] ASC
) 
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_C_D_D_S')
CREATE NONCLUSTERED INDEX [IX_Customer_C_D_D_S] ON [dbo].[Customer]
(
	[ClassCode] ASC, 
	[DepartmentCode] ASC,
	[DivisionCode] ASC,
	[SourceCode] ASC
) 
GO


-- 11/5/2021 By Jerry
IF COL_LENGTH('Customer', 'ChannelNum') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [ChannelNum] INT NOT NULL DEFAULT 0
END	

IF COL_LENGTH('Customer', 'ChannelAccountNum') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [ChannelAccountNum] INT NOT NULL DEFAULT 0
END	


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_ChannelNum_ChannelAccountNum')
CREATE NONCLUSTERED INDEX [IX_Customer_ChannelNum_ChannelAccountNum] ON [dbo].[Customer]
(
	[ChannelNum] ASC, 
	[ChannelAccountNum] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_MasterAccountNum_ChannelNum_ChannelAccountNum')
CREATE NONCLUSTERED INDEX [IX_Customer_MasterAccountNum_ChannelNum_ChannelAccountNum] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ChannelNum] ASC, 
	[ChannelAccountNum] ASC
) 
GO

-- 11/18/2021 By Jerry

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'UI_Customer_CustomerCode')
CREATE UNIQUE NONCLUSTERED INDEX [UI_Customer_CustomerCode] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[CustomerCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_CustomerName')
CREATE NONCLUSTERED INDEX [IX_Customer_CustomerName] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[CustomerName] ASC
) 
GO

IF COL_LENGTH('Customer', 'SalesRep') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [SalesRep] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('Customer', 'SalesRep2') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [SalesRep2] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('Customer', 'SalesRep3') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [SalesRep3] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('Customer', 'SalesRep4') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [SalesRep4] Varchar(100) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('Customer', 'CommissionRate') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [CommissionRate] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('Customer', 'CommissionRate2') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [CommissionRate2] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('Customer', 'CommissionRate3') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [CommissionRate3] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('Customer', 'CommissionRate4') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [CommissionRate4] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_SalesRep1234')
CREATE NONCLUSTERED INDEX [IX_Customer_SalesRep1234] ON [dbo].[Customer]
(
	[SalesRep] ASC,
	[SalesRep2] ASC,
	[SalesRep3] ASC,
	[SalesRep4] ASC
) 
GO


-- 12/27/2021 By Jerry
IF COL_LENGTH('Customer', 'OrderMiscAmount') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [OrderMiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('Customer', 'ItemMiscAmount') IS NULL					
BEGIN					
    ALTER TABLE Customer ADD [ItemMiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

