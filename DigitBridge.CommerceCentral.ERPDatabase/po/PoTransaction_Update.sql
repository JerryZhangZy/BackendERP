IF COL_LENGTH('PoTransaction', 'VendorCode') IS NULL
    BEGIN
        ALTER TABLE PoTransaction ADD [VendorCode] varchar(50) NOT NULL DEFAULT ''
    END

IF COL_LENGTH('PoTransaction', 'VendorName') IS NULL
    BEGIN
        ALTER TABLE PoTransaction ADD [VendorName] varchar(200) NOT NULL DEFAULT ''
    END


-- 11/21/2021 By Jerry
IF COL_LENGTH('PoTransaction', 'ShippingAmountAssign') IS NULL
BEGIN
    ALTER TABLE PoTransaction ADD [ShippingAmountAssign] INT NOT NULL DEFAULT 0
END

IF COL_LENGTH('PoTransaction', 'MiscAmountAssign') IS NULL
BEGIN
    ALTER TABLE PoTransaction ADD [MiscAmountAssign] INT NOT NULL DEFAULT 0
END
