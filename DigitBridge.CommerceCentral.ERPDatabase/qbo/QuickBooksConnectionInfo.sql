CREATE TABLE [dbo].[QuickBooksConnectionInfo]
(
	ConnectionProfileNum bigint NOT NULL IDENTITY(1000000, 1),
	MasterAccountNum int NOT NULL,
	ProfileNum int NOT NULL,
	ClientId NVARCHAR(500), --Encrypted
	ClientSecret NVARCHAR(500), --Encrypted
	RealmId varchar(200), --Encrypted
	AuthCode NVARCHAR(200), --Encrypted
	RefreshToken NVARCHAR(200),
	AccessToken NVARCHAR(1500),
	[RequestState] NVARCHAR(200) NULL,
	[QboOAuthTokenStatus] int Default 0, --0: Uninitiated, 1: Success 2: Error
	LastRefreshTokUpdate datetime Default GETUTCDATE(), 
	LastAccessTokUpdate datetime Default GETUTCDATE(), 
	[EnterDate] DATETIME Default GETUTCDATE(), 
    [LastUpdate] DATETIME NULL, 
 CONSTRAINT [PK_QuickBooksConnectionInfo] PRIMARY KEY CLUSTERED 
(
	[ConnectionProfileNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--ALTER TABLE [dbo].[QuickBooksConnectionInfo] ADD [RequestState] NVARCHAR(200) NULL
--GO
--ALTER TABLE [dbo].[QuickBooksConnectionInfo] ADD [QboOAuthTokenStatus] int Default 0 --0: Uninitiated, 1: Success 2: Error
--GO
ALTER TABLE [dbo].[QuickBooksConnectionInfo] ADD CONSTRAINT UC_QuickBooksConnectionInfo UNIQUE (MasterAccountNum,ProfileNum)
GO


