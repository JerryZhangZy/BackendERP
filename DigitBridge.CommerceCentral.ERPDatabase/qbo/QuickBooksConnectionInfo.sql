CREATE TABLE [dbo].[QuickBooksConnectionInfo]
(
	[ConnectionProfileNum] bigint NOT NULL IDENTITY(1000000, 1),	--(Readonly) Record Number. Required, <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.
	[ClientId] NVARCHAR(500) NOT NULL Default '', --Encrypted,ClientId
	[ClientSecret] NVARCHAR(500) NOT NULL Default '', --Encrypted,ClientSecret
	[RealmId] varchar(200) NOT NULL Default '', --Encrypted,RealmId
	[AuthCode] NVARCHAR(200) NOT NULL Default '', --Encrypted,AuthCode
	[RefreshToken] NVARCHAR(200) NOT NULL Default '',--RefreshToken
	[AccessToken] NVARCHAR(1500) NOT NULL Default '',--AccessToken
	[RequestState] NVARCHAR(200) NULL,--RequestState
	[QboOAuthTokenStatus] int NOT NULL Default 0, --0: Uninitiated, 1: Success 2: Error
	[LastRefreshTokUpdate] datetime NOT NULL Default GETUTCDATE(), --LastRefreshTokUpdate
	[LastAccessTokUpdate] datetime NOT NULL Default GETUTCDATE(), --LastAccessTokUpdate
	[EnterDate] DATETIME NOT NULL Default GETUTCDATE(), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [LastUpdate] DATETIME NULL, --(Radonly) LastUpdate Date time. <br> Title: Created At, Display: true, Editable: false
	
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
    [RowNum] BIGINT NOT NULL DEFAULT 0,	--(Ignore) dummy field for T4 template 
	[ConnectionUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Shipment uuid. <br> Display: false, Editable: false.
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) ,
    CONSTRAINT [PK_QuickBooksConnectionInfo] PRIMARY KEY ([ConnectionProfileNum]), 
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_QuickBooksConnectionInfo_OrderShipmentUuid] ON [dbo].[QuickBooksConnectionInfo]
(
    [ConnectionUuid] ASC
) 
GO


