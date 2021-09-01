
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
	

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
	, CentralOrderLineUuid = sph.CentralOrderLineUuid
    FROM OrderLineMiscExt spp
    INNER JOIN OrderLine sph ON (sph.CentralOrderNum = spp.CentralOrderNum and sph.CentralOrderLineNum = sph.CentralOrderLineNum);
*/