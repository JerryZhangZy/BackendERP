CREATE TABLE [dbo].[Event_ERP](
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.
	[ChannelNum] int NOT NULL DEFAULT 0,--(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] int NOT NULL DEFAULT 0,--(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
	[ERPEventType] int NOT NULL DEFAULT 0,--ERP Event type. <br> Title: EventType, Display: true, Editable: true
	[ProcessSource] varchar(50) NOT NULL DEFAULT '',--process source.<br> Title:ProcessSource,Display:true,Editable:false
	[ProcessUuid] varchar(50) NOT NULL DEFAULT '',--process uuid. <br> Display: false, Editable: false.
	[ProcessData] nvarchar(MAX) NOT NULL DEFAULT '',--process data. <br> Display: false, Editable: false.
	[ActionStatus] int NOT NULL DEFAULT 0,--Action Status. <br> Title: Type, Display: true, Editable: false
	[ActionDateUtc] datetime NOT NULL DEFAULT (getutcdate()),--Action Date. <br> Title: Type, Display: true, Editable: false
	[EventMessage] nvarchar(300) NOT NULL DEFAULT '',--event message. <br> Display: false, Editable: false.
	[EnterDateUtc] datetime NOT NULL DEFAULT (getutcdate()),--(Ignore)

    [EventUuid]	VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))),--Event uuid. <br> Display: false, Editable: false.
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

