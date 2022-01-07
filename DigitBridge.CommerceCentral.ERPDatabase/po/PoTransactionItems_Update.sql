
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


IF COL_LENGTH('PoTransactionItems', 'PoUuid') IS NULL
BEGIN
    ALTER TABLE PoTransactionItems ADD [PoUuid] VARCHAR(50) NOT NULL DEFAULT ''
END

IF COL_LENGTH('PoTransactionItems', 'PoNum') IS NULL
BEGIN
    ALTER TABLE PoTransactionItems ADD [PoNum] VARCHAR(50) NOT NULL DEFAULT ''
END

IF COL_LENGTH('PoTransactionItems', 'PoItemUuid') IS NULL
BEGIN
    ALTER TABLE PoTransactionItems ADD [PoItemUuid] VARCHAR(50)  NOT NULL DEFAULT ''
END

