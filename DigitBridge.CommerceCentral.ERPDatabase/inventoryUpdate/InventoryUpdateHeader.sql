CREATE TABLE [dbo].[InventoryUpdateHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [InventoryUpdateUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --InventoryUpdate uuid. <br> Display: false, Editable: false.
	[BatchNumber] VARCHAR(50) NOT NULL DEFAULT '', --Readable InventoryUpdate number, unique in same database and profile. <br> Parameter should pass ProfileNum-BatchNumber. <br> Title: InventoryUpdate Number, Display: true, Editable: true

    [InventoryUpdateType] INT NOT NULL DEFAULT 0, --InventoryUpdate type. <br> Title: Type, Display: true, Editable: true
    [InventoryUpdateStatus] INT NOT NULL DEFAULT 0, --InventoryUpdate status. <br> Title: Status, Display: true, Editable: true
	[UpdateDate] DATE NOT NULL, --InventoryUpdate date. <br> Title: Date, Display: true, Editable: true
	[UpdateTime] TIME NOT NULL, --InventoryUpdate time. <br> Title: Time, Display: true, Editable: true

	[CustomerUuid] VARCHAR(50) NOT NULL, --Customer uuid, load from customer data. <br> Display: false, Editable: false
	[CustomerCode] VARCHAR(50) NOT NULL DEFAULT '', --Customer number. use DatabaseNum-CustomerCode too load customer data. <br> Title: Customer Number, Display: true, Editable: true
	[CustomerName] NVARCHAR(200) NOT NULL DEFAULT '', --(Readonly) Customer name, load from customer data. <br> Title: Customer Name, Display: true, Editable: false

    [VendorUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Vendor uuid, load from Vendor data. <br> Display: false, Editable: false
	[VendorCode] VARCHAR(50) NULL, --Vendor number. use DatabaseNum-VendorCode too load Vendor data. <br> Title: Vendor code, Display: true, Editable: true
	[VendorName] NVARCHAR(200) NULL, --(Readonly) Vendor name, load from Vendor data. <br> Title: Vendor Name, Display: true, Editable: false

	[ReferenceType] INT NOT NULL, --Reference Transaction Type, reference to invoice, P/O. <br> Display: true, Editable: true
	[ReferenceUuid] VARCHAR(50) NOT NULL, --Reference Transaction uuid, reference to uuid of invoice, P/O#. <br> Display: false, Editable: false
	[ReferenceNum] VARCHAR(50) NOT NULL, --Reference Transaction number, reference to invoice#, P/O#. <br> Display: true, Editable: true

	[InventoryUpdateSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --(Readonly) InventoryUpdate created from other entity number, use to prevent import duplicate InventoryUpdate. <br> Title: Source Number, Display: false, Editable: false

    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this InventoryUpdate. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_InventoryUpdateHeader] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateHeader]') AND name = N'UK_InventoryUpdateHeader_InventoryUpdateId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InventoryUpdateHeader] ON [dbo].[InventoryUpdateHeader]
(
	[InventoryUpdateUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InventoryUpdateHeader]') AND name = N'UI_InventoryUpdateHeader_BatchNumber')
CREATE UNIQUE NONCLUSTERED INDEX [UI_InventoryUpdateHeader_BatchNumber] ON [dbo].[InventoryUpdateHeader]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[BatchNumber] ASC
) 
GO

