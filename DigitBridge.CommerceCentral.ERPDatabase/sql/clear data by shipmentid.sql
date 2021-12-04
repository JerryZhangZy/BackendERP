
select ProcessUuid into #tmpProcessUuid from EventProcessERP 
where ERPEventProcessType=4   and MasterAccountNum=10002 and ProfileNum=10003

select SalesOrderUuid,InvoiceUuid,OrderShipmentUuid
into #ToDeleteUuids
from OrderShipmentHeader 
where ShipmentID in 
(
 select * from #tmpProcessUuid
)  
 

--delete invoice
delete from InvoiceHeader where InvoiceUuid in (select  InvoiceUuid from #ToDeleteUuids)
delete from InvoiceHeaderAttributes where InvoiceUuid in (select  InvoiceUuid from #ToDeleteUuids)
delete from InvoiceHeaderInfo where InvoiceUuid in (select  InvoiceUuid from #ToDeleteUuids)
delete from InvoiceItems where InvoiceUuid in (select  InvoiceUuid from #ToDeleteUuids)
delete from InvoiceItemsAttributes where InvoiceUuid in (select  InvoiceUuid from #ToDeleteUuids)

--delete shipment
delete from OrderShipmentHeader where OrderShipmentUuid in (select  OrderShipmentUuid from #ToDeleteUuids)
delete from OrderShipmentPackage where OrderShipmentUuid in (select  OrderShipmentUuid from #ToDeleteUuids)
delete from OrderShipmentShippedItem where OrderShipmentUuid in (select  OrderShipmentUuid from #ToDeleteUuids)
delete from OrderShipmentCanceledItem where OrderShipmentUuid in (select  OrderShipmentUuid from #ToDeleteUuids) 

--delete eventprocess
delete from EventProcessERP
where ERPEventProcessType=4  
and ProcessUuid in 
(
 select * from #tmpProcessUuid
) 

drop table #tmpProcessUuid
drop table #ToDeleteUuids