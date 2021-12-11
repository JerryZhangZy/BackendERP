
-- 12/11/2021 By junxian
IF COL_LENGTH('InvoiceItems', 'Notes') IS NULL					
BEGIN					
    ALTER TABLE InvoiceItems ADD [CentralOrderLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END

IF COL_LENGTH('InvoiceItems', 'DBChannelOrderHeaderRowID') IS NULL					
BEGIN					
    ALTER TABLE InvoiceItems ADD [DBChannelOrderLineRowID] VARCHAR(50) NOT NULL DEFAULT ''
END	 

IF COL_LENGTH('InvoiceItems', 'OrderDCAssignmentNum') IS NULL					
BEGIN					
    ALTER TABLE InvoiceItems ADD [OrderDCAssignmentLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END

IF COL_LENGTH('InvoiceItems', 'OrderDCAssignmentNum') IS NULL					
BEGIN					
    ALTER TABLE InvoiceItems ADD [OrderDCAssignmentLineNum] BIGINT NOT NULL DEFAULT 0
END