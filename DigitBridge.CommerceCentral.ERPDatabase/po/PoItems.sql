CREATE TABLE [dbo].[PoItems]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [PoItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for P/O Item Line. <br> Display: false, Editable: false

    [PoUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O. <br> Display: false, Editable: false
    [Seq] INT NOT NULL DEFAULT 0, --P/O Item Line sort sequence. <br> Title: Line#, Display: true, Editable: false

    [PoItemType] INT NULL DEFAULT 0, --P/O item type.<br> Title: Type, Display: true, Editable: true
    [PoItemStatus] INT NULL DEFAULT 0, --P/O item status. <br> Title: Status, Display: true, Editable: true
	[PoDate] DATE NOT NULL, --(Ignore) P/O date
	[PoTime] TIME NOT NULL, --(Ignore) P/O time
	[EtaShipDate] DATE NULL, --Estimated vendor ship date . <br> Title: Ship Date, Display: true, Editable: true
	[EtaArrivalDate] DATE NULL, --Estimated date when item arrival to buyer. <br> Title: Arrival Date, Display: true, Editable: true
	[CancelDate] DATE NULL, --Usually it is related to shipping instruction. <br> Title: Cancel Date, Display: true, Editable: true

	[ProductUuid] Varchar(50) NOT NULL DEFAULT '',--(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
	[InventoryUuid] Varchar(50) NOT NULL DEFAULT '',--(Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
	[SKU] Varchar(100) NOT NULL DEFAULT '', --Product SKU. <br> Title: SKU, Display: true, Editable: true
	[Description] NVarchar(200) NOT NULL DEFAULT '', --Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
	[Notes] NVarchar(500) NOT NULL DEFAULT '',--P/O item notes . <br> Title: Notes, Display: true, Editable: true

	[ItemTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount include all. <br> Display: false, Editable: false
	[Currency] VARCHAR(10) NOT NULL DEFAULT '', --(Ignore)  
	[PoQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item P/O Qty.
	[QtyForOther] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item P/O Qty.
	[ReceivedQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item Received Qty. 
	[CancelledQty] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Ignore) Item Cancelled Qty. 
	[PriceRule] VARCHAR(50) NOT NULL DEFAULT '',  --Item P/O price rule. <br> Title: Price Type, Display: true, Editable: true
	[Price] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item P/O price.  <br> Title: Unit Price, Display: true, Editable: true
	[ExtAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item total amount.  <br> Title: Ext.Amount, Display: true, Editable: false
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for P/O items.  <br> Display: false, Editable: false
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total P/O tax amount (include shipping tax and misc tax) . <br> Display: false, Editable: false
	[DiscountPrice] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Item after discount price. <br> Title: Discount Price, Display: true, Editable: false
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount rate. <br> Title: Discount Rate, Display: true, Editable: true
	[DiscountAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount amount, base on SubTotalAmount.<br> Title: Discount Amount, Display: true, Editable: true
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total shipping fee for all items. <br> Display: false, Editable: false
	[ShippingTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of shipping fee. <br> Display: false, Editable: false
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O handling charge . <br> Display: false, Editable: false
	[MiscTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of handling charge. <br> Display: false, Editable: false
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O total Charg Allowance Amount. <br> Display: false, Editable: false

	[Stockable] TINYINT NOT NULL DEFAULT 1,--P/O item will update inventory instock qty . <br> Title: Stockable, Display: true, Editable: true
	[Costable] TINYINT NOT NULL DEFAULT 1,--P/O item will update inventory cost. <br> Title: Apply Cost, Display: true, Editable: true
	[Taxable] TINYINT NOT NULL DEFAULT 0,--P/O item will apply tax. <br> Title: Taxable, Display: true, Editable: true
	[TaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Amount should apply tax. <br> Title: Taxable Amount, Display: true, Editable: false
	[NonTaxableAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --(Readonly) Amount should not apply tax. <br> Title: NonTaxable, Display: true, Editable: false
	[IsAp] TINYINT NOT NULL DEFAULT 0,--P/O item will apply to total amount . <br> Title: A/P, Display: true, Editable: true
	
	[WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
	[WarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable warehouse code, load from inventory data. <br> Title: Warehouse Code, Display: true, Editable: true
	
    [EnterDateUtc] DATETIME NULL, --(Ignore)  
    [UpdateDateUtc] DATETIME NULL, --(Ignore)  
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore)  
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Ignore)  
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)  
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
CREATE NONCLUSTERED INDEX [UI_PoItems_PoUuid_Seq] ON [dbo].[PoItems]
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



