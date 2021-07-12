CREATE TABLE [dbo].[SalesOrderHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [OrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_SalesOrderHeaderAttributes] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeaderAttributes]') AND name = N'UK_SalesOrderHeaderAttributes_OrderId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_SalesOrderHeaderAttributes_OrderUuid] ON [dbo].[SalesOrderHeaderAttributes]
(
	[OrderUuid] ASC
) 
GO



