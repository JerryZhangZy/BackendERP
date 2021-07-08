CREATE TABLE [dbo].[CustomerAddress]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [AddressUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Customer

    [CustomerUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Customer
    [AddressCode] VARCHAR(50) NOT NULL DEFAULT '', --Address code, human readable
	[AddressType] INT NOT NULL DEFAULT 0, --Address type, billing, shipping, store
    [Description] NVARCHAR(200) NOT NULL DEFAULT '', --Address description

	[Name] NVARCHAR(100) NOT NULL DEFAULT '',
	[FirstName] NVARCHAR(50) NOT NULL DEFAULT '',
	[LastName] NVARCHAR(50) NOT NULL DEFAULT '',
	[Suffix] NVARCHAR(50) NOT NULL DEFAULT '',
	[Company] NVARCHAR(100) NOT NULL DEFAULT '',
	[CompanyJobTitle] NVARCHAR(100) NOT NULL DEFAULT '',
	[Attention] NVARCHAR(100) NOT NULL DEFAULT '',
	[AddressLine1] NVARCHAR(200) NOT NULL DEFAULT '',
	[AddressLine2] NVARCHAR(200) NOT NULL DEFAULT '',
	[AddressLine3] NVARCHAR(200) NOT NULL DEFAULT '',
	[City] NVARCHAR(100) NOT NULL DEFAULT '',
	[State] NVARCHAR(50) NOT NULL DEFAULT '',
	[StateFullName] NVARCHAR(100) NOT NULL DEFAULT '',
	[PostalCode] VARCHAR(50) NOT NULL DEFAULT '',
	[PostalCodeExt] VARCHAR(50) NOT NULL DEFAULT '',
	[County] NVARCHAR(100) NOT NULL DEFAULT '',
	[Country] NVARCHAR(100) NOT NULL DEFAULT '',
	[Email] VARCHAR(100) NOT NULL DEFAULT '',
	[DaytimePhone] VARCHAR(50) NOT NULL DEFAULT '',
	[NightPhone] VARCHAR(50) NOT NULL DEFAULT '',

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL DEFAULT '',
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '',
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_CustomerAddress] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomerAddress]') AND name = N'UI_CustomerAddress_AddressId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_CustomerAddress_AddressUuid] ON [dbo].[CustomerAddress]
(
	[AddressUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomerAddress]') AND name = N'UI_CustomerAddress_CustomerId_AddressCode')
CREATE UNIQUE NONCLUSTERED INDEX [UI_CustomerAddress_CustomerUuid_AddressCode] ON [dbo].[CustomerAddress]
(
	[CustomerUuid] ASC,
	[AddressCode] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomerAddress]') AND name = N'UI_CustomerAddress_CustomerId')
CREATE NONCLUSTERED INDEX [FK_CustomerAddress_CustomerUuid] ON [dbo].[CustomerAddress]
(
	[CustomerUuid] ASC
) 
GO


