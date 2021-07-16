CREATE TABLE [dbo].[SalesOrderHeaderInfo]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [SalesOrderUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Order

	-- drop ship S/O info
	[CentralFulfillmentNum] BIGINT NOT NULL DEFAULT 0, --CentralFulfillmentNum of dropship S/O
	[ShippingCarrier] VARCHAR(50) NOT NULL DEFAULT '',
	[ShippingClass] VARCHAR(50) NOT NULL DEFAULT '',
	[DistributionCenterNum] INT NOT NULL DEFAULT 0,
	[CentralOrderNum] BIGINT NOT NULL DEFAULT 0, --CentralOrderNum is DigitBridgeOrderId, use same DatabaseNum
	[ChannelNum] INT NOT NULL DEFAULT 0, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] INT NOT NULL DEFAULT 0, --The unique number of this profile’s channel account
	[ChannelOrderID] VARCHAR(130) NOT NULL DEFAULT 0, --This usually is the marketplace order ID, or merchant PO Number
	[SecondaryChannelOrderID] VARCHAR(200) NOT NULL DEFAULT '', --Secondary identifier provided by the channel. This is a secondary marketplace-generated Order ID. It is not populated most of the time.
	[ShippingAccount] VARCHAR(100) NOT NULL DEFAULT '', --requested Vendor use Account to ship
	[WarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --Warehouse Guid
	[RefNum] VARCHAR(100) NOT NULL DEFAULT '', --Reference Number
	[CustomerPoNum] VARCHAR(100) NOT NULL DEFAULT '', --Customer P/O Number

	[EndBuyerUserID] VARCHAR(255) NOT NULL DEFAULT '', --The marketplace user ID of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department.
	[EndBuyerName] NVARCHAR(255) NOT NULL DEFAULT '', --The marketplace name of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department.
	[EndBuyerEmail] VARCHAR(255) NOT NULL DEFAULT '', --The email of the end customer
	[ShipToName] NVARCHAR(100) NOT NULL DEFAULT '',
	[ShipToFirstName] NVARCHAR(50) NOT NULL DEFAULT '',
	[ShipToLastName] NVARCHAR(50) NOT NULL DEFAULT '',
	[ShipToSuffix] NVARCHAR(50) NOT NULL DEFAULT '',
	[ShipToCompany] NVARCHAR(100) NOT NULL DEFAULT '',
	[ShipToCompanyJobTitle] NVARCHAR(100) NOT NULL DEFAULT '',
	[ShipToAttention] NVARCHAR(100) NOT NULL DEFAULT '',
	[ShipToAddressLine1] NVARCHAR(200) NOT NULL DEFAULT '',
	[ShipToAddressLine2] NVARCHAR(200) NOT NULL DEFAULT '',
	[ShipToAddressLine3] NVARCHAR(200) NOT NULL DEFAULT '',
	[ShipToCity] NVARCHAR(100) NOT NULL DEFAULT '',
	[ShipToState] NVARCHAR(50) NOT NULL DEFAULT '',
	[ShipToStateFullName] NVARCHAR(100) NOT NULL DEFAULT '',
	[ShipToPostalCode] VARCHAR(50) NOT NULL DEFAULT '',
	[ShipToPostalCodeExt] VARCHAR(50) NOT NULL DEFAULT '',
	[ShipToCounty] NVARCHAR(100) NOT NULL DEFAULT '',
	[ShipToCountry] NVARCHAR(100) NOT NULL DEFAULT '',
	[ShipToEmail] VARCHAR(100) NOT NULL DEFAULT '',
	[ShipToDaytimePhone] VARCHAR(50) NOT NULL DEFAULT '',
	[ShipToNightPhone] VARCHAR(50) NOT NULL DEFAULT '',

	[BillToName] NVARCHAR(100) NOT NULL DEFAULT '',
	[BillToFirstName] NVARCHAR(50) NOT NULL DEFAULT '',
	[BillToLastName] NVARCHAR(50) NOT NULL DEFAULT '',
	[BillToSuffix] NVARCHAR(50) NOT NULL DEFAULT '',
	[BillToCompany] NVARCHAR(100) NOT NULL DEFAULT '',
	[BillToCompanyJobTitle] NVARCHAR(100) NOT NULL DEFAULT '',
	[BillToAttention] NVARCHAR(100) NOT NULL DEFAULT '',
	[BillToAddressLine1] NVARCHAR(200) NOT NULL DEFAULT '',
	[BillToAddressLine2] NVARCHAR(200) NOT NULL DEFAULT '',
	[BillToAddressLine3] NVARCHAR(200) NOT NULL DEFAULT '',
	[BillToCity] NVARCHAR(100) NOT NULL DEFAULT '',
	[BillToState] NVARCHAR(50) NOT NULL DEFAULT '',
	[BillToStateFullName] NVARCHAR(100) NOT NULL DEFAULT '',
	[BillToPostalCode] VARCHAR(50) NOT NULL DEFAULT '',
	[BillToPostalCodeExt] VARCHAR(50) NOT NULL DEFAULT '',
	[BillToCounty] NVARCHAR(50) NOT NULL DEFAULT '',
	[BillToCountry] NVARCHAR(100) NOT NULL DEFAULT '',
	[BillToEmail] VARCHAR(100) NOT NULL DEFAULT '',
	[BillToDaytimePhone] VARCHAR(50) NOT NULL DEFAULT '',
	[BillToNightPhone] VARCHAR(50) NOT NULL DEFAULT '',

    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL DEFAULT '',
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '',
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_SalesOrderHeaderInfo] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderHeaderInfo]') AND name = N'UK_SalesOrderHeaderInfo_OrderId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_SalesOrderHeaderInfo_OrderUuid] ON [dbo].[SalesOrderHeaderInfo]
(
	[SalesOrderUuid] ASC
) 
GO



