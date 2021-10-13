CREATE TABLE [dbo].[Event_ERP](
	[RowNum] bigint IDENTITY(1,1) NOT NULL,
	[DatabaseNum] int NOT NULL DEFAULT 0,
	[MasterAccountNum] int NOT NULL DEFAULT 0,
	[ProfileNum] int NOT NULL DEFAULT 0,
	[ChannelNum] int NOT NULL DEFAULT 0,
	[ChannelAccountNum] int NOT NULL DEFAULT 0,
	[ERPEventType] int NOT NULL DEFAULT 0,
	[ProcessSource] varchar(50) NOT NULL DEFAULT '',
	[ProcessUuid] varchar(50) NOT NULL DEFAULT '',
	[ProcessData] nvarchar(MAX) NOT NULL DEFAULT '',
	[ActionStatus] int NOT NULL DEFAULT 0,
	[ActionDateUtc] datetime NOT NULL DEFAULT (getutcdate()),
	[EventMessage] nvarchar(300) NOT NULL DEFAULT '',
	[EnterDateUtc] datetime NOT NULL DEFAULT (getutcdate()),

    [EventUuid]	VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))),
    [UpdateDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
	CONSTRAINT [PK_Event_ERP] PRIMARY KEY CLUSTERED([RowNum]),
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_ERP]') AND name = N'UK_Event_ERP_OrderId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_Event_ERP] ON [dbo].[Event_ERP]
(
	[EventUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_ERP]') AND name = N'IX_Event_ERP_ERPEventType')
CREATE NONCLUSTERED INDEX [IX_Event_ERP_ERPEventType] ON [dbo].[Event_ERP]
(
	[ProfileNum] ASC,
	[ERPEventType] ASC,
	[ActionStatus] ASC
)  
GO

