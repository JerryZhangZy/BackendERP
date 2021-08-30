CREATE TABLE [dbo].[PoItemsRef]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,
    [PoUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for P/O
    [PoItemUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for P/O Item Line
	[CentralFulfillmentNum] BIGINT NULL, --CentralFulfillmentNum of dropship S/O
	[ShippingCarrier] VARCHAR(50) NULL,
	[ShippingClass] VARCHAR(50) NULL,
	[DistributionCenterNum] INT NULL,
	[CentralOrderNum] BIGINT NULL, --CentralOrderNum is DigitBridgeOrderId, use same DatabaseNum
	[ChannelNum] INT NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] INT NOT NULL, --The unique number of this profile’s channel account
	[ChannelOrderID] VARCHAR(130) NOT NULL, --This usually is the marketplace order ID, or merchant PO Number
	[SecondaryChannelOrderID] VARCHAR(200) NULL, --Secondary identifier provided by the channel. This is a secondary marketplace-generated Order ID. It is not populated most of the time.
	[ShippingAccount] VARCHAR(100) NULL, --requested Vendor use Account to ship
	[WarehouseUuid] VARCHAR(50) NULL, --Warehouse Guid
	[CustomerUuid] VARCHAR(50) NULL, --Customer Guid
	[EndBuyerUserID] VARCHAR(255) NULL, --The marketplace user ID of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department.
	[EndBuyerName] VARCHAR(255) NULL, --The marketplace name of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department.
	[EndBuyerEmail] VARCHAR(255) NULL, --The email of the end customer
	[ShipToName] VARCHAR(100) NULL,
	[ShipToFirstName] VARCHAR(50) NULL,
	[ShipToLastName] VARCHAR(50) NULL,
	[ShipToSuffix] VARCHAR(50) NULL,
	[ShipToCompany] VARCHAR(100) NULL,
	[ShipToCompanyJobTitle] VARCHAR(100) NULL,
	[ShipToAttention] VARCHAR(100) NULL,
	[ShipToAddressLine1] VARCHAR(100) NULL,
	[ShipToAddressLine2] VARCHAR(100) NULL,
	[ShipToAddressLine3] VARCHAR(100) NULL,
	[ShipToCity] VARCHAR(50) NULL,
	[ShipToState] VARCHAR(50) NULL,
	[ShipToStateFullName] VARCHAR(100) NULL,
	[ShipToPostalCode] VARCHAR(50) NULL,
	[ShipToPostalCodeExt] VARCHAR(50) NULL,
	[ShipToCounty] VARCHAR(50) NULL,
	[ShipToCountry] VARCHAR(50) NULL,
	[ShipToEmail] VARCHAR(100) NULL,
	[ShipToDaytimePhone] VARCHAR(50) NULL,
	[ShipToNightPhone] VARCHAR(50) NULL,

	[BillToName] VARCHAR(100) NULL,
	[BillToFirstName] VARCHAR(50) NULL,
	[BillToLastName] VARCHAR(50) NULL,
	[BillToSuffix] VARCHAR(50) NULL,
	[BillToCompany] VARCHAR(100) NULL,
	[BillToCompanyJobTitle] VARCHAR(100) NULL,
	[BillToAttention] VARCHAR(100) NULL,
	[BillToAddressLine1] VARCHAR(100) NULL,
	[BillToAddressLine2] VARCHAR(100) NULL,
	[BillToAddressLine3] VARCHAR(100) NULL,
	[BillToCity] VARCHAR(50) NULL,
	[BillToState] VARCHAR(50) NULL,
	[BillToStateFullName] VARCHAR(100) NULL,
	[BillToPostalCode] VARCHAR(50) NULL,
	[BillToPostalCodeExt] VARCHAR(50) NULL,
	[BillToCounty] VARCHAR(50) NULL,
	[BillToCountry] VARCHAR(50) NULL,
	[BillToEmail] VARCHAR(100) NULL,
	[BillToDaytimePhone] VARCHAR(50) NULL,
	[BillToNightPhone] VARCHAR(50) NULL,

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_PoItemsRef] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItemsRef]') AND name = N'UI_PoItemsRef_PoItemId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PoItemsRef_PoItemUuid] ON [dbo].[PoItemsRef]
(
	[PoItemUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItemsRef]') AND name = N'UI_PoItemsRef_PoId')
CREATE NONCLUSTERED INDEX [FK_PoItemsRef_PoUuid] ON [dbo].[PoItemsRef]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO



