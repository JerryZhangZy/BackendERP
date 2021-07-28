
-- 07/28/20201 By Jerry Z 
IF COL_LENGTH('OrderTransactionLog', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderTransactionLog ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
    ALTER TABLE OrderTransactionLog ADD [CentralOrderTransactionLogUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderTransactionLog_CentralOrderTransactionLogUuid] ON [dbo].[OrderTransactionLog]
	(
		[CentralOrderTransactionLogUuid] ASC
	) 

END					

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderTransactionLog spp
    INNER JOIN OrderHeader sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/