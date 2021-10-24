CREATE TABLE [dbo].[SalesOrderHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [SalesOrderUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Order uuid. <br> Display: false, Editable: false.
	[OrderNumber] VARCHAR(50) NOT NULL DEFAULT '', --Readable order number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true

    [OrderType] INT NOT NULL DEFAULT 0, --Order type. <br> Title: Type, Display: true, Editable: true
    [OrderStatus] INT NOT NULL DEFAULT 0, --Order status. <br> Title: Status, Display: true, Editable: true
	[OrderDate] DATE NOT NULL, --Order date. <br> Title: Date, Display: true, Editable: true
	[OrderTime] TIME NOT NULL, --Order time. <br> Title: Time, Display: true, Editable: true
	[ShipDate] DATE NULL, --Estimated vendor ship date. <br> Title: Ship Date, Display: true, Editable: true
	[DueDate] DATE NULL, --(Ignore) Order due date. <br> Display: false, Editable: false
	[BillDate] DATE NULL, --(Ignore) Order bill date. <br> Display: false, Editable: false

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
	[TaxRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order Tax rate. <br> Title: Tax, Display: true, Editable: true
	[TaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order tax amount (include shipping tax and misc tax). <br> Title: Tax Amount, Display: true, Editable: false
	[DiscountRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order discount rate base on SubTotalAmount. If user enter discount rate, should recalculate discount amount. <br> Title: Discount, Display: true, Editable: true
	[DiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order discount amount base on SubTotalAmount. If user enter discount amount, should set discount rate to zero. <br> Title: Discount Amount, Display: true, Editable: true
	[ShippingAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order shipping fee. <br> Title: Shipping, Display: true, Editable: true
	[ShippingTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) tax amount for shipping fee. <br> Title: Shipping Tax, Display: true, Editable: false
	[MiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order handling charge. <br> Title: Handling, Display: true, Editable: true 
	[MiscTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) tax amount for handling charge. <br> Title: Handling Tax, Display: true, Editable: false
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: true, Editable: true

	[PaidAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Total Paid amount. <br> Display: false, Editable: false
	[CreditAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Total Credit amount. <br> Display: false, Editable: false
	[Balance] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Current balance of Order. <br> Display: false, Editable: false

	[UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Total Unit Cost. <br> Display: false, Editable: false
	[AvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Total Avg.Cost. <br> Display: false, Editable: false
	[LotCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Total Lot Cost. <br> Display: false, Editable: false

	[OrderSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --(Readonly) Order created from other entity number, use to prevent import duplicate order. <br> Title: Source Number, Display: false, Editable: false

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Readonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_SalesOrderHeader] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'UK_SalesOrderHeader_OrderId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_SalesOrderHeader] ON [dbo].[SalesOrderHeader]
(
	[SalesOrderUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'UI_SalesOrderHeader_OrderNumber')
CREATE UNIQUE NONCLUSTERED INDEX [UI_SalesOrderHeader_OrderNumber] ON [dbo].[SalesOrderHeader]
(
	[ProfileNum] ASC,
	[OrderNumber] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_CustomerID')
CREATE NONCLUSTERED INDEX [FK_SalesOrderHeader_CustomerUuid] ON [dbo].[SalesOrderHeader]
(
	[CustomerUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_OrderSourceCode')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_OrderSourceCode] ON [dbo].[SalesOrderHeader]
(
	[OrderSourceCode] ASC
)  
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_OrderSourceCode')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_OrderDate] ON [dbo].[SalesOrderHeader]
(
	[OrderDate] ASC,
	[OrderTime] ASC
)  
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_OrderSourceCode')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_ShipDate] ON [dbo].[SalesOrderHeader]
(
	[ShipDate] ASC
)  
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_MasterAccountNum_ProfileNum')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_MasterAccountNum_ProfileNum] ON [dbo].[SalesOrderHeader]
(
	[MasterAccountNum] ASC, 
	[ProfileNum] ASC
)  
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeader]') AND name = N'IX_SalesOrderHeader_OrderSourceCode')
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeader_OrderType_OrderStatus] ON [dbo].[SalesOrderHeader]
(
	[OrderType] ASC,
	[OrderStatus] ASC
)  
GO

CREATE NONCLUSTERED INDEX [FK_SalesOrderHeader_CustomerUuid_CustomerCode] ON [dbo].[SalesOrderHeader]
(
	[CustomerUuid] ASC,
	[CustomerCode] ASC
) 
GO