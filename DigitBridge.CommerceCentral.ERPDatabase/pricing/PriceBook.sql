CREATE TABLE [dbo].[PriceBook]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [PriceBookUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Order Item Line uuid. <br> Display: false, Editable: false
	[PriceCode] Varchar(50) NOT NULL DEFAULT '', --VariationSet Code. <br> Title: Variation Set, Display: true, Editable: true

	[SKU] Varchar(100) NOT NULL DEFAULT '', --Product SKU. <br> Title: SKU, Display: true, Editable: true
	[ProductUuid] VARCHAR(50) NOT NULL, --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
	[InventoryUuid] VARCHAR(50) NOT NULL, --(Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false

	[BusinessType] VARCHAR(50) NOT NULL DEFAULT '', --Customer business type. <br> Title: Business Type, Display: true, Editable: true
	[PriceType] VARCHAR(50) NOT NULL DEFAULT '', --Customer price type. <br> Title: Price Type, Display: true, Editable: true

    [CustomerUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Customer uuid. <br> Display: false, Editable: false.
	[CustomerCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable customer number, unique in same database and profile. <br> Parameter should pass ProfileNum-CustomerCode. <br> Title: Customer Number, Display: true, Editable: true
	[CustomerName] NVARCHAR(200) NOT NULL DEFAULT '', --Customer name. <br> Title: Name, Display: true, Editable: true

	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --(Ignore)  
	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item unit price. <br> Title: Unit Price, Display: true, Editable: true
	[DiscountRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level discount rate. <br> Title: Discount Rate, Display: true, Editable: true
	[DiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level discount amount. <br> Title: Discount Amount, Display: true, Editable: true
	[MarkdownRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level discount rate. <br> Title: Discount Rate, Display: true, Editable: true
	[MarkupRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level discount rate. <br> Title: Discount Rate, Display: true, Editable: true
	[FromQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Qty price . <br> Title: Unit Price, Display: true, Editable: true
	[ToQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item unit price. <br> Title: Unit Price, Display: true, Editable: true

    [UpdateDateUtc] DATETIME NULL, --(Ignore) 
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore) 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
   CONSTRAINT [PK_PriceBook] PRIMARY KEY ([RowNum]), 
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceBook]') AND name = N'UK_PriceBook_PriceBookUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PriceBook_PriceBookUuid] ON [dbo].[PriceBook]
(
	[PriceBookUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceBook]') AND name = N'FK_PriceBook_SalesOrderUuid_Seq')
CREATE NONCLUSTERED INDEX [FK_PriceBook_SalesOrderUuid_Seq] ON [dbo].[PriceBook]
(
	[SalesOrderUuid] ASC,
	[Seq] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceBook]') AND name = N'BLK_PriceBook_SalesOrderUuid_Seq')
CREATE NONCLUSTERED INDEX [BLK_PriceBook_SalesOrderUuid_Seq] ON [dbo].[PriceBook]
(
	[SKU] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceBook]') AND name = N'IX_PriceBook_SalesOrderUuid')
CREATE NONCLUSTERED INDEX [IX_PriceBook_SalesOrderUuid] ON [dbo].[PriceBook]
(
	[SalesOrderUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PriceBook]') AND name = N'IX_PriceBook_InventoryUuid')
CREATE NONCLUSTERED INDEX [IX_PriceBook_InventoryUuid] ON [dbo].[PriceBook]
(
	[InventoryUuid] ASC
) 
GO


