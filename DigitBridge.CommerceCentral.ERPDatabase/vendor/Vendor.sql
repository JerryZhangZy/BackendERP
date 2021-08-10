CREATE TABLE [dbo].[Vendor]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [Digit_supplier_id] VARCHAR(50) NULL DEFAULT '', --Digit bridge supplier_id

    [VendorUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Vendor
	[VendorCode] VARCHAR(50) NULL, --Vendor readable number, DatabaseNum + VendorCode is DigitBridgeVendorCode, which is global unique
	[VendorName] NVARCHAR(200) NULL, --Vendor name
	[Contact] NVARCHAR(200) NULL, --Vendor contact person
	[Phone1] VARCHAR(50) NULL, --Vendor phone 1
	[Phone2] VARCHAR(50) NULL, --Vendor phone 2
	[Phone3] VARCHAR(50) NULL, --Vendor phone 3
	[Phone4] VARCHAR(50) NULL, --Vendor phone 4
	[Email] VARCHAR(200) NULL, --Vendor email

    [VendorType] INT NULL DEFAULT 0, --Vendor type
    [VendorStatus] INT NULL DEFAULT 0, --Vendor status
	[BusinessType] VARCHAR(50) NULL,
	[PriceRule] VARCHAR(50) NULL,
	[FirstDate] DATE NOT NULL, --Vendor create date

	[Currency] VARCHAR(10) NULL,
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --Vendor default discount rate. 
	[ShippingCarrier] VARCHAR(50) NULL,
	[ShippingClass] VARCHAR(50) NULL,
	[ShippingAccount] VARCHAR(50) NULL,
	[Priority] VARCHAR(10) NULL,
	[Area] VARCHAR(20) NULL,
	[TaxId] VARCHAR(50) NULL,
	[ResaleLicense] VARCHAR(50) NULL,
	[ClassCode] VARCHAR(50) NULL,
	[DepartmentCode] VARCHAR(50) NULL,

	[CreditAccount] BIGINT NULL DEFAULT 0, --G/L Credit account
	[DebitAccount] BIGINT NULL DEFAULT 0, --G/L Debit account

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_Vendor] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Vendor]') AND name = N'UI_Vendor_VendorId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_Vendor_VendorUuid] ON [dbo].[Vendor]
(
	[VendorUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Vendor]') AND name = N'UI_Vendor_VendorCode')
CREATE UNIQUE NONCLUSTERED INDEX [UI_Vendor_VendorCode] ON [dbo].[Vendor]
(
	[VendorCode] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Vendor]') AND name = N'IX_Vendor_VendorID')
CREATE NONCLUSTERED INDEX [IX_Vendor_VendorName] ON [dbo].[Vendor]
(
	[VendorName] ASC
) ON [PRIMARY]
GO

