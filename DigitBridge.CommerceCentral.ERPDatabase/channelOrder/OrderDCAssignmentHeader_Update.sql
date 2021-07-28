
-- 07/28/20201 By Yunman Li 
IF COL_LENGTH('OrderDCAssignmentHeader', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentHeader ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ''
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderDCAssignmentHeader_CentralOrderUuid] ON [dbo].[OrderDCAssignmentHeader]
	(
		[CentralOrderUuid] ASC
	) 
END					

IF COL_LENGTH('OrderDCAssignmentHeader', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentHeader ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('OrderDCAssignmentHeader', 'OrderDCAssignmentUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderDCAssignmentHeader ADD [OrderDCAssignmentUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderDCAssignmentHeader_OrderDCAssignmentUuid] ON [dbo].[OrderDCAssignmentHeader]
	(
		[OrderDCAssignmentUuid] ASC
	) 
END					

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderDCAssignmentHeader spp
    INNER JOIN OrderHeader sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/