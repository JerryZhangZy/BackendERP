CREATE TABLE [dbo].[PoHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [PoId] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[Fields] VARCHAR(max) NULL, --JSON string, store any document fields

    CONSTRAINT [PK_PoHeaderAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoHeaderAttributes]') AND name = N'UI_PoHeaderAttributes_PoId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PoHeaderAttributes_PoId] ON [dbo].[PoHeaderAttributes]
(
	[PoId] ASC
) ON [PRIMARY]
GO



