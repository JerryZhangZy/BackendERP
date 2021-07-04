CREATE TABLE [dbo].[InvoiceHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [InvoiceUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Invoice
	[InvoiceNumber] VARCHAR(50) NOT NULL, --Unique in this database, ProfileNum + InvoiceNumber is DigitBridgeInvoiceNumber, which is global unique

    [InvoiceType] INT NOT NULL DEFAULT 0, --Invoice type
    [InvoiceStatus] INT NOT NULL DEFAULT 0, --Invoice status
	[InvoiceDate] DATE NOT NULL, --Invoice date
	[InvoiceTime] TIME NOT NULL, --Invoice time
	[DueDate] DATE NULL, --Balance Due date
	[BillDate] DATE NULL, --Next Billing date

	[CustomerUuid] VARCHAR(50) NOT NULL DEFAULT '', --Customer Guid
	[CustomerNum] VARCHAR(50) NOT NULL DEFAULT '', --Customer readable number, DatabaseNum + CustomerNum is DigitBridgeCustomerNum, which is global unique
	[CustomerName] NVARCHAR(200) NOT NULL DEFAULT '', --Customer name

	[Currency] VARCHAR(10) NOT NULL DEFAULT '',
	[SubTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sub total amount is sumary items amount. 
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation 
	[TaxRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default Tax rate for Invoice items. 
	[TaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Invoice tax amount (include shipping tax and misc tax) 
	[DiscountRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice level discount amount, base on SubTotalAmount
	[ShippingAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice total Charg Allowance Amount

	[PaidAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Paid amount 
	[CreditAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Credit amount 
	[Balance] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Current balance of invoice 

	[UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Unit Cost. 
	[AvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Avg.Cost. 
	[LotCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Lot Cost. 

    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL DEFAULT '',
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '',
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_InvoiceHeader] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'UK_InvoiceHeader_InvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceHeader] ON [dbo].[InvoiceHeader]
(
	[InvoiceUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'UI_InvoiceHeader_InvoiceNumber')
CREATE UNIQUE NONCLUSTERED INDEX [UI_InvoiceHeader_InvoiceNumber] ON [dbo].[InvoiceHeader]
(
	[ProfileNum] ASC,
	[InvoiceNumber] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_CustomerID')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_CustomerUuid] ON [dbo].[InvoiceHeader]
(
	[CustomerUuid] ASC
) ON [PRIMARY]
GO

