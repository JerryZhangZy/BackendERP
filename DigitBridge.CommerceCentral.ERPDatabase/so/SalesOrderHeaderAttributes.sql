CREATE TABLE [dbo].[SalesOrderHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [SalesOrderUuid] VARCHAR(50) NOT NULL, --Order uuid. <br> Display: false, Editable: false.
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --(Ignore) JSON string. 

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore) 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_SalesOrderHeaderAttributes] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeaderAttributes]') AND name = N'UK_SalesOrderHeaderAttributes_OrderId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_SalesOrderHeaderAttributes_SalesOrderUuid] ON [dbo].[SalesOrderHeaderAttributes]
(
	[SalesOrderUuid] ASC
) 
GO



