
-- 11/21/2021 By Jerry
IF COL_LENGTH('PoTransactionItems', 'WarehouseCod') IS NULL
BEGIN
    ALTER TABLE PoTransaction ADD [WarehouseCode] VARCHAR(50) NOT NULL DEFAULT ''
END

IF COL_LENGTH('PoTransactionItems', 'PoPrice') IS NULL
BEGIN
    ALTER TABLE PoTransaction ADD [PoPrice] DECIMAL(24, 6) NOT NULL DEFAULT 0
END

