CREATE TABLE [dbo].[PaidbyMap]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [PaidbyMapUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Code
	[ChannelNum] INT NOT NULL DEFAULT 0, --(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] INT NOT NULL DEFAULT 0, --(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
	[ChannelPaidBy] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Payment method from channel. <br> Title: Channel Paid By, Display: true, Editable: true
	[PaidBy] INT NOT NULL DEFAULT 1, --ERP Payment method. <br> Title: Paid By, Display: true, Editable: true
	[BankAccountUuid] VARCHAR(50) NOT NULL DEFAULT '', --Payment bank account uuid. 
	[BankAccountCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable payment Bank account code. <br> Title: Bank, Display: true, Editable: true
	[Description] NVARCHAR(100) NOT NULL, --Code description, 
	[AutoPaid] INT NOT NULL DEFAULT 0, --Channel will auto paid to merchant. <br> Title: Auto Paid, Display: true, Editable: true

	[JsonFields] VARCHAR(max) NULL, --JSON string, store any Code fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_PaidbyMap] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PaidbyMap]') AND name = N'UK_PaidbyMapUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_PaidbyMapUuid] ON [dbo].[PaidbyMap]
(
	[PaidbyMapUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PaidbyMap]') AND name = N'UI_ChannelNum_ChannelAccountNum_ChannelPaidBy')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ChannelNum_ChannelAccountNum_ChannelPaidBy] ON [dbo].[PaidbyMap]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ChannelNum] ASC,
	[ChannelAccountNum] ASC,
	[ChannelPaidBy] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PaidbyMap]') AND name = N'UI_ChannelPaidBy')
CREATE NONCLUSTERED INDEX [IX_ChannelPaidBy] ON [dbo].[PaidbyMap]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ChannelPaidBy] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PaidbyMap]') AND name = N'IX_PaidBy')
CREATE NONCLUSTERED INDEX [IX_PaidBy] ON [dbo].[PaidbyMap]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[PaidBy] ASC
) ON [PRIMARY]
GO

