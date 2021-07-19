CREATE TABLE [dbo].[CustomerAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
    [CustomerUuid] VARCHAR(50) NOT NULL,  --Customer uuid. <br> Display: false, Editable: false.
	[JsonFields] NVARCHAR(max) NOT NULL DEFAULT '',  --(Ignore) JSON string

    [EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Ignore)
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
    CONSTRAINT [PK_CustomerAttributes] PRIMARY KEY ([RowNum]), 
) 
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomerAttributes]') AND name = N'UI_CustomerAttributes_CustomerId')
CREATE UNIQUE NONCLUSTERED INDEX [UK_CustomerAttributes_CustomerUuid] ON [dbo].[CustomerAttributes]
(
	[CustomerUuid] ASC
) 
GO



