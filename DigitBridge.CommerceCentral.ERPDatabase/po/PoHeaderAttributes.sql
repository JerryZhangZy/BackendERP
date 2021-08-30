CREATE TABLE [dbo].[PoHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [PoUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[JsonFields] NVARCHAR(max) NULL, --JSON string, store any document fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_PoHeaderAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoHeaderAttributes]') AND name = N'UI_PoHeaderAttributes_PoId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PoHeaderAttributes_PoUuid] ON [dbo].[PoHeaderAttributes]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO



