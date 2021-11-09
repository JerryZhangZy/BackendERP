IF COL_LENGTH('PoTransaction', 'VendorCode') IS NULL
    BEGIN
        ALTER TABLE PoTransaction ADD [VendorCode] varchar(50) NOT NULL DEFAULT ''
    END

IF COL_LENGTH('PoTransaction', 'VendorName') IS NULL
    BEGIN
        ALTER TABLE PoTransaction ADD [VendorName] varchar(200) NOT NULL DEFAULT ''
    END