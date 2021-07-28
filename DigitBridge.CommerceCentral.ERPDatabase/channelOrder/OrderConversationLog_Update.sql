
-- 07/28/20201 By Jerry Z 
IF COL_LENGTH('OrderConversationLog', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderConversationLog ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
    ALTER TABLE OrderConversationLog ADD [CentralOrderConversationLogUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderConversationLog_CentralOrderConversationLogUuid] ON [dbo].[OrderConversationLog]
	(
		[CentralOrderConversationLogUuid] ASC
	) 

END					

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderConversationLog spp
    INNER JOIN OrderHeader sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/