CREATE TABLE [dbo].[Customer]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [Digit_seller_id] VARCHAR(50) NOT NULL DEFAULT '', --Digit bridge seller_id. <br> Title: Digit Seller Id, Display: true, Editable: true.

    [CustomerUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Customer uuid. <br> Display: false, Editable: false.
	[CustomerCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable customer number, unique in same database and profile. <br> Parameter should pass ProfileNum-CustomerCode. <br> Title: Customer Number, Display: true, Editable: true
	[CustomerName] NVARCHAR(200) NOT NULL DEFAULT '', --Customer name. <br> Title: Name, Display: true, Editable: true
	[Contact] NVARCHAR(200) NOT NULL DEFAULT '', --Customer contact person. <br> Title: Contact, Display: true, Editable: true
	[Contact2] NVARCHAR(200) NOT NULL DEFAULT '', --Customer contact person 2. <br> Title: Contact 2, Display: true, Editable: true
	[Contact3] NVARCHAR(200) NOT NULL DEFAULT '', --Customer contact person 3. <br> Title: Contact 3, Display: true, Editable: true
	[Phone1] VARCHAR(50) NOT NULL DEFAULT '', --Customer phone 1. <br> Title: Phone, Display: true, Editable: true
	[Phone2] VARCHAR(50) NOT NULL DEFAULT '', --Customer phone 2. <br> Title: Phone 2, Display: true, Editable: true
	[Phone3] VARCHAR(50) NOT NULL DEFAULT '', --Customer phone 3. <br> Title: Phone 3, Display: true, Editable: true
	[Phone4] VARCHAR(50) NOT NULL DEFAULT '', --Customer phone 4. <br> Title: Fax, Display: true, Editable: true
	[Email] VARCHAR(200) NOT NULL DEFAULT '', --Customer email. <br> Title: Email, Display: true, Editable: true
	[WebSite] VARCHAR(200) NOT NULL DEFAULT '', --Customer WebSite. <br> Title: WebSite, Display: true, Editable: true
	[ChannelNum] INT NOT NULL DEFAULT 0, --(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] INT NOT NULL DEFAULT 0, --(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false

    [CustomerType] INT NULL DEFAULT 0, --Customer type. <br> Title: Type, Display: true, Editable: true
    [CustomerStatus] INT NULL DEFAULT 0, --Customer status. <br> Title: Status, Display: true, Editable: true
	[BusinessType] VARCHAR(50) NOT NULL DEFAULT '', --Customer business type. <br> Title: Business Type, Display: true, Editable: true
	[PriceRule] VARCHAR(50) NOT NULL DEFAULT '', --Customer default price rule. <br> Title: Price Rule, Display: true, Editable: true
	[FirstDate] DATE NOT NULL, --Customer create date. <br> Title: Since, Display: true, Editable: true

	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --Customer default Currency. <br> Title: Currency, Display: true, Editable: true
	[CreditLimit] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Customer Credit Limit. <br> Title: Credit Limit, Display: true, Editable: true
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate. <br> Title: Tax Rate, Display: true, Editable: true
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --Customer default discount rate. <br> Title: Discount Rate, Display: true, Editable: true
	[ShippingCarrier] VARCHAR(50) NOT NULL DEFAULT '', --Customer default ShippingCarrier. <br> Title: Shipping Carrier, Display: true, Editable: true
	[ShippingClass] VARCHAR(50) NOT NULL DEFAULT '', --Customer default ShippingClass. <br> Title: Shipping Method, Display: true, Editable: true
	[ShippingAccount] VARCHAR(50) NOT NULL DEFAULT '', --Customer default Shipping Account. <br> Title: Shipping Account, Display: true, Editable: true
	[Priority] VARCHAR(10) NOT NULL DEFAULT '', --Customer Priority. <br> Title: Priority, Display: true, Editable: true
	[Area] VARCHAR(20) NOT NULL DEFAULT '', --Customer Area. <br> Title: Area, Display: true, Editable: true
	[Region] VARCHAR(20) NOT NULL DEFAULT '', --Customer Region. <br> Title: Region, Display: true, Editable: true
	[Districtn] VARCHAR(20) NOT NULL DEFAULT '', --Customer Districtn. <br> Districtn: Area, Display: true, Editable: true
	[Zone] VARCHAR(20) NOT NULL DEFAULT '', --Customer Zone. <br> Title: Zone, Display: true, Editable: true
	[TaxId] VARCHAR(50) NOT NULL DEFAULT '', --Customer Tax Id. <br> Title: Tax Id, Display: true, Editable: true
	[ResaleLicense] VARCHAR(50) NOT NULL DEFAULT '', --Customer Resale License number. <br> Title: Resale License, Display: true, Editable: true
	[ClassCode] VARCHAR(50) NOT NULL DEFAULT '', --Customer Class. <br> Title: Class, Display: true, Editable: true
	[DepartmentCode] VARCHAR(50) NOT NULL DEFAULT '', --Customer Department. <br> Title: Department, Display: true, Editable: true
	[DivisionCode] VARCHAR(50) NOT NULL DEFAULT '', --Customer Division. <br> Title: Division, Display: true, Editable: true
	[SourceCode] VARCHAR(100) NOT NULL DEFAULT '', --Customer Source. <br> Title: Source, Display: true, Editable: true

	[Terms] VARCHAR(50) NOT NULL DEFAULT '', --Payment terms. <br> Title: Terms, Display: true, Editable: true
	[TermsDays] INT NOT NULL DEFAULT 0, --Payment terms days. <br> Title: Days, Display: true, Editable: true

    [SalesRep] Varchar(100) NOT NULL DEFAULT '', --Sales Rep Code <br> Title: Sales Rep 1, Display: true, Editable: true
    [SalesRep2] Varchar(100) NOT NULL DEFAULT '', --Sales Rep Code <br> Title: Sales Rep 2, Display: true, Editable: true
    [SalesRep3] Varchar(100) NOT NULL DEFAULT '', --Sales Rep Code <br> Title: Sales Rep 3, Display: true, Editable: true
    [SalesRep4] Varchar(100) NOT NULL DEFAULT '', --Sales Rep Code <br> Title: Sales Rep 4, Display: true, Editable: true
	[CommissionRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sales Rep Commission Rate, Title: Commission%, Display: true, Editable: true
	[CommissionRate2] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sales Rep Commission Rate, Title: Commission%, Display: true, Editable: true
	[CommissionRate3] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sales Rep Commission Rate, Title: Commission%, Display: true, Editable: true
	[CommissionRate4] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sales Rep Commission Rate, Title: Commission%, Display: true, Editable: true

	[OrderMiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Handlling fee by order, Title: Order Handling, Display: true, Editable: true
	[ItemMiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Handling fee by item SKU, Title: Item Handling, Display: true, Editable: true

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_Customer] PRIMARY KEY ([RowNum]), 
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'UK_Customer_CustomerUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_Customer_CustomerUuid] ON [dbo].[Customer]
(
	[CustomerUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'UI_Customer_CustomerCode')
CREATE UNIQUE NONCLUSTERED INDEX [UI_Customer_CustomerCode] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[CustomerCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_CustomerName')
CREATE NONCLUSTERED INDEX [IX_Customer_CustomerName] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[CustomerName] ASC
) 
GO

 
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_C_C_C_P_E_W')
CREATE NONCLUSTERED INDEX [IX_Customer_C_C_C_P_E_W] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[CustomerCode] ASC, 
	[CustomerName] ASC,
	[Contact] ASC,
	[Phone1] ASC,
	[Email] ASC,
	[WebSite] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_C_C_B_F_P')
CREATE NONCLUSTERED INDEX [IX_Customer_C_C_B_F_P] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[CustomerType] ASC, 
	[CustomerStatus] ASC,
	[BusinessType] ASC,
	[FirstDate] ASC,
	[Priority] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_A_R_D_Z')
CREATE NONCLUSTERED INDEX [IX_Customer_A_R_D_Z] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[Area] ASC, 
	[Region] ASC,
	[Districtn] ASC,
	[Zone] ASC
) 
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_C_D_D_S')
CREATE NONCLUSTERED INDEX [IX_Customer_C_D_D_S] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ClassCode] ASC, 
	[DepartmentCode] ASC,
	[DivisionCode] ASC,
	[SourceCode] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_ChannelNum_ChannelAccountNum')
CREATE NONCLUSTERED INDEX [IX_Customer_ChannelNum_ChannelAccountNum] ON [dbo].[Customer]
(
	[ChannelNum] ASC, 
	[ChannelAccountNum] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_MasterAccountNum_ChannelNum_ChannelAccountNum')
CREATE NONCLUSTERED INDEX [IX_Customer_MasterAccountNum_ChannelNum_ChannelAccountNum] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ChannelNum] ASC, 
	[ChannelAccountNum] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_SalesRep1234')
CREATE NONCLUSTERED INDEX [IX_Customer_SalesRep1234] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[SalesRep] ASC,
	[SalesRep2] ASC,
	[SalesRep3] ASC,
	[SalesRep4] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_SourceCode')
CREATE NONCLUSTERED INDEX [IX_Customer_SourceCode] ON [dbo].[Customer]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[SourceCode] ASC
) 
GO
