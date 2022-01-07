CREATE TABLE [dbo].[QuickBooksSettingInfo]
( 
	[RowNum] bigint NOT NULL IDENTITY(1000000, 1),
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[SettingUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Setting uuid. <br> Display: false, Editable: false.

	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '',  --Quickbooks Setting JSON string. 
	
    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
	
    CONSTRAINT [PK_QuickBooksSettingInfo] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[QuickBooksSettingInfo]') AND name = N'UK_QuickBooksSettingInfo_SettingUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_QuickBooksSettingInfo_SettingUuid] ON [dbo].[QuickBooksSettingInfo]
(
    [SettingUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[QuickBooksSettingInfo]') AND name = N'UK_QuickBooksSettingInfo_MasterAccountNum_ProfileNum')
CREATE UNIQUE NONCLUSTERED INDEX [UK_QuickBooksSettingInfo_MasterAccountNum_ProfileNum] ON [dbo].[QuickBooksSettingInfo]
(
	[MasterAccountNum] ASC,
	[ProfileNum]
) 