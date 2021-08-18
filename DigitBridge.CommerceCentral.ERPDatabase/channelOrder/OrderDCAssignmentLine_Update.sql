
-- 08/18/20201 By Jerry Z 
IF COL_LENGTH('OrderDCAssignmentLine', 'OrderDCAssignmentLineUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentLine ADD [OrderDCAssignmentLineUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
END					

IF COL_LENGTH('OrderDCAssignmentLine', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentLine ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('OrderDCAssignmentLine', 'OrderDCAssignmentUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentLine ADD [OrderDCAssignmentUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('OrderDCAssignmentLine', 'DigitBridgeGuid') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentLine ADD [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid())
END					


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderDCAssignmentLine]') AND name = N'UK_OrderDCAssignmentLine_OrderDCAssignmentLineUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderDCAssignmentLine_OrderDCAssignmentLineUuid] ON [dbo].[OrderDCAssignmentLine]
(
	[OrderDCAssignmentLineUuid] ASC
);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderDCAssignmentLine]') AND name = N'FK_OrderDCAssignmentLine_OrderDCAssignmentUuid')
CREATE NONCLUSTERED INDEX [FK_OrderDCAssignmentLine_OrderDCAssignmentUuid] ON [dbo].[OrderDCAssignmentLine]
(
	[OrderDCAssignmentUuid] ASC,
	[OrderDCAssignmentLineNum] ASC
);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderDCAssignmentLine]') AND name = N'FK_OrderDCAssignmentLine_OrderDCAssignmentNum')
CREATE NONCLUSTERED INDEX [FK_OrderDCAssignmentLine_OrderDCAssignmentNum] ON [dbo].[OrderDCAssignmentLine]
(
	[OrderDCAssignmentNum] ASC,
	[OrderDCAssignmentLineNum] ASC
);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderDCAssignmentLine]') AND name = N'BLK_OrderDCAssignmentLine_sku')
CREATE NONCLUSTERED INDEX [BLK_OrderDCAssignmentLine_sku] ON [dbo].[OrderDCAssignmentLine]
(
	[SKU] ASC
) 
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


/*
    UPDATE spp
    SET spp.OrderDCAssignmentUuid = sph.OrderDCAssignmentUuid
    FROM OrderDCAssignmentLine spp
    INNER JOIN OrderHeader sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/