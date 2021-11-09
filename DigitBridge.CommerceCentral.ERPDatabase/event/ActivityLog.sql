CREATE TABLE [dbo].[ActivityLog]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[LogUuid] varchar(50) NOT NULL DEFAULT '',--process uuid. <br> Display: false, Editable: false.
	[ProcessUuid] varchar(50) NOT NULL DEFAULT '',--process uuid. <br> Display: false, Editable: false.
	[ProcessNumber] varchar(50) NOT NULL DEFAULT '',--process uuid. <br> Display: false, Editable: false.
	[Action] int NOT NULL DEFAULT 0,--process uuid. <br> Display: false, Editable: false.
    [LogDate] DATETIME NOT NULL DEFAULT (getutcdate()), --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
	[LogMessage] varchar(MAX) NOT NULL DEFAULT '',--process uuid. <br> Display: false, Editable: false.

    [EventUuid]	VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))),--Event uuid. <br> Display: false, Editable: false.
    [UpdateDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
	CONSTRAINT [PK_ActivityLog] PRIMARY KEY CLUSTERED([RowNum]),
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ActivityLog]') AND name = N'UK_ActivityLog_OrderId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_ActivityLog] ON [dbo].[ActivityLog]
(
	[LogUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ActivityLog]') AND name = N'UI_ERPEventProcessType_ProcessUuid')
CREATE NONCLUSTERED INDEX [UI_ERPEventProcessType_ProcessUuid] ON [dbo].[ActivityLog]
(
	[ProcessUuid] ASC,
	[Action] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ActivityLog]') AND name = N'IX_ActivityLog_LogDate_ProcessNumber_Action')
CREATE NONCLUSTERED INDEX [IX_ActivityLog_LogDate_ProcessNumber_Action] ON [dbo].[ActivityLog]
(
	[ProfileNum] ASC,
	[LogDate] ASC,
	[ProcessNumber] ASC,
	[Action] ASC
)  
GO

