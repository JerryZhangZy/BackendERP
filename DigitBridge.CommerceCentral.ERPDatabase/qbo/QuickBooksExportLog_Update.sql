IF COL_LENGTH('QuickBooksExportLog', 'SyncToken') IS NOT NULL					
BEGIN					
    ALTER TABLE QuickBooksExportLog DROP [SyncToken] 
END

IF COL_LENGTH('QuickBooksExportLog', 'LogStatus') IS NULL					
BEGIN					
    ALTER TABLE QuickBooksExportLog ADD [LogStatus]  int not null default 0
END

IF COL_LENGTH('QuickBooksExportLog', 'ErrorMessage') IS NULL					
BEGIN					
    ALTER TABLE QuickBooksExportLog ADD [ErrorMessage] NText NOT NULL DEFAULT ''
END

IF COL_LENGTH('QuickBooksExportLog', 'RequestInfo') IS NULL					
BEGIN					
    ALTER TABLE QuickBooksExportLog ADD [RequestInfo] NVARCHAR(2000) NOT NULL DEFAULT ''
END