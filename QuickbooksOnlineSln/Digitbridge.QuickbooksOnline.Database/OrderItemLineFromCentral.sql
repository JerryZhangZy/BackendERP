CREATE TABLE [dbo].[OrderItemLineFromCentral]
(	
	ItemLineNum	bigint NOT NULL IDENTITY(1000000, 1),
	DatabaseNum		int NULL,
	QboLineId	bigint NULL, -- Required for update in QB.
	[MasterAccountNum] int NOT NULL,
	[ProfileNum] int NOT NULL,
	[ChannelNum] int NOT NULL,
	[ChannelName] NVARCHAR(200) NOT NULL,
	[ChannelAccountNum] int NOT NULL,
	[ChannelAccountName] NVARCHAR(200) NOT NULL,
	CentralProductNum	bigint NULL,
	ChannelItemId varchar(200) NULL,
	CentralOrderLineNum bigint Null,
	SalesOrderNum  bigint NOT NULL, -- FK [SalesOrderFromCentral]
	ChannelOrderId	varchar(130) NULL,
	SecondaryChannelOrderId	varchar(200) NULL,
	DigitbridgeOrderId	NVARCHAR(1000) NULL,  --Unique in this database, DatabaseNum + CentralOrderNum is DigitBridgeOrderId, which is global unique
	CentralSyncStatus	int Default 0, -- 0: To be Synced to Central 1: Completed 2. Error
	QboSyncStatus	int Default 0, -- NotSynced = 0, SyncedSuccess = 1, Skipped = 2, PendingSummary = 3 ,SyncedWithError = 255, SyncStart = 250
	DetailType	TINYINT NULL, -- QB Detail Type -> 0: SalesItemLineDetail 1: GroupLineDetail.
	SyncToken	NVARCHAR(2000) NULL, -- required for update to QB.
	[Description]	NVARCHAR(4000) NULL, -- itemTitle in Central.
	Sku	NVARCHAR(1000) NULL, -- Sku in Central.
	LineNum		int Default -1, -- line position in order / invoice, default -1 as not specified.
	Amount		money NULL, -- Only in SalesItemLine and is required, additional rules for update. itemTotalAmount in Central. 
	ItemRef		bigint NULL, -- Reference to item id in QB.
	Quantity	decimal(10, 2) NULL, -- orderQty in Central, Quantity for GroupLineDetail, Qty for SalesItemLineDetail.
	UnitPrice	decimal(10, 2) NULL, -- unitPrice in Central, Only in SalesItemLineDetail.
	ServiceDate		DateTime NULL, -- Only in SalesItemLineDetail.
	CentralCreateTime    DateTime NULL, --  createDateUtc
	CentralUpdatedTime    DateTime NULL,
	ItemAccountRef	int NULL, -- Only in SalesItemLineDetail in Invoice. For ReimburseCharge.
	MarkupInfo	int NULL, -- Only in SalesItemLineDetail.
	TaxCodeRef	int NULL, -- Only in SalesItemLineDetail.
	ClassRef	int NULL, -- Only in SalesItemLineDetail.
	[EnterDate] DATETIME Default GETUTCDATE(), 
    [LastUpdate] DATETIME Default GETUTCDATE(),

 CONSTRAINT [FK_OrderItemLineFromCentral_SalesOrderFromCentral] FOREIGN KEY (SalesOrderNum) REFERENCES SalesOrderFromCentral(SalesOrderNum),
 CONSTRAINT [PK_OrderItemLineFromCentral] PRIMARY KEY CLUSTERED 
(
	[ItemLineNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


