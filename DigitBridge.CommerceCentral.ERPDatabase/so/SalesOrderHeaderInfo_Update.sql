

-- 07/29/20201 By Jerry Z 
IF COL_LENGTH('SalesOrderHeaderInfo', 'Notes') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeaderInfo ADD [Notes] NVarchar(1000) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('SalesOrderHeaderInfo', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeaderInfo ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeaderInfo]') AND name = N'IX_SalesOrderHeaderInfo_DistributionCenterNum_CentralOrderNum')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaderInfo_DistributionCenterNum_CentralOrderNum] ON [dbo].[SalesOrderHeaderInfo]
(
	[DistributionCenterNum] ASC,
	[CentralOrderNum] ASC
)  
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeaderInfo]') AND name = N'IX_SalesOrderHeaderInfo_ShippingCarrier')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaderInfo_ShippingCarrier] ON [dbo].[SalesOrderHeaderInfo]
(
	[ShippingCarrier] ASC
)  
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeaderInfo]') AND name = N'IX_SalesOrderHeaderInfo_ChannelNum_ChannelAccountNum_ChannelOrderID')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaderInfo_ChannelNum_ChannelAccountNum_ChannelOrderID] ON [dbo].[SalesOrderHeaderInfo]
(
	[ChannelNum] ASC,
	[ChannelAccountNum] ASC,
	[ChannelOrderID] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeaderInfo]') AND name = N'IX_SalesOrderHeaderInfo_WarehouseCode_RefNum_CustomerPoNum')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaderInfo_WarehouseCode_RefNum_CustomerPoNum] ON [dbo].[SalesOrderHeaderInfo]
(
	[WarehouseCode] ASC,
	[RefNum] ASC,
	[CustomerPoNum] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeaderInfo]') AND name = N'IX_SalesOrderHeaderInfo_ShipToName_ShipToState_ShipToPostalCode')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaderInfo_ShipToName_ShipToState_ShipToPostalCode] ON [dbo].[SalesOrderHeaderInfo]
(
	[ShipToName] ASC,
	[ShipToState] ASC,
	[ShipToPostalCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeaderInfo]') AND name = N'IX_SalesOrderHeaderInfo_CentralOrderNum')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaderInfo_CentralOrderNum] ON [dbo].[SalesOrderHeaderInfo]
(
	[CentralOrderNum] ASC
)  
GO


-- 12/11/20201 By Jerry Z 
IF COL_LENGTH('SalesOrderHeaderInfo', 'OrderDCAssignmentNum') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeaderInfo ADD [OrderDCAssignmentNum] [bigint] NOT NULL DEFAULT 0
END					

IF COL_LENGTH('SalesOrderHeaderInfo', 'DBChannelOrderHeaderRowID') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeaderInfo ADD [DBChannelOrderHeaderRowID] VARCHAR(50) NOT NULL DEFAULT ''
END					

