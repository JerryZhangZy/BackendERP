﻿

-- 08/10/20201 By Jerry Z 
IF COL_LENGTH('ApInvoiceHeader', 'PoUuid') IS NULL					
BEGIN					
    ALTER TABLE ApInvoiceHeader ADD [PoUuid] VARCHAR(50) NOT NULL DEFAULT ''
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_SalesOrderUuid')
	CREATE NONCLUSTERED INDEX [IX_ApInvoiceHeader_PoUuid] ON [dbo].[ApInvoiceHeader]
	(
		[PoUuid] ASC
	) 
END					

IF COL_LENGTH('ApInvoiceHeader', 'PoNum') IS NULL					
BEGIN					
    ALTER TABLE ApInvoiceHeader ADD [PoNum] VARCHAR(50) NOT NULL DEFAULT ''
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_OrderNumber')
	CREATE NONCLUSTERED INDEX [IX_ApInvoiceHeader_PoNum] ON [dbo].ApInvoiceHeader
	(
		[PoNum] ASC
	) 
END	
 
IF COL_LENGTH('ApInvoiceHeader', 'VendorNum') IS NOT NULL					
BEGIN
    exec sp_rename 'ApInvoiceHeader.VendorNum', 'VendorCode', 'COLUMN'
END


-- 25/11/20201 By zhengjia

IF COL_LENGTH('ApInvoiceHeader', 'TransUuid') IS NULL					
BEGIN					
    ALTER TABLE ApInvoiceHeader ADD [TransUuid] VARCHAR(50) NOT NULL DEFAULT ''
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_SalesOrderUuid')
	CREATE NONCLUSTERED INDEX [IX_ApInvoiceHeader_TransUuid] ON [dbo].[ApInvoiceHeader]
	(
		[TransUuid] ASC
	) 
END					

IF COL_LENGTH('ApInvoiceHeader', 'TransNum') IS NULL					
BEGIN					
    ALTER TABLE ApInvoiceHeader ADD [TransNum] int NOT NULL DEFAULT 0
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeader]') AND name = N'IX_InvoiceHeader_OrderNumber')
	CREATE NONCLUSTERED INDEX [IX_ApInvoiceHeader_TransNum] ON [dbo].ApInvoiceHeader
	(
		[TransNum] ASC
	) 
END	