CREATE TABLE [dbo].[InvoiceItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [InvoiceItemsId] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Invoice Item Line

    [InvoiceId] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Invoice
    [Seq] INT NOT NULL DEFAULT 0, --Invoice Item Line sort sequence
    [InvoiceItemType] INT NULL DEFAULT 0, --Invoice item type
    [InvoiceItemStatus] INT NULL DEFAULT 0, --Invoice item status
	[ItemDate] DATE NOT NULL, --Invoice date
	[ItemTime] TIME NOT NULL, --Invoice time
	[ShipDate] DATE NULL, --Estimated vendor ship date
	[EtaArrivalDate] DATE NULL, --Estimated date when item arrival to buyer 

	[SKU] Varchar(100) NOT NULL,--Product SKU 
	[InventoryId] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for Inventory Item Line
	[WarehouseID] VARCHAR(50) NULL, --Warehouse Guid
	[LotNum] Varchar(100) NOT NULL,--Product SKU Lot Number 
	[Description] NVarchar(200) NOT NULL,--Invoice item description 
	[Notes] NVarchar(500) NOT NULL,--Invoice item notes 

	[Currency] VARCHAR(10) NULL,
	[UOM] Varchar(50) NULL,--Product SKU Qty unit of measure 
	[PackType] Varchar(50) NULL,--Product SKU Qty pack type, for example: Case, Box, Each 
	[PackQty] DECIMAL(24, 6) NOT NULL DEFAULT 1, --Item Qty each per pack. 
	[OrderPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Order number of pack. 
	[ShipPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Shipped number of pack. 
	[CancelledPack] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Cancelled number of pack. 
	[OrderQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Order Qty. 
	[ShipQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Shipped Qty. 
	[CancelledQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Cancelled Qty. 

	[PriceRule] VARCHAR(50) NOT NULL DEFAULT '', --Item Invoice price rule. 
	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Invoice price. 
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice level discount amount, base on SubTotalAmount
	[DiscountPrice] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Invoice after discount price. 
	[ExtAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for Invoice items. 
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total Invoice tax amount (include shipping tax and misc tax) 
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Invoice total Charg Allowance Amount
	[ItemTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 

	[Stockable] TINYINT NOT NULL DEFAULT 1,--Invoice item will update inventory instock qty 
	[IsAr] TINYINT NOT NULL DEFAULT 1,--Invoice item will add to invoice total amount
	[Taxable] TINYINT NOT NULL DEFAULT 0,--Invoice item will apply tax
	[Costable] TINYINT NOT NULL DEFAULT 1,--Invoice item will calculate total cost

	[UnitCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Unit Cost. 
	[AvgCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Avg.Cost. 
	[LotCost] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Lot Cost. 
	[LotInDate] DATE NULL, --Lot receive Date
	[LotExpDate] DATE NULL, --Lot Expiration date

    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_InvoiceItems] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItems]') AND name = N'UK_InvoiceItems_InvoiceItemsId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InvoiceItems_InvoiceItemsId] ON [dbo].[InvoiceItems]
(
	[InvoiceItemsId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItems]') AND name = N'FK_InvoiceItems_InvoiceId_Seq')
CREATE NONCLUSTERED INDEX [FK_InvoiceItems_InvoiceId_Seq] ON [dbo].[InvoiceItems]
(
	[InvoiceId] ASC,
	[Seq] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItems]') AND name = N'BLK_InvoiceItems_InvoiceId_Seq')
CREATE NONCLUSTERED INDEX [BLK_InvoiceItems_InvoiceId_Seq] ON [dbo].[InvoiceItems]
(
	[SKU] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItems]') AND name = N'IX_InvoiceItems_InvoiceId')
CREATE NONCLUSTERED INDEX [IX_InvoiceItems_InvoiceId] ON [dbo].[InvoiceItems]
(
	[InvoiceId] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceItems]') AND name = N'IX_InvoiceItems_InventoryId')
CREATE NONCLUSTERED INDEX [IX_InvoiceItems_InventoryId] ON [dbo].[InvoiceItems]
(
	[InventoryId] ASC
) ON [PRIMARY]
GO


