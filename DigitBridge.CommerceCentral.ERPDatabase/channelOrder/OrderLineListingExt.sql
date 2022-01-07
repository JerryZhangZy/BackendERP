CREATE TABLE [dbo].[OrderLineListingExt]
(
	[RowNum]	BIGINT IDENTITY(1,1) NOT NULL,
	[CentralOrderLineNum]	bigint NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[CentralOrderNum]	bigint NOT NULL,
	[MasterAccountNum]	int NOT NULL,
	[ProfileNum]	int NOT NULL,
	[ChannelNum] int NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL, --The unique number of this profile’s channel account
	[ChannelOrderID]	varchar(130) NULL,
	[ChannelListID]	Varchar(50) NULL, 
    [EnterDateUtc] DATETIME NULL, 
    [DigitBridgeGuid] uniqueidentifier NOT NULL, 
   
    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderLineUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line
    [CentralOrderLineListingExtUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line

 CONSTRAINT [PK_OrderLineListingExt] PRIMARY KEY CLUSTERED 
(
	[RowNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderLineListingExt] ADD  CONSTRAINT [DF_OrderLineListingExt_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderLineListingExt] ADD  CONSTRAINT [DF_OrderLineListingExt_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderLineListingExt] ADD  CONSTRAINT [DF_OrderLineListingExt_CentralOrderLineUuid]  DEFAULT ('') FOR [CentralOrderLineUuid]
GO

ALTER TABLE [dbo].[OrderLineListingExt] ADD  CONSTRAINT [DF_OrderLineListingExt_CentralOrderLineListingExtUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [CentralOrderLineListingExtUuid]
GO

