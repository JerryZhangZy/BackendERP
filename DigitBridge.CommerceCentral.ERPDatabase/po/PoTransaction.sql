CREATE TABLE [dbo].[PoTransaction]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,--(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [TransUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for purchase order Transaction
    [TransNum] INT NOT NULL DEFAULT 1, --Transaction number

    [PoUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O, '0' for multiple P/O
	[PoNum] VARCHAR(50) NOT NULL DEFAULT '', --Readable invoice number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true

    [TransType] INT NULL DEFAULT 0, --P/O Transaction type (Receive, return, cancel)
    [TransStatus] INT NULL DEFAULT 0, --P/O Transaction status, new, close
	[TransDate] DATE NOT NULL, --Transaction date
	[TransTime] TIME NOT NULL, --Transaction time
    [Description] NVARCHAR(200) NULL DEFAULT '', --Description of purchase order Transaction
    [Notes] NVARCHAR(500) NULL DEFAULT '', --Notes of Invoice Transaction

    [VendorUuid] VARCHAR(50) NULL DEFAULT '', --reference Vendor Unique Guid
	[VendorCode] VARCHAR(50) NULL, --Vendor readable number, DatabaseNum + VendorCode is DigitBridgeVendorCode, which is global unique
	[VendorName] NVARCHAR(200) NULL, --Vendor name
	[VendorInvoiceNum] VARCHAR(50) NOT NULL DEFAULT '', --Vendor Invoice number
	[VendorInvoiceDate] DATE NULL, --Vendor Invoice date
	[DueDate] DATE NULL, --Balance Due date

	[Currency] VARCHAR(10) NULL,
	[SubTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sub total amount is sumary items amount. 
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation 
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for P/O items. 
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total P/O tax amount (include shipping tax and misc tax) 
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount amount, base on SubTotalAmount
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O total Charg Allowance Amount

    [EnterDateUtc] DATETIME NULL,--(Readonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [UpdateDateUtc] DATETIME NULL,--(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL,--(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL,--(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_PoTransaction] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransaction]') AND name = N'UI_PoTransaction_PoId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PoTransaction_TransUuid] ON [dbo].[PoTransaction]
(
	[TransUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransaction]') AND name = N'IX_PoTransaction_InvoiceNum')
CREATE NONCLUSTERED INDEX [FK_PoTransaction_PoUuid] ON [dbo].[PoTransaction]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransaction]') AND name = N'IX_PoTransaction_InvoiceNum')
CREATE NONCLUSTERED INDEX [IX_PoTransaction_TransNum] ON [dbo].[PoTransaction]
(
	[PoUuid] ASC,
	[TransNum] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransaction]') AND name = N'UI_PoTransaction_ProfileNum_PoNum_TransNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PoTransaction_ProfileNum_PoNum_TransNum] ON [dbo].[PoTransaction]
(
	[ProfileNum] ASC,
	[PoNum] ASC,
	[TransNum] ASC
) 
GO

