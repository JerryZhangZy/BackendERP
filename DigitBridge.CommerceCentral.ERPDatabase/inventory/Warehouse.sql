CREATE TABLE [dbo].[Warehouse]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,
	[WarehouseUuid] VARCHAR(50) NULL, --Warehouse Guid

	[DistributionCenterNum] [int] NULL,
	[DistributionCenterName] [nvarchar](200) NULL,
	[DistributionCenterCode] [nvarchar](50) NULL,
	[DistributionCenterType] [int] NULL,

    [WarehouseType] INT NULL DEFAULT 0, --Warehouse type
    [WarehouseStatus] INT NULL DEFAULT 0, --Warehouse status
    [Priority] INT NULL DEFAULT 0, --Warehouse Priority

	[WarehouseCode] VARCHAR(50) NULL, --Warehouse Code
	[WarehouseName] NVARCHAR(200) NULL, --Warehouse Name
	[CustomerUuid] VARCHAR(50) NULL, --Customer Guid
	[VendorUuid] VARCHAR(50) NULL, --Vendor Guid

	[ShippingCarrier] VARCHAR(50) NULL,
	[ShippingClass] VARCHAR(50) NULL,

	[ShipToName] NVARCHAR(100) NULL,
	[ShipToFirstName] NVARCHAR(50) NULL,
	[ShipToLastName] NVARCHAR(50) NULL,
	[ShipToSuffix] NVARCHAR(50) NULL,
	[ShipToCompany] NVARCHAR(100) NULL,
	[ShipToCompanyJobTitle] NVARCHAR(100) NULL,
	[ShipToAttention] NVARCHAR(100) NULL,
	[ShipToAddressLine1] NVARCHAR(200) NULL,
	[ShipToAddressLine2] NVARCHAR(200) NULL,
	[ShipToAddressLine3] NVARCHAR(200) NULL,
	[ShipToCity] NVARCHAR(100) NULL,
	[ShipToState] NVARCHAR(50) NULL,
	[ShipToStateFullName] NVARCHAR(100) NULL,
	[ShipToPostalCode] VARCHAR(50) NULL,
	[ShipToPostalCodeExt] VARCHAR(50) NULL,
	[ShipToCounty] NVARCHAR(100) NULL,
	[ShipToCountry] NVARCHAR(100) NULL,
	[ShipToEmail] VARCHAR(100) NULL,
	[ShipToDaytimePhone] VARCHAR(50) NULL,
	[ShipToNightPhone] VARCHAR(50) NULL,

	[ShipFromName] NVARCHAR(100) NULL,
	[ShipFromFirstName] NVARCHAR(50) NULL,
	[ShipFromLastName] NVARCHAR(50) NULL,
	[ShipFromSuffix] NVARCHAR(50) NULL,
	[ShipFromCompany] NVARCHAR(100) NULL,
	[ShipFromCompanyJobTitle] NVARCHAR(100) NULL,
	[ShipFromAttention] NVARCHAR(100) NULL,
	[ShipFromAddressLine1] NVARCHAR(200) NULL,
	[ShipFromAddressLine2] NVARCHAR(200) NULL,
	[ShipFromAddressLine3] NVARCHAR(200) NULL,
	[ShipFromCity] NVARCHAR(100) NULL,
	[ShipFromState] NVARCHAR(50) NULL,
	[ShipFromStateFullName] NVARCHAR(100) NULL,
	[ShipFromPostalCode] VARCHAR(50) NULL,
	[ShipFromPostalCodeExt] VARCHAR(50) NULL,
	[ShipFromCounty] NVARCHAR(50) NULL,
	[ShipFromCountry] NVARCHAR(100) NULL,
	[ShipFromEmail] VARCHAR(100) NULL,
	[ShipFromDaytimePhone] VARCHAR(50) NULL,
	[ShipFromNightPhone] VARCHAR(50) NULL,

    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_Warehouse] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Warehouse]') AND name = N'UK_Warehouse_WarehouseID')
CREATE UNIQUE NONCLUSTERED INDEX [UK_Warehouse_WarehouseUuid] ON [dbo].[Warehouse]
(
	[WarehouseUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Warehouse]') AND name = N'FK_Warehouse_DistributionCenterNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_Warehouse_DistributionCenterNum] ON [dbo].[Warehouse]
(
	[DistributionCenterNum] ASC,
	[Priority] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Warehouse]') AND name = N'FK_Warehouse_DistributionCenterNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_Warehouse_WarehouseCode] ON [dbo].[Warehouse]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[WarehouseCode] ASC
) ON [PRIMARY]
GO

