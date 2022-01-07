CREATE TABLE [dbo].[OrderLineMiscExt]
(
	RowNum	BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	CentralOrderNum	bigint NOT NULL,
	MasterAccountNum	int NOT NULL,
	ProfileNum	int NOT NULL,
	[ChannelNum] int NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL, --The unique number of this profile’s channel account
	ChannelOrderID	varchar(130) NULL,
	ChannelListID	Varchar(50) NULL,
	ItemUrl	Varchar(256) NULL, --The URL of the order item on the origin website.
	ShoppingCartSKU	Varchar(50) NULL, 
    [EnterDateUtc] DATETIME NULL, 
    [DigitBridgeGuid] uniqueidentifier NOT NULL, 
   
    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderLineMiscExtUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line
	[LineDueToSellerAmount] [money] NOT NULL,
	[LineCommissionAmount] [money] NOT NULL,
	[LineCommissionTaxAmount] [money] NOT NULL,
	[LineRemittedTaxAmount] [money] NOT NULL,
	[LineAdditionalInfo] [nvarchar](max) NOT NULL,

 CONSTRAINT [PK_OrderLineMiscExt] PRIMARY KEY CLUSTERED 
(
	[RowNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderLineMiscExt] ADD  CONSTRAINT [DF_OrderLineMiscExt_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderLineMiscExt] ADD  CONSTRAINT [DF_OrderLineMiscExt_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderLineMiscExt] ADD  CONSTRAINT [DF_OrderLineMiscExt_CentralOrderLineMiscExtUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [CentralOrderLineMiscExtUuid]
GO

ALTER TABLE [dbo].[OrderLineMiscExt] ADD  CONSTRAINT [DF_OrderLineMiscExt_LineDueToSellerAmount]  DEFAULT ((0)) FOR [LineDueToSellerAmount]
GO

ALTER TABLE [dbo].[OrderLineMiscExt] ADD  CONSTRAINT [DF_OrderLineMiscExt_LineCommissionAmount]  DEFAULT ((0)) FOR [LineCommissionAmount]
GO

ALTER TABLE [dbo].[OrderLineMiscExt] ADD  CONSTRAINT [DF_OrderLineMiscExt_LineCommissionTaxAmount]  DEFAULT ((0)) FOR [LineCommissionTaxAmount]
GO

ALTER TABLE [dbo].[OrderLineMiscExt] ADD  CONSTRAINT [DF_OrderLineMiscExt_LineRemittedTaxAmount]  DEFAULT ((0)) FOR [LineRemittedTaxAmount]
GO

ALTER TABLE [dbo].[OrderLineMiscExt] ADD  CONSTRAINT [DF_OrderLineMiscExt_LineAdditionalInfo]  DEFAULT ('') FOR [LineAdditionalInfo]
GO

