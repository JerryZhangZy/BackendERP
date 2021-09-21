CREATE TABLE [dbo].[ItemFromQuickBooks]
(	
	[ItemNum]	bigint NOT NULL IDENTITY(1000000, 1),
	DatabaseNum		int NULL,
	[Name]	varchar(100) NOT NULL, -- Sku in Central, required for QB, unique.
	FullyQualifiedName	NVARCHAR(100) NULL, -- prepends the topmost parent, followed by each sub element separated by colons. Takes the form of Item:SubItem.
	QbItemId	bigint NULL, -- Required for update in QB.
	[MasterAccountNum] int NULL,
	[ProfileNum] int NULL,
	CentralProductNum	bigint NULL,
	ResourceFrom	int NULL, -- 0: Central 1: QBO
	CentralSyncStatus	int NULL, -- 0: To be Synced to Central 1: Completed 2. Error
	QbSyncStatus	int NULL, -- 0: To be Synced to QBO 1: Completed 2. Error
	[Type]	varchar(100) NOT NULL, -- QB Item Type -> 0: Inventory 1: Group, bundleStatus in Central.
	SyncToken	NVARCHAR(2000) NULL, -- required for update to QB.
	InvStartDate	DateTime NULL, -- required for QB. enterDateUtc? in Central.
	QbCreateTime	DateTime NOT NULL, -- read only from QB.
	QbLastUpdatedTime	DateTime NOT NULL, -- read only from QB.
	[EnterDate] DateTime Default GETUTCDATE(), 
    [LastUpdate] DateTime NULL, 
	QtyOnHand	Decimal NOT NULL, -- required for Inventory Items.
	ReorderPoint	Decimal NULL, -- The minimum quantity of a particular inventory item that you need to restock.
	[Description]	NVARCHAR(4000) NULL, -- itemTitle? in Central.
	PurchaseDesc	NVARCHAR(1000) NULL, -- Purchase description for the item.
	PurchaseCost	Decimal NULL, -- Amount paid when buying or ordering the item.
	UnitPrice	Decimal NULL, --  Price/Rate column specify either unit price, a discount, or a tax rate for item. unitPrice in Central.
	AssetAccountRef	int NULL, -- QBO Inventory Asset Account ID, from Config.
	IncomeAccountRef	int NULL, -- QBO Posting Account ID, from Config.
	ExpenseAccountRef	int NULL, -- QBO Expense Account ID, from Config.
	TaxClassificationRef	int NULL, -- QBO Tax Classification Reference.
	PurchaseTaxCodeRef	int NULL, -- QBO Purchase Tax Code for the item.
	ParentRef	int NULL, -- QBO The immediate parent Item ID of the sub item in the hierarchical Item.
	PrefVendorRef	int NULL, -- Reference to the preferred QBO vendor.
	ClassRef	int NULL, -- QBO Class ID.
	SKU	varchar(100), -- itemTitle? in Central.
	SalesTaxIncluded	TINYINT NULL, -- 0: NO  1: YES.
	PurchaseTaxIncluded	TINYINT NULL, -- 0: NO  1: YES.
	SubItem	TINYINT NULL, -- Is subItem in QB ? 0: NO  1: YES.
	[Level]	int NULL, -- For SubItem: Level of the hierarchy in which the entity is located. Zero specifies the top.
	Taxable	TINYINT NULL, -- 0: NO  1: YES.
	Active	TINYINT NULL, -- 0: NO  1: YES.

 CONSTRAINT [PK_ItemFromQuickBooks] PRIMARY KEY CLUSTERED 
(
	[ItemNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

