

-- 1/2/2022 By Jerry Z 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'InitNumbers' AND COLUMN_NAME = 'Number' AND DATA_TYPE = 'bigint')
BEGIN					
	ALTER TABLE [dbo].[InitNumbers] DROP CONSTRAINT [DF_InitNumbers_Number]
	ALTER TABLE [dbo].InitNumbers ALTER COLUMN [Number] bigint
	ALTER TABLE [dbo].[InitNumbers] ADD CONSTRAINT [DF_InitNumbers_Number] DEFAULT ((0)) FOR [Number]
END			


IF COL_LENGTH('InitNumbers', 'EndNumber') IS NULL					
BEGIN					
    ALTER TABLE InitNumbers ADD [EndNumber] BIGINT NOT NULL DEFAULT 0
END	
