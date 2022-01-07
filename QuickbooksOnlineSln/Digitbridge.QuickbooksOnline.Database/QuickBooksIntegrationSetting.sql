CREATE TABLE [dbo].[QuickBooksIntegrationSetting]
(
	IntegrationSettingNum bigint NOT NULL IDENTITY(1000000, 1),
	MasterAccountNum int NOT NULL,
	ProfileNum int NOT NULL,
	ExportByOrderStatus int NOT NULL DEFAULT 0, -- 0: All, 1: Shipped
	ExportOrderAs int NOT NULL DEFAULT 4, -- 0: Invoice, 1: Sales Receipt, 2: Daily Summary Sales Receipt, 3: Daily Summary Invoice, 4. Do Not Export Sales Order
	ExportOrderDateType int NOT NULL DEFAULT 0, -- 0: By Date Exported, 1: By Payment Date 
	ExportOrderFromDate datetime NOT NULL DEFAULT getutcdate(),
	ExportOrderToDate datetime NULL ,
	--LastestOrderImportDate datetime NULL,
	CONSTRAINT check_dates check (ExportOrderFromDate <= ExportOrderToDate),
	DailySummaryOrderDateType int NOT NULL DEFAULT 0, -- 0: By Date Exported, 1: By Shipping Date, 1: By Payment Date
	QboCustomerCreateRule int NOT NULL DEFAULT 0, -- 0: Per Marketplace, 1: Per Order
	QboItemCreateRule int NOT NULL DEFAULT 0, -- 0: Default Item, 1: Skip Order if is not in QBO, 2. Create New Item if is not in QBO. Item handling option while creating Invoice/Sales Receipt in Qbo when matching item not found.
	SalesTaxExportRule int NOT NULL DEFAULT 0, -- 0: Export To default QboSalesTaxAcc, 1: Do not export sales tax
	QboEndCustomerPoNumCustFieldName NVARCHAR(150) Null, -- Customized Field Name for EndCustomerPoNum in Qbo Invoice/Sells Receipt
	QboEndCustomerPoNumCustFieldId int Null, -- Customized Field Id for EndCustomerPoNum in Qbo Invoice/Sells Receipt
	QboChnlOrderIdCustFieldName NVARCHAR(150) Null, -- Customized Field Name for Central Channel Order Id in Qbo Invoice/Sells Receipt
	QboChnlOrderIdCustFieldId int Null, -- Customized Field Id for Central Channel Order Id in Qbo Invoice/Sells Receipt
	Qbo2ndChnlOrderIdCustFieldName NVARCHAR(150) Null, -- Customized Field Name for Central Secondary Channel Order Name in Qbo Invoice/Sells Receipt 
	Qbo2ndChnlOrderIdCustFieldId int Null, -- Customized Field Name for Central Secondary Channel Order Id in Qbo Invoice/Sells Receipt 
	QboDefaultItemName NVARCHAR(150) Null, -- Used when unmatch item found from Central orders
	QboDefaultItemId int Null,
	QboSalesTaxItemName NVARCHAR(150) Null, -- Non-Inventory Item for Central calculated sales tax line
	QboSalesTaxItemId int Null,
	QboHandlingServiceItemName NVARCHAR(150) Null, -- Item for Service provided to customer, ex: item handling
	QboHandlingServiceItemId int Null,
	QboDiscountItemName NVARCHAR(150) Null, -- Item for daily summary defualt discount item
	QboDiscountItemId int Null,
	QboShippingItemName NVARCHAR(150) Null, -- Item for daily summary defualt shipping cost item
	QboShippingItemId int Null,
	QboHandlingServiceAccName NVARCHAR(150) Null, -- Income Account for Handling Service Item
	QboHandlingServiceAccId int Null,
	QboSalesTaxAccName NVARCHAR(150) Null, -- Type: Other Current Liabilities, Detail Type: Sales Tax Payable
	QboSalesTaxAccId int Null,
	QboDiscountAccName NVARCHAR(150) Null,　-- Discount Account for Default Item
	QboDiscountAccId int Null, 
	QboItemAssetAccName NVARCHAR(150) Null, -- For New/Default? Item creation
	QboItemAssetAccId int Null, -- TBD
	QboItemExpenseAccName NVARCHAR(150) Null, -- For New/Default? Item creation
	QboItemExpenseAccId int Null, -- TBD
	QboItemIncomeAccName NVARCHAR(150) Null, -- For New/Default? Item creation
	QboItemIncomeAccId int Null, -- TBD
	QboPostageRule int NULL DEFAULT 0, -- 0: None, 1: Seperate Bill
	QboInvoiceImportRule int NOT NULL DEFAULT 0, -- 0: None, 1: All, 2: Paid, 3: Unpaid
	QboSalesOrderImportRule int NOT NULL DEFAULT 0, -- 0: None, 1: All
	QboSettingStatus int Default 0, -- 0: Uninitiated, 1: Active, 100: inactive, 255: Error
	QboImportOrderAfterUpdateDate datetime NOT NULL DEFAULT getutcdate(),
	[EnterDate] datetime DEFAULT getutcdate(), 
    [LastUpdate] datetime DEFAULT getutcdate(), 
	CONSTRAINT [PK_QuickBooksIntegrationSetting] PRIMARY KEY CLUSTERED 
(
	IntegrationSettingNum ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ADD CONSTRAINT [UC_QuickBooksIntegrationSetting] UNIQUE (MasterAccountNum,ProfileNum)
GO

ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ADD CONSTRAINT [DF_QuickBooksIntegrationSetting_QboEndCustomerPoNumCustFieldName]  DEFAULT ('CustomerPoNum') FOR [QboEndCustomerPoNumCustFieldName]
GO

ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ADD CONSTRAINT [DF_QuickBooksIntegrationSetting_QboEndCustomerPoNumCustFieldId]  DEFAULT ((1)) FOR [QboEndCustomerPoNumCustFieldId]
GO

ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ADD CONSTRAINT [DF_QuickBooksIntegrationSetting_QboChnlOrderIdCustFieldName]  DEFAULT ('ChnlOrderID') FOR [QboChnlOrderIdCustFieldName]
GO

ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ADD CONSTRAINT [DF_QuickBooksIntegrationSetting_QboChnlOrderIdCustFieldId]  DEFAULT ((2)) FOR [QboChnlOrderIdCustFieldId]
GO

ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ADD CONSTRAINT [DF_QuickBooksIntegrationSetting_Qbo2ndChnlOrderIdCustFieldName]  DEFAULT ('2ndChnlOrderID') FOR [Qbo2ndChnlOrderIdCustFieldName]
GO

ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ADD CONSTRAINT [DF_QuickBooksIntegrationSetting_Qbo2ndChnlOrderIdCustFieldId]  DEFAULT ((3)) FOR [Qbo2ndChnlOrderIdCustFieldId]
GO

--ALTER TABLE  [dbo].[QuickBooksIntegrationSetting] ALTER COLUMN QboSalesTaxAccName NVARCHAR(150) NULL;
--GO

--ALTER TABLE  [dbo].[QuickBooksIntegrationSetting] ALTER COLUMN QboSalesTaxAccId int NULL;
--GO

--ALTER TABLE  [dbo].[QuickBooksIntegrationSetting] ALTER COLUMN QboSalesTaxItemName NVARCHAR(150) NULL;
--GO

--ALTER TABLE  [dbo].[QuickBooksIntegrationSetting] ALTER COLUMN QboSalesTaxItemId int NULL;
--GO

--ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ADD [QboSettingStatus] int Default 0 -- 0: Uninitiated, 1: Active, 100: inactive, 255: Error
--GO

--ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ALTER COLUMN [ExportOrderToDate] datetime NULL;
--GO