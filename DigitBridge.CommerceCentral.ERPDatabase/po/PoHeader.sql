CREATE TABLE [dbo].[PoHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,--(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [PoUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for P/O. <br> Display: false, Editable: false.
	[PoNum] VARCHAR(50) NOT NULL, --Unique in this database. <br> ProfileNum + PoNum is DigitBridgePoNum, which is global unique. <br> Title: PoNum, Display: true, Editable: true
    [PoType] INT NULL DEFAULT 0, --P/O type. <br> Title: Type, Display: true, Editable: true
    [PoStatus] INT NULL DEFAULT 0, --P/O status. <br> Title: Status, Display: true, Editable: true
	[PoDate] DATE NOT NULL, --P/O date. <br> Title: Date, Display: true, Editable: true
	[PoTime] TIME NOT NULL, --P/O time. <br> Title: Time, Display: true, Editable: true
	[EtaShipDate] DATE NULL, --Estimated vendor ship date. <br> Title: Ship Date, Display: true, Editable: true
	[EtaArrivalDate] DATE NULL, --Estimated date when item arrival to buyer . <br> Title: Arrival Date, Display: true, Editable: true
	[CancelDate] DATE NULL, --Usually it is related to shipping instruction. <br> Title: Cancel Date, Display: false, Editable: false

	[Terms] VARCHAR(50) NOT NULL DEFAULT '', --Payment terms, default from customer data. <br> Title: Terms, Display: true, Editable: true

    [VendorUuid] VARCHAR(50) NULL DEFAULT '', --reference Vendor Unique Guid. <br> Display: false, Editable: false
	[VendorCode] VARCHAR(50) NULL, --Vendor readable number.<br> DatabaseNum + VendorCode is DigitBridgeVendorCode, which is global unique. <br> Display: false, Editable: false
	[VendorName] NVARCHAR(200) NULL, --Vendor name. <br> Display: false, Editable: false

	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --Currency code. <br> Title: Currency, Display: true, Editable: true
	[SubTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sub total amount is sumary items amount. . <br> Title: Subtotal, Display: true, Editable: false
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation <br>(Sum of all items OrderItems[Quantity] x OrderItems[UnitPrice] ) + TotalTaxPrice + Total ShippingPrice + TotalInsurancePrice + TotalGiftOptionPrice + AdditionalCostOrDiscount +PromotionAmount + (Sum of all items OrderItems[Promotions[Amount]] + OrderItems[Promotions[ShippingAmount]] + OrderItems[RecyclingFee]). <br> Title: Order Amount, Display: true, Editable: false
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for P/O items. . <br> Title: Tax, Display: true, Editable: true
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total P/O tax amount (include shipping tax and misc tax) . <br> Title: Tax Amount, Display: true, Editable: false
	[TaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Amount should apply tax. <br> Title: Taxable Amount, Display: true, Editable: false
	[NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Amount should not apply tax. <br> Title: NonTaxable, Display: true, Editable: false
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount rate. <br> Title: Discount, Display: true, Editable: true
	[DiscountAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount amount, base on SubTotalAmount. <br> Title: Discount Amount, Display: true, Editable: true
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total shipping fee for all items. <br> Title: Shipping, Display: true, Editable: true
	[ShippingTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of shipping fee. <br> Title: Shipping Tax, Display: true, Editable: false
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O handling charge . <br> Title: Handling, Display: true, Editable: true 
	[MiscTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of handling charge. <br> Title: Handling Tax, Display: true, Editable: false
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O total Charg Allowance Amount. <br> Title: Charge&Allowance, Display: true, Editable: true

	[PoSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --P/O import or create from other entity number, use to prevent import duplicate P/O. <br> Title: Source Code, Display: false, Editable: false

    [EnterDateUtc] DATETIME NULL, --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_PoHeader] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoHeader]') AND name = N'UK_PoHeader_PoUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PoHeader_PoUuid] ON [dbo].[PoHeader]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoHeader]') AND name = N'UI_PoHeader_PoNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PoHeader_PoNum] ON [dbo].[PoHeader]
(
    [MasterAccountNum] ASC, 
    [ProfileNum] ASC, 
	[PoNum] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UI_PoHeader]') AND name = N'IX_PoHeader_VendorUuid')
CREATE NONCLUSTERED INDEX [IX_PoHeader_VendorUuid] ON [dbo].[PoHeader]
(
	[VendorUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UI_PoHeader]') AND name = N'IX_PoHeader_VendorCode')
CREATE NONCLUSTERED INDEX [IX_PoHeader_VendorCode] ON [dbo].[PoHeader]
(
    [MasterAccountNum] ASC, 
    [ProfileNum] ASC, 
	[VendorCode] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UI_PoHeader]') AND name = N'IX_PoHeader_PoSourceCode')
CREATE NONCLUSTERED INDEX [IX_PoHeader_PoSourceCode] ON [dbo].[PoHeader]
(
    [MasterAccountNum] ASC, 
    [ProfileNum] ASC, 
	[PoSourceCode] ASC
) ON [PRIMARY]
GO

