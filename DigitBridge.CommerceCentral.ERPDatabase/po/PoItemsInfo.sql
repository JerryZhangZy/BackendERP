CREATE TABLE [dbo].[PoItemsInfo]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [PoItemId] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O Item Line
    [PoId] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O

	-- drop ship S/O info 
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

	[WarehouseID] VARCHAR(50) NULL, --Warehouse Guid
	[CustomerID] VARCHAR(50) NULL, --Customer Guid

	[EndBuyerUserID] VARCHAR(255) NULL, --The marketplace user ID of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department.
	[EndBuyerName] NVARCHAR(255) NULL, --The marketplace name of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department.
	[EndBuyerEmail] VARCHAR(255) NULL, --The email of the end customer
	[ShipToName] NVARCHAR(100) NULL,
	[ShipToFirstName] NVARCHAR(50) NULL,
	[ShipToLastName] NVARCHAR(50) NULL,
	[ShipToSuffix] NVARCHAR(50) NULL,
	[ShipToCompany] NVARCHAR(100) NULL,
	[ShipToCompanyJobTitle] NVARCHAR(100) NULL,
	[ShipToAttention] NVARCHAR(100) NULL,
	[ShipToAddressLine1] NVARCHAR(200) NULL,
	[ShipToAddressLine2] NVARCHAR(200) NULL,
	[ShipToAddressLine3] NVARCHAR(200) NULL,
	[ShipToCity] NVARCHAR(100) NULL,
	[ShipToState] NVARCHAR(50) NULL,
	[ShipToStateFullName] NVARCHAR(100) NULL,
	[ShipToPostalCode] VARCHAR(50) NULL,
	[ShipToPostalCodeExt] VARCHAR(50) NULL,
	[ShipToCounty] NVARCHAR(100) NULL,
	[ShipToCountry] NVARCHAR(100) NULL,
	[ShipToEmail] VARCHAR(100) NULL,
	[ShipToDaytimePhone] VARCHAR(50) NULL,
	[ShipToNightPhone] VARCHAR(50) NULL,

	[BillToName] NVARCHAR(100) NULL,
	[BillToFirstName] NVARCHAR(50) NULL,
	[BillToLastName] NVARCHAR(50) NULL,
	[BillToSuffix] NVARCHAR(50) NULL,
	[BillToCompany] NVARCHAR(100) NULL,
	[BillToCompanyJobTitle] NVARCHAR(100) NULL,
	[BillToAttention] NVARCHAR(100) NULL,
	[BillToAddressLine1] NVARCHAR(200) NULL,
	[BillToAddressLine2] NVARCHAR(200) NULL,
	[BillToAddressLine3] NVARCHAR(200) NULL,
	[BillToCity] NVARCHAR(100) NULL,
	[BillToState] NVARCHAR(50) NULL,
	[BillToStateFullName] NVARCHAR(100) NULL,
	[BillToPostalCode] VARCHAR(50) NULL,
	[BillToPostalCodeExt] VARCHAR(50) NULL,
	[BillToCounty] NVARCHAR(50) NULL,
	[BillToCountry] NVARCHAR(100) NULL,
	[BillToEmail] VARCHAR(100) NULL,
	[BillToDaytimePhone] VARCHAR(50) NULL,
	[BillToNightPhone] VARCHAR(50) NULL,

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_PoItemsInfo] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItemsInfo]') AND name = N'UI_PoItemsInfo_PoItemId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PoItemsInfo_PoItemId] ON [dbo].[PoItemsInfo]
(
	[PoItemId] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoItemsInfo]') AND name = N'IX_PoItemsInfo_PoId')
CREATE NONCLUSTERED INDEX [IX_PoItemsInfo_PoId] ON [dbo].[PoItemsInfo]
(
	[PoId] ASC
) ON [PRIMARY]
GO



