CREATE TABLE [dbo].[InvoiceReturnItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [ReturnItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Invoice Return Item Line

    [TransUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for Invoice Transaction
    [Seq] INT NOT NULL DEFAULT 0, --Invoice Item Line sort sequence
    [InvoiceUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for Invoice
    [InvoiceItemsUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Invoice Item
    [ReturnItemType] INT NOT NULL DEFAULT 0, --Return item type
    [ReturnItemStatus] INT NOT NULL DEFAULT 0, --Return item status
	[ReturnDate] DATE NOT NULL, --Return date
	[ReturnTime] TIME NOT NULL, --Return time
	[ReceiveDate] DATE NULL, --Return item received date
	[StockDate] DATE NULL, --Stock Return Item Date

	[SKU] Varchar(100) NOT NULL,--Product SKU 
	[ProductUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Inventory Item Line
	[InventoryUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Inventory Item Line
	[WarehouseUuid] VARCHAR(50) NULL, --Warehouse Guid
	[LotNum] Varchar(100) NOT NULL,--Product SKU Lot Number 
	[Description] NVarchar(200) NOT NULL,--Invoice item description 
	[Notes] NVarchar(500) NOT NULL,--Invoice item notes 

	[Currency] VARCHAR(10) NOT NULL DEFAULT '',
	[UOM] Varchar(50) NOT NULL DEFAULT '',--Product SKU Qty unit of measure 
	[PackType] Varchar(50) NOT NULL DEFAULT '',--Product SKU Qty pack type, for example: Case, Box, Each 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty each per pack. 
	[ReturnPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Claim return Qty. 
	[ReceivePack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Receive return qty. 
	[StockPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Putback stock qty. 
	[NonStockPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Damage or not putback stock qty. 
	[ReturnQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Claim return Qty. 
	[ReceiveQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Receive return qty. 
	[StockQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Putback stock qty. 
	[NonStockQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Damage or not putback stock qty. 

	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Invoice price. 
	[DiscountRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice level discount amount, base on SubTotalAmount
	[DiscountPrice] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Invoice after discount price. 
	[ExtAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 
	[TaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should apply tax
	[NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Amount should not apply tax
	[TaxRate] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Default Tax rate for Invoice items. 
	[TaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total Invoice tax amount (include shipping tax and misc tax) 
	[ShippingAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Invoice total Charg Allowance Amount
	[Stockable] TINYINT NOT NULL DEFAULT 1,--Invoice item will update inventory instock qty 
	[IsAr] TINYINT NOT NULL DEFAULT 1,--Invoice item will add to invoice total amount
	[Taxable] TINYINT NOT NULL DEFAULT 0,--Invoice item will apply tax

    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_InvoiceReturnItems] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'UK_InvoiceReturnItems_InvoiceReturnItemId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceReturnItems_InvoiceReturnItemUuid] ON [dbo].[InvoiceReturnItems]
(
	[ReturnItemUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'FK_InvoiceReturnItems_TransId_Seq')
CREATE NONCLUSTERED INDEX [FK_InvoiceReturnItems_TransUuid_Seq] ON [dbo].[InvoiceReturnItems]
(
	[TransUuid] ASC,
	[Seq] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'IX_InvoiceReturnItems_TransId')
CREATE NONCLUSTERED INDEX [IX_InvoiceReturnItems_TransUuid] ON [dbo].[InvoiceReturnItems]
(
	[TransUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'IX_InvoiceReturnItems_InvoiceId_Seq')
CREATE NONCLUSTERED INDEX [FK_InvoiceReturnItems_InvoiceUuid_Seq] ON [dbo].[InvoiceReturnItems]
(
	[InvoiceUuid] ASC,
	[Seq] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'IX_InvoiceReturnItems_InvoiceId')
CREATE NONCLUSTERED INDEX [IX_InvoiceReturnItems_InvoiceUuid] ON [dbo].[InvoiceReturnItems]
(
	[InvoiceUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'IX_InvoiceReturnItems_InvoiceItemsId')
CREATE NONCLUSTERED INDEX [FK_InvoiceReturnItems_InvoiceItemsUuid] ON [dbo].[InvoiceReturnItems]
(
	[InvoiceItemsUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceReturnItems]') AND name = N'IX_InvoiceReturnItems_InventoryId')
CREATE NONCLUSTERED INDEX [IX_InvoiceItems_InventoryUuid] ON [dbo].[InvoiceReturnItems]
(
	[InventoryUuid] ASC
) ON [PRIMARY]
GO




