CREATE TABLE [dbo].[SalesOrderItemsAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [OrderItemsUuid] VARCHAR(50) NOT NULL, --Order item line uuid. <br> Display: false, Editable: false.
    [OrderUuid] VARCHAR(50) NOT NULL,  --Order uuid. <br> Display: false, Editable: false.
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --(Ignore) JSON string

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
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


