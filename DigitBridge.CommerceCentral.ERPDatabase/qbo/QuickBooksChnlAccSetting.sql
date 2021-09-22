CREATE TABLE [dbo].[QuickBooksChnlAccSetting]
(
	ChannelAccSettingNum bigint NOT NULL IDENTITY(1000000, 1),
	MasterAccountNum int NOT NULL,
	ProfileNum int NOT NULL,
	ChannelAccountName nvarchar(150) NOT NULL, -- Central Channel Account name, Max 10 chars ( because of qbo doc Number 21 chars restrictions )
	ChannelAccountNum int NOT NULL, -- Central Channel Account Number
	ChannelName nvarchar(150) NOT NULL, -- Central Channel name
	ChannelNum int NOT NULL, -- Central Channel Number
	ChannelQboCustomerName nvarchar(150) NULL, -- Use if select Create Customer Records per Marketplace
	ChannelQboCustomerId int NULL, -- Use if select Create Customer Records per Marketplace
	ChannelQboFeeAcountName nvarchar(150) NULL, -- Account for fee in this Marketplace
	ChannelQboFeeAcountId int NULL,
	ChannelQboBankAcountName nvarchar(150) NULL, -- Account for collecting money in this Mrketplace
	ChannelQboBankAcountId int NULL,
	[EnterDate] datetime DEFAULT getutcdate(), 
    [LastUpdate] datetime DEFAULT getutcdate(),
	[DailySummaryLastExport] datetime NULL, -- Last DateTime that the system exported the orders in this ChnlAcc
 CONSTRAINT [PK_QuickBooksChnlAccSetting] PRIMARY KEY CLUSTERED 
(
	ChannelAccSettingNum ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[QuickBooksChnlAccSetting] ADD CONSTRAINT UC_QuickBooksChnlAccSetting UNIQUE (MasterAccountNum,ProfileNum,ChannelAccountNum);

--ALTER TABLE [dbo].[QuickBooksChnlAccSetting] ADD [DailySummaryLastExport] datetime NULL;
