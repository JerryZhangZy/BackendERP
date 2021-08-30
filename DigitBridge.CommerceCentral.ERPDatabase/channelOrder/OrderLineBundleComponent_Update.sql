
-- 07/28/20201 By Yunman Li 
IF COL_LENGTH('OrderLineBundleComponent', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineBundleComponent ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
    CREATE NONCLUSTERED INDEX [FK_OrderLineBundleComponent_CentralOrderUuid] ON [dbo].[OrderLineBundleComponent]
    (
	    [CentralOrderUuid] ASC,
	    [CentralOrderLineNum] ASC
    );
END			

IF COL_LENGTH('OrderLineBundleComponent', 'CentralOrderLineUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineBundleComponent ADD [CentralOrderLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END	

IF COL_LENGTH('OrderLineBundleComponent', 'CentralOrderLineBundleComponentUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineBundleComponent ADD CentralOrderLineBundleComponentUuid VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderLineBundleComponent_CentralOrderLineBundleComponentUuid] ON [dbo].[OrderLineBundleComponent]
	(
		[CentralOrderLineBundleComponentUuid] ASC
	) 
END					
	

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderLineBundleComponent spp
    INNER JOIN OrderLine sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/