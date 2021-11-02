CREATE TABLE [dbo].[PoTransactionItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [TransItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Invoice Return Item Line

    [TransUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for Invoice Transaction
    [Seq] INT NOT NULL DEFAULT 0, --Invoice Item Line sort sequence

    [PoUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for Invoice
    [PoItemUuid] VARCHAR(50)  NOT NULL DEFAULT '', --Global Unique Guid for Invoice

    [ItemType] INT NULL DEFAULT 0, --Invoice item type
    [ItemStatus] INT NULL DEFAULT 0, --Invoice item status
	[ItemDate] DATE NOT NULL, --Invoice date
	[ItemTime] TIME NOT NULL, --Invoice time

    [ProductUuid] VARCHAR(50) NULL DEFAULT '',--(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
    [InventoryUuid] VARCHAR(50) NULL DEFAULT '',--(Readonly) Inventory uuid. <br> Display: false, Editable: false
	[SKU] Varchar(100) NOT NULL,--Product SKU 
	[WarehouseUuid] VARCHAR(50) NULL, --Warehouse Guid
	[LotNum] Varchar(100) NOT NULL,--Product SKU Lot Number 
	[LotDescription] NVarchar(200) NOT NULL,--Invoice item description 
	[LotInDate] DATE NULL, --Lot receive Date
	[LotExpDate] DATE NULL, --Lot Expiration date
	[Description] NVarchar(200) NOT NULL,--Invoice item description 
	[Notes] NVarchar(500) NOT NULL,--Invoice item notes 

	[Currency] VARCHAR(10)  NOT NULL DEFAULT '',--(Ignore) po transaction price in currency. <br> Title: Currency, Display: false, Editable: false
	[UOM] Varchar(50)  NOT NULL DEFAULT '',--Product SKU Qty unit of measure 
	[TransQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Claim return Qty. 

	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Invoice price. 
	[ExtAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for Invoice items. 
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total Invoice tax amount (include shipping tax and misc tax) 
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice level discount amount, base on SubTotalAmount
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice total Charg Allowance Amount

	[Stockable] TINYINT NOT NULL DEFAULT 1,--Invoice item will update inventory instock qty 
	[IsAp] TINYINT NOT NULL DEFAULT 1,--Invoice item will add to invoice total amount
	[Taxable] TINYINT NOT NULL DEFAULT 0,--Invoice item will apply tax
	[Costable] TINYINT NOT NULL DEFAULT 0,--Invoice item will apply tax

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_PoTransactionItems] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransactionItems]') AND name = N'UI_PoTransactionItems_PoTransactionItemsId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PoTransactionItems_PoTransactionItemsUuid] ON [dbo].[PoTransactionItems]
(
	[TransItemUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransactionItems]') AND name = N'IX_PoTransactionItems_PoId')
CREATE NONCLUSTERED INDEX [IX_PoTransactionItems_PoUuid] ON [dbo].[PoTransactionItems]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransactionItems]') AND name = N'IX_PoTransactionItems_TransId_Seq')
CREATE NONCLUSTERED INDEX [IX_PoTransactionItems_TransUuid_Seq] ON [dbo].[PoTransactionItems]
(
	[TransUuid] ASC,
	[Seq] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransactionItems]') AND name = N'IX_PoTransactionItems_TransId')
CREATE NONCLUSTERED INDEX [IX_PoTransactionItems_TransUuid] ON [dbo].[PoTransactionItems]
(
	[TransUuid] ASC
) ON [PRIMARY]
GO


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransactionItems]') AND name = N'IX_PoTransactionItems_PoItemId')
CREATE NONCLUSTERED INDEX [IX_PoTransactionItems_PoItemUuid] ON [dbo].[PoTransactionItems]
(
	[PoItemUuid] ASC
) ON [PRIMARY]
GO



