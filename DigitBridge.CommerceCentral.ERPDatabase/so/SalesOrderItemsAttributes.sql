CREATE TABLE [dbo].[SalesOrderItemsAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [SalesOrderItemsUuid] VARCHAR(50) NOT NULL, --Order item line uuid. <br> Display: false, Editable: false.
    [SalesOrderUuid] VARCHAR(50) NOT NULL,  --Order uuid. <br> Display: false, Editable: false.
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --(Ignore) JSON string

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_SalesOrderItemsAttributes] PRIMARY KEY ([RowNum]), 
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderItemsAttributes]') AND name = N'UK_SalesOrderItemsAttributes_SalesOrderItemsUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_SalesOrderItemsAttributes_SalesOrderItemsUuid] ON [dbo].[SalesOrderItemsAttributes]
(
	[SalesOrderItemsUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderItemsAttributes]') AND name = N'FK_SalesOrderItemsAttributes_SalesOrderUuid')
CREATE NONCLUSTERED INDEX [FK_SalesOrderItemsAttributes_SalesOrderUuid] ON [dbo].[SalesOrderItemsAttributes]
(
	[SalesOrderUuid] ASC
) 
GO


