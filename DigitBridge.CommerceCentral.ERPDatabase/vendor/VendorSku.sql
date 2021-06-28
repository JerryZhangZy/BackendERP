CREATE TABLE [dbo].[VendorSku]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [VendorSkuId] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Vendor Sku

    [VendorId] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Vendor
	[SKU] Varchar(100) NOT NULL,--Our Product SKU 
	[VendorSKU] Varchar(100) NULL,--Vendor Product SKU 

	[PackType] Varchar(50) NULL,--Product SKU Qty pack type, for example: Case, Box, Each 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty each per pack. 

	[PriceRule] VARCHAR(50) NOT NULL DEFAULT '', --Item Invoice price rule. 
	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Vendor SKU reserved price 
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --Vendor SKU reserved discount 
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for Invoice items. 
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice total Charg Allowance Amount

	[EffectStartDate] DATE NULL, --Reserve price start date
	[EffectEndDate] DATE NULL, --Reserve price start date

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_VendorSku] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VendorSku]') AND name = N'UI_Vendor_VendorId')
CREATE NONCLUSTERED INDEX [UI_Vendor_VendorId_SKU] ON [dbo].[VendorSku]
(
	[VendorId] ASC,
	[SKU] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VendorSku]') AND name = N'UI_Vendor_VendorId')
CREATE NONCLUSTERED INDEX [UI_Vendor_VendorId_VendorSKU] ON [dbo].[VendorSku]
(
	[VendorId] ASC,
	[VendorSKU] ASC
) ON [PRIMARY]
GO

