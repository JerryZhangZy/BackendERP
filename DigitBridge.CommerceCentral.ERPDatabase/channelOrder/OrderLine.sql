CREATE TABLE [dbo].[OrderLine]
(
	--[RowNum] bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CentralOrderLineNum]	bigint NOT NULL IDENTITY(10000000, 1), --Global unique in this database. CommerceCentral
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[CentralOrderNum]	bigint NOT NULL,
	[MasterAccountNum]	int NOT NULL,
	[ProfileNum]	int NOT NULL,
	[ChannelNum] int NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL, --The unique number of this profile’s channel account
	[ChannelOrderID]	varchar(130) NOT NULL,
	[CentralProductNum]	BIGINT NULL, --Unique product (item) number
	[ChannelItemID]	Varchar(50) NULL, --The item ID on channel, which mostly is not the SKU unless it is the webstore order.
	[SKU]	Varchar(100) NULL,
	[ItemTitle]	NVARCHAR(200) NULL, --The title comes with the original order. If the original title is longer, it will be truncated.
	[OrderQty]	DECIMAL(24, 6) NULL, --Use int here to be consistent with the majority of the channels.
	[UnitPrice]	money NULL, --For dropship, The cost, to the merchant, per unit (wholesale price).
	[LineItemTaxAmount]	money NULL,
	[LineShippingAmount]	money NULL,
	[LineShippingTaxAmount]	money NULL,
	[LineShippingDiscount]	money NULL,
	[LineShippingDiscountTaxAmount]	money NULL,
	[LineRecyclingFee]	money NULL,
	[LineGiftMsg]	NVARCHAR(500) NULL, --Gift message printed on the line level.
	[LineGiftNotes]	NVARCHAR(400) NULL, --Description of the gift wrapping.
	[LineGiftAmount]	money NULL, --The gift wrapping cost for all quantity.
	[LineGiftTaxAmount]	money NULL, --The gift wrapping tax cost for all quantity.
	[LinePromotionCodes]	NVARCHAR(500) NULL, 
	[LinePromotionAmount]	MONEY NULL, 
	[LinePromotionTaxAmount]	MONEY NULL, 
	[BundleStatus]	TINYINT NULL, --Indicates if the order item is a bundle.
	[HarmonizedCode]	Varchar(20) NULL, 
	[UPC]	Varchar(20) NULL,
	[EAN]	Varchar(20) NULL,
	[UnitOfMeasure]	Varchar(20) NULL, 
    [EnterDateUtc] DATETIME NULL, 
    [DigitBridgeGuid] uniqueidentifier NOT NULL, 
 [DBChannelOrderLineRowID] VARCHAR(50) NULL, 
    CONSTRAINT [PK_OrderLine] PRIMARY KEY CLUSTERED 
(
	[CentralOrderLineNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderLine] ADD  CONSTRAINT [DF_OrderLine_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UI_OrderLine_CentralOrderLineNum]
    ON [dbo].[OrderLine]([CentralOrderLineNum] ASC);
GO

CREATE NONCLUSTERED INDEX [NUI_OrderLine_DatabaseNum_CentralOrderNum] ON [dbo].[OrderLine]
(
	[DatabaseNum] ASC,
	[CentralOrderNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [NUI_OrderLine_DatabaseNum_CentralOrderNum_CentralProductNum_SKU] ON [dbo].[OrderLine]
(
	[CentralOrderNum] ASC,
	[CentralProductNum] ASC,
	[SKU] ASC,
	[OrderQty] ASC,
	[DigitBridgeGuid] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [NUI_OrderLine_MasterAccountNum_ProfileNum_CentralOrderNum_ChannelNum_CentralProductNum] ON [dbo].[OrderLine]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC
)
INCLUDE([CentralOrderNum],[ChannelNum],[CentralProductNum]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
