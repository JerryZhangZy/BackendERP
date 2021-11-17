CREATE TABLE [dbo].[MiscInvoiceTransaction]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [TransUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Invoice Transaction uuid. <br> Display: false, Editable: false.
    [TransNum] INT NOT NULL DEFAULT 1, --Readable invoice transaction number, unique in same database and profile. <br> Parameter should pass ProfileNum-InvoiceNumber-TransNum. <br> Title: Order Number, Display: true, Editable: true

    [MiscInvoiceUuid] VARCHAR(50) NOT NULL, --Invoice uuid. <br> Display: false, Editable: false.
	[MiscInvoiceNumber] VARCHAR(50) NOT NULL DEFAULT '', --Readable invoice number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true

    [TransType] INT NOT NULL DEFAULT 0, --Transaction type, payment, return. <br> Title: Type, Display: true, Editable: true
    [TransStatus] INT NOT NULL DEFAULT 0, --Transaction status. <br> Title: Status, Display: true, Editable: true
	[TransDate] DATE NOT NULL, --Invoice date. <br> Title: Date, Display: true, Editable: true
	[TransTime] TIME NOT NULL, --Invoice time. <br> Title: Time, Display: true, Editable: true
    [Description] NVARCHAR(100) NOT NULL DEFAULT '', --Description of Invoice Transaction. <br> Title: Description, Display: true, Editable: true
    [Notes] NVARCHAR(500) NOT NULL DEFAULT '', --Notes of Invoice Transaction. <br> Title: Notes, Display: true, Editable: true

	[PaidBy] INT NOT NULL DEFAULT 1, --Payment method number. <br> Title: Paid By, Display: true, Editable: true
	[BankAccountUuid] VARCHAR(50) NOT NULL DEFAULT '', --Payment bank account uuid. 
	[BankAccountCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable payment Bank account code. <br> Title: Bank, Display: true, Editable: true
	[CheckNum] VARCHAR(100) NOT NULL DEFAULT '', --Check number. <br> Title: Check No., Display: true, Editable: true
	[AuthCode] VARCHAR(100) NOT NULL DEFAULT '', --Auth code from merchant bank. <br> Title: Auth. No., Display: true, Editable: true

	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --Currency code. <br> Title: Currency, Display: true, Editable: true
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Total amount. Include every charge (tax, shipping, misc...). <br> Title: Total, Display: true, Editable: false

	[CreditAccount] BIGINT NOT NULL DEFAULT 0, --G/L Credit account. <br> Display: true, Editable: true
	[DebitAccount] BIGINT NOT NULL DEFAULT 0, --G/L Debit account. <br> Display: true, Editable: true

	[TransSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --(Readonly) Invoice transaction created from other entity number, use to prevent import duplicate invoice transaction. <br> Title: Source Number, Display: false, Editable: false

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_MiscInvoiceTransaction] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[MiscInvoiceTransaction]') AND name = N'UK_MiscInvoiceTransaction_TransId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_MiscInvoiceTransaction_TransUuid] ON [dbo].[MiscInvoiceTransaction]
(
	[TransUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[MiscInvoiceTransaction]') AND name = N'FK_MiscInvoiceTransaction_InvoiceId')
CREATE NONCLUSTERED INDEX [FK_MiscInvoiceTransaction_MiscInvoiceUuid] ON [dbo].[MiscInvoiceTransaction]
(
	[MiscInvoiceUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[MiscInvoiceTransaction]') AND name = N'UI_MiscInvoiceTransaction_TransNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_MiscInvoiceTransaction_TransNum] ON [dbo].[MiscInvoiceTransaction]
(
	[ProfileNum] ASC,
	[MiscInvoiceNumber] ASC,
	[TransNum] ASC
) 
GO

CREATE NONCLUSTERED INDEX [IK_MiscInvoiceTransaction_AuthCode] ON [dbo].[MiscInvoiceTransaction]
(
	[AuthCode] ASC
) 
GO
