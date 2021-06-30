CREATE TABLE [dbo].[Customer]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [Digit_seller_id] VARCHAR(50) NULL DEFAULT '', --Digit bridge seller_id

    [CustomerUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Customer
	[CustomerNum] VARCHAR(50) NULL, --Customer readable number, DatabaseNum + CustomerNum is DigitBridgeCustomerNum, which is global unique
	[CustomerName] NVARCHAR(200) NULL, --Customer name
	[Contact] NVARCHAR(200) NULL, --Customer contact person
	[Phone1] VARCHAR(50) NULL, --Customer phone 1
	[Phone2] VARCHAR(50) NULL, --Customer phone 2
	[Phone3] VARCHAR(50) NULL, --Customer phone 3
	[Phone4] VARCHAR(50) NULL, --Customer phone 4
	[Email] VARCHAR(200) NULL, --Customer email

    [CustomerType] INT NULL DEFAULT 0, --Customer type
    [CustomerStatus] INT NULL DEFAULT 0, --Customer status
	[BusinessType] VARCHAR(50) NULL,
	[PriceRule] VARCHAR(50) NULL,
	[FirstDate] DATE NOT NULL, --Customer create date

	[Currency] VARCHAR(10) NULL,
	[CreditLimit] DECIMAL(24, 6) NOT NULL DEFAULT 0,
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --Customer default discount rate
	[ShippingCarrier] VARCHAR(50) NULL,
	[ShippingClass] VARCHAR(50) NULL,
	[ShippingAccount] VARCHAR(50) NULL,
	[Priority] VARCHAR(10) NULL,
	[Area] VARCHAR(20) NULL,
	[TaxId] VARCHAR(50) NULL,
	[ResaleLicense] VARCHAR(50) NULL,
	[ClassCode] VARCHAR(50) NULL,
	[DepartmentCode] VARCHAR(50) NULL,

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_Customer] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'UI_Customer_CustomerId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_Customer_CustomerUuid] ON [dbo].[Customer]
(
	[CustomerUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'UI_Customer_CustomerNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_Customer_CustomerNum] ON [dbo].[Customer]
(
	[CustomerNum] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_CustomerID')
CREATE NONCLUSTERED INDEX [IX_Customer_CustomerName] ON [dbo].[Customer]
(
	[CustomerName] ASC
) ON [PRIMARY]
GO

