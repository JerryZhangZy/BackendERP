CREATE TABLE [dbo].[CustomAttributeProfile] (
    [DatabaseNum]       INT            NOT NULL,
    [AttributeNum]      BIGINT         IDENTITY (10001, 1) NOT NULL,
    [MasterAccountNum]  INT            NOT NULL,
    [ProfileNum]        INT            NOT NULL,

    [AttributeUuid]     VARCHAR(50)    NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50))), --Global Unique Guid for Invoice
    [AttributeFor]      VARCHAR(50)   NOT NULL DEFAULT '',
    [AttributeName]     NVARCHAR(200) NOT NULL DEFAULT '',
    [AttributeType]     INT            NOT NULL DEFAULT 1,
    [AttributeDataType] INT            NOT NULL DEFAULT 3,
    [DefaultValue]      NVARCHAR(200) NOT NULL DEFAULT '',
    [OptionList]        NVARCHAR(MAX) NOT NULL DEFAULT '',
    [Group1]            NVARCHAR(200) NOT NULL DEFAULT '',
    [Group2]            NVARCHAR(200) NOT NULL DEFAULT '',
    [Group3]            NVARCHAR(200) NOT NULL DEFAULT '',
    [MaxLength]         INT NOT NULL DEFAULT 0,
    [Searchable]        TINYINT NOT NULL DEFAULT 1,
    [Seq]               INT NOT NULL DEFAULT 0,
    [RowNum]            BIGINT NOT NULL DEFAULT 0,

    [UpdateDateUtc]     DATETIME NULL,
    [CreateBy]          NVARCHAR(100)   DEFAULT ('') NULL,
    [UpdateBy]          NVARCHAR(100)  DEFAULT ('') NULL,
    [EnterDateUtc]      DATETIME NOT NULL DEFAULT (getutcdate()),
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    CONSTRAINT [PK_CustomAttributeProfile] PRIMARY KEY CLUSTERED ([AttributeNum])
);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomAttributeProfile]') AND name = N'UK_CustomAttributeProfile')
CREATE UNIQUE NONCLUSTERED INDEX [UK_CustomAttributeProfile] ON [dbo].[CustomAttributeProfile]
(
	[AttributeUuid] ASC
) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomAttributeProfile]') AND name = N'UI_CustomAttributeProfile_Type')
CREATE UNIQUE NONCLUSTERED INDEX [UI_CustomAttributeProfile_Type] ON [dbo].[CustomAttributeProfile]
(
    [MasterAccountNum] ASC,
    [ProfileNum] ASC,
	[AttributeFor] ASC,
	[AttributeName] ASC
) ON [PRIMARY]
GO


EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Normal 2: ClassificationOnly' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomAttributeProfile', @level2type=N'COLUMN',@level2name=N'AttributeType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:int, 2:decimal, 3:string' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomAttributeProfile', @level2type=N'COLUMN',@level2name=N'AttributeDataType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomAttributeProfile', @level2type=N'COLUMN',@level2name=N'OptionList'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Content' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomAttributeProfile', @level2type=N'COLUMN',@level2name=N'Group1'
GO
