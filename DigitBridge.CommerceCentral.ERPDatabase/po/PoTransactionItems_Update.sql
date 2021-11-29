
-- 11/21/2021 By Jerry
IF COL_LENGTH('PoTransactionItems', 'WarehouseCode') IS NULL
BEGIN
    ALTER TABLE PoTransactionItems ADD [WarehouseCode] VARCHAR(50) NOT NULL DEFAULT ''
END

IF COL_LENGTH('PoTransactionItems', 'PoPrice') IS NULL
BEGIN
    ALTER TABLE PoTransactionItems ADD [PoPrice] DECIMAL(24, 6) NOT NULL DEFAULT 0
END

IF COL_LENGTH('PoTransactionItems', 'DiscountPrice') IS NULL
BEGIN
    ALTER TABLE PoTransactionItems ADD [DiscountPrice] DECIMAL(24, 6) NOT NULL DEFAULT 0
END

IF COL_LENGTH('PoTransactionItems', 'BaseCost') IS NULL
BEGIN
    ALTER TABLE PoTransactionItems ADD [BaseCost] DECIMAL(24, 6) NOT NULL DEFAULT 0
END

IF COL_LENGTH('PoTransactionItems', 'UnitCost') IS NULL
BEGIN
    ALTER TABLE PoTransactionItems ADD [UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0
END

 

IF COL_LENGTH('PoTransactionItems', 'PoNum') IS NULL					
BEGIN					
    ALTER TABLE PoTransactionItems ADD [PoNum] VARCHAR(50) NOT NULL DEFAULT ''
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_SalesOrderUuid')
	CREATE NONCLUSTERED INDEX [IX_PoTransactionItems_PoNum] ON [dbo].[PoTransactionItems]
	(
		[TPoNum] ASC
	) 
END			