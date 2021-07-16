CREATE TABLE [dbo].[SalesOrderHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [SalesOrderUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Order
	[OrderNumber] VARCHAR(50) NOT NULL DEFAULT '', --Unique in this database, ProfileNum + OrderNumber is DigitBridgeOrderNumber, which is global unique

    [OrderType] INT NOT NULL DEFAULT 0, --Order type
    [OrderStatus] INT NOT NULL DEFAULT 0, --Order status
	[OrderDate] DATE NOT NULL, --Order date
	[OrderTime] TIME NOT NULL, --Order time
	[DueDate] DATE NULL, --Balance Due date
	[BillDate] DATE NULL, --Next Billing date

	[CustomerUuid] VARCHAR(50) NOT NULL, --Customer Guid
	[CustomerNum] VARCHAR(50) NOT NULL DEFAULT '', --Customer readable number, DatabaseNum + CustomerNum is DigitBridgeCustomerNum, which is global unique
	[CustomerName] NVARCHAR(200) NOT NULL DEFAULT '', --Customer name

	[Terms] VARCHAR(50) NOT NULL DEFAULT '', --Payment terms
	[TermsDays] INT NOT NULL DEFAULT 0, --Payment terms

	[Currency] VARCHAR(10) NOT NULL DEFAULT '',
	[SubTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sub total amount is sumary items amount. 
	[SalesAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sub Total amount deduct discount, but not include other charge
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total amount. Include every charge (tax, shipping, misc...). 
	[TaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should apply tax
	[NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should not apply tax
	[TaxRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default Tax rate for Order items. 
	[TaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Order tax amount (include shipping tax and misc tax) 
	[DiscountRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order level discount amount, base on SubTotalAmount
	[ShippingAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Order total Charg Allowance Amount

	[PaidAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Paid amount 
	[CreditAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Credit amount 
	[Balance] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Current balance of Order 

	[UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Unit Cost. 
	[AvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Avg.Cost. 
	[LotCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Lot Cost. 

	[OrderSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --Order import or create from other entity number, use to prevent import duplicate order

    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL DEFAULT '',
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '',
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
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
