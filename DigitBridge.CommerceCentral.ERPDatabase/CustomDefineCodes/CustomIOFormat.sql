CREATE TABLE [dbo].[CustomIOFormat]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [CustomIOFormatUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Code
    [FormatType] VARCHAR(50) NOT NULL DEFAULT '', --Entity type, for example: SalesOrder, Invoice, Inventory
	[FormatNumber] INT NOT NULL DEFAULT 0, --Format number, 
	[FormatName] VARCHAR(50) NOT NULL DEFAULT '', --Format name, 
	[Description] NVARCHAR(200) NOT NULL, --Format description, 
	[FormatObject] VARCHAR(max) NULL, --JSON string, format define

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_CustomIOFormat] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomIOFormat]') AND name = N'UI_CustomIOFormatId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_CustomIOFormatUuid] ON [dbo].[CustomIOFormat]
(
	[CustomIOFormatUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomIOFormat]') AND name = N'IX_CustomIOFormat_FormatType_FormatNumber)
CREATE UNIQUE NONCLUSTERED INDEX [UI_CustomIOFormat_FormatType_FormatNumber] ON [dbo].[CustomIOFormat]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[FormatType] ASC,
	[FormatNumber] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomIOFormat]') AND name = N'IX_CustomIOFormat_FormatType)
CREATE NONCLUSTERED INDEX [IX_CustomIOFormat_FormatType] ON [dbo].[CustomIOFormat]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[FormatType] ASC
) ON [PRIMARY]
GO

