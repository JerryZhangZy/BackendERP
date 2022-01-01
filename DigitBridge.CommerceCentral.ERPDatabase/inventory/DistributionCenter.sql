CREATE TABLE [dbo].[DistributionCenter](
	[DatabaseNum] [int] NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[DistributionCenterNum] [int] IDENTITY(10001,1) NOT NULL, --(Readonly) Distribution Center Unique Number. Required, <br> Title: Dc Number, Display: true, Editable: false.
	[MasterAccountNum] [int] NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] [int] NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[DistributionCenterName] [nvarchar](200) NOT NULL DEFAULT '', --Distribution Center name. Required. <br> Title: Name, Display: true, Editable: true
	[DistributionCenterCode] [nvarchar](50) NOT NULL DEFAULT '', --Distribution Center code. Required. <br> Title: Code, Display: true, Editable: true
	[DistributionCenterType] [int] NOT NULL DEFAULT 0, --Distribution Center type. Required. <br> Title: Type, Display: true, Editable: true
	[Status] [int] NOT NULL DEFAULT 0, --Distribution Center status. Required. <br> Title: Status, Display: true, Editable: true
	[DefaultLevel] [tinyint] NOT NULL DEFAULT 0, --Distribution Center level. Required. <br> Title: level, Display: true, Editable: true
	[AddressLine1] [nvarchar](200) NULL, --Address 1. <br> Title: Address 1, Display: true, Editable: true
	[AddressLine2] [nvarchar](200) NULL, --Address 2. <br> Title: Address 2, Display: true, Editable: true
	[City] [nvarchar](50) NULL, --City. <br> Title: City, Display: true, Editable: true
	[State] NVARCHAR(100) NULL, --State. <br> Title: State, Display: true, Editable: true
	[ZipCode] [nvarchar](50) NULL, --Zip Code. <br> Title: Zip, Display: true, Editable: true
	[CompanyName] [nvarchar](200) NULL, --Company Name. <br> Title: Company, Display: true, Editable: true
	[ContactName] [nvarchar](200) NULL, --Contact person. <br> Title: Attn, Display: true, Editable: true
	[ContactEmail] [nvarchar](100) NULL, --Contact Email. <br> Title: Email, Display: true, Editable: true
	[ContactPhone] [varchar](50) NULL, --Contact Phone. <br> Title: Phone 2, Display: true, Editable: true
	[MainPhone] [varchar](50) NULL, --Main Phone. <br> Title: Phone 1, Display: true, Editable: true
	[Fax] [varchar](50) NULL, --Fax. <br> Title: Fax, Display: true, Editable: true
	[Website] [nvarchar](100) NULL, --Website. <br> Title: Website, Display: true, Editable: true
	[Email] [nvarchar](100) NULL, --Email. <br> Title: Email 2, Display: true, Editable: true
	[BusinessHours] [nvarchar](50) NULL, --BusinessHours. <br> Title: Business Hours, Display: false, Editable: false
	[Notes] [nvarchar](max) NULL, --Notes. <br> Title: Notes, Display: true, Editable: true
	[Priority] [int] NOT NULL DEFAULT 0, --Priority. <br> Title: Priority, Display: true, Editable: true

    [RowNum] BIGINT NOT NULL DEFAULT 0, --(Ignore)
    [DistributionCenterUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
	CONSTRAINT [PK_DistributionCenter] PRIMARY KEY CLUSTERED ([DistributionCenterNum] ASC)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[DistributionCenter] ADD  CONSTRAINT [DF_DistributionCenter_Status]  DEFAULT ((0)) FOR [Status]
GO

ALTER TABLE [dbo].[DistributionCenter] ADD  CONSTRAINT [DF_DistributionCenter_DefaultLevel]  DEFAULT ((0)) FOR [DefaultLevel]
GO

ALTER TABLE [dbo].[DistributionCenter] ADD  CONSTRAINT [DF_DistributionCenter_Priority]  DEFAULT ((0)) FOR [Priority]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DistributionCenter]') AND name = N'UK_DistributionCenter_DistributionCenterUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_DistributionCenter_DistributionCenterUuid] ON [dbo].[DistributionCenter]
(
	[DistributionCenterUuid] ASC
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[DistributionCenter]') AND name = N'IX_DistributionCenter_DistributionCenterCode')
CREATE UNIQUE NONCLUSTERED INDEX [UC_DistributionCenter_DistributionCenterCode_MasterAccountNum_ProfileNum] ON [dbo].[DistributionCenter]
(
    [MasterAccountNum] ASC, 
	[ProfileNum] ASC, 
	[DistributionCenterCode] ASC
);
GO
