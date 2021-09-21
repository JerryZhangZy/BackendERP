CREATE TABLE [dbo].[SalesOrderFromQuickBooks]
(	
	SalesOrderNum	bigint NOT NULL IDENTITY(1000000, 1),
	DatabaseNum		int NULL,
	[MasterAccountNum] [int] NOT NULL,
	[ProfileNum] [int] NOT NULL,
	QbRefId    bigint NULL,
	ChannelOrderId	varchar(130) NULL,
	SecondaryChannelOrderId	varchar(200) NULL,
	DigitbridgeOrderId	NVARCHAR(1000) NULL,  --Unique in this database, DatabaseNum + CentralOrderNum is DigitBridgeOrderId, which is global unique
	CentralOrderNum	bigint NULL,
	SaleOrderQboType	TINYINT NULL, -- 0: Invoice  1: SelsReceipt
	CentralSyncStatus	int Default 0, -- 0: To be Synced to Central 1: Completed 2. Error
	QbSyncStatus	int Default 0, -- 0: To be Synced to QBO 1: Completed 2. Error
	CustomerRef    bigint NULL, -- QBO Customer Id, from config
	SyncToken	NVARCHAR(2000) NULL,
	CurrencyRef [int] NULL,
	DocNumber    NVARCHAR(21) NULL, --  endCustomerPoNum
	BillEmail    NVARCHAR(100) NULL,
	BillEmailCc    NVARCHAR(100) NULL,
	BillEmailBcc    NVARCHAR(100) NULL,
	TxnDate    DateTime NULL, --  orderdate
	FreeFormAddress		tinyint NULL, -- 0: No  1: Yes
	ShipToAddrLine1    NVARCHAR(200) NULL,
	ShipToAddrLine2    NVARCHAR(200) NULL,
	ShipToAddrLine3    NVARCHAR(200) NULL,
	ShipToAddrLine4    NVARCHAR(200) NULL,
	ShipToAddrLine5    NVARCHAR(200) NULL,
	ShipToCity    NVARCHAR(50) NULL,
	ShipToCountry    NVARCHAR(50) NULL,
	ShipToState    NVARCHAR(100) NULL,
	ShipToPostCode     NVARCHAR(50) NULL,
	ShipToCompanyName    NVARCHAR(200) NULL,
	ShipToName    NVARCHAR(200) NULL,
	BillToAddrLine1    NVARCHAR(200) NULL,
	BillToAddrLine2    NVARCHAR(200) NULL,
	BillToAddrLine3    NVARCHAR(200) NULL,
	BillToAddrLine4    NVARCHAR(200) NULL,
	BillToAddrLine5    NVARCHAR(200) NULL,
	BillToCity    NVARCHAR(50) NULL,
	BillToCountry    NVARCHAR(50) NULL,
	BillToState    NVARCHAR(100) NULL,
	BillToPostCode    NVARCHAR(50) NULL,
	BillToCompanyName    NVARCHAR(200) NULL,
	BillToName    NVARCHAR(200) NULL,
	ShipDate    DateTime NULL, --  shippedDateUtc
	TrackingNum    NVARCHAR(500) NULL, --  trackingNumber
	ClassRef	int NUll,
	PrintStatus		NVARCHAR(20) NULL,  --  Valid values: NotSet, NeedToPrint, PrintComplete .
	PaymentRefNum	NVARCHAR(21) NULL,
	TxnSource	NVARCHAR(500) NULL,
	ApplyTaxAfterDiscount	tinyint NULL, -- 0: No  1: Yes
	AllowOnlineACHPayment	tinyint NULL, -- 0: No  1: Yes
	DueDate    DateTime NULL,
	CentralCreateTime    DateTime NULL, --  createDateUtc
	CentralUpdatedTime    DateTime NULL,
	PrivateNote		NVARCHAR(2000) NULL, --  sellerPrivateNote
	CustomerMemo	NVARCHAR(1000) NULL, --  sellerPublicNote
	DepositToAccountRef    int Null,
	EmailStatus		varchar(20) NULL, --  Valid values: NotSet, NeedToSend, EmailSent.
	ExchangeRate	decimal(10, 2) NULL,
	Deposit		decimal(10, 2) NULL,
	AllowOnlineCreditCardPayment	tinyint NULL, -- 0: No  1: Yes
	DepartmentRef	int NULL,
	ShipMethodRef	NVARCHAR(500) NULL , -- shippingCarrier + " " + ShippingClass
	HomeBalance		MONEY NULL, -- Read Only in QBO
	Balance 	MONEY NULL, -- Read Only in QBO
	HomeTotalAmt		MONEY NULL, -- Read Only in QBO
	TotalAmt	MONEY NULL, -- Read Only in QBO
	EmailDeliveryTime	DateTime NULL, -- Read Only in QBO, DeliveryInfo : EmailDeliveryTime 
	InvoiceLink 	NVARCHAR(500) NULL, --  Read Only in QBO
	TaxExemptionRef		int NULL, --  Read Only in QBO Invoice
	PaymentMethodRef	int NULL,  -- Only in QBO SalesReceipt  
	[EnterDate] DateTime Default GETUTCDATE(), 
    [LastUpdate] DateTime NULL,   

 CONSTRAINT [PK_SalesOrderFromQuickBooks] PRIMARY KEY CLUSTERED 
(
	[SalesOrderNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


