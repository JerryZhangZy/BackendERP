CREATE TABLE [dbo].[OrderHeaderMarketplaceExt]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL , 
	[DatabaseNum]	Int NOT NULL , --Each database has its own default value.
	CentralOrderNum	BigInt NOT NULL, --Unique in Commerce Central internal system, DatabaseID + CentralOrderNum is DigitBridge unique
	[MasterAccountNum] Int NOT NULL,
	[ProfileNum] Int NOT NULL,
	[ChannelNum] int NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL, --The unique number of this profile’s channel account
	PaymentTransactionID	Varchar(50) NULL,
	PaymentPaypalAccountID	Varchar(50) NULL,
	PaymentCreditCardLast4	Varchar(4) NULL,
	PaymentMerchantReferenceNumber	Varchar(50) NULL,
	OrderTaxType	Varchar(50) NULL,
	ShippingTaxType	Varchar(50) NULL,
	GiftOptionsTaxType	Varchar(50) NULL, 
	[EarliestShipDateUtc] Datetime Null,
	[EarliestDeliveryDateUtc] Datetime Null,
    [EnterDateUtc] DATETIME NULL, 
    [DigitBridgeGuid] uniqueidentifier NOT NULL,

    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderHeaderMarketplaceExtUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line
 CONSTRAINT [PK_OrderHeaderMarketplaceExt] PRIMARY KEY CLUSTERED 
(
	[DatabaseNum] ASC,
	[CentralOrderNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderHeaderMarketplaceExt] ADD  CONSTRAINT [DF_OrderHeaderMarketplaceExt_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderHeaderMarketplaceExt] ADD  CONSTRAINT [DF_OrderHeaderMarketplaceExt_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderHeaderMarketplaceExt] ADD  CONSTRAINT [DF_OrderHeaderMarketplaceExt_CentralOrderHeaderMarketplaceExtUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [CentralOrderHeaderMarketplaceExtUuid]
GO

