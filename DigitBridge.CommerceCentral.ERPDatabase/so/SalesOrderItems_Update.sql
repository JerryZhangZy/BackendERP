

-- 08/20/20201 By Jerry Z 
IF COL_LENGTH('SalesOrderItems', 'CentralOrderLineUuid') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderItems ADD [CentralOrderLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('SalesOrderItems', 'DBChannelOrderLineRowID') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderItems ADD [DBChannelOrderLineRowID] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('SalesOrderItems', 'OrderDCAssignmentLineUuid') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderItems ADD [OrderDCAssignmentLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END		

-- 08/31/20201 By Yunman
IF COL_LENGTH('SalesOrderItems', 'OrderDCAssignmentLineNum') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderItems ADD [OrderDCAssignmentLineNum] bigint NOT NULL DEFAULT 0
END	


-- 11/16/20201 By Jerry
IF COL_LENGTH('SalesOrderItems', 'EtaArrivalDate') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderItems ADD [EtaArrivalDate] DATE NULL
END	

IF COL_LENGTH('SalesOrderItems', 'EarliestShipDate') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderItems ADD [EarliestShipDate] DATE NULL
END	

-- 11/17/20201 By junxian
IF COL_LENGTH('SalesOrderItems', 'LatestShipDate') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderItems ADD [LatestShipDate] DATE NULL
END	


IF COL_LENGTH('SalesOrderItems', 'SignatureFlag') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderItems ADD [SignatureFlag] TINYINT NOT NULL DEFAULT 0
END	

