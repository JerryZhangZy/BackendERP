
--TODO put frequently used filter columns in this index.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderInfo]') AND name = N'IX_InvoiceHeaderInfo_Complex')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeaderInfo_Complex] ON [dbo].[InvoiceHeaderInfo]
( 
	[CentralFulfillmentNum] ASC,
	[OrderShipmentNum] ASC, 
	[CentralOrderNum]ASC,
	[ChannelNum] ASC ,
	[ChannelOrderID]ASC ,
	[ShipToName]ASC 
) 
GO