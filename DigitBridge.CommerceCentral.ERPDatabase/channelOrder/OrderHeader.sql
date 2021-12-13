CREATE TABLE [dbo].[OrderHeader]
(
    [DatabaseNum] INT NOT NULL, --Database Number. Required, Title: Database Number, Display: false, Editable: false--
	[CentralOrderNum] BigInt IDENTITY(100000000, 1) NOT NULL, --Unique in this database, DatabaseNum + CentralOrderNum is DigitBridgeOrderId, which is global unique--
	[MasterAccountNum] Int NOT NULL,
	[ProfileNum] Int NOT NULL,
	[ChannelNum] int NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL, --The unique number of this profile’s channel account
	[UserDataPresent] TinyInt NOT NULL,  --Default 1 Has User data. Some channel may require to remove the user data after a certain time frame. 0, unknow. 1, Present. 255, Removed. Default 0.
	[UserDataRemoveDateUtc] datetime NULL, --The UTC date user data was removed
	[ChannelOrderID] Varchar(130) NOT NULL, --This usually is the marketplace order ID, or merchant PO Number
	[SecondaryChannelOrderID] Varchar(200) NULL, --Secondary identifier provided by the channel. This is a secondary marketplace-generated Order ID. It is not populated most of the time.
	[SellerOrderID] Varchar(30) NULL, --Order identifier assigned by the seller. Usually it is not used.
	[Currency] Varchar(10) NULL,
	[OriginalOrderDateUtc] DateTime NOT NULL, --Timestamp when the order was created at the original channel
	[SellerPublicNote] Varchar(4500) NULL, --The note from the seller may be included on the invoice or packing list
	[SellerPrivateNote] Varchar(4500) NULL, --The note from the seller for internal use. Cannot be printed on invoice or packing listing
	[EndBuyerInstruction] Varchar(4500) NULL, --Usually it is related to shipping instruction
	[TotalOrderAmount] Money NULL, --Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation 
	[TotalTaxAmount] Money NULL, --Reference calculation. The real amount is provided by the channel. 
	[TotalShippingAmount] Money NULL, --Sum of all OrderItems ShippingPrice (Related to VAT. Refer to tax info for more detail) Does not include shipping item-level shipping promotions. 
	[TotalShippingTaxAmount] Money NULL, --Sum of all OrderItems ShippingTaxPrice. Number generated here is estimate based on tax (VAT) settings in the profile. For representation only. No actual data provided by channels.
	[TotalShippingDiscount] Money NULL, --Sum of all OrderItems ShippingDiscount Negative. 
	[TotalShippingDiscountTaxAmount] Money NULL, --Sum of all OrderItems ShippingDiscount Negative. 
	[TotalInsuranceAmount] Money NULL, --TotalInsurancePrice data - provided at order level only.
	[TotalGiftOptionAmount] Money NULL, --Sum of all OrderItems GiftPrice (Related to VAT setting)
	[TotalGiftOptionTaxAmount] Money NULL, --Number generated here is estimate based on VAT settings in the profile. For representation only. No actual data provided by channels.
	[AdditionalCostOrDiscount] Money NULL, --provided at order level only. Not commonly populated. Miscellaneous cost modification which may be positive to indicate a cost or negative to indicate a discount.
	[PromotionAmount] Money NULL, --provided at order level. Not a sum of Promotion Level pricing. A negative decimal. Will not be populated if item level promotions exist.
	[EstimatedShipDateUtc] DateTime NULL, --Timestamp estimating when the order will be fulfilled.
	[DeliverByDateUtc] DateTime NULL, --Timestamp indicating the deadline for fulfilling the order.
	[RequestedShippingCarrier] Varchar(50) NULL, --Original requested shipping carrier from the channel.
	[RequestedShippingClass] Varchar(50) NULL, --Original requested shipping class provided from the channel
	[ResellerID] Varchar(300) NULL, --Identifier for a reseller agency. Amazon: FulfillmentChannel(AFN/MFN)
	[FlagNum] smallint NULL, --Enum. Identify the flag of an order, which may signal special attention is required.
	[FlagDesc] Varchar(100) NULL, --Describe the flag reason or other notice
	[PaymentStatus] tinyint NULL, --Enum. 0, unknown. 1, Paid. 200, NotPaid.
	[PaymentUpdateUtc] datetime NULL, --Timestamp indicating the latest update to PaymentStatus.
	[ShippingUpdateUtc] datetime NULL, --Timestamp indicating the latest update to ShippingStatus.
	[EndBuyerUserID] Varchar(255) NULL, --The marketplace user ID of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department.
	[EndBuyerEmail] Varchar(255) NULL, --The email of the end customer
	[PaymentMethod] nVarchar(50) NULL,
	[ShipToName] nVarchar(100) NULL,
	[ShipToFirstName] nVarchar(50) NULL,
	[ShipToLastName] nVarchar(50) NULL,
	[ShipToSuffix] nVarchar(50) NULL,
	[ShipToCompany] nVarchar(100) NULL,
	[ShipToCompanyJobTitle] nVarchar(100) NULL,
	[ShipToAttention] nVarchar(100) NULL,
	[ShipToDaytimePhone] Varchar(50) NULL,
	[ShipToNightPhone] Varchar(50) NULL,
	[ShipToAddressLine1] nVarchar(100) NULL,
	[ShipToAddressLine2] nVarchar(100) NULL,
	[ShipToAddressLine3] nVarchar(100) NULL,
	[ShipToCity] nVarchar(50) NULL,
	[ShipToState] nVarchar(50) NULL,
	[ShipToStateFullName] nVarchar(100) NULL,
	[ShipToPostalCode] nVarchar(50) NULL,
	[ShipToPostalCodeExt] nVarchar(50) NULL,
	[ShipToCounty] nvarchar(50) NULL,
	[ShipToCountry] nVarchar(50) NULL,
	[ShipToEmail] nVarchar(100) NULL,
	[BillToName] nVarchar(100) NULL,
	[BillToFirstName] nVarchar(50) NULL,
	[BillToLastName] nVarchar(50) NULL,
	[BillToSuffix] nVarchar(50) NULL,
	[BillToCompany] nVarchar(100) NULL,
	[BillToCompanyJobTitle] nVarchar(100) NULL,
	[BillToAttention] nVarchar(100) NULL,
	[BillToAddressLine1] nVarchar(100) NULL,
	[BillToAddressLine2] nVarchar(100) NULL,
	[BillToAddressLine3] nVarchar(100) NULL,
	[BillToCity] nVarchar(50) NULL,
	[BillToState] nVarchar(50) NULL,
	[BillToStateFullName] nVarchar(100) NULL,
	[BillToPostalCode] nVarchar(50) NULL,
	[BillToPostalCodeExt] nVarchar(50) NULL,
	[BillToCounty] nvarchar(50) NULL,
	[BillToCountry] nVarchar(50) NULL,
	[BillToEmail] nVarchar(100) NULL,
	[BillToDaytimePhone] Varchar(50) NULL,
	[BillToNightPhone] Varchar(50) NULL,
	[SignatureFlag] Varchar(15) NULL, --Shipping signature
	[PickupFlag] Varchar(15) NULL, --Flag if a customer will pick up the order.
	[MerchantDivision] Varchar(30) NULL, --The merchant’s division ID or name.
	[MerchantBatchNumber] Varchar(30) NULL, --The merchant assigned batch number from which the purchase order came.
	[MerchantDepartmentSiteID] Varchar(50) NULL, --This field is used to identify the merchant’s sales department or web site.
	[ReservationNumber] Varchar(100) NULL,
	[MerchantShipToAddressType] Varchar(50) NULL,
	[GiftIndicator] Varchar(10), --Value A means this order is a gift
	[CustomerOrganizationType] tinyint NULL, -- 0-Consumer Order, 1-Businees Order
	[OrderMark] tinyint NULL, -- 0-NotPrime Order(Amazon) 1-Prime Order(Amazon)
	[OrderMark2] tinyint NULL, -- 0-ThirdPartyFulfilled (AFN) 1-SellerFulfilled (MFN) 2 Pickup 3 ShipToStore
	[OrderStatus] TINYINT null, -- 0 Processing (Unshipped default), 1 Shipped, 2 PartiallyShipped, 4 PendingShipment, 8 ReadyToPickup, 16 Canceled, 100 OnHold
	[OrderStatusUpdateDateUtc] datetime null,
    [EnterDateUtc] DATETIME NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL,
	[DBChannelOrderHeaderRowID] VARCHAR(50) NULL, 
    [DCAssignmentStatus] INT NULL, 
    [DCAssignmentDateUtc] DATETIME NULL, 

    [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))),
    [RowNum] BIGINT NOT NULL DEFAULT 0,
    [TotalDueSellerAmount] MONEY NOT NULL DEFAULT 0,
	[TotalCommissionAmount] [money] NOT NULL,
	[TotalCommissionTaxAmount] [money] NOT NULL,
	[TotalRemittedTaxAmount] [money] NULL,
    CONSTRAINT [PK_OrderHeader] PRIMARY KEY CLUSTERED ([CentralOrderNum])
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_UserDataPresent]  DEFAULT ((1)) FOR [UserDataPresent]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_SecondaryChannelOrderID]  DEFAULT ('') FOR [SecondaryChannelOrderID]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_SellerOrderID]  DEFAULT ('') FOR [SellerOrderID]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_Currency]  DEFAULT ('') FOR [Currency]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_OrderStatus]  DEFAULT ((0)) FOR [OrderStatus]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_OrderStatusUpdateDateUtc]  DEFAULT (getutcdate()) FOR [OrderStatusUpdateDateUtc]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_DigitBridgeGuid]  DEFAULT (newid()) FOR [DigitBridgeGuid]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_RowNum]  DEFAULT ((0)) FOR [RowNum]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_TotalDueSellerAmount]  DEFAULT ((0)) FOR [TotalDueSellerAmount]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_TotalCommissionAmount]  DEFAULT ((0)) FOR [TotalCommissionAmount]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_TotalCommissionTaxAmount]  DEFAULT ((0)) FOR [TotalCommissionTaxAmount]
GO

ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_TotalRemittedTaxAmount]  DEFAULT ((0)) FOR [TotalRemittedTaxAmount]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_OrderHeader_ChannelAccountNum_ChannelNum_ChannelOrderID] ON [dbo].[OrderHeader]
(
	[ChannelNum] ASC,
	[ChannelAccountNum] ASC,	[ChannelOrderID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [NI_OrderHeader_MasterAccountNum_ProfileNum_ChannelNum_OrderStatus_OrderStatusUpdateDateUtc]
    ON [dbo].[OrderHeader]([MasterAccountNum] ASC, [ProfileNum] ASC, [ChannelNum] ASC, [OrderStatus] ASC, [OrderStatusUpdateDateUtc] ASC);
GO

CREATE NONCLUSTERED INDEX [NUI_OrderHeader_MasterAccountNum_ProfileNum_ChannelNum_OrderStatus_EnterDateUtc] ON [dbo].[OrderHeader]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ChannelNum] ASC,
	[OrderStatus] ASC,
	[EnterDateUtc] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderHeader] ON [dbo].[OrderHeader]
(
	[CentralOrderUuid] ASC
) 
GO


CREATE NONCLUSTERED INDEX [UK_OrderHeader_OriginalOrderDateUtc] ON [dbo].[OrderHeader]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[OriginalOrderDateUtc] ASC
) 
GO

