CREATE TABLE [dbo].[ApInvoiceHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [ApInvoiceId] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for ApInvoice
	[ApInvoiceNum] VARCHAR(50) NOT NULL, --Unique in this database, ProfileNum + ApInvoiceNum is DigitBridgeApInvoiceNum, which is global unique

    [ApInvoiceType] INT NULL DEFAULT 0, -- A/P Invoice type
    [ApInvoiceStatus] INT NULL DEFAULT 0, -- A/P Invoice status
	[ApInvoiceDate] DATE NOT NULL, --A/P Invoice date
	[ApInvoiceTime] TIME NOT NULL, --A/P Invoice time

    [VendorId] VARCHAR(50) NULL DEFAULT '', --reference Vendor Unique Guid
	[VendorNum] VARCHAR(50) NULL, --Vendor readable number, DatabaseNum + VendorNum is DigitBridgeVendorNum, which is global unique
	[VendorName] NVARCHAR(100) NULL, --Vendor name
	[VendorInvoiceNum] VARCHAR(50) NOT NULL DEFAULT '', --Vendor Invoice number
	[VendorInvoiceDate] DATE NULL, --Vendor Invoice date
	[DueDate] DATE NULL, --Balance Due date
	[BillDate] DATE NULL, --Next Billing date

	[Currency] VARCHAR(10) NULL,
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total A/P invoice amount. 
	[PaidAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total Paid amount 
	[CreditAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total Credit amount 
	[Balance] DECIMAL(24, 6) NULL DEFAULT 0, --Current balance of invoice 

	[CreditAccount] BIGINT NULL DEFAULT 0, --G/L Credit account, A/P invoice total should specify G/L Credit account
	[DebitAccount] BIGINT NULL DEFAULT 0, --G/L Debit account 

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_ApInvoiceHeader] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceHeader]') AND name = N'UI_ApInvoiceHeader_ApInvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ApInvoiceHeader_ApInvoiceId] ON [dbo].[ApInvoiceHeader]
(
	[ApInvoiceId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceHeader]') AND name = N'UI_ApInvoiceHeader_ApInvoiceNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ApInvoiceHeader_ApInvoiceNum] ON [dbo].[ApInvoiceHeader]
(
	[ApInvoiceNum] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UI_ApInvoiceHeader]') AND name = N'UI_ApInvoiceHeader_ApInvoiceNum')
CREATE NONCLUSTERED INDEX [UI_ApInvoiceHeader_VendorID] ON [dbo].[ApInvoiceHeader]
(
	[VendorID] ASC
) ON [PRIMARY]
GO


