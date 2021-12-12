
-- 12/11/2021 By jerry Z
IF COL_LENGTH('InvoiceItems', 'CentralOrderLineUuid') IS NULL					
BEGIN					
    ALTER TABLE InvoiceItems ADD [CentralOrderLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('InvoiceItems', 'DBChannelOrderLineRowID') IS NULL					
BEGIN					
    ALTER TABLE InvoiceItems ADD [DBChannelOrderLineRowID] VARCHAR(50) NOT NULL DEFAULT ''
END					

IF COL_LENGTH('InvoiceItems', 'OrderDCAssignmentLineUuid') IS NULL					
BEGIN					
    ALTER TABLE InvoiceItems ADD [OrderDCAssignmentLineUuid] VARCHAR(50) NOT NULL DEFAULT ''
END		

IF COL_LENGTH('InvoiceItems', 'OrderDCAssignmentLineNum') IS NULL					
BEGIN					
    ALTER TABLE InvoiceItems ADD [OrderDCAssignmentLineNum] bigint NOT NULL DEFAULT 0
END	

IF COL_LENGTH('InvoiceItems', 'CommissionRate') IS NULL					
BEGIN					
    ALTER TABLE InvoiceItems ADD [CommissionRate] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					

IF COL_LENGTH('InvoiceItems', 'CommissionAmount') IS NULL					
BEGIN					
    ALTER TABLE InvoiceItems ADD [CommissionAmount] DECIMAL(24, 6) NOT NULL DEFAULT 0
END					
