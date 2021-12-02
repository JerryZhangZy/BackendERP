
select ProcessUuid into #tmpProcessUuid from EventProcessERP 
where ERPEventProcessType=4  
and ProcessUuid in 
(
 '113-10000000496',
 '113-10000000497',
 '113-10000000498',
 '113-10000000499',
 '113-10000000450'
) 

select SalesOrderUuid,InvoiceUuid,OrderShipmentUuid
into #ToDeleteUuids
from OrderShipmentHeader 
where ShipmentID in 
(
 select * from #tmpProcessUuid
)  

--delete salesorder
delete from SalesOrderHeader where SalesOrderUuid in (select  SalesOrderUuid from #ToDeleteUuids)
delete from SalesOrderHeaderAttributes where SalesOrderUuid in (select  SalesOrderUuid from #ToDeleteUuids)
delete from SalesOrderHeaderInfo where SalesOrderUuid in (select  SalesOrderUuid from #ToDeleteUuids)
delete from SalesOrderItems where SalesOrderUuid in (select  SalesOrderUuid from #ToDeleteUuids)
delete from SalesOrderItemsAttributes where SalesOrderUuid in (select  SalesOrderUuid from #ToDeleteUuids) 

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