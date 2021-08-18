

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


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_MasterAccountNum_ProfileNum')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_MasterAccountNum_ProfileNum] ON [dbo].[SalesOrderHeader]
(
	[MasterAccountNum] ASC, 
	[ProfileNum] ASC
)  
GO
