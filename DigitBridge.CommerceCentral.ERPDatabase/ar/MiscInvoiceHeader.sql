CREATE TABLE [dbo].[MiscInvoiceHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [MiscInvoiceUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Misc.Invoice uuid. <br> Display: false, Editable: false.
	[MiscInvoiceNumber] VARCHAR(50) NOT NULL DEFAULT '', --Readable Misc. invoice number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true
	[QboDocNumber] VARCHAR(50) NOT NULL DEFAULT '', --Readable QboDocNumber, when push record to quickbook update number. <br> when push record to quickbook update number.

    [MiscInvoiceType] INT NOT NULL DEFAULT 0, --Invoice type. <br> Title: Type, Display: true, Editable: true
    [MiscInvoiceStatus] INT NOT NULL DEFAULT 0, --Invoice status. <br> Title: Status, Display: true, Editable: true
	[MiscInvoiceDate] DATE NOT NULL, --Invoice date. <br> Title: Date, Display: true, Editable: true
	[MiscInvoiceTime] TIME NOT NULL, --Invoice time. <br> Title: Time, Display: true, Editable: true

	[CustomerUuid] VARCHAR(50) NOT NULL, --Customer uuid, load from customer data. <br> Display: false, Editable: false
	[CustomerCode] VARCHAR(50) NOT NULL DEFAULT '', --Customer number. use DatabaseNum-CustomerCode too load customer data. <br> Title: Customer Number, Display: true, Editable: true
	[CustomerName] NVARCHAR(200) NOT NULL DEFAULT '', --(Readonly) Customer name, load from customer data. <br> Title: Customer Name, Display: true, Editable: false

    [Notes] NVARCHAR(500) NOT NULL DEFAULT '', --Notes of Invoice Transaction. <br> Title: Notes, Display: true, Editable: true

	[PaidBy] INT NOT NULL DEFAULT 1, --Payment method number. <br> Title: Paid By, Display: true, Editable: true
	[BankAccountUuid] VARCHAR(50) NOT NULL DEFAULT '', --Payment bank account uuid. 
	[BankAccountCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable payment Bank account code. <br> Title: Bank, Display: true, Editable: true
	[CheckNum] VARCHAR(100) NOT NULL DEFAULT '', --Check number. <br> Title: Check No., Display: true, Editable: true
	[AuthCode] VARCHAR(100) NOT NULL DEFAULT '', --Auth code from merchant bank. <br> Title: Auth. No., Display: true, Editable: true

	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --Currency code. <br> Title: Currency, Display: true, Editable: true
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Total amount. Include every charge (tax, shipping, misc...). <br> Title: Total, Display: true, Editable: false
	[PaidAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Paid amount. <br> Display: true, Editable: false
	[CreditAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Credit amount. <br> Display: true, Editable: false
	[Balance] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Current balance of Invoice. <br> Display: true, Editable: false

	[InvoiceSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --(Readonly) Invoice created from other entity number, use to prevent import duplicate invoice. <br> Title: Source Number, Display: false, Editable: false

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_MiscInvoiceHeader] PRIMARY KEY ([RowNum]), 
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[MiscInvoiceHeader]') AND name = N'UK_MiscInvoiceHeader')
CREATE UNIQUE NONCLUSTERED INDEX [UK_MiscInvoiceHeader] ON [dbo].[MiscInvoiceHeader]
(
	[MiscInvoiceUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[MiscInvoiceHeader]') AND name = N'UI_MiscInvoiceHeader_MiscInvoiceNumber')
CREATE UNIQUE NONCLUSTERED INDEX [UI_MiscInvoiceHeader_MiscInvoiceNumber] ON [dbo].[MiscInvoiceHeader]
(
	[ProfileNum] ASC,
	[MiscInvoiceNumber] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[MiscInvoiceHeader]') AND name = N'IX_MiscInvoiceHeader_CustomerUuid')
CREATE NONCLUSTERED INDEX [IX_MiscInvoiceHeader_CustomerUuid] ON [dbo].[MiscInvoiceHeader]
(
	[CustomerUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[MiscInvoiceHeader]') AND name = N'IX_MiscInvoiceHeader_InvoiceSourceCode')
CREATE NONCLUSTERED INDEX [IX_MiscInvoiceHeader_InvoiceSourceCode] ON [dbo].[MiscInvoiceHeader]
(
	[ProfileNum] ASC,
	[InvoiceSourceCode] ASC
) 
GO
