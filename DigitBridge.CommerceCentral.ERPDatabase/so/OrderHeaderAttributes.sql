CREATE TABLE [dbo].[OrderHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [OrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[JsonFields] VARCHAR(max) NOT NULL DEFAULT '', --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_OrderHeaderAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderHeaderAttributes]') AND name = N'UK_OrderHeaderAttributes_OrderId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderHeaderAttributes_OrderUuid] ON [dbo].[OrderHeaderAttributes]
(
	[OrderUuid] ASC
) ON [PRIMARY]
GO



