CREATE TABLE [dbo].[OrderDCAssignmentHeader](
	[OrderDCAssignmentNum] [bigint] IDENTITY(1,1) NOT NULL,
	[DatabaseNum] [int] NOT NULL,
	[MasterAccountNum] [int] NULL,
	[ProfileNum] [int] NULL,
	[ChannelNum] [int] NULL,
	[ChannelAccountNum] [int] NULL,
	[CentralOrderNum] [bigint] NULL,
	[ChannelOrderID] [varchar](50) NULL,
	[ShippingCost] [money] NULL,
	[InsuranceCost] [money] NULL,
	[TaxCost] [money] NULL,
	[FulfillmentType] [int] NULL,
	[DistributionCenterNum] [int] NULL,
	[SellerWarehouseID] [varchar](50) NULL,
	[UseSystemShippingLabel] [int] NULL,
	[UseChannelPackingSlip] [int] NULL,
	[UseSystemReturnLabel] [int] NULL,
	[ShippingLabelFormat] [int] NULL,
	[ReturnLabelFormat] [int] NULL,
	[DBChannelOrderHeaderRowID] [varchar](50) NULL,
	[FulfillmentProcessStatus] [int] NULL,
	[IntegrationStatus] [int] NULL,
	[IntegrationDateUtc] [datetime] NULL,
	[EnterDateUtc] [datetime] NULL,

    [RowNum]      BIGINT NOT NULL,
    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [OrderDCAssignmentUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
 CONSTRAINT [PK_OrderDCAssignmentHeader] PRIMARY KEY CLUSTERED 
(
	[OrderDCAssignmentNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderDCAssignmentHeader] ADD  CONSTRAINT [DF_OrderDCAssignmentHeader_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderDCAssignmentHeader] ADD  CONSTRAINT [DF_OrderDCAssignmentHeader_RowNum]  DEFAULT ((0)) FOR [RowNum]
GO

ALTER TABLE [dbo].[OrderDCAssignmentHeader] ADD  CONSTRAINT [DF_OrderDCAssignmentHeader_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderDCAssignmentHeader] ADD  CONSTRAINT [DF_OrderDCAssignmentHeader_OrderDCAssignmentUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [OrderDCAssignmentUuid]
GO

CREATE NONCLUSTERED INDEX [DI_OrderDCAssignmentHeader_DatabaseNum_CentralOrderNum_DistributionCenterNum]
    ON [dbo].[OrderDCAssignmentHeader]([DatabaseNum] ASC, [CentralOrderNum] ASC)
    INCLUDE([DistributionCenterNum]);
GO