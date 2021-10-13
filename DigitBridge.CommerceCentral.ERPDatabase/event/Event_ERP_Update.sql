

-- 10/13/20201 By Jerry Z 
IF COL_LENGTH('Event_ERP', 'EventUuid') IS NULL					
BEGIN					
    ALTER TABLE Event_ERP ADD [EventUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event_ERP]') AND name = N'UK_Event_ERP')
	CREATE UNIQUE NONCLUSTERED INDEX [UK_Event_ERP] ON [dbo].[Event_ERP]
	(
		[EventUuid] ASC
	) 
END					

IF COL_LENGTH('Event_ERP', 'UpdateDateUtc') IS NULL					
BEGIN					
    ALTER TABLE Event_ERP ADD [UpdateDateUtc] DATETIME NULL DEFAULT (getutcdate())
END	

IF COL_LENGTH('Event_ERP', 'EnterBy') IS NULL					
BEGIN					
    ALTER TABLE Event_ERP ADD [EnterBy] Varchar(100) NOT NULL DEFAULT ''
END	

IF COL_LENGTH('Event_ERP', 'UpdateBy') IS NULL					
BEGIN					
    ALTER TABLE Event_ERP ADD [UpdateBy] Varchar(100) NOT NULL DEFAULT ''
END	

IF COL_LENGTH('Event_ERP', 'DigitBridgeGuid') IS NULL					
BEGIN					
    ALTER TABLE Event_ERP ADD [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid())
END	
