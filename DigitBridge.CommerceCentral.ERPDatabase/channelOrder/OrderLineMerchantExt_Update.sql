
-- 07/28/20201 By Yunman Li 
IF COL_LENGTH('OrderLineMerchantExt', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMerchantExt ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
    CREATE NONCLUSTERED INDEX [FK_OrderLineMerchantExt_CentralOrderUuid] ON [dbo].[OrderLineMerchantExt]
    (
	    [CentralOrderUuid] ASC,
	    [CentralOrderLineNum] ASC
    );
END			

IF COL_LENGTH('OrderLineMerchantExt', 'CentralOrderLineUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMerchantExt ADD [CentralOrderLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END	

IF COL_LENGTH('OrderLineMerchantExt', 'CentralOrderLineMerchantExtUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineMerchantExt ADD CentralOrderLineMerchantExtUuid VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderLineMerchantExt_CentralOrderLineMerchantExtUuid] ON [dbo].[OrderLineMerchantExt]
	(
		[CentralOrderLineMerchantExtUuid] ASC
	) 
END					
	

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
	, CentralOrderLineUuid = sph.CentralOrderLineUuid
    FROM OrderLineMerchantExt spp
    INNER JOIN OrderLine sph ON (sph.CentralOrderNum = spp.CentralOrderNum and sph.CentralOrderLineNum = sph.CentralOrderLineNum);
*/