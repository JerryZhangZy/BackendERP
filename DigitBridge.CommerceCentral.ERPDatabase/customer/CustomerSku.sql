CREATE TABLE [dbo].[CustomerSku]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [CustomerSkuUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --line uuid. <br> Display: false, Editable: false.

    [CustomerUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Customer uuid. <br> Display: false, Editable: false.
	[ProductUuid] Varchar(100) NOT NULL, --(Readonly) Our Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
	[SKU] Varchar(100) NOT NULL, --Our Product SKU. load from ProductBasic data. <br> Title: Our Sku, Display: true, Editable: true
	[CustomerSKU] Varchar(100) NULL, --Customer Product SKU. load from ProductBasic data. <br> Title: Customer Sku, Display: true, Editable: true

	[PackType] Varchar(50) NOT NULL DEFAULT '', --Product SKU Qty pack type, for example: Case, Box, Each 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty each per pack. 

	[PriceRule] VARCHAR(50) NOT NULL DEFAULT '', --Item price rule. <br> Title: Price Type, Display: true, Editable: true
	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item unit price. <br> Title: Unit Price, Display: true, Editable: true
	[DiscountRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level discount rate. <br> Title: Discount Rate, Display: true, Editable: true
	[TaxRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default Tax rate for item. <br> Title: Tax Rate, Display: true, Editable: true
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level Charge and Allowance Amount. <br> Title: Charge and Allowance, Display: true, Editable: true

	[EffectStartDate] DATE NULL, --Reserve price start date. <br> Title: Start Date, Display: true, Editable: true
	[EffectEndDate] DATE NULL, --Reserve price start date. <br> Title: End Date, Display: true, Editable: true

    [UpdateDateUtc] DATETIME NULL, --(Ignore) 
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore) 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
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

