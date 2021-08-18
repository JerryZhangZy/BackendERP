

-- 07/29/20201 By Jerry Z 
IF COL_LENGTH('SalesOrderHeaderInfo', 'Notes') IS NULL					
BEGIN					
    ALTER TABLE SalesOrderHeaderInfo ADD [Notes] NVarchar(1000) NOT NULL DEFAULT ''
END					
