CREATE TABLE [dbo].[PoItemsAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [PoItemUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O Item Line
    [PoUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[JsonFields] NVARCHAR(max) NULL, --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_PoItemsAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItemsAttributes]') AND name = N'UI_PoItemsAttributes_PoId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PoItemsAttributes_PoItemUuid] ON [dbo].[PoItemsAttributes]
(
	[PoItemUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItemsAttributes]') AND name = N'UI_PoItemsAttributes_PoId')
CREATE NONCLUSTERED INDEX [UI_PoItemsAttributes_PoUuid] ON [dbo].[PoItemsAttributes]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO



