select * from Event_ERP order by RowNum desc

select * from SalesOrderHeader ord
inner join SalesOrderHeaderInfo soi ON (soi.SalesOrderUuid = ord.SalesOrderUuid)
where soi.CentralOrderUuid IN ('4d629cd3-9fdb-463d-b5bc-30d3e93127e7')

select * from SalesOrderItems olg
inner join SalesOrderHeaderInfo soi ON (soi.SalesOrderUuid = olg.SalesOrderUuid)
where soi.CentralOrderUuid IN ('4d629cd3-9fdb-463d-b5bc-30d3e93127e7')

--select * from OrderHeader ord 
--where ord.CentralOrderUuid IN ('666b1417-8b50-4bb5-875c-4c76c447495a')

--select * from OrderHeader where channelorderid='111-0197827-7032209'

select * from EventProcessERP where ProcessUuid = '3f58ad1e-327a-4f8b-81fc-2e109e5bb950'
select * from ActivityLog where ProcessUuid = '3f58ad1e-327a-4f8b-81fc-2e109e5bb950'

select * from DistributionCenter
/*
{"ClassName":"System.NullReferenceException","Message":"Object reference not set to an instance of an object.","Data":null,"InnerException":null,"HelpURL":null,"StackTraceString":"   
at DigitBridge.CommerceCentral.ERPDb.ItemCostClass..ctor(Inventory inv) in C:\\DigitBridge.CommerceCentral.ERP\\Digit


{"ClassName":"Microsoft.Data.SqlClient.SqlException","Message":
"Cannot insert duplicate key row in object 'dbo.Customer' with unique index 
'UI_Customer_CustomerCode'. The duplicate key value is (10001, 10001, Channel_10001_10003).",
"Data":{"HelpLink.ProdName":"Microsoft SQL Server","HelpLink.ProdVer

{"ClassName":"Microsoft.Data.SqlClient.SqlException","Message":"Incorrect syntax near the keyword 'AND'.\r\nIncorrect syntax near ','.\r\nIncorrect syntax near ','.\r\nIncorrect syntax near ','.","Data":{"HelpLink.ProdName":"Microsoft SQL Server","HelpLink.ProdVer":"12.00.2255","HelpLink.EvtSrc":"MS
*/