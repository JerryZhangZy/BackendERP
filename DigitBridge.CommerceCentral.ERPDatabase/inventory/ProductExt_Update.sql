
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProductExt]') AND name = N'IX_ProductExt_C_S_D_D_O_A_R_M_C_G_S')
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
