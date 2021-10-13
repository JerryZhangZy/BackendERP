CREATE TABLE [dbo].[InvoiceReturnItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [ReturnItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) Invoice Return Item Line uuid. <br> Display: false, Editable: false

    [TransUuid] VARCHAR(50) NOT NULL DEFAULT '', --Invoice Transaction uuid. <br> Display: false, Editable: false.
    [Seq] INT NOT NULL DEFAULT 0, --Item Line sequence number. <br> Title: Line#, Display: true, Editable: false
    [InvoiceUuid] VARCHAR(50) NOT NULL, --Invoice uuid. <br> Display: false, Editable: false.
    [InvoiceItemsUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Invoice Item Line uuid. <br> Display: false, Editable: false
    [ReturnItemType] INT NOT NULL DEFAULT 0, --Return item type. <br> Title: Type, Display: true, Editable: true
    [ReturnItemStatus] INT NOT NULL DEFAULT 0, --Return item status. <br> Title: Status, Display: true, Editable: true
	[ReturnDate] DATE NOT NULL, --Return date. <br> Title: Date, Display: true, Editable: true
	[ReturnTime] TIME NOT NULL, --Return time. <br> Title: Time, Display: true, Editable: true
	[ReceiveDate] DATE NULL, --Return item received date. <br> Title: Receive Date, Display: true, Editable: true
	[StockDate] DATE NULL, --Stock Return Item Date. <br> Title: Processed Date, Display: true, Editable: true

	[SKU] Varchar(100) NOT NULL DEFAULT '', --Product SKU. <br> Title: SKU, Display: true, Editable: true
	[ProductUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
	[InventoryUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
	[InvoiceWarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) invoice Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
	[InvoiceWarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable invoice warehouse code, load from inventory data. <br> Title: Invoice Warehouse Code, Display: true, Editable: true
	[WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) return Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
	[WarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable return warehouse code, load from inventory data. <br> Title: Return Warehouse Code, Display: true, Editable: true
	[LotNum] Varchar(100) NOT NULL DEFAULT '', --Lot Number. <br> Title: Lot Number, Display: true, Editable: true 
	[Reason] NVarchar(200) NOT NULL DEFAULT '', --Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
	[Description] NVarchar(200) NOT NULL DEFAULT '', --Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
	[Notes] NVarchar(500) NOT NULL DEFAULT '', --item line notes. <br> Title: Notes, Display: true, Editable: true

	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --(Ignore)  
	[UOM] Varchar(50) NOT NULL DEFAULT '', --(Readonly) Product unit of measure, load from ProductBasic data. <br> Title: UOM, Display: true, Editable: false
	[PackType] Varchar(50) NOT NULL DEFAULT '', --(Ignore) Product SKU Qty pack type, for example: Case, Box, Each 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --(Ignore) Item Qty each per pack. 
	[ReturnPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item Claim return number of pack.
	[ReceivePack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Receive return number of pack. 
	[StockPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Putback stock number of pack. 
	[NonStockPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Damage or not putback stock number of pack. 
	[ReturnQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Claim return Qty. <br> Title: Claim Qty, Display: true, Editable: true 
	[ReceiveQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Receive return qty. <br> Title: Receive Qty, Display: true, Editable: true 
	[StockQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Putback stock qty. <br> Title: Putback Qty, Display: true, Editable: true 
	[NonStockQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Damage or not putback stock qty. <br> Title: Damage Qty, Display: true, Editable: true 
	[PutBackWarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) putback Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
	[PutBackWarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable putback warehouse code, load from inventory data. <br> Title: Putback Warehouse Code, Display: true, Editable: true
	[DamageWarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Damage Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
	[DamageWarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable Damage warehouse code, load from inventory data. <br> Title: Damage Warehouse Code, Display: true, Editable: true

	[InvoiceDiscountPrice] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item invoice after discount price. <br> Title: Unit Price, Title: Invoice Discount Price, Display: true, Editable: false
	[InvoiceDiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item invoice discount amount. <br> Title: Item total discount amount, Title: Invoice item discount amount, Display: true, Editable: false
	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item return price. <br> Title: Return Price, Display: true, Editable: true
	[ExtAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. <br> Title: Ext.Amount, Display: true, Editable: false
	[TaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should apply tax. <br> Display: false, Editable: false
	[NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should not apply tax. <br> Display: false, Editable: false
	[TaxRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default Tax rate for item. <br> Display: false, Editable: false
	[TaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level tax amount (include shipping tax and misc tax). <br> Display: false, Editable: false
	[ShippingAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level Shipping fee for this item. <br> Display: false, Editable: false
	[ShippingTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Item level tax amount for shipping fee. <br> Title: Shipping Tax, Display: false, Editable: false
	[MiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level handling charge. <br> Title: Handling, Display: false, Editable: true 
	[MiscTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Item level tax amount for handling charge. <br> Title: Handling Tax, Display: false, Editable: false
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item level other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: false, Editable: true

	[Stockable] TINYINT NOT NULL DEFAULT 1, --item will update inventory instock qty. <br> Title: Stockable, Display: true, Editable: true
	[IsAr] TINYINT NOT NULL DEFAULT 1, --item will add to Invoice total amount. <br> Title: A/R, Display: true, Editable: true
	[Taxable] TINYINT NOT NULL DEFAULT 0, --item will apply tax. <br> Title: Taxable, Display: true, Editable: true

    [UpdateDateUtc] DATETIME NULL, --(Ignore) 
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore) 
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore) 
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
    CONSTRAINT [PK_InvoiceReturnItems] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'UK_InvoiceReturnItems_InvoiceReturnItemId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceReturnItems_InvoiceReturnItemUuid] ON [dbo].[InvoiceReturnItems]
(
	[ReturnItemUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'FK_InvoiceReturnItems_TransId_Seq')
CREATE NONCLUSTERED INDEX [FK_InvoiceReturnItems_TransUuid_Seq] ON [dbo].[InvoiceReturnItems]
(
	[TransUuid] ASC,
	[Seq] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'IX_InvoiceReturnItems_TransId')
CREATE NONCLUSTERED INDEX [IX_InvoiceReturnItems_TransUuid] ON [dbo].[InvoiceReturnItems]
(
	[TransUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'IX_InvoiceReturnItems_InvoiceId_Seq')
CREATE NONCLUSTERED INDEX [FK_InvoiceReturnItems_InvoiceUuid_Seq] ON [dbo].[InvoiceReturnItems]
(
	[InvoiceUuid] ASC,
	[Seq] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'IX_InvoiceReturnItems_InvoiceId')
CREATE NONCLUSTERED INDEX [IX_InvoiceReturnItems_InvoiceUuid] ON [dbo].[InvoiceReturnItems]
(
	[InvoiceUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'IX_InvoiceReturnItems_InvoiceItemsId')
CREATE NONCLUSTERED INDEX [FK_InvoiceReturnItems_InvoiceItemsUuid] ON [dbo].[InvoiceReturnItems]
(
	[InvoiceItemsUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'IX_InvoiceReturnItems_InventoryId')
CREATE NONCLUSTERED INDEX [IX_InvoiceItems_InventoryUuid] ON [dbo].[InvoiceReturnItems]
(
	[InventoryUuid] ASC
) 
GO




