CREATE TABLE [dbo].[InvoiceTransaction]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [TransUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Invoice Transaction
    [TransNum] INT NOT NULL DEFAULT 1, --Transaction number

    [InvoiceUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Invoice
    [TransType] INT NOT NULL DEFAULT 0, --Transaction type, payment, return
    [TransStatus] INT NOT NULL DEFAULT 0, --Transaction status
	[TransDate] DATE NOT NULL, --Invoice date
	[TransTime] TIME NOT NULL, --Invoice time
    [Description] NVARCHAR(100) NOT NULL DEFAULT '', --Description of Invoice Transaction
    [Notes] NVARCHAR(500) NOT NULL DEFAULT '', --Notes of Invoice Transaction

	[PaidBy] INT NOT NULL DEFAULT 1, --Payment method number
	[BankAccountUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Bank account
	[CheckNum] VARCHAR(100) NOT NULL DEFAULT '', --Check number
	[AuthCode] VARCHAR(100) NOT NULL DEFAULT '', --Auth code from merchant bank

	[Currency] VARCHAR(10) NOT NULL DEFAULT '',
	[ExchangeRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Realtime Exchange Rate when process transaction
	[SubTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sub total amount is sumary items amount. 
	[SalesAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sub Total amount deduct discount, but not include other charge
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation 
	[TaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should apply tax
	[NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should not apply tax
	[TaxRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default Tax rate for P/O items. 
	[TaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total P/O tax amount (include shipping tax and misc tax) 
	[DiscountRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --P/O level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --P/O level discount amount, base on SubTotalAmount
	[ShippingAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --P/O handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --P/O total Charg Allowance Amount

	[CreditAccount] BIGINT NOT NULL DEFAULT 0, --G/L Credit account
	[DebitAccount] BIGINT NOT NULL DEFAULT 0, --G/L Debit account

    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL DEFAULT '',
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '',
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
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
	[InvoiceUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceTransaction]') AND name = N'IX_InvoiceTransaction_InvoiceNum')
CREATE NONCLUSTERED INDEX [IX_InvoiceTransaction_TransNum] ON [dbo].[InvoiceTransaction]
(
	[InvoiceUuid] ASC,
	[TransNum] ASC
) 
GO

