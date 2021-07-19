CREATE TABLE [dbo].[InventoryAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [ProductUuid] VARCHAR(50) NOT NULL, --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
    [InventoryUuid] VARCHAR(50) NOT NULL, --(Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --(Ignore) JSON string

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
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


