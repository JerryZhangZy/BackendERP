CREATE TABLE [dbo].[SystemSetting]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [SystemSettingUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Code
	[Description] NVARCHAR(100) NOT NULL, --Code description, 
	[JsonFields] VARCHAR(max) NULL, --JSON string, store any Code fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_SystemSetting] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SystemSetting]') AND name = N'UK_SystemSettingUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_SystemSettingUuid] ON [dbo].[SystemSetting]
(
	[SystemSettingUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SystemSetting]') AND name = N'UI_MasterAccountNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_MasterAccountNum] ON [dbo].[SystemSetting]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC
) ON [PRIMARY]
GO


