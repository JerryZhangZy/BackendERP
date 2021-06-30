CREATE TABLE [dbo].[CustomerSku]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [CustomerSkuUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Customer Sku

    [CustomerUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Customer
	[ProductUuid] Varchar(100) NOT NULL,--Our Product SKU 
	[SKU] Varchar(100) NOT NULL,--Our Product SKU 
	[CustomerSKU] Varchar(100) NULL,--Customer Product SKU 

	[PackType] Varchar(50) NULL,--Product SKU Qty pack type, for example: Case, Box, Each 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty each per pack. 

	[PriceRule] VARCHAR(50) NOT NULL DEFAULT '', --Item Invoice price rule. 
	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Customer SKU reserved price 
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --Customer SKU reserved discount 
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for Invoice items. 
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice total Charg Allowance Amount

	[EffectStartDate] DATE NULL, --Reserve price start date
	[EffectEndDate] DATE NULL, --Reserve price start date

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_CustomerSku] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSku]') AND name = N'UI_Customer_CustomerId')
CREATE NONCLUSTERED INDEX [UI_Customer_CustomerUuid_SKU] ON [dbo].[CustomerSku]
(
	[CustomerUuid] ASC,
	[SKU] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomerSku]') AND name = N'UI_Customer_CustomerId')
CREATE NONCLUSTERED INDEX [UI_Customer_CustomerUuid_CustomerSKU] ON [dbo].[CustomerSku]
(
	[CustomerUuid] ASC,
	[CustomerSKU] ASC
) ON [PRIMARY]
GO

