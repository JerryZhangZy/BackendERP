CREATE TABLE [dbo].[ApInvoiceTransaction]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [TransId] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for ApInvoice Transaction
    [TransNum] INT NOT NULL DEFAULT 1, --Transaction number

    [ApInvoiceId] VARCHAR(50) NOT NULL, --Global Unique Guid for ApInvoice
    [TransType] INT NULL DEFAULT 0, --Transaction type
    [TransStatus] INT NULL DEFAULT 0, --Transaction status
	[TransDate] DATE NOT NULL, --Transaction date
	[TransTime] TIME NOT NULL, --Transaction time
    [Description] NVARCHAR(200) NULL DEFAULT '', --Description of ApInvoice Transaction
    [Notes] NVARCHAR(500) NULL DEFAULT '', --Notes of ApInvoice Transaction

	[PaidBy] INT NOT NULL DEFAULT 1, --Payment method number
	[BankAccountId] VARCHAR(50) NULL, --Global Unique Guid for Bank account
	[CheckNum] VARCHAR(100) NULL, --Check number
	[AuthCode] VARCHAR(100) NULL, --Auth code from merchant bank

	[Currency] VARCHAR(10) NULL,
	[ExchangeRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Realtime Exchange Rate when process transaction
	[Amount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation 

	[CreditAccount] BIGINT NULL DEFAULT 0, --G/L Credit account
	[DebitAccount] BIGINT NULL DEFAULT 0, --G/L Debit account

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_ApInvoiceTransaction] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceTransaction]') AND name = N'UI_ApInvoiceTransaction_ApInvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ApInvoiceTransaction_TransId] ON [dbo].[ApInvoiceTransaction]
(
	[TransId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceTransaction]') AND name = N'UI_ApInvoiceTransaction_ApInvoiceNum')
CREATE NONCLUSTERED INDEX [IX_ApInvoiceTransaction_ApInvoiceId] ON [dbo].[ApInvoiceTransaction]
(
	[ApInvoiceId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceTransaction]') AND name = N'UI_ApInvoiceTransaction_TransNum')
CREATE NONCLUSTERED INDEX [IX_ApInvoiceTransaction_TransNum] ON [dbo].[ApInvoiceTransaction]
(
	[ApInvoiceId] ASC,
	[TransNum] ASC
) ON [PRIMARY]
GO

