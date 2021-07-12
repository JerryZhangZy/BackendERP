CREATE TABLE [dbo].[VendorAttributes]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
    [VendorUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for P/O
	[JsonFields] NVARCHAR(max) NULL, --JSON string, store any document fields

    CONSTRAINT [PK_VendorAttributes] PRIMARY KEY ([RowNum]), 
) ON [PRIMARY]
GO

--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VendorAttributes]') AND name = N'UI_VendorAttributes_VendorId')
CREATE UNIQUE NONCLUSTERED INDEX [UI_VendorAttributes_VendorUuid] ON [dbo].[VendorAttributes]
(
	[VendorUuid] ASC
) ON [PRIMARY]
GO



