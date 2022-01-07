CREATE TABLE [dbo].[ExportFiles](
	[RowNum] [bigint] IDENTITY(1,1) NOT NULL,
	[DatabaseNum] [int] NOT NULL,
	[MasterAccountNum] [int] NOT NULL,
	[ProfileNum] [int] NOT NULL,
	[Status] [tinyint] NOT NULL DEFAULT 0,
	[ProcessUuid] [varchar](50) NOT NULL,
	[UpdateDateUtc] [datetime] NULL,
	[EnterDateUtc] [datetime] NOT NULL DEFAULT (getutcdate()),
 CONSTRAINT [PK_ExportFiles] PRIMARY KEY CLUSTERED 
(
	[RowNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ExportFiles]') AND name = N'IX_ExportFiles_AuthStatus')
CREATE NONCLUSTERED INDEX [IX_ExportFiles_AuthStatusUuid] ON [dbo].[ExportFiles]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[Status] ASC,
	[ProcessUuid] ASC
) 
GO
