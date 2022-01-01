CREATE TABLE [dbo].[OrderLineBundleComponent]
(
	RowNum	BIGINT IDENTITY(1,1) NOT NULL,
	CentralOrderLineNum	bigint NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	CentralOrderNum	bigint NOT NULL,
	MasterAccountNum	int NOT NULL,
	ProfileNum	int NOT NULL,
	[ChannelNum] int NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL, --The unique number of this profile’s channel account
	CentralItemNum	int NULL, --component centeral item number
	BundleCentralItemNum	int NULL,
	[SKU]	Varchar(50) NULL, --component SKU
	[BundleSku]	Varchar(50) NULL,
	Title	Varchar(120) NULL,
	OrderQty	decimal(24,6) NULL, --The number of units ordered (value is provided in the order already multiplied by OrderItem Quantity for the Bundle SKU).
    [EnterDateUtc] DATETIME NULL, 
    [DigitBridgeGuid] uniqueidentifier NOT NULL, 
   
    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderLineUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line
    [CentralOrderLineBundleComponentUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line

 CONSTRAINT [PK_OrderLineBundleComponent] PRIMARY KEY CLUSTERED 
(
	[RowNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderLineBundleComponent] ADD  CONSTRAINT [DF_OrderLineBundleComponent_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderLineBundleComponent] ADD  CONSTRAINT [DF_OrderLineBundleComponent_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderLineBundleComponent] ADD  CONSTRAINT [DF_OrderLineBundleComponent_CentralOrderLineUuid]  DEFAULT ('') FOR [CentralOrderLineUuid]
GO

ALTER TABLE [dbo].[OrderLineBundleComponent] ADD  CONSTRAINT [DF_OrderLineBundleComponent_CentralOrderLineBundleComponentUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [CentralOrderLineBundleComponentUuid]
GO
