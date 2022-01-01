CREATE TABLE [dbo].[PoHeaderAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,  --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [PoUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O. <br> Display: false, Editable: false.
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '',  --(Ignore) JSON string. 

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_PoHeaderAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoHeaderAttributes]') AND name = N'UK_PoHeaderAttributes_PoUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PoHeaderAttributes_PoUuid] ON [dbo].[PoHeaderAttributes]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO



