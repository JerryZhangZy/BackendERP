CREATE TABLE [dbo].[PoTransaction]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [TransId] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Invoice Transaction
    [TransNum] INT NOT NULL DEFAULT 1, --Transaction number

    [PoId] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O, '0' for multiple P/O
    [TransType] INT NULL DEFAULT 0, --P/O Transaction type (Receive, return, cancel)
    [TransStatus] INT NULL DEFAULT 0, --P/O Transaction status
	[TransDate] DATE NOT NULL, --Invoice date
	[TransTime] TIME NOT NULL, --Invoice time
    [Description] NVARCHAR(200) NULL DEFAULT '', --Description of Invoice Transaction
    [Notes] NVARCHAR(500) NULL DEFAULT '', --Notes of Invoice Transaction

    [VendorId] VARCHAR(50) NULL DEFAULT '', --reference Vendor Unique Guid
	[VendorInvoiceNum] VARCHAR(50) NOT NULL DEFAULT '', --Vendor Invoice number
	[VendorInvoiceDate] DATE NULL, --Vendor Invoice date
	[DueDate] DATE NULL, --Balance Due date

	[Currency] VARCHAR(10) NULL,
	[SubTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sub total amount is sumary items amount. 
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation 
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for P/O items. 
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total P/O tax amount (include shipping tax and misc tax) 
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount amount, base on [SubTotalAmount]
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O total Charg Allowance Amount

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_PoTransaction] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransaction]') AND name = N'UI_PoTransaction_PoId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PoTransaction_TransId] ON [dbo].[PoTransaction]
(
	[TransId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransaction]') AND name = N'IX_PoTransaction_InvoiceNum')
CREATE NONCLUSTERED INDEX [IX_PoTransaction_PoId] ON [dbo].[PoTransaction]
(
	[PoId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransaction]') AND name = N'IX_PoTransaction_InvoiceNum')
CREATE NONCLUSTERED INDEX [IX_PoTransaction_TransNum] ON [dbo].[PoTransaction]
(
	[PoId] ASC,
	[TransNum] ASC
) ON [PRIMARY]
GO

