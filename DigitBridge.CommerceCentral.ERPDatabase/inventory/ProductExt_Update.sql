
-- 11/15/2021 By Jerry
IF COL_LENGTH('ProductExt', 'ProductStatus') IS NULL					
BEGIN					
    ALTER TABLE ProductExt ADD [ProductStatus] INT NOT NULL DEFAULT 0
END					

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProductExt]') AND name = N'IX_IProductExt_Status')
CREATE NONCLUSTERED INDEX [IX_IProductExt_Status] ON [dbo].[ProductExt]
(
    [MasterAccountNum] ASC, 
    [ProfileNum] ASC, 
    [ProductStatus] ASC
) 
GO


IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProductExt]') AND name = N'IX_IProductExt_S_C_S_W_L_W_L_L')
CREATE NONCLUSTERED INDEX [IX_IProductExt_S_C_S_W_L_W_L_L] ON [dbo].[ProductExt]
(
	[ClassCode] ASC, 
	[SubClassCode] ASC,
	[DepartmentCode] ASC,
	[DivisionCode] ASC,
	[OEMCode] ASC,
	[AlternateCode] ASC,
	[Remark] ASC,
	[Model] ASC,
	[CategoryCode] ASC,
	[GroupCode] ASC,
	[SubGroupCode] ASC
) 
GO
