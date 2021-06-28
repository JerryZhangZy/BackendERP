CREATE TABLE [dbo].[ApInvoiceItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [ApInvoiceItemsId] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for ApInvoice Item Line

    [ApInvoiceId] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for ApInvoice
    [Seq] INT NOT NULL DEFAULT 0, --ApInvoice Item Line sort sequence
    [ApInvoiceItemType] INT NULL DEFAULT 0, --ApInvoice item type
    [ApInvoiceItemStatus] INT NULL DEFAULT 0, --ApInvoice item status
	[ItemDate] DATE NOT NULL, --ApInvoice date
	[ItemTime] TIME NOT NULL, --ApInvoice time

	[ApDistributionNum] Varchar(100) NOT NULL DEFAULT '',--A/P invoice pre define Distribution code
	[Description] NVarchar(200) NOT NULL,--ApInvoice item description 
	[Notes] NVarchar(500) NOT NULL,--ApInvoice item notes 

	[Currency] VARCHAR(10) NULL,
	[Amount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 
	[IsAp] TINYINT NOT NULL DEFAULT 1,--ApInvoice item will add to invoice total amount

	[CreditAccount] BIGINT NULL DEFAULT 0, --G/L Credit account
	[DebitAccount] BIGINT NULL DEFAULT 0, --G/L Debit account , A/P invoice distribution should specify G/L Debit account

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_ApInvoiceItems] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceItems]') AND name = N'UI_ApInvoiceItems_ApInvoiceItemsId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ApInvoiceItems_ApInvoiceItemsId] ON [dbo].[ApInvoiceItems]
(
	[ApInvoiceItemsId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceItems]') AND name = N'IX_ApInvoiceItems_ApInvoiceId_Seq')
CREATE NONCLUSTERED INDEX [IX_ApInvoiceItems_ApInvoiceId_Seq] ON [dbo].[ApInvoiceItems]
(
	[ApInvoiceId] ASC,
	[Seq] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceItems]') AND name = N'IX_ApInvoiceItems_ApInvoiceId')
CREATE NONCLUSTERED INDEX [IX_ApInvoiceItems_ApInvoiceId] ON [dbo].[ApInvoiceItems]
(
	[ApInvoiceId] ASC
) ON [PRIMARY]
GO



