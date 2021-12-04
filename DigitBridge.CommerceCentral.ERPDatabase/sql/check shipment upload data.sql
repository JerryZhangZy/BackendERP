
select ProcessUuid into #tmpProcessUuid from EventProcessERP 
where ERPEventProcessType=4  
and ProcessUuid not in 
(
 '113-10000000496',
 '113-10000000497',
 '113-10000000498',
 '113-10000000499',
 '113-10000000450'
) 

select SalesOrderUuid,InvoiceUuid,OrderShipmentUuid
into #checkUuids
from OrderShipmentHeader 
where ShipmentID in 
(
 select * from #tmpProcessUuid
)  
  

--select *salesorder
select * from SalesOrderHeader where SalesOrderUuid in (select  SalesOrderUuid from #checkUuids Uuids) 
select * from SalesOrderHeaderInfo where SalesOrderUuid in (select  SalesOrderUuid from #checkUuids Uuids)
select * from SalesOrderItems where SalesOrderUuid in (select  SalesOrderUuid from #checkUuids Uuids) 

--select *invoice
select * from InvoiceHeader where InvoiceUuid in (select  InvoiceUuid from #checkUuids Uuids) 
select * from InvoiceHeaderInfo where InvoiceUuid in (select  InvoiceUuid from #checkUuids Uuids)
select * from InvoiceItems where InvoiceUuid in (select  InvoiceUuid from #checkUuids Uuids) 

--select *shipment
select * from OrderShipmentHeader where OrderShipmentUuid in (select  OrderShipmentUuid from #checkUuids Uuids)
select * from OrderShipmentPackage where OrderShipmentUuid in (select  OrderShipmentUuid from #checkUuids Uuids)
select * from OrderShipmentShippedItem where OrderShipmentUuid in (select  OrderShipmentUuid from #checkUuids Uuids)
select * from OrderShipmentCanceledItem where OrderShipmentUuid in (select  OrderShipmentUuid from #checkUuids Uuids) 
