CREATE TABLE [dbo].[ApInvoiceHeaderInfo]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,--(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [ApInvoiceUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for ApInvoice
    [PoUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O
    [ReceiveUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O Receive
	[CentralFulfillmentNum] BIGINT NULL, --CentralFulfillmentNum of dropship S/O
	[ShippingCarrier] VARCHAR(50) NULL, --Shipping Carrier. <br> Title: Shipping Carrier: Display: true, Editable: true
	[ShippingClass] VARCHAR(50) NULL,--Shipping Method. <br> Title: Shipping Method: Display: true, Editable: true
	[DistributionCenterNum] INT NULL,--(Readonly) Original DC number. <br> Title: DC number: Display: false, Editable: false
	[CentralOrderNum] BIGINT NULL, --CentralOrderNum is DigitBridgeOrderId, use same DatabaseNum
	[ChannelNum] INT NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] INT NOT NULL, --The unique number of this profile’s channel account
	[ChannelOrderID] VARCHAR(130) NOT NULL, --This usually is the marketplace order ID, or merchant PO Number
	[SecondaryChannelOrderID] VARCHAR(200) NULL, --Secondary identifier provided by the channel. This is a secondary marketplace-generated Order ID. It is not populated most of the time.
	[ShippingAccount] VARCHAR(100) NULL, --requested Vendor use Account to ship
	[RefNum] VARCHAR(100) NULL, --Reference Number
	[CustomerPoNum] VARCHAR(100) NULL, --Customer P/O Number
	[BillToName] NVARCHAR(100) NULL,--Bill to name <br> Title: Bill to name: Display: true, Editable: true
	[BillToFirstName] NVARCHAR(50) NULL,--(Ignore)
	[BillToLastName] NVARCHAR(50) NULL,--(Ignore)
	[BillToSuffix] NVARCHAR(50) NULL,--(Ignore)
	[BillToCompany] NVARCHAR(100) NULL,--Bill to company name. <br> Title: Bill to company: Display: true, Editable: true
	[BillToCompanyJobTitle] NVARCHAR(100) NULL, --(Ignore)
	[BillToAttention] NVARCHAR(100) NULL, --Bill to contact <br> Title: Bill to contact: Display: true, Editable: true
	[BillToAddressLine1] NVARCHAR(200) NULL,--Bill to address 1 <br> Title: Bill to address 1: Display: true, Editable: true
	[BillToAddressLine2] NVARCHAR(200) NULL,--Bill to address 2 <br> Title: Bill to address 2: Display: true, Editable: true
	[BillToAddressLine3] NVARCHAR(200) NULL,--Bill to address 3 <br> Title: Bill to address 3: Display: true, Editable: true
	[BillToCity] NVARCHAR(100) NULL,--Bill to city <br> Title: Bill to city: Display: true, Editable: true
	[BillToState] NVARCHAR(50) NULL,--Bill to state <br> Title: Bill to state: Display: true, Editable: true
	[BillToStateFullName] NVARCHAR(100) NULL,--(Ignore)
	[BillToPostalCode] VARCHAR(50) NULL,--Bill to zip code <br> Title: Bill to zip Display: true, Editable: true
	[BillToPostalCodeExt] VARCHAR(50) NULL,--(Ignore)
	[BillToCounty] NVARCHAR(50) NULL,--Bill to county <br> Title: Bill to county: Display: true, Editable: true
	[BillToCountry] NVARCHAR(100) NULL, --Bill to country <br> Title: Bill to country: Display: true, Editable: true
	[BillToEmail] VARCHAR(100) NULL,--Bill to email <br> Title: Bill to email: Display: true, Editable: true
	[BillToDaytimePhone] VARCHAR(50) NULL,--Bill to phone <br> Title: Bill to phone: Display: true, Editable: true
	[BillToNightPhone] VARCHAR(50) NULL,--(Ignore)

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),--(Ignore)
    [UpdateDateUtc] DATETIME NULL,--(Ignore)
    [EnterBy] Varchar(100) NOT NULL,--(Ignore)
    [UpdateBy] Varchar(100) NOT NULL,--(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),--(Ignore)
    CONSTRAINT [PK_ApInvoiceHeaderInfo] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceHeaderInfo]') AND name = N'UK_ApInvoiceHeaderInfo_ApInvoiceUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_ApInvoiceHeaderInfo_ApInvoiceUuid] ON [dbo].[ApInvoiceHeaderInfo]
(
	[ApInvoiceUuid] ASC
) ON [PRIMARY]
GO



