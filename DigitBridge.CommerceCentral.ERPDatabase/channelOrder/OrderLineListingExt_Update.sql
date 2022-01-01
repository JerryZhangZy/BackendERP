
-- 07/28/20201 By Yunman Li 
IF COL_LENGTH('OrderLineListingExt', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineListingExt ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
    CREATE NONCLUSTERED INDEX [FK_OrderLineListingExt_CentralOrderUuid] ON [dbo].[OrderLineListingExt]
    (
	    [CentralOrderUuid] ASC,
	    [CentralOrderLineNum] ASC
    );
END			

IF COL_LENGTH('OrderLineListingExt', 'CentralOrderLineUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineListingExt ADD [CentralOrderLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END	

IF COL_LENGTH('OrderLineListingExt', 'CentralOrderLineListingExtUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineListingExt ADD CentralOrderLineListingExtUuid VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderLineListingExt_CentralOrderLineListingExtUuid] ON [dbo].[OrderLineListingExt]
	(
		[CentralOrderLineListingExtUuid] ASC
	) 
END					
	

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderLineListingExt spp
    INNER JOIN OrderLine sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/