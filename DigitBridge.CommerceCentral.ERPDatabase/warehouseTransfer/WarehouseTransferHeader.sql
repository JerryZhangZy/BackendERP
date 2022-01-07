CREATE TABLE [dbo].[WarehouseTransferHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

    [WarehouseTransferUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --WarehouseTransfer uuid. <br> Display: false, Editable: false.
	[BatchNumber] VARCHAR(50) NOT NULL DEFAULT '', --Readable WarehouseTransfer number, unique in same database and profile. <br> Parameter should pass ProfileNum-BatchNumber. <br> Title: WarehouseTransfer Number, Display: true, Editable: true
	 
    [WarehouseTransferType] INT NOT NULL DEFAULT 0, --WarehouseTransfer type (Adjust/Damage/Cycle Count/Physical Count). <br> Title: Type, Display: true, Editable: false
    [WarehouseTransferStatus] INT NOT NULL DEFAULT 0, --WarehouseTransfer status. <br> Title: Status, Display: true, Editable: false
	[TransferDate] DATE NOT NULL, --WarehouseTransfer date. <br> Title: Date, Display: true, Editable: true
	[TransferTime] TIME NOT NULL, --WarehouseTransfer time. <br> Title: Time, Display: true, Editable: true
	[Processor] VARCHAR(50) NOT NULL DEFAULT '', --WarehouseTransfer processor account. <br> Title: Processor, Display: true, Editable: true
	[ReceiveDate] DATE NOT NULL, --WarehouseTransfer date. <br> Title: Date, Display: true, Editable: true
	[ReceiveTime] TIME NOT NULL, --WarehouseTransfer time. <br> Title: Time, Display: true, Editable: true
	[ReceiveProcessor] VARCHAR(50) NOT NULL DEFAULT '', --WarehouseTransfer processor account. <br> Title: Processor, Display: true, Editable: true

	[FromWarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Warehouse uuid, transfer from warehouse. <br> Display: false, Editable: false
	[FromWarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable warehouse code, transfer from warehouse. <br> Title: Warehouse Code, Display: true, Editable: true
	[ToWarehouseUuid] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) Warehouse uuid, transfer to warehouse. <br> Display: false, Editable: false
	[ToWarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable warehouse code, transfer to warehouse. <br> Title: Warehouse Code, Display: true, Editable: true
	[InTransitToWarehouseCode] VARCHAR(50) NOT NULL DEFAULT '', --Readable InTransitToWarehouseCode code, transfer to warehouse. <br> Title: Warehouse Code, Display: true, Editable: true
	[ReferenceType] INT NOT NULL, --Reference Transaction Type, reference to invoice, P/O. <br> Display: true, Editable: true
	[ReferenceUuid] VARCHAR(50) NOT NULL, --Reference Transaction uuid, reference to uuid of invoice, P/O#. <br> Display: false, Editable: false
	[ReferenceNum] VARCHAR(50) NOT NULL, --Reference Transaction number, reference to invoice#, P/O#. <br> Display: true, Editable: true
	[WarehouseTransferSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --(Readonly) WarehouseTransfer created from other entity number, use to prevent import duplicate WarehouseTransfer. <br> Title: Source Number, Display: false, Editable: false
    [UpdateDateUtc] DATETIME NULL, --(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
    [EnterBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) User who created this WarehouseTransfer. <br> Title: Created By, Display: true, Editable: false
    [UpdateBy] Varchar(100) NOT NULL DEFAULT '', --(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_WarehouseTransferHeader] PRIMARY KEY ([RowNum]), 
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseTransferHeader]') AND name = N'UK_WarehouseTransferHeader')
CREATE UNIQUE NONCLUSTERED INDEX [UK_WarehouseTransferHeader] ON [dbo].[WarehouseTransferHeader]
(
	[WarehouseTransferUuid] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[WarehouseTransferHeader]') AND name = N'UI_WarehouseTransferHeader_BatchNumber')
CREATE UNIQUE NONCLUSTERED INDEX [UI_WarehouseTransferHeader_BatchNumber] ON [dbo].[WarehouseTransferHeader]
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[BatchNumber] ASC
) 
GO

