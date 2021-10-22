
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_C_C_C_P_E_W')
CREATE NONCLUSTERED INDEX [IX_Customer_C_C_C_P_E_W] ON [dbo].[Customer]
(
	[CustomerCode] ASC, 
	[CustomerName] ASC,
	[Contact] ASC,
	[Phone1] ASC,
	[Email] ASC,
	[WebSite] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_C_C_B_F_P')
CREATE NONCLUSTERED INDEX [IX_Customer_C_C_B_F_P] ON [dbo].[Customer]
(
	[CustomerType] ASC, 
	[CustomerStatus] ASC,
	[BusinessType] ASC,
	[FirstDate] ASC,
	[Priority] ASC
) 
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_A_R_D_Z')
CREATE NONCLUSTERED INDEX [IX_Customer_A_R_D_Z] ON [dbo].[Customer]
(
	[Area] ASC, 
	[Region] ASC,
	[Districtn] ASC,
	[Zone] ASC
) 
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND name = N'IX_Customer_C_D_D_S')
CREATE NONCLUSTERED INDEX [IX_Customer_A_R_D_Z] ON [dbo].[Customer]
(
	[ClassCode] ASC, 
	[DepartmentCode] ASC,
	[DivisionCode] ASC,
	[SourceCode] ASC
) 
GO
