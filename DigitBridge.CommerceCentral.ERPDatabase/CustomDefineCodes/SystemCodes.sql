CREATE TABLE [dbo].[SystemCodes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [SystemCodeUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Code
    [SystemCodeName] VARCHAR(50) NOT NULL DEFAULT '', --Code type, for example: class, department, style
	[Description] NVARCHAR(100) NOT NULL, --Code description, 
    [InActive] TINYINT NOT NULL DEFAULT 0, --Disable this Code
	[EffectiveStart] DATE NULL,
	[EffectiveEnd] DATE NULL,
	[JsonFields] VARCHAR(max) NULL, --JSON string, store any Code fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_SystemCodes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SystemCodes]') AND name = N'UI_SystemCodesId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_SystemCodesUuid] ON [dbo].[SystemCodes]
(
	[SystemCodeUuid] ASC
) ON [PRIMARY]
GO



