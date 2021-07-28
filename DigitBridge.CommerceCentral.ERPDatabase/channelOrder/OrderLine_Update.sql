
-- 07/28/20201 By Jerry Z 
IF COL_LENGTH('OrderLine', 'CentralOrderLineUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLine ADD [CentralOrderLineUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderLine_CentralOrderLineUuid] ON [dbo].[OrderLine]
	(
		[CentralOrderLineUuid] ASC
	) 
END					

IF COL_LENGTH('OrderLine', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE OrderLine ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('OrderLine', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderLine ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
    CREATE NONCLUSTERED INDEX [FK_OrderLine_CentralOrderUuid] ON [dbo].[OrderLine]
    (
	    [CentralOrderUuid] ASC,
	    [CentralOrderLineNum] ASC
    );
END					

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderLine spp
    INNER JOIN OrderHeader sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/