CREATE TABLE [dbo].[ApInvoiceHeaderInfo]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [ApInvoiceId] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for ApInvoice
    [PoId] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O
    [ReceiveId] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O Receive

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
	[RefNum] VARCHAR(100) NULL, --Reference Number
	[CustomerPoNum] VARCHAR(100) NULL, --Customer P/O Number

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
    CONSTRAINT [PK_ApInvoiceHeaderInfo] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ApInvoiceHeaderInfo]') AND name = N'UI_ApInvoiceHeaderInfo_ApInvoiceId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ApInvoiceHeaderInfo_ApInvoiceId] ON [dbo].[ApInvoiceHeaderInfo]
(
	[ApInvoiceId] ASC
) ON [PRIMARY]
GO



