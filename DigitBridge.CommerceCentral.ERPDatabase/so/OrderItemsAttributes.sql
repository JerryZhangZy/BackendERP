CREATE TABLE [dbo].[OrderItemsAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [OrderItemsUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O
    [OrderUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Order
	[JsonFields] VARCHAR(max) NOT NULL DEFAULT '', --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_OrderItemsAttributes] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderItemsAttributes]') AND name = N'UK_OrderItemsAttributes_OrderItemsId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderItemsAttributes_OrderItemsUuid] ON [dbo].[OrderItemsAttributes]
(
	[OrderItemsUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderItemsAttributes]') AND name = N'IX_OrderItemsAttributes_OrderId')
CREATE NONCLUSTERED INDEX [FK_OrderItemsAttributes_OrderUuid] ON [dbo].[OrderItemsAttributes]
(
	[OrderUuid] ASC
) 
GO


