CREATE TABLE [dbo].[PoItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [PoItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for P/O Item Line

    [PoUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O
    [Seq] INT NOT NULL DEFAULT 0, --P/O Item Line sort sequence

    [PoItemType] INT NULL DEFAULT 0, --P/O item type
    [PoItemStatus] INT NULL DEFAULT 0, --P/O item status
	[PoDate] DATE NOT NULL, --P/O date
	[PoTime] TIME NOT NULL, --P/O time
	[EtaShipDate] DATE NULL, --Estimated vendor ship date
	[EtaArrivalDate] DATE NULL, --Estimated date when item arrival to buyer 
	[CancelDate] DATE NULL, --Usually it is related to shipping instruction

	[ProductUuid] Varchar(50) NOT NULL,--Product product uuid 
	[InventoryUuid] Varchar(50) NOT NULL,--Product Inventory uuid 
	[SKU] Varchar(100) NOT NULL,--Product SKU 
	[Description] NVarchar(200) NOT NULL,--P/O item description 
	[Notes] NVarchar(500) NOT NULL,--P/O item notes 

	[Currency] VARCHAR(10) NULL,
	[PoQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item P/O Qty. 
	[ReceivedQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Received Qty. 
	[CancelledQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item Cancelled Qty. 
	[PriceRule] VARCHAR(50) NOT NULL DEFAULT '', --Item P/O price rule. 
	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item P/O price. 
	[ExtAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount. 
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for P/O items. 
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total P/O tax amount (include shipping tax and misc tax) 
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount amount, base on SubTotalAmount
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O total Charg Allowance Amount

	[Stockable] TINYINT NOT NULL DEFAULT 1,--P/O item will update inventory instock qty 
	[Costable] TINYINT NOT NULL DEFAULT 1,--P/O item will update inventory cost
	[Taxable] TINYINT NOT NULL DEFAULT 0,--P/O item will apply tax
	[IsAp] TINYINT NOT NULL DEFAULT 0,--P/O item will apply to total amount 

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_PoItems] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItems]') AND name = N'UI_PoItems_PoItemId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PoItems_PoItemUuid] ON [dbo].[PoItems]
(
	[PoItemUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItems]') AND name = N'UI_PoItems_PoId_Seq')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PoItems_PoUuid_Seq] ON [dbo].[PoItems]
(
	[PoUuid] ASC,
	[Seq] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItems]') AND name = N'UI_PoItems_PoId')
CREATE NONCLUSTERED INDEX [UI_PoItems_PoUuid] ON [dbo].[PoItems]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO



