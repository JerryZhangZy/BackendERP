CREATE TABLE [dbo].[ApInvoiceHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,--(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL,--(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL,--(Readonly) Login user profile. <br> Display: false, Editable: false.
    [ApInvoiceUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) ApInvoice uuid. <br> Display: false, Editable: false
	[ApInvoiceNum] VARCHAR(50) NOT NULL, --Unique in this database, ProfileNum + ApInvoiceNum is DigitBridgeApInvoiceNum, which is global unique
	[PoUuid] VARCHAR(50) NOT NULL DEFAULT '', --Link to PoHeader uuid. <br> Display: false, Editable: false.
	[PoNum] VARCHAR(50) NOT NULL DEFAULT '', --Link to PoHeader number, unique in same database and profile. <br> Title: PoHeader Number, Display: true, Editable: false
    [ApInvoiceType] INT NULL DEFAULT 0, -- A/P Invoice type
    [ApInvoiceStatus] INT NULL DEFAULT 0, -- A/P Invoice status
	[ApInvoiceDate] DATE NOT NULL, --A/P Invoice date
	[ApInvoiceTime] TIME NOT NULL, --A/P Invoice time

    [VendorUuid] VARCHAR(50) NULL DEFAULT '', --reference Vendor Unique Guid
	[VendorNum] VARCHAR(50) NULL, --Vendor readable number, DatabaseNum + VendorNum is DigitBridgeVendorNum, which is global unique
	[VendorName] NVARCHAR(100) NULL, --Vendor name
	[VendorInvoiceNum] VARCHAR(50) NOT NULL DEFAULT '', --Vendor Invoice number
	[VendorInvoiceDate] DATE NULL, --Vendor Invoice date
	[DueDate] DATE NULL, --Balance Due date
	[BillDate] DATE NULL, --Next Billing date

	[Currency] VARCHAR(10) NULL,--(Ignore) ApInvoice price in currency. <br> Title: Currency, Display: false, Editable: false
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total A/P invoice amount. 
	[PaidAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total Paid amount 
	[CreditAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total Credit amount 
	[Balance] DECIMAL(24, 6) NULL DEFAULT 0, --Current balance of invoice 

	[CreditAccount] BIGINT NULL DEFAULT 0, --G/L Credit account, A/P invoice total should specify G/L Credit account
	[DebitAccount] BIGINT NULL DEFAULT 0, --G/L Debit account 

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [UpdateDateUtc] DATETIME NULL,--(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL, --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_ApInvoiceHeader] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceHeader]') AND name = N'UI_ApInvoiceHeader_ApInvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_ApInvoiceHeader_ApInvoiceUuid] ON [dbo].[ApInvoiceHeader]
(
	[ApInvoiceUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceHeader]') AND name = N'UI_ApInvoiceHeader_ApInvoiceNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ApInvoiceHeader_ApInvoiceNum] ON [dbo].[ApInvoiceHeader]
(
	[ApInvoiceNum] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UI_ApInvoiceHeader]') AND name = N'UI_ApInvoiceHeader_ApInvoiceNum')
CREATE NONCLUSTERED INDEX [UI_ApInvoiceHeader_VendorUuid] ON [dbo].[ApInvoiceHeader]
(
	[VendorUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_PoUuid')
CREATE NONCLUSTERED INDEX [IX_ApInvoiceHeader_PoUuid] ON [dbo].[ApInvoiceHeader]
(
	[PoUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_PoNum')
CREATE NONCLUSTERED INDEX [IX_ApInvoiceHeader_PoNum] ON [dbo].[ApInvoiceHeader]
(
	[PoNum] ASC
) 
GO
