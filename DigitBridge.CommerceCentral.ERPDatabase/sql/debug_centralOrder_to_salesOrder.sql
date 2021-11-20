select * from Event_ERP order by RowNum desc

select * from SalesOrderHeader ord
inner join SalesOrderHeaderInfo soi ON (soi.SalesOrderUuid = ord.SalesOrderUuid)
where soi.CentralOrderUuid IN ('bbf79acd-d61b-46dc-904b-9e9851a0e119')

select * from SalesOrderItems olg
inner join SalesOrderHeaderInfo soi ON (soi.SalesOrderUuid = olg.SalesOrderUuid)
where soi.CentralOrderUuid IN ('bbf79acd-d61b-46dc-904b-9e9851a0e119')


--select * from OrderHeader ord 
--where ord.CentralOrderUuid IN ('666b1417-8b50-4bb5-875c-4c76c447495a')

--select * from OrderHeader where channelorderid='111-0197827-7032209'

select * from EventProcessERP 
--order by rownum desc
where ProcessUuid = '795dde8f-1625-4b6e-ad88-a65ca5e0ddb9'

/*
{"ClassName":"System.NullReferenceException","Message":"Object reference not set to an instance of an object.","Data":null,"InnerException":null,"HelpURL":null,"StackTraceString":"   
at DigitBridge.CommerceCentral.ERPDb.ItemCostClass..ctor(Inventory inv) in C:\\DigitBridge.CommerceCentral.ERP\\Digit
*/