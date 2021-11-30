
-- 07/28/20201 By Jerry Z 
IF COL_LENGTH('OrderHeader', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderHeader ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderHeader_CentralOrderUuid] ON [dbo].[OrderHeader]
	(
		[CentralOrderUuid] ASC
	) 
END					

IF COL_LENGTH('OrderHeader', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE OrderHeader ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('OrderHeader', 'TotalDueSellerAmount') IS NULL					
BEGIN					
    ALTER TABLE OrderHeader ADD [TotalDueSellerAmount] MONEY NOT NULL DEFAULT 0
END					


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderHeader]') AND name = N'IX_OrderHeader_OriginalOrderDateUtc')
CREATE NONCLUSTERED INDEX [IX_OrderHeader_OriginalOrderDateUtc] ON [dbo].[OrderHeader]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[OriginalOrderDateUtc] ASC
) 
GO
