CREATE TABLE [dbo].[BusinessType]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [BusinessTypeUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Code
    [BusinessType] VARCHAR(50) NOT NULL DEFAULT '', --Business type code. <br> Title: Paid By, Display: true, Editable: true
	[PriceRule] VARCHAR(50) NOT NULL DEFAULT '', --Item price rule. <br> Title: Price Type, Display: true, Editable: true
	[Description] NVARCHAR(100) NOT NULL, --Code description, 

	[JsonFields] VARCHAR(max) NULL, --JSON string, store any Code fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_BusinessType] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[BusinessType]') AND name = N'UK_BusinessTypeUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_BusinessTypeUuid] ON [dbo].[BusinessType]
(
	[BusinessTypeUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[BusinessType]') AND name = N'UI_BusinessType')
CREATE UNIQUE NONCLUSTERED INDEX [UI_BusinessType] ON [dbo].[BusinessType]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[BusinessType] ASC
) ON [PRIMARY]
GO

