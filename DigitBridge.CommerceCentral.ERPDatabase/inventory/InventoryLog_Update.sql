--12/25/2021 add by junxian
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_LogUuid')
CREATE NONCLUSTERED INDEX [IX_InventoryLog_LogUuid] ON [dbo].[InventoryLog]
(
	[LogUuid] ASC
) 
GO


--12/26/2021 add by junxian
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryLog]') AND name = N'IX_InventoryLog_BatchNum')
CREATE NONCLUSTERED INDEX [IX_InventoryLog_BatchNum] ON [dbo].[InventoryLog]
(
	[BatchNum] DESC
) 
GO
