CREATE TABLE [dbo].[OrderHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [OrderUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Order
	[OrderNumber] VARCHAR(50) NOT NULL, --Unique in this database, ProfileNum + OrderNumber is DigitBridgeOrderNumber, which is global unique

    [OrderType] INT NULL DEFAULT 0, --Order type
    [OrderStatus] INT NULL DEFAULT 0, --Order status
	[OrderDate] DATE NOT NULL, --Order date
	[OrderTime] TIME NOT NULL, --Order time
	[DueDate] DATE NULL, --Balance Due date
	[BillDate] DATE NULL, --Next Billing date

	[CustomerUuid] VARCHAR(50) NULL, --Customer Guid
	[CustomerNum] VARCHAR(50) NULL, --Customer readable number, DatabaseNum + CustomerNum is DigitBridgeCustomerNum, which is global unique
	[CustomerName] NVARCHAR(200) NULL, --Customer name

	[Currency] VARCHAR(10) NULL,
	[SubTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sub total amount is sumary items amount. 
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation 
									--(Sum of all items OrderItems Quantity x OrderItems UnitPrice ) + TotalTaxPrice + Total ShippingPrice + TotalInsurancePrice + TotalGiftOptionPrice + AdditionalCostOrDiscount +PromotionAmount + (Sum of all items OrderItems Promotions Amount + OrderItems Promotions ShippingAmount + OrderItems RecyclingFee)
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for Order items. 
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total Order tax amount (include shipping tax and misc tax) 
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --Order level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Order level discount amount, base on SubTotalAmount
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Order handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Order total Charg Allowance Amount

	[PaidAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total Paid amount 
	[CreditAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total Credit amount 
	[Balance] DECIMAL(24, 6) NULL DEFAULT 0, --Current balance of Order 

	[UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Unit Cost. 
	[AvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Avg.Cost. 
	[LotCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Lot Cost. 

	[OrderSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --Order import or create from other entity number, use to prevent import duplicate order

    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_OrderHeader] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderHeader]') AND name = N'UK_OrderHeader_OrderId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderHeader] ON [dbo].[OrderHeader]
(
	[OrderUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderHeader]') AND name = N'UI_OrderHeader_OrderNumber')
CREATE UNIQUE NONCLUSTERED INDEX [UI_OrderHeader_OrderNumber] ON [dbo].[OrderHeader]
(
	[ProfileNum] ASC,
	[OrderNumber] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderHeader]') AND name = N'IX_OrderHeader_CustomerID')
CREATE NONCLUSTERED INDEX [IX_OrderHeader_CustomerUuid] ON [dbo].[OrderHeader]
(
	[CustomerUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderHeader]') AND name = N'IX_OrderHeader_OrderSourceCode')
CREATE NONCLUSTERED INDEX [IX_OrderHeader_OrderSourceCode] ON [dbo].[OrderHeader]
(
	[OrderSourceCode] ASC
) ON [PRIMARY]
GO
