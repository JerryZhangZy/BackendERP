-- 08/18/20201 By Jerry Z 
ALTER TABLE [dbo].[OrderLineMerchantExt] ADD  CONSTRAINT [DF_OrderLineMerchantExt_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderLineMerchantExt] ADD  CONSTRAINT [DF_OrderLineMerchantExt_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderLineMerchantExt] ADD  CONSTRAINT [DF_OrderLineMerchantExt_CentralOrderLineUuid]  DEFAULT ('') FOR [CentralOrderLineUuid]
GO

ALTER TABLE [dbo].[OrderLineMerchantExt] ADD  CONSTRAINT [DF_OrderLineMerchantExt_CentralOrderLineMerchantExtUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [CentralOrderLineMerchantExtUuid]
GO


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderLineMerchantExt]') AND name = N'UK_OrderLineMerchantExt_CentralOrderLineMerchantExtUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderLineMerchantExt_CentralOrderLineMerchantExtUuid] ON [dbo].[OrderLineMerchantExt]
(
	[CentralOrderLineMerchantExtUuid] ASC
);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderLineMerchantExt]') AND name = N'FK_OrderLineMerchantExt_CentralOrderUuid')
CREATE NONCLUSTERED INDEX [FK_OrderLineMerchantExt_CentralOrderUuid] ON [dbo].[OrderLineMerchantExt]
(
	[CentralOrderUuid] ASC, 
	[CentralOrderLineNum] ASC
);
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderLineMerchantExt]') AND name = N'FK_OrderLineMerchantExt_CentralOrderLineUuid')
CREATE NONCLUSTERED INDEX [FK_OrderLineMerchantExt_CentralOrderLineUuid] ON [dbo].[OrderLineMerchantExt]
(
	[CentralOrderLineUuid] ASC
);
GO
