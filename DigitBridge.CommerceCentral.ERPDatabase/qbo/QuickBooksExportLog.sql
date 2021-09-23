CREATE TABLE [dbo].[QuickBooksExportLog]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [QuickBooksExportLogUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --(Readonly) QuickBooksExport log Line uuid. <br> Display: false, Editable: false

    [BatchNum] BIGINT NULL DEFAULT 0, --Batch number for log update. <br> Title: Batch Number, Display: true, Editable: false
    [LogType] VARCHAR(50) NOT NULL DEFAULT '', --Log type. Which transaction to update QuickBooksExport. For Example: Shippment, P/O Receive, Adjust. <br> Title: Type, Display: true, Editable: false
    [LogUuid] VARCHAR(50) NOT NULL DEFAULT '', --Transaction ID (for example: PO receive, Shhipment). <br> Display: false, Editable: false
    [DocNumber] VARCHAR(100) NOT NULL DEFAULT '', --Transaction Number (for example: PO receive number, Shhipment number). <br> Title: Number, Display: true, Editable: false	
    [DocStatus] INT NULL DEFAULT 0, --Log status. <br> Title: Status, Display: true, Editable: false
	[LogDate] DATE NOT NULL, --Log date. <br> Title: Date, Display: true, Editable: false
	[LogTime] TIME NOT NULL, --Log time. <br> Title: Time, Display: true, Editable: false
	[LogBy] Varchar(100) NOT NULL DEFAULT '', --Log create by. <br> Title: By, Display: true, Editable: false

    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_QuickBooksExportLog] PRIMARY KEY ([RowNum]), 
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[QuickBooksExportLog]') AND name = N'FK_QuickBooksExportLog_QuickBooksExportUuid')
CREATE NONCLUSTERED INDEX [FK_QuickBooksExportLog_QuickBooksExportUuid] ON [dbo].[QuickBooksExportLog]
(
	[QuickBooksExportUuid] ASC
) 
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[QuickBooksExportLog]') AND name = N'UK_QuickBooksExportLog_QuickBooksExportLogUuid')
CREATE UNIQUE NONCLUSTERED INDEX [UK_QuickBooksExportLog_QuickBooksExportLogUuid] ON [dbo].[QuickBooksExportLog]
(
	[QuickBooksExportLogUuid] ASC
) 
GO 

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[QuickBooksExportLog]') AND name = N'[IX_QuickBooksExportLog_DocNumber]')
CREATE NONCLUSTERED INDEX [IX_QuickBooksExportLog_DocNumber] ON [dbo].[QuickBooksExportLog]
(
	[DocNumber] ASC
) 
GO



