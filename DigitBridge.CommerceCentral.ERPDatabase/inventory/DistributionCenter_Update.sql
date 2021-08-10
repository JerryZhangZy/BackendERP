
IF COL_LENGTH('DistributionCenter', 'DistributionCenterUuid') IS NULL					
BEGIN					
    ALTER TABLE DistributionCenter ADD [DistributionCenterUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
    CREATE UNIQUE NONCLUSTERED INDEX [UK_DistributionCenter_DistributionCenterUuid] ON [dbo].[DistributionCenter]
    (
	    [DistributionCenterUuid] ASC
    ) 
END					

IF COL_LENGTH('DistributionCenter', 'RowNum') IS NULL					
BEGIN					
    ALTER TABLE DistributionCenter ADD [RowNum] BIGINT NOT NULL DEFAULT 0
END					

IF COL_LENGTH('DistributionCenter', 'EnterDateUtc') IS NULL					
BEGIN					
    ALTER TABLE DistributionCenter ADD [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate())
END					

IF COL_LENGTH('DistributionCenter', 'DigitBridgeGuid') IS NULL					
BEGIN					
    ALTER TABLE DistributionCenter ADD [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid())
END					

