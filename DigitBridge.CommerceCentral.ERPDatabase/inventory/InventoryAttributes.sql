CREATE TABLE [dbo].[InventoryAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [ProductUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for Produc
    [InventoryUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for Inventory
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_InventoryAttributes] PRIMARY KEY ([RowNum]), 
)
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryAttributes]') AND name = N'UK_InventoryAttributes_InventoryUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InventoryAttributes_InventoryUuid] ON [dbo].[InventoryAttributes]
(
	[InventoryUuid] ASC
)
GO

CREATE NONCLUSTERED INDEX [FK_InventoryAttributes_ProductUuid] ON [dbo].[InventoryAttributes]
(
	[ProductUuid] ASC
)
GO


