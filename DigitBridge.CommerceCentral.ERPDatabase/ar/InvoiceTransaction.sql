CREATE TABLE [dbo].[InvoiceTransaction]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [TransUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Invoice Transaction uuid. <br> Display: false, Editable: false.
    [TransNum] INT NOT NULL DEFAULT 1, --Readable invoice transaction number, unique in same database and profile. <br> Parameter should pass ProfileNum-InvoiceNumber-TransNum. <br> Title: Order Number, Display: true, Editable: true

    [InvoiceUuid] VARCHAR(50) NOT NULL, --Invoice uuid. <br> Display: false, Editable: false.
	[InvoiceNumber] VARCHAR(50) NOT NULL DEFAULT '', --Readable invoice number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true

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
	[ExchangeRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Realtime Exchange Rate when process transaction. <br> Title: Exchange Rate, Display: true, Editable: true
	[SubTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Sub total amount of items. Sales amount without discount, tax and other charge. <br> Title: Subtotal, Display: true, Editable: false
	[SalesAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Sub Total amount deduct discount, but not include tax and other charge. <br> Title: Sales Amount, Display: true, Editable: false
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Total amount. Include every charge (tax, shipping, misc...). <br> Title: Total, Display: true, Editable: false
	[TaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Amount should apply tax. <br> Title: Taxable Amount, Display: true, Editable: false
	[NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Amount should not apply tax. <br> Title: NonTaxable, Display: true, Editable: false
	[TaxRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice Tax rate. <br> Title: Tax, Display: true, Editable: true
	[TaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice tax amount (include shipping tax and misc tax). <br> Title: Tax Amount, Display: true, Editable: false
	[DiscountRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice discount rate base on SubTotalAmount. If user enter discount rate, should recalculate discount amount. <br> Title: Discount, Display: true, Editable: true
	[DiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice discount amount base on SubTotalAmount. If user enter discount amount, should set discount rate to zero. <br> Title: Discount Amount, Display: true, Editable: true
	[ShippingAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice shipping fee. <br> Title: Shipping, Display: true, Editable: true
	[ShippingTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) tax amount for shipping fee. <br> Title: Shipping Tax, Display: true, Editable: false
	[MiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice handling charge. <br> Title: Handling, Display: true, Editable: true 
	[MiscTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) tax amount for handling charge. <br> Title: Handling Tax, Display: true, Editable: false
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: true, Editable: true

	[CreditAccount] BIGINT NOT NULL DEFAULT 0, --G/L Credit account. <br> Display: true, Editable: true
	[DebitAccount] BIGINT NOT NULL DEFAULT 0, --G/L Debit account. <br> Display: true, Editable: true

	[TransSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --(Readonly) Invoice transaction created from other entity number, use to prevent import duplicate invoice transaction. <br> Title: Source Number, Display: false, Editable: false

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_InvoiceTransaction] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceTransaction]') AND name = N'UK_InvoiceTransaction_TransId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceTransaction_TransUuid] ON [dbo].[InvoiceTransaction]
(
	[TransUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceTransaction]') AND name = N'FK_InvoiceTransaction_InvoiceId')
CREATE NONCLUSTERED INDEX [FK_InvoiceTransaction_InvoiceUuid] ON [dbo].[InvoiceTransaction]
(
	[InvoiceUuid] ASC,
	[TransNum] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceTransaction]') AND name = N'UI_InvoiceTransaction_TransNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_InvoiceTransaction_TransNum] ON [dbo].[InvoiceTransaction]
(
	[ProfileNum] ASC,
	[InvoiceNumber] ASC,
	[TransNum] ASC
) 
GO
