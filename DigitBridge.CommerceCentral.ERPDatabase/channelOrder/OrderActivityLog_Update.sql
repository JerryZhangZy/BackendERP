
-- 07/28/20201 By Jerry Z 
IF COL_LENGTH('OrderActivityLog', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderActivityLog ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
    ALTER TABLE OrderActivityLog ADD [CentralOrderActivityLogUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderActivityLog_CentralOrderActivityLogUuid] ON [dbo].[OrderActivityLog]
	(
		[CentralOrderActivityLogUuid] ASC
	) 
END					

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderActivityLog spp
    INNER JOIN OrderHeader sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/