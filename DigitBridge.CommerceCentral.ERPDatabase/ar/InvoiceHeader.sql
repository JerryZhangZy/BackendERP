CREATE TABLE [dbo].[InvoiceHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [InvoiceUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Invoice uuid. <br> Display: false, Editable: false.
	[InvoiceNumber] VARCHAR(50) NOT NULL DEFAULT '', --Readable invoice number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true

    [InvoiceType] INT NOT NULL DEFAULT 0, --Invoice type. <br> Title: Type, Display: true, Editable: true
    [InvoiceStatus] INT NOT NULL DEFAULT 0, --Invoice status. <br> Title: Status, Display: true, Editable: true
	[InvoiceDate] DATE NOT NULL, --Invoice date. <br> Title: Date, Display: true, Editable: true
	[InvoiceTime] TIME NOT NULL, --Invoice time. <br> Title: Time, Display: true, Editable: true
	[DueDate] DATE NULL, --Balance Due date. <br> Title: Date, Display: true, Editable: false
	[BillDate] DATE NULL, --(Ignore) Next Billing date.

	[CustomerUuid] VARCHAR(50) NOT NULL, --Customer uuid, load from customer data. <br> Display: false, Editable: false
	[CustomerCode] VARCHAR(50) NOT NULL DEFAULT '', --Customer number. use DatabaseNum-CustomerCode too load customer data. <br> Title: Customer Number, Display: true, Editable: true
	[CustomerName] NVARCHAR(200) NOT NULL DEFAULT '', --(Readonly) Customer name, load from customer data. <br> Title: Customer Name, Display: true, Editable: false

	[Terms] VARCHAR(50) NOT NULL DEFAULT '', --Payment terms, default from customer data. <br> Title: Terms, Display: true, Editable: true
	[TermsDays] INT NOT NULL DEFAULT 0, --Payment terms days, default from customer data. <br> Title: Days, Display: true, Editable: true

	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --Currency code. <br> Title: Currency, Display: true, Editable: true
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

	[PaidAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Paid amount. <br> Display: true, Editable: false
	[CreditAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Credit amount. <br> Display: true, Editable: false
	[Balance] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Current balance of Invoice. <br> Display: true, Editable: false

	[UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Total Unit Cost. <br> Display: false, Editable: false
	[AvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Total Avg.Cost. <br> Display: false, Editable: false
	[LotCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Total Lot Cost. <br> Display: false, Editable: false

	[InvoiceSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --(Readonly) Invoice created from other entity number, use to prevent import duplicate invoice. <br> Title: Source Number, Display: false, Editable: false

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_InvoiceHeader] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'UK_InvoiceHeader_InvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceHeader] ON [dbo].[InvoiceHeader]
(
	[InvoiceUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'UI_InvoiceHeader_InvoiceNumber')
CREATE UNIQUE NONCLUSTERED INDEX [UI_InvoiceHeader_InvoiceNumber] ON [dbo].[InvoiceHeader]
(
	[ProfileNum] ASC,
	[InvoiceNumber] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_CustomerID')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_CustomerUuid] ON [dbo].[InvoiceHeader]
(
	[CustomerUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_InvoiceSourceCode')
CREATE NONCLUSTERED INDEX [IX_InvoiceHeader_InvoiceSourceCode] ON [dbo].[InvoiceHeader]
(
	[InvoiceSourceCode] ASC
) 
GO
