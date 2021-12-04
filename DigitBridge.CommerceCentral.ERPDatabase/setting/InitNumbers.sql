﻿CREATE TABLE [dbo].[InitNumbers]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.
    [InitNumbersUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid
    [CustomerUuid] VARCHAR(50) NOT NULL DEFAULT '', --Customer uuid. CustomerUuid = DEFAULT for system init number <br> Display: false, Editable: false.
    [InActive] TINYINT NOT NULL DEFAULT 0, --Disable this record
	[Type] VARCHAR(20) NOT NULL DEFAULT '', --InitNumber type, like Invoice#, S/O#, P/O# 
	[CurrentNumber] INT NOT NULL DEFAULT 0, --Init number,  Current maximum  
	[Number] INT NOT NULL DEFAULT 0, --Init number, real number will be more than init number and not exist number  
	[MaxNumber] bigint NOT NULL DEFAULT 0, --The last assigned number, real number will be more than init number and not exist number  
	[Prefix] VARCHAR(20) NOT NULL DEFAULT '', --Prefix append to Init number 
	[Suffix] VARCHAR(20) NOT NULL DEFAULT '', --Suffix follow by Init number
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [UpdateDateUtc] DATETIME NULL,--(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL,--(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL,--(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_InitNumbers] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InitNumbers]') AND name = N'UI_InitNumbersId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_InitNumbersUuid] ON [dbo].[InitNumbers]
(
	[InitNumbersUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InitNumbers]') AND name = N'FK_CustomerUuid')
CREATE INDEX [FK_CustomerUuid] ON [dbo].[InitNumbers]
(
	[CustomerUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InitNumbers]') AND name = N'UI_CustomerUuid_Type')
CREATE UNIQUE NONCLUSTERED INDEX [UI_CustomerUuid_Type] ON [dbo].[InitNumbers]
(
	[CustomerUuid] ASC,
	[Type] ASC
) ON [PRIMARY]
GO



