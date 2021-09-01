CREATE TABLE [dbo].[OrderHeaderMerchantExt]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL , 
	[DatabaseNum] Int NOT NULL , --Each database has its own default value.
	[CentralOrderNum] BigInt NOT NULL, --Unique in Commerce Central internal system, DatabaseID + CentralOrderNum is DigitBridge unique
	[MasterAccountNum] Int NOT NULL,
	[ProfileNum] Int NOT NULL,
	[ChannelNum] int NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL, --The unique number of this profile’s channel account
	[PoType] Varchar(20) NULL, --A value indicates the Po Type. For example, “R” indicates this order was that this purchase order is being re-issued.
	[HubOrderID] varchar(50) NULL,
	[MerchantID] Varchar(50) NULL, --A value represents the merchant ID on the integration partner
	[MerchantVendorID] Varchar(50) NULL, --Vendor ID assigned by the merchant
	[MerchantOrderDateUtc] DATETIME NULL, --Purchase order date assigned by the merchant
	[PackingSlipMessage] Varchar(500) NULL,
	[VendorNotes] Varchar (500) NULL, --Special instructions to the vendor regarding this purchase order.
	[VendorWarehouseID] Varchar(30) NULL, 
	[VendorCommitmentID] Varchar(30) NULL, --An ID from a previous fulfillment commitment transaction from the vendor to the merchant.
	[SalesDivision] Varchar(50) NULL, --The merchant-defined code for the retailing brand.
	[MerchantPhone1] Varchar(50) NULL,
	[MerchantPhone2] Varchar(50) NULL,
	[MerchantPhone3] Varchar(50) NULL,
	[MerchantCustomerID] Varchar(50) NULL, --The merchant’s ID for the online customer who S 50 placed the order. This is sometimes referred to as ‘member number’.
	[MerchantCustomerOrderID] Varchar(100) NULL, --The ID of the end customer’s order. Usually retailers will use this field.
	[MerchantCustomerOrderDateUtc] DATETIME NULL,
	[MerchantCustomerPaymentMethod] Varchar(50) NULL,
	[CancelAfterDateUtc] DATETIME NULL,
	[RequiredShipDateUtc] DATETIME NULL,
	[PromoID] Varchar(50) NULL, --The merchant assigned ID for the promotion under which the order was taken.
	[PromoStartDateUtc] DATETIME NULL, --The date which the promotion begins.
	[MerchandiseTypeCode] Varchar(50) NULL, --A merchant defined code for orders indicating the class of the merchandise contained in the order.
	[AuthorizationForExpenseNumber] Varchar(50) NULL, --The merchant assigned authorization number.
	[ShipToAccountNO] NVARCHAR(50) NULL,
	[ShipToGender] NVarchar(20) NULL,
	[ShipToReceipt] NVarchar(50) NULL,
	[ShipToVcdID] Varchar(50) NULL,
	[ShipToDEANumber] Varchar(50) NULL,
	[MerchantBillToAddressType] Varchar(50) NULL,
	[SoldToAddressType] Varchar(50) NULL,
	[SoldToAttention] NVarchar(100) NULL,
	[SoldToCompanyname] NVarchar(100) NULL,
	[SoldToName] NVarchar(100) NULL,
	[SoldToFirstName] nVarchar(50) NULL,
	[SoldToLastName] nVarchar(50) NULL,
	[SoldToAddressLine1] NVarchar(100) NULL,
	[SoldToAddressLine2] NVarchar(100) NULL,
	[SoldToAddressLine3] NVarchar(100) NULL,
	[SoldToCity] nVarchar(50) NULL,
	[SoldToState] nVarchar(50) NULL,
	[SoldToPostalCode] nVarchar(50) NULL,
	[SoldToPostalCodeExt] nVarchar(10) NULL,
	[SoldToCountry] nVarchar(50) NULL,
	[SoldToEmail] nVarchar(100) NULL,
	[SoldToDayPhone] Varchar(50) NULL,
	[SoldToNightPhone] Varchar(50) NULL,
	[SoldToTaxExemptNO] Varchar(50) NULL,
	[SoldToAccountNO] NVarchar(50) NULL,
	[SoldToGender] NVarchar(20) NULL,
	[CustomerGiftMessage] NVarchar(200) NULL,
	[ReturnAddressee] NVarchar(100) NULL,
	[ReturnAddressLine1] NVarchar(100) NULL,
	[ReturnAddressLine2] NVarchar(100) NULL,
	[ReturnAddrelssLine3] NVarchar(100) NULL,
	[ReturnCity] nVarchar(50) NULL,
	[ReturnState] nVarchar(50) NULL,
	[ReturnPostalCode] nVarchar(50) NULL,
	[ReturnPostalCodeExt] nVarchar(10) NULL,
	[ReturnCountry] nVarchar(50) NULL,
	[ReturnEmail] nVarchar(100) NULL,
	[ReturnDayPhone] Varchar(50) NULL,
	[ReturnNightPhone] Varchar(50) NULL,
	[ReturnLocationID] Varchar(50) NULL, --This field will contain the account number or ID for the Return To party.
	[InvoiceToAddressType] Varchar(50) NULL,
	[InvoiceToAttention] NVarchar(100) NULL,
	[InvoiceToCompany] NVarchar(100) NULL,
	[InoviceToName] NVarchar(100) NULL,
	[InvoiceToFirstName] nVarchar(50) NULL,
	[InvoiceToLastName] nVarchar(50) NULL,
	[InvoiceToAddressLine1] NVarchar(100) NULL,
	[InvoiceToAddressLine2] NVarchar(100) NULL,
	[InvoiceToAddressLine3] NVarchar(100) NULL,
	[InvoiceToCity] nVarchar(50) NULL,
	[InvoiceToState] nVarchar(50) NULL,
	[InvoiceToPostalCode] nVarchar(50) NULL,
	[InvoiceToPostalCodeExt] nVarchar(10) NULL,
	[InvoiceToCountry] nVarchar(50) NULL,
	[InvoiceToDayPhone] Varchar(50) NULL,
	[InvoviceToNightPhone] Varchar(50) NULL,
	[InvoiceToEmail] NVarchar(100) NULL,
	[InvoiceToAccountNO] Varchar(50) NULL,
	[InvoiceToTaxExemptNO] Varchar(50) NULL,
	[InvoiceToGender] NVarchar(20) NULL,
	[BuyingContract] NVarchar(100) NULL,
	[CcParty] NVarchar(100) NULL,
	[APVendor] NVarchar(100) NULL,
	[RMEmail] NVarchar(100) NULL,
	[MarketingInserts] NVarchar(100) NULL,
	[BusinessRuleCode] NVarchar(100) NULL,
	[ReleaseNumber] Varchar(50) NULL,
	[MerchantBuyerName] NVarchar(100) NULL,
	[SalesCurrency] Varchar(10) NULL,
	[FreightCollectAccount] Varchar(50) NULL,
	[FreightPaymentTerms] Varchar(50) NULL,
	[MerchantSalesAgent] Varchar(50) NULL,
	[PaymentMethodDescription] Varchar(50) NULL,
	[OrderFulfillmentFee] Money NULL,
	[PackingSlipTemplateIndicator] Varchar(50) NULL,
	[SalesTax] Money NULL,
	[ResponsibilityRole] Varchar(50) NULL,
	[ExpectedProcessingPriority] Varchar(50) NULL,
	[PreassignedWaybillNumber] Varchar(50) NULL,
	[URL] Varchar(100) NULL, 
	[ShipperID] [varchar](50) NULL,
	[FileIdentifier] Varchar(100) NULL, 
	[ERPCustomerOrderNumber] varchar(50) NULL,
	[CustomerOrderPOIndex] varchar(50) NULL,
	[MultiSource] varchar(3) NULL,
	[NetDaysDue] int NULL,
	[TotalOrderCost] [money] NULL,
    [EnterDateUtc] DATETIME NULL, 
    [DigitBridgeGuid] uniqueidentifier NOT NULL, 

    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderHeaderMerchantExtUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line

 CONSTRAINT [PK_OrderHeaderMerchantExt] PRIMARY KEY CLUSTERED 
(
	[DatabaseNum] ASC,
	[CentralOrderNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderHeaderMerchantExt] ADD  CONSTRAINT [DF_OrderHeaderMerchantExt_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderHeaderMerchantExt] ADD  CONSTRAINT [DF_OrderHeaderMerchantExt_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderHeaderMerchantExt] ADD  CONSTRAINT [DF_OrderHeaderMerchantExt_CentralOrderHeaderMerchantExtUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [CentralOrderHeaderMerchantExtUuid]
GO
