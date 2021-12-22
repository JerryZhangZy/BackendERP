CREATE TABLE [dbo].[ShippingCodes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [ShippingCodesUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Code
    [ShippingCode] VARCHAR(50) NOT NULL DEFAULT '', --Code type, for example: class, department, style
	[Description] NVARCHAR(100) NOT NULL, --Code description, 

	[ShippingCarrier] VARCHAR(50) NOT NULL DEFAULT '', --Shipping Carrier. <br> Title: Shipping Carrier: Display: true, Editable: true
	[ShippingClass] VARCHAR(50) NOT NULL DEFAULT '', --Shipping Method. <br> Title: Shipping Method: Display: true, Editable: true
	[Scac] VARCHAR(50) NOT NULL DEFAULT '', --Standard Carrier Alpha Codes. <br> Title: SCAC: Display: true, Editable: true

	[JsonFields] VARCHAR(max) NULL, --JSON string, store any Code fields

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_ShippingCodes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ShippingCodes]') AND name = N'UK_ShippingCodesUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_ShippingCodesUuid] ON [dbo].[ShippingCodes]
(
	[ShippingCodesUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ShippingCodes]') AND name = N'UI_ShippingCode')
CREATE UNIQUE NONCLUSTERED INDEX [UI_ShippingCode] ON [dbo].[ShippingCodes]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ShippingCode] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ShippingCodes]') AND name = N'IX_ShippingCarrier_ShippingClass')
CREATE NONCLUSTERED INDEX [IX_ShippingCarrier_ShippingClass] ON [dbo].[ShippingCodes]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ShippingCarrier] ASC,
	[ShippingClass] ASC
) ON [PRIMARY]
GO

