
-- 07/28/20201 By Yunman Li 
IF COL_LENGTH('OrderLineMiscExt', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMiscExt ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
    CREATE NONCLUSTERED INDEX [FK_OrderLineMiscExt_CentralOrderUuid] ON [dbo].[OrderLineMiscExt]
    (
	    [CentralOrderUuid] ASC,
	    [CentralOrderLineNum] ASC
    );
END			

IF COL_LENGTH('OrderLineMiscExt', 'CentralOrderLineUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMiscExt ADD [CentralOrderLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END	

IF COL_LENGTH('OrderLineMiscExt', 'CentralOrderLineMiscExtUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMiscExt ADD CentralOrderLineMiscExtUuid VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderLineMiscExt_CentralOrderLineMiscExtUuid] ON [dbo].[OrderLineMiscExt]
	(
		[CentralOrderLineMiscExtUuid] ASC
	) 
END					

IF COL_LENGTH('OrderLineMiscExt', 'LineDueToSellerAmount') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMiscExt ADD [LineDueToSellerAmount] MONEY NOT NULL DEFAULT 0
END		

IF COL_LENGTH('OrderLineMiscExt', 'LineCommissionAmount') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMiscExt ADD [LineCommissionAmount] MONEY NOT NULL DEFAULT 0
END		

IF COL_LENGTH('OrderLineMiscExt', 'LineCommissionTaxAmount') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMiscExt ADD [LineCommissionTaxAmount] MONEY NOT NULL DEFAULT 0
END		

IF COL_LENGTH('OrderLineMiscExt', 'LineRemittedTaxAmount') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMiscExt ADD [LineRemittedTaxAmount] MONEY NOT NULL DEFAULT 0
END			
	

IF COL_LENGTH('OrderLineMiscExt', 'LineAdditionalInfo') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMiscExt ADD [LineAdditionalInfo]  [nvarchar](max) NOT NULL DEFAULT ''
END		
/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
	, CentralOrderLineUuid = sph.CentralOrderLineUuid
    FROM OrderLineMiscExt spp
    INNER JOIN OrderLine sph ON (sph.CentralOrderNum = spp.CentralOrderNum and sph.CentralOrderLineNum = sph.CentralOrderLineNum);
*/