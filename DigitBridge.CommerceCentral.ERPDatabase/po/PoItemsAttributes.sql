CREATE TABLE [dbo].[PoItemsAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [PoItemUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O Item Line <br> Display: false, Editable: false.
    [PoUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O <br> Display: false, Editable: false.
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '', --JSON string, store any document fields <br> Display: false, Editable: false.

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_PoItemsAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItemsAttributes]') AND name = N'UK_PoItemsAttributes_PoItemUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PoItemsAttributes_PoItemUuid] ON [dbo].[PoItemsAttributes]
(
	[PoItemUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItemsAttributes]') AND name = N'UI_PoItemsAttributes_PoUuid')
CREATE NONCLUSTERED INDEX [UI_PoItemsAttributes_PoUuid] ON [dbo].[PoItemsAttributes]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO



