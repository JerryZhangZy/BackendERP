CREATE TABLE [dbo].[OrderHeaderMerchantExt]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL , 
	[DatabaseNum] Int NOT NULL , --Each database has its own default value.
	[CentralOrderNum] BigInt NOT NULL, --Unique in Commerce Central internal system, DatabaseID + CentralOrderNum is DigitBridge unique
	[MasterAccountNum] Int NOT NULL,
	[ProfileNum] Int NOT NULL,

	[ChannelNum] int NOT NULL DEFAULT 0, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL DEFAULT 0, --The unique number of this profile’s channel account
	[PoType] Varchar(20) NOT NULL DEFAULT '', --A value indicates the Po Type. For example, “R” indicates this order was that this purchase order is being re-issued.
	[HubOrderID] varchar(50) NOT NULL DEFAULT '',
	[MerchantID] Varchar(50) NOT NULL DEFAULT '', --A value represents the merchant ID on the integration partner
	[MerchantVendorID] Varchar(50) NOT NULL DEFAULT '', --Vendor ID assigned by the merchant
	[MerchantOrderDateUtc] DATETIME NULL, --Purchase order date assigned by the merchant
	[PackingSlipMessage] Varchar(500) NOT NULL DEFAULT '',
	[VendorNotes] Varchar (500) NOT NULL DEFAULT '', --Special instructions to the vendor regarding this purchase order.
	[VendorWarehouseID] Varchar(30) NOT NULL DEFAULT '', 
	[VendorCommitmentID] Varchar(30) NOT NULL DEFAULT '', --An ID from a previous fulfillment commitment transaction from the vendor to the merchant.
	[SalesDivision] Varchar(50) NOT NULL DEFAULT '', --The merchant-defined code for the retailing brand.
	[MerchantPhone1] Varchar(50) NOT NULL DEFAULT '',
	[MerchantPhone2] Varchar(50) NOT NULL DEFAULT '',
	[MerchantPhone3] Varchar(50) NOT NULL DEFAULT '',
	[MerchantCustomerID] Varchar(50) NOT NULL DEFAULT '', --The merchant’s ID for the online customer who S 50 placed the order. This is sometimes referred to as ‘member number’.
	[MerchantCustomerOrderID] Varchar(100) NOT NULL DEFAULT '', --The ID of the end customer’s order. Usually retailers will use this field.
	[MerchantCustomerOrderDateUtc] DATETIME NULL,
	[MerchantCustomerPaymentMethod] Varchar(50) NOT NULL DEFAULT '',
	[CancelAfterDateUtc] DATETIME NULL,
	[RequiredShipDateUtc] DATETIME NULL,
	[PromoID] Varchar(50) NOT NULL DEFAULT '', --The merchant assigned ID for the promotion under which the order was taken.
	[PromoStartDateUtc] DATETIME NULL, --The date which the promotion begins.
	[MerchandiseTypeCode] Varchar(50) NOT NULL DEFAULT '', --A merchant defined code for orders indicating the class of the merchandise contained in the order.
	[AuthorizationForExpenseNumber] Varchar(50) NOT NULL DEFAULT '', --The merchant assigned authorization number.
	[ShipToAccountNO] NVARCHAR(50) NOT NULL DEFAULT '',
	[ShipToGender] NVarchar(20) NOT NULL DEFAULT '',
	[ShipToReceipt] NVarchar(50) NOT NULL DEFAULT '',
	[ShipToVcdID] Varchar(50) NOT NULL DEFAULT '',
	[ShipToDEANumber] Varchar(50) NOT NULL DEFAULT '',
	[MerchantBillToAddressType] Varchar(50) NOT NULL DEFAULT '',
	[SoldToAddressType] Varchar(50) NOT NULL DEFAULT '',
	[SoldToAttention] NVarchar(100) NOT NULL DEFAULT '',
	[SoldToCompanyname] NVarchar(100) NOT NULL DEFAULT '',
	[SoldToName] NVarchar(100) NOT NULL DEFAULT '',
	[SoldToFirstName] nVarchar(50) NOT NULL DEFAULT '',
	[SoldToLastName] nVarchar(50) NOT NULL DEFAULT '',
	[SoldToAddressLine1] NVarchar(100) NOT NULL DEFAULT '',
	[SoldToAddressLine2] NVarchar(100) NOT NULL DEFAULT '',
	[SoldToAddressLine3] NVarchar(100) NOT NULL DEFAULT '',
	[SoldToCity] nVarchar(50) NOT NULL DEFAULT '',
	[SoldToState] nVarchar(50) NOT NULL DEFAULT '',
	[SoldToPostalCode] nVarchar(50) NOT NULL DEFAULT '',
	[SoldToPostalCodeExt] nVarchar(10) NOT NULL DEFAULT '',
	[SoldToCountry] nVarchar(50) NOT NULL DEFAULT '',
	[SoldToEmail] nVarchar(100) NOT NULL DEFAULT '',
	[SoldToDayPhone] Varchar(50) NOT NULL DEFAULT '',
	[SoldToNightPhone] Varchar(50) NOT NULL DEFAULT '',
	[SoldToTaxExemptNO] Varchar(50) NOT NULL DEFAULT '',
	[SoldToAccountNO] NVarchar(50) NOT NULL DEFAULT '',
	[SoldToGender] NVarchar(20) NOT NULL DEFAULT '',
	[CustomerGiftMessage] NVarchar(200) NOT NULL DEFAULT '',
	[ReturnAddressee] NVarchar(100) NOT NULL DEFAULT '',
	[ReturnAddressLine1] NVarchar(100) NOT NULL DEFAULT '',
	[ReturnAddressLine2] NVarchar(100) NOT NULL DEFAULT '',
	[ReturnAddrelssLine3] NVarchar(100) NOT NULL DEFAULT '',
	[ReturnCity] nVarchar(50) NOT NULL DEFAULT '',
	[ReturnState] nVarchar(50) NOT NULL DEFAULT '',
	[ReturnPostalCode] nVarchar(50) NOT NULL DEFAULT '',
	[ReturnPostalCodeExt] nVarchar(10) NOT NULL DEFAULT '',
	[ReturnCountry] nVarchar(50) NOT NULL DEFAULT '',
	[ReturnEmail] nVarchar(100) NOT NULL DEFAULT '',
	[ReturnDayPhone] Varchar(50) NOT NULL DEFAULT '',
	[ReturnNightPhone] Varchar(50) NOT NULL DEFAULT '',
	[ReturnLocationID] Varchar(50) NOT NULL DEFAULT '', --This field will contain the account number or ID for the Return To party.
	[InvoiceToAddressType] Varchar(50) NOT NULL DEFAULT '',
	[InvoiceToAttention] NVarchar(100) NOT NULL DEFAULT '',
	[InvoiceToCompany] NVarchar(100) NOT NULL DEFAULT '',
	[InoviceToName] NVarchar(100) NOT NULL DEFAULT '',
	[InvoiceToFirstName] nVarchar(50) NOT NULL DEFAULT '',
	[InvoiceToLastName] nVarchar(50) NOT NULL DEFAULT '',
	[InvoiceToAddressLine1] NVarchar(100) NOT NULL DEFAULT '',
	[InvoiceToAddressLine2] NVarchar(100) NOT NULL DEFAULT '',
	[InvoiceToAddressLine3] NVarchar(100) NOT NULL DEFAULT '',
	[InvoiceToCity] nVarchar(50) NOT NULL DEFAULT '',
	[InvoiceToState] nVarchar(50) NOT NULL DEFAULT '',
	[InvoiceToPostalCode] nVarchar(50) NOT NULL DEFAULT '',
	[InvoiceToPostalCodeExt] nVarchar(10) NOT NULL DEFAULT '',
	[InvoiceToCountry] nVarchar(50) NOT NULL DEFAULT '',
	[InvoiceToDayPhone] Varchar(50) NOT NULL DEFAULT '',
	[InvoviceToNightPhone] Varchar(50) NOT NULL DEFAULT '',
	[InvoiceToEmail] NVarchar(100) NOT NULL DEFAULT '',
	[InvoiceToAccountNO] Varchar(50) NOT NULL DEFAULT '',
	[InvoiceToTaxExemptNO] Varchar(50) NOT NULL DEFAULT '',
	[InvoiceToGender] NVarchar(20) NOT NULL DEFAULT '',
	[BuyingContract] NVarchar(100) NOT NULL DEFAULT '',
	[CcParty] NVarchar(100) NOT NULL DEFAULT '',
	[APVendor] NVarchar(100) NOT NULL DEFAULT '',
	[RMEmail] NVarchar(100) NOT NULL DEFAULT '',
	[MarketingInserts] NVarchar(100) NOT NULL DEFAULT '',
	[BusinessRuleCode] NVarchar(100) NOT NULL DEFAULT '',
	[ReleaseNumber] Varchar(50) NOT NULL DEFAULT '',
	[MerchantBuyerName] NVarchar(100) NOT NULL DEFAULT '',
	[SalesCurrency] Varchar(10) NOT NULL DEFAULT '',
	[FreightCollectAccount] Varchar(50) NOT NULL DEFAULT '',
	[FreightPaymentTerms] Varchar(50) NOT NULL DEFAULT '',
	[MerchantSalesAgent] Varchar(50) NOT NULL DEFAULT '',
	[PaymentMethodDescription] Varchar(50) NOT NULL DEFAULT '',
	[OrderFulfillmentFee] Money NOT NULL DEFAULT 0,
	[PackingSlipTemplateIndicator] Varchar(50) NOT NULL DEFAULT '',
	[SalesTax] Money NOT NULL DEFAULT 0,
	[ResponsibilityRole] Varchar(50) NOT NULL DEFAULT '',
	[ExpectedProcessingPriority] Varchar(50) NOT NULL DEFAULT '',
	[PreassignedWaybillNumber] Varchar(50) NOT NULL DEFAULT '',
	[URL] Varchar(100) NOT NULL DEFAULT '', 
	[ShipperID] [varchar](50) NOT NULL DEFAULT '',
	[FileIdentifier] Varchar(100) NOT NULL DEFAULT '', 
	[ERPCustomerOrderNumber] varchar(50) NOT NULL DEFAULT '',
	[CustomerOrderPOIndex] varchar(50) NOT NULL DEFAULT '',
	[MultiSource] varchar(3) NOT NULL DEFAULT '',
	[NetDaysDue] int NOT NULL DEFAULT 0,
	[TotalOrderCost] [money] NOT NULL DEFAULT 0,

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)

    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderHeaderMerchantExtUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line
	CONSTRAINT [PK_OrderHeaderMerchantExt] PRIMARY KEY CLUSTERED ([RowNum] ASC)
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'UK_OrderHeaderMerchantExt')
CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderHeaderMerchantExt] ON [dbo].[OrderHeaderMerchantExt]
(
	[CentralOrderUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'UI_OrderHeaderMerchantExt_CentralOrderNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_OrderHeaderMerchantExt_CentralOrderNum] ON [dbo].[OrderHeaderMerchantExt]
(
	[DatabaseNum] ASC,
	[CentralOrderNum] ASC
) 
GO



ALTER TABLE [dbo].[OrderHeaderMerchantExt] ADD  CONSTRAINT [DF_OrderHeaderMerchantExt_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderHeaderMerchantExt] ADD  CONSTRAINT [DF_OrderHeaderMerchantExt_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderHeaderMerchantExt] ADD  CONSTRAINT [DF_OrderHeaderMerchantExt_CentralOrderHeaderMerchantExtUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [CentralOrderHeaderMerchantExtUuid]
GO
