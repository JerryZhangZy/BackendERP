CREATE TABLE [dbo].[OrderDCAssignmentLine](
	[OrderDCAssignmentLineNum] [bigint] IDENTITY(1,1) NOT NULL,
	[DatabaseNum] [int] NOT NULL,
	[MasterAccountNum] [int] NOT NULL,
	[ProfileNum] [int] NOT NULL,
	[ChannelNum] [int] NOT NULL,
	[ChannelAccountNum] [int] NOT NULL,
	[OrderDCAssignmentNum] [bigint] NOT NULL,
	[CentralOrderNum] [bigint] NOT NULL,
	[CentralOrderLineNum] [bigint] NOT NULL,
	[ChannelOrderID] [varchar](130) NOT NULL,
	[CentralProductNum] [bigint] NULL,
	[DistributionProductNum] [bigint] NOT NULL,
	[SKU] [varchar](100) NULL,
	[ChannelItemID] [varchar](50) NULL,
	[OrderQty] [decimal](24, 6) NULL,
	[DBChannelOrderLineRowID] [varchar](50) NULL,
	[EnterDateUtc] [datetime] NULL,

    [RowNum]      BIGINT NOT NULL,
    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderLineUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [OrderDCAssignmentUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [OrderDCAssignmentLineUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
	CONSTRAINT [PK_OrderDCAssignmentLine] PRIMARY KEY CLUSTERED ([OrderDCAssignmentLineNum] ASC)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderDCAssignmentLine] ADD  CONSTRAINT [DF_OrderDCAssignmentLine_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderDCAssignmentLine] ADD  CONSTRAINT [DF_OrderDCAssignmentLine_RowNum]  DEFAULT ((0)) FOR [RowNum]
GO

ALTER TABLE [dbo].[OrderDCAssignmentLine] ADD  CONSTRAINT [DF_OrderDCAssignmentLine_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderDCAssignmentLine] ADD  CONSTRAINT [DF_OrderDCAssignmentLine_CentralOrderLineUuid]  DEFAULT ('') FOR [CentralOrderLineUuid]
GO

ALTER TABLE [dbo].[OrderDCAssignmentLine] ADD  CONSTRAINT [DF_OrderDCAssignmentLine_OrderDCAssignmentUuid]  DEFAULT ('') FOR [OrderDCAssignmentUuid]
GO

ALTER TABLE [dbo].[OrderDCAssignmentLine] ADD  CONSTRAINT [DF_OrderDCAssignmentLine_OrderDCAssignmentLineUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [OrderDCAssignmentLineUuid]
GO

CREATE NONCLUSTERED INDEX [DI_OrderDCAssignmentLine_CentralOrderNum]
    ON [dbo].[OrderDCAssignmentLine]([CentralOrderNum] ASC);
GO
