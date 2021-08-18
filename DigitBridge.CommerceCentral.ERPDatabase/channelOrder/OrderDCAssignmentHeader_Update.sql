
-- 07/28/20201 By Jerry Z 
IF COL_LENGTH('OrderDCAssignmentHeader', 'OrderDCAssignmentUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentHeader ADD [OrderDCAssignmentUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderDCAssignmentHeader_OrderDCAssignmentUuid] ON [dbo].[OrderDCAssignmentHeader]
	(
		[OrderDCAssignmentUuid] ASC
	) 
END					

IF COL_LENGTH('OrderDCAssignmentHeader', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentHeader ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('OrderDCAssignmentHeader', 'DigitBridgeGuid') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentHeader ADD [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid())
END					

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderDCAssignmentHeader]') AND name = N'FK_OrderDCAssignmentHeader_CentralOrderUuid')
CREATE NONCLUSTERED INDEX [FK_OrderDCAssignmentHeader_CentralOrderUuid] ON [dbo].[OrderDCAssignmentHeader]
(
	[CentralOrderUuid] ASC
);
GO

ALTER TABLE [dbo].[OrderDCAssignmentHeader] ADD  CONSTRAINT [DF_OrderDCAssignmentHeader_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderDCAssignmentHeader] ADD  CONSTRAINT [DF_OrderDCAssignmentHeader_RowNum]  DEFAULT ((0)) FOR [RowNum]
GO

ALTER TABLE [dbo].[OrderDCAssignmentHeader] ADD  CONSTRAINT [DF_OrderDCAssignmentHeader_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderDCAssignmentHeader] ADD  CONSTRAINT [DF_OrderDCAssignmentHeader_OrderDCAssignmentUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [OrderDCAssignmentUuid]
GO

