CREATE TABLE [dbo].[PoHeaderInfo]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [PoUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for P/O. <br> Display: false, Editable: false.

	[CentralFulfillmentNum] BIGINT NULL,  --(Ignore) Reference to CentralFulfillmentNum. <br> Display: false, Editable: false
	[ShippingCarrier] VARCHAR(50) NULL, --Shipping Carrier. <br> Title: Shipping Carrier: Display: true, Editable: true
	[ShippingClass] VARCHAR(50) NULL, --Shipping Method. <br> Title: Shipping Method: Display: true, Editable: true
	[DistributionCenterNum] INT NULL, --(Readonly) Original DC number. <br> Title: DC number: Display: false, Editable: false
	[CentralOrderNum] BIGINT NULL, --(Readonly) CentralOrderNum. <br> Title: Central Order: Display: true, Editable: false
	[ChannelNum] INT NOT NULL, --(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] INT NOT NULL, --(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
	[ChannelOrderID] VARCHAR(130) NOT NULL,  --(Readonly) This usually is the marketplace order ID, or merchant PO Number. <br> Title: Channel Order: Display: true, Editable: false
	[SecondaryChannelOrderID] VARCHAR(200) NULL,--(Readonly) Secondary identifier provided by the channel. This is a secondary marketplace-generated Order ID. It is not populated most of the time. <br> Title: Other Channel Order: Display: true, Editable: false
	[ShippingAccount] VARCHAR(100) NULL,--(Readonly) requested Vendor use Account to ship. <br> Title: Shipping Account: Display: false, Editable: false
	[RefNum] VARCHAR(100) NULL, --Reference Number. <br> Title: Reference Number: Display: true, Editable: true
	[CustomerPoNum] VARCHAR(100) NULL, --Customer P/O Number. <br> Title: Customer PO: Display: true, Editable: true

	[WarehouseUuid] VARCHAR(50) NULL, --(Readonly) Warehouse uuid, load from warehouse data. <br> Display: false, Editable: false
	[CustomerUuid] VARCHAR(50) NULL, --Customer uuid, load from customer data. <br> Display: false, Editable: false

	[EndBuyerUserID] VARCHAR(255) NULL, --The marketplace user ID of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department.<br> Display: false, Editable: false
	[EndBuyerName] NVARCHAR(255) NULL, --The marketplace name of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department.<br> Display: false, Editable: false
	[EndBuyerEmail] VARCHAR(255) NULL, --The email of the end customer。<br> Display: false, Editable: false
	[ShipToName] NVARCHAR(100) NULL,--Ship to name <br> Title: Ship to name: Display: true, Editable: true
	[ShipToFirstName] NVARCHAR(50) NULL,--(Ignore)
	[ShipToLastName] NVARCHAR(50) NULL,--(Ignore)
	[ShipToSuffix] NVARCHAR(50) NULL,--(Ignore)
	[ShipToCompany] NVARCHAR(100) NULL, --Ship to company name. <br> Title: Ship to company: Display: true, Editable: true
	[ShipToCompanyJobTitle] NVARCHAR(100) NULL,--(Ignore)
	[ShipToAttention] NVARCHAR(100) NULL,--Ship to contact <br> Title: Ship to contact: Display: true, Editable: true
	[ShipToAddressLine1] NVARCHAR(200) NULL,--Ship to address 1 <br> Title: Ship to address 1: Display: true, Editable: true
	[ShipToAddressLine2] NVARCHAR(200) NULL,--Ship to address 2 <br> Title: Ship to address 2: Display: true, Editable: true
	[ShipToAddressLine3] NVARCHAR(200) NULL,--Ship to address 3 <br> Title: Ship to address 3: Display: true, Editable: true
	[ShipToCity] NVARCHAR(100) NULL,--Ship to city <br> Title: Ship to city: Display: true, Editable: true
	[ShipToState] NVARCHAR(50) NULL,--Ship to state <br> Title: Ship to state: Display: true, Editable: true
	[ShipToStateFullName] NVARCHAR(100) NULL,--(Ignore)
	[ShipToPostalCode] VARCHAR(50) NULL,--Ship to zip code <br> Title: Ship to zip Display: true, Editable: true
	[ShipToPostalCodeExt] VARCHAR(50) NULL,--(Ignore)
	[ShipToCounty] NVARCHAR(100) NULL,--Ship to county <br> Title: Ship to county: Display: true, Editable: true
	[ShipToCountry] NVARCHAR(100) NULL,--Ship to country <br> Title: Ship to country: Display: true, Editable: true
	[ShipToEmail] VARCHAR(100) NULL,--Ship to email <br> Title: Ship to email: Display: true, Editable: true
	[ShipToDaytimePhone] VARCHAR(50) NULL,--Ship to phone <br> Title: Ship to phone: Display: true, Editable: true
	[ShipToNightPhone] VARCHAR(50) NULL,--(Ignore)

	[BillToName] NVARCHAR(100) NULL,--Bill to name <br> Title: Bill to name: Display: true, Editable: true
	[BillToFirstName] NVARCHAR(50) NULL, --(Ignore)
	[BillToLastName] NVARCHAR(50) NULL, --(Ignore)
	[BillToSuffix] NVARCHAR(50) NULL, --(Ignore)
	[BillToCompany] NVARCHAR(100) NULL, --Bill to company name. <br> Title: Bill to company: Display: true, Editable: true
	[BillToCompanyJobTitle] NVARCHAR(100) NULL, --(Ignore)
	[BillToAttention] NVARCHAR(100) NULL,--Bill to contact <br> Title: Bill to contact: Display: true, Editable: true
	[BillToAddressLine1] NVARCHAR(200) NULL, --Bill to address 1 <br> Title: Bill to address 1: Display: true, Editable: true
	[BillToAddressLine2] NVARCHAR(200) NULL, --Bill to address 2 <br> Title: Bill to address 2: Display: true, Editable: true
	[BillToAddressLine3] NVARCHAR(200) NULL, --Bill to address 3 <br> Title: Bill to address 3: Display: true, Editable: true
	[BillToCity] NVARCHAR(100) NULL,--Bill to city <br> Title: Bill to city: Display: true, Editable: true
	[BillToState] NVARCHAR(50) NULL, --Bill to state <br> Title: Bill to state: Display: true, Editable: true
	[BillToStateFullName] NVARCHAR(100) NULL, --(Ignore)
	[BillToPostalCode] VARCHAR(50) NULL,--Bill to zip code <br> Title: Bill to zip Display: true, Editable: true
	[BillToPostalCodeExt] VARCHAR(50) NULL, --(Ignore)
	[BillToCounty] NVARCHAR(50) NULL, --Bill to county <br> Title: Bill to county: Display: true, Editable: true
	[BillToCountry] NVARCHAR(100) NULL, --Bill to country <br> Title: Bill to country: Display: true, Editable: true
	[BillToEmail] VARCHAR(100) NULL, --Bill to email <br> Title: Bill to email: Display: true, Editable: true
	[BillToDaytimePhone] VARCHAR(50) NULL, --Bill to phone <br> Title: Bill to phone: Display: true, Editable: true
	[BillToNightPhone] VARCHAR(50) NULL, --(Ignore)

    [EnterDateUtc] DATETIME NULL, --(Ignore)
    [UpdateDateUtc] DATETIME NULL, --(Ignore)
    [EnterBy] Varchar(100) NOT NULL, --(Ignore)
    [UpdateBy] Varchar(100) NOT NULL, --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_PoHeaderInfo] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoHeaderInfo]') AND name = N'UI_PoHeaderInfo_PoId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PoHeaderInfo_PoUuid] ON [dbo].[PoHeaderInfo]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO



