CREATE TABLE [dbo].[CustomerAddress]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [AddressUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Customer Address uuid. <br> Display: false, Editable: false.

    [CustomerUuid] VARCHAR(50) NOT NULL, --Customer uuid. <br> Display: false, Editable: false.
    [AddressCode] VARCHAR(50) NOT NULL DEFAULT '', --Address code, human readable. <br> Title: Address Code, Display: true, Editable: true.
	[AddressType] INT NOT NULL DEFAULT 0, --Address type, billing, shipping, store. <br> Title: Address Type, Display: true, Editable: true.
    [Description] NVARCHAR(200) NOT NULL DEFAULT '', --Address description. <br> Title: Address Description, Display: true, Editable: true.

	[Name] NVARCHAR(100) NOT NULL DEFAULT '', --Name. <br> Title: Name, Display: true, Editable: true.
	[FirstName] NVARCHAR(50) NOT NULL DEFAULT '', --First Name. <br> Title: First Name, Display: true, Editable: true.
	[LastName] NVARCHAR(50) NOT NULL DEFAULT '', --Last Name. <br> Title: Last Name, Display: true, Editable: true.
	[Suffix] NVARCHAR(50) NOT NULL DEFAULT '', --Suffix <br> Title: Suffix, Display: true, Editable: true.
	[Company] NVARCHAR(100) NOT NULL DEFAULT '', --Company Name <br> Title: Company, Display: true, Editable: true.
	[CompanyJobTitle] NVARCHAR(100) NOT NULL DEFAULT '', --Job Title <br> Title: Job Title, Display: true, Editable: true.
	[Attention] NVARCHAR(100) NOT NULL DEFAULT '', --Attention <br> Title: Attention, Display: true, Editable: true.
	[AddressLine1] NVARCHAR(200) NOT NULL DEFAULT '', --Address Line 1 <br> Title: Address Line 1, Display: true, Editable: true.
	[AddressLine2] NVARCHAR(200) NOT NULL DEFAULT '', --Address Line 2 <br> Title: Address Line 2, Display: true, Editable: true.
	[AddressLine3] NVARCHAR(200) NOT NULL DEFAULT '', --Address Line 3 <br> Title: Address Line 3, Display: true, Editable: true.
	[City] NVARCHAR(100) NOT NULL DEFAULT '', --City <br> Title: City, Display: true, Editable: true.
	[State] NVARCHAR(50) NOT NULL DEFAULT '', --State <br> Title: State, Display: true, Editable: true.
	[StateFullName] NVARCHAR(100) NOT NULL DEFAULT '', --State Full Name <br> Title: State Name, Display: true, Editable: true.
	[PostalCode] VARCHAR(50) NOT NULL DEFAULT '', --PostalCode <br> Title: Postal Code, Display: true, Editable: true.
	[PostalCodeExt] VARCHAR(50) NOT NULL DEFAULT '', --PostalCodeExt <br> Title: Postal Code Ext., Display: true, Editable: true.
	[County] NVARCHAR(100) NOT NULL DEFAULT '', --County <br> Title: County, Display: true, Editable: true.
	[Country] NVARCHAR(100) NOT NULL DEFAULT '', --Country <br> Title: Country, Display: true, Editable: true.
	[Email] VARCHAR(100) NOT NULL DEFAULT '', --Email <br> Title: Email, Display: true, Editable: true.
	[DaytimePhone] VARCHAR(50) NOT NULL DEFAULT '', --DaytimePhone <br> Title: Phone, Display: true, Editable: true.
	[NightPhone] VARCHAR(50) NOT NULL DEFAULT '', --NightPhone <br> Title: Phone 2, Display: true, Editable: true.

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
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


