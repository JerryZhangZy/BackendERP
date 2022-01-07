CREATE TABLE [dbo].[OrderLineAdjustment]
(
	RowNum	BIGINT IDENTITY(1,1) NOT NULL,
	CentralOrderLineNum	bigint NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	CentralOrderNum	bigint NOT NULL,
	MasterAccountNum	int NOT NULL,
	ProfileNum	int NOT NULL,
	[ChannelNum] int NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL, --The unique number of this profile’s channel account
	AdjustQty	decimal(24,6) NULL,
	IsRestock	decimal(24,6) NULL,
	Reason	NVarchar(200) NULL,
	[ItemAdjustAmount]	money NULL,
	[TaxAdjustAmount]	money NULL,
	[ShippingAdjustAmount]	money NULL,
	[ShippinTaxAdjustAmount]	money NULL,
	[GiftWrapAdjustAmount]	money NULL,
	[GiftWrapTaxAdjustAmount]	money NULL,
	[RecyclingFeeAdjustAmount]	money NULL,
	AdjustmentType	varchar(50) NULL,
	SellerAdjustmentID	varchar(50) NULL,
	ChannelAdjustmentID	varchar(50) NULL,
	RmaNumber	varchar(50) NULL, --RMA Number for Buyer Initiated Returns.
	AdjustmentNotes	NVarchar(200) NULL,
	PublicNotes	NVarchar(200) NULL,
	CreateDateUtc	date NULL,
	RequestStatus	varchar(50) NULL,
	RestockStatus	varchar(50) NULL,
	ReturnShippingFee	money NULL,
	RestockingFee	money NULL,
	ReturnTracking	varchar(100) NULL, --Tracking number or tracking URL of the return shipment
	ReturnShippingMethod	varchar(50) NULL, 
    [EnterDateUtc] DATETIME NULL, 
    [DigitBridgeGuid] uniqueidentifier NOT NULL,
   
    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderLineUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line
    [CentralOrderLineAdjustmentUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line

 CONSTRAINT [PK_OrderLineAdjustment] PRIMARY KEY CLUSTERED 
(
	[RowNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderLineAdjustment] ADD  CONSTRAINT [DF_OrderLineAdjustment_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderLineAdjustment] ADD  CONSTRAINT [DF_OrderLineAdjustment_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderLineAdjustment] ADD  CONSTRAINT [DF_OrderLineAdjustment_CentralOrderLineUuid]  DEFAULT ('') FOR [CentralOrderLineUuid]
GO

ALTER TABLE [dbo].[OrderLineAdjustment] ADD  CONSTRAINT [DF_OrderLineAdjustment_CentralOrderLineAdjustmentUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [CentralOrderLineAdjustmentUuid]
GO