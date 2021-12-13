
--This columns for filter.
--[CentralFulfillmentNum] ASC,
--[OrderShipmentNum] ASC, 
--[CentralOrderNum]ASC,
--[ChannelNum] ASC ,
--[ChannelOrderID]ASC ,
--[ShipToName]ASC 

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderInfo]') AND name = N'IX_InvoiceHeaderInfo_CentralFulfillmentNum')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeaderInfo_CentralFulfillmentNum] ON [dbo].[InvoiceHeaderInfo]
(
	[CentralFulfillmentNum] ASC
) 

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderInfo]') AND name = N'IX_InvoiceHeaderInfo_OrderShipmentNum')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeaderInfo_OrderShipmentNum] ON [dbo].[InvoiceHeaderInfo]
(
	[OrderShipmentNum] ASC
)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderInfo]') AND name = N'IX_InvoiceHeaderInfo_CentralOrderNum')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeaderInfo_CentralOrderNum] ON [dbo].[InvoiceHeaderInfo]
(
	[CentralOrderNum] ASC
) 

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderInfo]') AND name = N'IX_InvoiceHeaderInfo_ChannelNum')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeaderInfo_ChannelNum] ON [dbo].[InvoiceHeaderInfo]
(
	[ChannelNum] ASC
)


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderInfo]') AND name = N'IX_InvoiceHeaderInfo_ChannelOrderID')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeaderInfo_ChannelOrderID] ON [dbo].[InvoiceHeaderInfo]
(
	[ChannelOrderID] ASC
)

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderInfo]') AND name = N'IX_InvoiceHeaderInfo_ShipToName')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeaderInfo_ShipToName] ON [dbo].[InvoiceHeaderInfo]
(
	[ShipToName] ASC
)


-- 11/19/2021 By jerry
IF COL_LENGTH('InvoiceHeaderInfo', 'Notes') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeaderInfo ADD [Notes] NVarchar(1000) NOT NULL DEFAULT ''
END	 

-- 12/11/2021 By junxian
IF COL_LENGTH('InvoiceHeaderInfo', 'DBChannelOrderHeaderRowID') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeaderInfo ADD [DBChannelOrderHeaderRowID] VARCHAR(50) NOT NULL Default ''
END	 

IF COL_LENGTH('InvoiceHeaderInfo', 'OrderDCAssignmentNum') IS NULL					
BEGIN					
    ALTER TABLE InvoiceHeaderInfo ADD [OrderDCAssignmentNum] [bigint] NOT NULL DEFAULT 0
END					

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderInfo]') AND name = N'IX_InvoiceHeaderInfo_OrderShipmentUuid')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeaderInfo_OrderShipmentUuid] ON [dbo].[InvoiceHeaderInfo]
(
	[OrderShipmentUuid] ASC
) 
GO
