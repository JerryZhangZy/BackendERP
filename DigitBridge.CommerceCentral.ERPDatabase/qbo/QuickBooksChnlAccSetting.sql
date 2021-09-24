CREATE TABLE [dbo].[QuickBooksChnlAccSetting]
(
	[RowNum] bigint NOT NULL IDENTITY(1000000, 1),--(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[SettingUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Setting uuid. <br> Display: false, Editable: false.
	[ChannelAccountName] nvarchar(150) NOT NULL, -- Central Channel Account name, Max 10 chars ( because of qbo doc Number 21 chars restrictions )
	[ChannelAccountNum] int NOT NULL, -- Central Channel Account Number

	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '',  --(Ignore) JSON string. 
	
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore),
    [LastUpdate] datetime DEFAULT getutcdate(),
	[DailySummaryLastExport] datetime NULL, -- Last DateTime that the system exported the orders in this ChnlAcc	
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
    CONSTRAINT [PK_QuickBooksChnlAccSetting] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_QuickBooksChnlAccSetting_OrderShipmentUuid] ON [dbo].[QuickBooksChnlAccSetting]
(
    [SettingUuid] ASC
) 
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_QuickBooksChnlAccSetting_MasterAccountNum_ProfileNum_ChannelAccountNum] ON [dbo].[QuickBooksChnlAccSetting]
(
	MasterAccountNum,ProfileNum,ChannelAccountNum
) 
GO

--ALTER TABLE [dbo].[QuickBooksChnlAccSetting] ADD [DailySummaryLastExport] datetime NULL;
