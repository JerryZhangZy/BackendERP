
IF COL_LENGTH('OrderDCAssignmentLine', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentLine ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('OrderDCAssignmentLine', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentLine ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
    ALTER TABLE OrderDCAssignmentLine ADD [CentralOrderLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
    ALTER TABLE OrderDCAssignmentLine ADD [OrderDCAssignmentUuid] VARCHAR(50) NOT NULL DEFAULT ''
    CREATE NONCLUSTERED INDEX [FK_OrderLine_CentralOrderUuid_CentralOrderLineUuid] ON [dbo].[OrderDCAssignmentLine]
    (
	    [CentralOrderUuid] ASC,
	    [CentralOrderLineUuid] ASC,
	    [OrderDCAssignmentLineNum] ASC
    );
END					

-- 07/28/20201 By Jerry Z 
IF COL_LENGTH('OrderDCAssignmentLine', 'OrderDCAssignmentLineUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentLine ADD [OrderDCAssignmentLineUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderDCAssignmentLine_OrderDCAssignmentLineUuid] ON [dbo].[OrderDCAssignmentLine]
	(
		[OrderDCAssignmentLineUuid] ASC
	) 
END					

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
	, spp.CentralOrderLineUuid = sph.CentralOrderLineUuid
    FROM OrderDCAssignmentLine spp
    INNER JOIN OrderLine sph ON (sph.CentralOrderNum = spp.CentralOrderNum and sph.CentralOrderLineNum = spp.CentralOrderLineNum);

    	    UPDATE spp
    SET spp.OrderDCAssignmentUuid = sph.OrderDCAssignmentUuid
    FROM OrderDCAssignmentLine spp
    INNER JOIN OrderDCAssignmentHeader sph ON (sph.OrderDCAssignmentNum = spp.OrderDCAssignmentNum);
*/