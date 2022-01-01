
-- 07/28/20201 By Yunman Li 
IF COL_LENGTH('OrderLineAdjustment', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineAdjustment ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
    CREATE NONCLUSTERED INDEX [FK_OrderLineAdjustment_CentralOrderUuid] ON [dbo].[OrderLineAdjustment]
    (
	    [CentralOrderUuid] ASC,
	    [CentralOrderLineNum] ASC
    );
END			

IF COL_LENGTH('OrderLineAdjustment', 'CentralOrderLineUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineAdjustment ADD [CentralOrderLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END	

IF COL_LENGTH('OrderLineAdjustment', 'CentralOrderLineAdjustmentUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLineAdjustment ADD CentralOrderLineAdjustmentUuid VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderLineAdjustment_CentralOrderLineAdjustmentUuid] ON [dbo].[OrderLineBundleComponent]
	(
		[CentralOrderLineAdjustmentUuid] ASC
	) 
END					
	

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderLineAdjustment spp
    INNER JOIN OrderLine sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/