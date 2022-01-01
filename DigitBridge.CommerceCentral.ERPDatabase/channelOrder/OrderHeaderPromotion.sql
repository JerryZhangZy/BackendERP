CREATE TABLE [dbo].[OrderHeaderPromotion]
(
	RowNum	BIGINT IDENTITY(1,1) 	NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	CentralOrderNum	BigInt	NOT NULL,
	[MasterAccountNum] Int NOT NULL,
	[ProfileNum] Int NOT NULL,
	[ChannelNum] int NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL, --The unique number of this profile’s channel account
	[PromotionalCode]	VARCHAR(500)	NULL,
	PromotionalAmount	Money	NULL,
	PromotionalDesc	NVarchar(100)	NULL, 
    [EnterDateUtc] DATETIME NULL, 
    [DigitBridgeGuid] uniqueidentifier NOT NULL,

    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderHeaderPromotionUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line

 CONSTRAINT [PK_OrderHeaderPromotion] PRIMARY KEY CLUSTERED 
(
	[RowNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderHeaderPromotion] ADD  CONSTRAINT [DF_OrderHeaderPromotion_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderHeaderPromotion] ADD  CONSTRAINT [DF_OrderHeaderPromotion_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderHeaderPromotion] ADD  CONSTRAINT [DF_OrderHeaderPromotion_CentralOrderHeaderPromotionUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [CentralOrderHeaderPromotionUuid]
GO


