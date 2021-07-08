﻿CREATE TABLE [dbo].[CustomerAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [CustomerUuid] VARCHAR(50) NOT NULL DEFAULT '', --Global Unique Guid for P/O
	[JsonFields] VARCHAR(max) NOT NULL DEFAULT '', --JSON string, store any document fields
    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_CustomerAttributes] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomerAttributes]') AND name = N'UI_CustomerAttributes_CustomerId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_CustomerAttributes_CustomerUuid] ON [dbo].[CustomerAttributes]
(
	[CustomerUuid] ASC
) 
GO



