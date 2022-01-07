CREATE TABLE [dbo].[ItemLineFromQuickBooks]
(	
	ItemLineNum	bigint NOT NULL IDENTITY(1000000, 1),
	DatabaseNum		int NULL,
	QbLineId	bigint NULL, -- Required for update in QB.
	[MasterAccountNum] int NULL,
	[ProfileNum] int NULL,
	CentralProductNum	bigint NULL,
	SalesOrderNum  bigint NOT NULL,
	ChannelOrderId	varchar(130) NULL,
	SecondaryChannelOrderId	varchar(200) NULL,
	DigitbridgeOrderId	NVARCHAR(1000) NULL,  --Unique in this database, DatabaseNum + CentralOrderNum is DigitBridgeOrderId, which is global unique
	CentralSyncStatus	int Default 0, -- 0: To be Synced to Central 1: Completed 2. Error
	QbSyncStatus	int Default 0, -- 0: To be Synced to QBO 1: Completed 2. Error
	DetailType	TINYINT NULL, -- QB Detail Type -> 0: SalesItemLineDetail 1: GroupLineDetail.
	SyncToken	NVARCHAR(2000) NULL, -- required for update to QB.
	[Description]	NVARCHAR(4000) NULL, -- itemTitle in Central.
	LineNum		int NULL, -- line position in order / invoice.
	Amount		money NULL, -- Only in SalesItemLine and is required, additional rules for update. itemTotalAmount in Central. 
	ItemRef		bigint NULL, -- Reference to item id in QB.
	Quantity	decimal(10, 2) NULL, -- orderQty in Central, Quantity for GroupLineDetail, Qty for SalesItemLineDetail.
	UnitPrice	decimal(10, 2) NULL, -- unitPrice in Central, Only in SalesItemLineDetail.
	ServiceDate		DateTime NULL, -- Only in SalesItemLineDetail.
	ItemAccountRef	int NULL, -- Only in SalesItemLineDetail in Invoice. For ReimburseCharge.
	MarkupInfo	int NULL, -- Only in SalesItemLineDetail.
	TaxCodeRef	int NULL, -- Only in SalesItemLineDetail.
	ClassRef	int NULL, -- Only in SalesItemLineDetail.
	CentralCreateTime    DateTime NULL, --  createDateUtc
	CentralUpdatedTime    DateTime NULL,
	[EnterDate] DATETIME Default GETUTCDATE(), 
    [LastUpdate] DATETIME NULL, 

 CONSTRAINT [FK_ItemLineFromQuickBooks_SalesOrderFromQuickBooks] FOREIGN KEY (SalesOrderNum) REFERENCES SalesOrderFromQuickBooks(SalesOrderNum),
 CONSTRAINT [PK_ItemLineFromQuickBooks] PRIMARY KEY CLUSTERED 
(
	[ItemLineNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


