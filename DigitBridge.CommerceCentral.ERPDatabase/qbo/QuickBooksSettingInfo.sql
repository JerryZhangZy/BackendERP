CREATE TABLE [dbo].[QuickBooksIntegrationSetting]
( 
	[RowNum] bigint NOT NULL IDENTITY(1000000, 1),
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[SettingUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Setting uuid. <br> Display: false, Editable: false.
	[ChannelAccountName] nvarchar(150) NOT NULL, -- Central Channel Account name, Max 10 chars ( because of qbo doc Number 21 chars restrictions )
	[ChannelAccountNum] int NOT NULL, -- Central Channel Account Number

	[ExportByOrderStatus] int NOT NULL DEFAULT 0, -- 0: All, 1: Shipped
	[ExportOrderAs] int NOT NULL DEFAULT 4, -- 0: Invoice, 1: Sales Receipt, 2: Daily Summary Sales Receipt, 3: Daily Summary Invoice, 4. Do Not Export Sales Order
	[ExportOrderDateType] int NOT NULL DEFAULT 0, -- 0: By Date Exported, 1: By Payment Date 
	[ExportOrderFromDate] datetime NOT NULL DEFAULT getutcdate(),
	[ExportOrderToDate] datetime NULL ,
	[QboSettingStatus] int Default 0, -- 0: Uninitiated, 1: Active, 100: inactive, 255: Error
	[QboImportOrderAfterUpdateDate] datetime NOT NULL DEFAULT getutcdate(),

	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '',  --(Ignore) JSON string. 
	[EnterDate] datetime DEFAULT getutcdate(), 
    [LastUpdate] datetime DEFAULT getutcdate(), 

    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore) 
	
    CONSTRAINT [PK_QuickBooksIntegrationSetting] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_QuickBooksIntegrationSetting_OrderShipmentUuid] ON [dbo].[QuickBooksIntegrationSetting]
(
    [SettingUuid] ASC
) 
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_QuickBooksIntegrationSetting_MasterAccountNum_ProfileNum_ChannelAccountNum] ON [dbo].[QuickBooksIntegrationSetting]
(
	MasterAccountNum,ProfileNum,ChannelAccountNum
) 
--ALTER TABLE  [dbo].[QuickBooksIntegrationSetting] ALTER COLUMN QboSalesTaxAccName NVARCHAR(150) NULL;
--GO

--ALTER TABLE  [dbo].[QuickBooksIntegrationSetting] ALTER COLUMN QboSalesTaxAccId int NULL;
--GO

--ALTER TABLE  [dbo].[QuickBooksIntegrationSetting] ALTER COLUMN QboSalesTaxItemName NVARCHAR(150) NULL;
--GO

--ALTER TABLE  [dbo].[QuickBooksIntegrationSetting] ALTER COLUMN QboSalesTaxItemId int NULL;
--GO

--ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ADD [QboSettingStatus] int Default 0 -- 0: Uninitiated, 1: Active, 100: inactive, 255: Error
--GO

--ALTER TABLE [dbo].[QuickBooksIntegrationSetting] ALTER COLUMN [ExportOrderToDate] datetime NULL;
--GO