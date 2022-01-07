CREATE TABLE [dbo].[EventProcessERP]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.
    [EventUuid]	VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))),--Event uuid. <br> Display: false, Editable: false.

	[ChannelNum] int NOT NULL DEFAULT 0,--(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] int NOT NULL DEFAULT 0,--(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
	[ERPEventProcessType] int NOT NULL DEFAULT 0,--ERP Event type. <br> Title: EventType, Display: true, Editable: true
	[ProcessSource] varchar(50) NOT NULL DEFAULT '',--process source.<br> Title:ProcessSource,Display:true,Editable:false
	[ProcessUuid] varchar(50) NOT NULL DEFAULT '',--process uuid. <br> Display: false, Editable: false.
	[ProcessData] nvarchar(MAX) NOT NULL DEFAULT '',--process data. <br> Display: false, Editable: false.
	[EventMessage] nvarchar(300) NOT NULL DEFAULT '',--event message. <br> Display: false, Editable: false.

	[ActionStatus] int NOT NULL DEFAULT 0,--Download Acknowledge Status. <br> Title: Type, Display: true, Editable: false
	[ActionDate] datetime NOT NULL DEFAULT (getutcdate()),--Download Acknowledge Date. <br> Title: Type, Display: true, Editable: false
	[ProcessStatus] int NOT NULL DEFAULT 0,--Process Status. <br> Title: Type, Display: true, Editable: false
	[ProcessDate] datetime NOT NULL DEFAULT (getutcdate()),--Process Date. <br> Title: Type, Display: true, Editable: false
	[CloseStatus] int NOT NULL DEFAULT 0,--Close Status. <br> Title: Type, Display: true, Editable: false
	[CloseDate] datetime NOT NULL DEFAULT (getutcdate()),--Close Date. <br> Title: Type, Display: true, Editable: false
	[LastUpdateDate] datetime NOT NULL DEFAULT (getutcdate()),--Close Date. <br> Title: Type, Display: true, Editable: false

	[EnterDateUtc] datetime NOT NULL DEFAULT (getutcdate()),--(Ignore)
    [UpdateDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
	CONSTRAINT [PK_EventProcessERP] PRIMARY KEY CLUSTERED([RowNum]),
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EventProcessERP]') AND name = N'UK_EventProcessERP')
CREATE UNIQUE NONCLUSTERED INDEX [UK_EventProcessERP] ON [dbo].[EventProcessERP]
(
	[EventUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EventProcessERP]') AND name = N'UI_ERPEventProcessType_ProcessUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ERPEventProcessType_ProcessUuid] ON [dbo].[EventProcessERP]
(
	[ERPEventProcessType] ASC,
	[ProcessUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EventProcessERP]') AND name = N'IX_EventProcessERP_ERPEventProcessType')
CREATE NONCLUSTERED INDEX [IX_EventProcessERP_ERPEventProcessType] ON [dbo].[EventProcessERP]
(
	[ProfileNum] ASC,
	[ERPEventProcessType] ASC,
	[ActionStatus] ASC
)  
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EventProcessERP]') AND name = N'IX_EventProcessERP_LastUpdateDate')
CREATE NONCLUSTERED INDEX [IX_EventProcessERP_LastUpdateDate] ON [dbo].[EventProcessERP]
(
	[ProfileNum] ASC,
	[ERPEventProcessType] ASC,
	[LastUpdateDate] ASC
)  
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EventProcessERP]') AND name = N'IX_EventProcessERP_Type_Process_Action')
CREATE NONCLUSTERED INDEX [IX_EventProcessERP_Type_Process_Action] ON [dbo].[EventProcessERP]
(
	[ERPEventProcessType] ASC,
	[ProcessUuid] ASC,
	[ActionStatus] ASC
)  
GO
