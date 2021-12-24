
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_LotNum')
CREATE NONCLUSTERED INDEX [IX_Inventory_LotNum] ON [dbo].[Inventory]
(
	[LotNum] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_S_C_S_W_L_W_L_L')
CREATE NONCLUSTERED INDEX [IX_Inventory_S_C_S_W_L_W_L_L] ON [dbo].[Inventory]
(
	[SKU] ASC, 
	[ColorPatternCode] ASC,
	[SizeCode] ASC,
	[WidthCode] ASC,
	[LengthCode] ASC,
	[WarehouseCode] ASC,
	[LpnNum] ASC,
	[LotNum] ASC
) 
GO


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_ProductUuid')
CREATE NONCLUSTERED INDEX [IX_Inventory_ProductUuid] ON [dbo].[Inventory]
(
	[ProductUuid] ASC
) 
GO


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND name = N'IX_Inventory_PN_S')
CREATE NONCLUSTERED INDEX [IX_Inventory_PN_S] ON [dbo].[Inventory]
(
	[ProfileNum] ASC,
	[SKU] ASC
) 
GO
