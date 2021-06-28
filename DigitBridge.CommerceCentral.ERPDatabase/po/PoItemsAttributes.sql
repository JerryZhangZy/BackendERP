CREATE TABLE [dbo].[PoItemsAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [PoItemId] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O Item Line
    [PoId] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[Fields] VARCHAR(max) NULL, --JSON string, store any document fields

    CONSTRAINT [PK_PoItemsAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItemsAttributes]') AND name = N'UI_PoItemsAttributes_PoId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PoItemsAttributes_PoItemId] ON [dbo].[PoItemsAttributes]
(
	[PoItemId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItemsAttributes]') AND name = N'UI_PoItemsAttributes_PoId')
CREATE NONCLUSTERED INDEX [UI_PoItemsAttributes_PoId] ON [dbo].[PoItemsAttributes]
(
	[PoId] ASC
) ON [PRIMARY]
GO



