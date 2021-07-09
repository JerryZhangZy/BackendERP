CREATE TABLE [dbo].[CustomDefineCodes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [CustomDefineCodesUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Code
    [CodeType] VARCHAR(50) NOT NULL DEFAULT '', --Code type, for example: class, department, style
	[Code] VARCHAR(50) NOT NULL, --Code readable code, 
	[Description] NVARCHAR(200) NOT NULL, --Code description, 
    [InActive] TINYINT NOT NULL DEFAULT 0, --Disable this Code
	[EffectiveStart] DATE NULL,
	[EffectiveEnd] DATE NULL,
	[JsonFields] VARCHAR(max) NULL, --JSON string, store any Code fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_CustomDefineCodes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomDefineCodes]') AND name = N'UI_CustomDefineCodesId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_CustomDefineCodesUuid] ON [dbo].[CustomDefineCodes]
(
	[CustomDefineCodesUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomDefineCodes]') AND name = N'UI_CodeType_Code)
CREATE UNIQUE NONCLUSTERED INDEX [UI_CodeType_Code] ON [dbo].[CustomDefineCodes]
(
	[CodeType] ASC, 
	[Code] ASC
) ON [PRIMARY]
GO


