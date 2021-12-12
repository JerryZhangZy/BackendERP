

-- 07/29/20201 By Jerry Z 
IF COL_LENGTH('SalesOrderHeaderInfo', 'Notes') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeaderInfo ADD [Notes] NVarchar(1000) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('SalesOrderHeaderInfo', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeaderInfo ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_OrderSourceCode')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaderInfo_DistributionCenterNum_CentralOrderNum] ON [dbo].[SalesOrderHeaderInfo]
(
	[DistributionCenterNum] ASC,
	[CentralOrderNum] ASC
)  
GO

CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaderInfo_ShippingCarrier] ON [dbo].[SalesOrderHeaderInfo]
(
	[ShippingCarrier] ASC
)  
GO

CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaderInfo_ChannelNum_ChannelAccountNum_ChannelOrderID] ON [dbo].[SalesOrderHeaderInfo]
(
	[ChannelNum] ASC,
	[ChannelAccountNum] ASC,
	[ChannelOrderID] ASC
) 
GO

CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaderInfo_WarehouseCode_RefNum_CustomerPoNum] ON [dbo].[SalesOrderHeaderInfo]
(
	[WarehouseCode] ASC,
	[RefNum] ASC,
	[CustomerPoNum] ASC
) 
GO

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

IF COL_LENGTH('SalesOrderHeaderInfo', 'DBChannelOrderHeaderRowID') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeaderInfo ADD [DBChannelOrderHeaderRowID] VARCHAR(50) NOT NULL Default ''
END	 

IF COL_LENGTH('SalesOrderHeaderInfo', 'OrderDCAssignmentNum') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeaderInfo ADD [OrderDCAssignmentNum] [bigint] NOT NULL Default 0
END
