CREATE TABLE [dbo].[SalesOrderItemsAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [OrderItemsUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
    [OrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for Order
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_SalesOrderItemsAttributes] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderItemsAttributes]') AND name = N'UK_SalesOrderItemsAttributes_OrderItemsId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_SalesOrderItemsAttributes_OrderItemsUuid] ON [dbo].[SalesOrderItemsAttributes]
(
	[OrderItemsUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderItemsAttributes]') AND name = N'IX_SalesOrderItemsAttributes_OrderId')
CREATE NONCLUSTERED INDEX [FK_SalesOrderItemsAttributes_OrderUuid] ON [dbo].[SalesOrderItemsAttributes]
(
	[OrderUuid] ASC
) 
GO


