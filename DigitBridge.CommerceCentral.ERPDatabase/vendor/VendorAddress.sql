CREATE TABLE [dbo].[VendorAddress]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [AddressId] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Address

    [VendorId] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Vendor
    [AddressCode] VARCHAR(50) NOT NULL DEFAULT '', --Address code, human readable
    [AddressType] INT NOT NULL DEFAULT '0', --Address type, billing, shipping, store
    [Description] NVARCHAR(200) NOT NULL DEFAULT '', --Address description

	[Name] NVARCHAR(100) NULL,
	[FirstName] NVARCHAR(50) NULL,
	[LastName] NVARCHAR(50) NULL,
	[Suffix] NVARCHAR(50) NULL,
	[Company] NVARCHAR(100) NULL,
	[CompanyJobTitle] NVARCHAR(100) NULL,
	[Attention] NVARCHAR(100) NULL,
	[AddressLine1] NVARCHAR(200) NULL,
	[AddressLine2] NVARCHAR(200) NULL,
	[AddressLine3] NVARCHAR(200) NULL,
	[City] NVARCHAR(100) NULL,
	[State] NVARCHAR(50) NULL,
	[StateFullName] NVARCHAR(100) NULL,
	[PostalCode] VARCHAR(50) NULL,
	[PostalCodeExt] VARCHAR(50) NULL,
	[County] NVARCHAR(100) NULL,
	[Country] NVARCHAR(100) NULL,
	[Email] VARCHAR(100) NULL,
	[DaytimePhone] VARCHAR(50) NULL,
	[NightPhone] VARCHAR(50) NULL,

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_VendorAddress] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VendorAddress]') AND name = N'UI_VendorAddress_AddressId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_VendorAddress_AddressId] ON [dbo].[VendorAddress]
(
	[AddressId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VendorAddress]') AND name = N'UI_VendorAddress_VendorId_AddressCode')
CREATE UNIQUE NONCLUSTERED INDEX [UI_VendorAddress_VendorId_AddressCode] ON [dbo].[VendorAddress]
(
	[VendorId] ASC,
	[AddressCode] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VendorAddress]') AND name = N'UI_VendorAddress_VendorId')
CREATE NONCLUSTERED INDEX [UI_VendorAddress_VendorId] ON [dbo].[VendorAddress]
(
	[VendorId] ASC
) ON [PRIMARY]
GO

