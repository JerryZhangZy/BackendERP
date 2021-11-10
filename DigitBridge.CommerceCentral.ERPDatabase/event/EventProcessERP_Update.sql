
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EventProcessERP]') AND name = N'IX_EventProcessERP_Type_Process_Action')
CREATE NONCLUSTERED INDEX [IX_EventProcessERP_Type_Process_Action] ON [dbo].[EventProcessERP]
(
	[ERPEventProcessType] ASC,
	[ProcessUuid] ASC,
	[ActionStatus] ASC
)  
GO
