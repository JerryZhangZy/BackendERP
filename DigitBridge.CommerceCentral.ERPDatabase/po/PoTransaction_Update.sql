IF COL_LENGTH('PoTransaction', 'VendorCode') IS NULL
    BEGIN
        ALTER TABLE PoTransaction ADD [VendorCode] varchar(50) NOT NULL DEFAULT ''
    END

IF COL_LENGTH('PoTransaction', 'VendorName') IS NULL
    BEGIN
        ALTER TABLE PoTransaction ADD [VendorName] varchar(200) NOT NULL DEFAULT ''
    END


-- 11/21/2021 By Jerry
IF COL_LENGTH('PoTransaction', 'ShippingAmountAssign') IS NULL
BEGIN
    ALTER TABLE PoTransaction ADD [ShippingAmountAssign] INT NOT NULL DEFAULT 0
END

IF COL_LENGTH('PoTransaction', 'MiscAmountAssign') IS NULL
BEGIN
    ALTER TABLE PoTransaction ADD [MiscAmountAssign] INT NOT NULL DEFAULT 0
END

-- 11/23/2021 by junxian
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransaction]') AND name = N'IX_PoTransaction_TransNum')
  DROP INDEX [dbo].[PoTransaction].IX_PoTransaction_TransNum
CREATE NONCLUSTERED INDEX [IX_PoTransaction_TransNum] ON [dbo].[PoTransaction]
(
	[VendorUuid] ASC,
	[TransNum] ASC
) ON [PRIMARY]
GO

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransaction]') AND name = N'UI_PoTransaction_ProfileNum_PoNum_TransNum')
  DROP INDEX [dbo].[PoTransaction].UI_PoTransaction_ProfileNum_PoNum_TransNum
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PoTransaction]') AND name = N'UI_PoTransaction_ProfileNum_VendorCode_TransNum')
CREATE UNIQUE NONCLUSTERED INDEX [UI_PoTransaction_ProfileNum_VendorCode_TransNum] ON [dbo].[PoTransaction]
(
	[ProfileNum] ASC,
	[VendorCode] ASC,
	[TransNum] ASC
) 
GO



-- 12/16/2021 By junxian
IF COL_LENGTH('PoTransaction', 'WMSBatchNum') IS NULL
BEGIN
    ALTER TABLE PoTransaction ADD [WMSBatchNum] varchar(50) not null default ''
END

-- 12/27/2021 By ZHENGJIA				
ALTER TABLE PoTransaction ADD [EnterDateUtc] DATETIME NOT NULL  DEFAULT (getutcdate())