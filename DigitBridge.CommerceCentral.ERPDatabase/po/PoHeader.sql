CREATE TABLE [dbo].[PoHeader]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[MasterAccountNum] INT NOT NULL,
	[ProfileNum] INT NOT NULL,

    [PoUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for P/O
	[PoNum] VARCHAR(50) NOT NULL, --Unique in this database, ProfileNum + PoNum is DigitBridgePoNum, which is global unique
    [PoType] INT NULL DEFAULT 0, --P/O type
    [PoStatus] INT NULL DEFAULT 0, --P/O status
	[PoDate] DATE NOT NULL, --P/O date
	[PoTime] TIME NOT NULL, --P/O time
	[EtaShipDate] DATE NULL, --Estimated vendor ship date
	[EtaArrivalDate] DATE NULL, --Estimated date when item arrival to buyer 
	[CancelDate] DATE NULL, --Usually it is related to shipping instruction

    [VendorUuid] VARCHAR(50) NULL DEFAULT '', --reference Vendor Unique Guid
	[VendorNum] VARCHAR(50) NULL, --Vendor readable number, DatabaseNum + VendorNum is DigitBridgeVendorNum, which is global unique
	[VendorName] NVARCHAR(100) NULL, --Vendor name

	[Currency] VARCHAR(10) NULL,
	[SubTotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Sub total amount is sumary items amount. 
	[TotalAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0, --Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation 
									--(Sum of all items OrderItems[Quantity] x OrderItems[UnitPrice] ) + TotalTaxPrice + Total ShippingPrice + TotalInsurancePrice + TotalGiftOptionPrice + AdditionalCostOrDiscount +PromotionAmount + (Sum of all items OrderItems[Promotions[Amount]] + OrderItems[Promotions[ShippingAmount]] + OrderItems[RecyclingFee])
	[TaxRate] DECIMAL(24, 6) NULL DEFAULT 0, --Default Tax rate for P/O items. 
	[TaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total P/O tax amount (include shipping tax and misc tax) 
	[DiscountRate] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount rate. 
	[DiscountAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O level discount amount, base on [SubTotalAmount]
	[ShippingAmount] DECIMAL(24, 6) NULL DEFAULT 0, --Total shipping fee for all items
	[ShippingTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of shipping fee
	[MiscAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O handling charge 
	[MiscTaxAmount] DECIMAL(24, 6) NULL DEFAULT 0, --tax amount of handling charge
	[ChargeAndAllowanceAmount] DECIMAL(24, 6) NULL DEFAULT 0, --P/O total Charg Allowance Amount

	[PoSourceCode] VARCHAR(100) NOT NULL DEFAULT '', --P/O import or create from other entity number, use to prevent import duplicate P/O

    [EnterDateUtc] DATETIME NULL,
    [UpdateDateUtc] DATETIME NULL,
    [EnterBy] Varchar(100) NOT NULL,
    [UpdateBy] Varchar(100) NOT NULL,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_PoHeader] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoHeader]') AND name = N'UI_PoHeader_PoId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PoHeader_PoUuid] ON [dbo].[PoHeader]
(
	[PoUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoHeader]') AND name = N'UI_PoHeader_PoNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PoHeader_PoNum] ON [dbo].[PoHeader]
(
	[PoNum] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UI_PoHeader]') AND name = N'UI_PoHeader_VendorID')
CREATE NONCLUSTERED INDEX [IX_PoHeader_VendorUuid] ON [dbo].[PoHeader]
(
	[VendorUuid] ASC
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UI_PoHeader]') AND name = N'UI_PoHeader_PoSourceCode')
CREATE NONCLUSTERED INDEX [IX_PoHeader_PoSourceCode] ON [dbo].[PoHeader]
(
	[PoSourceCode] ASC
) ON [PRIMARY]
GO

