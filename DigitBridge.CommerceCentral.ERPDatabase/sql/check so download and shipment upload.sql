 --{"shipmentHeader":{"shipmentID":"113-10000000665","shippingCarrier":"FedEx","shippingClass":"FedEx Ground®","shippingCost":64.0000,"mainTrackingNumber":"FedEx-10000000665","mainReturnTrackingNumber":"","billOfLadingID":"","totalPackages":0,"totalHandlingFee":0.0,"totalShippedQty":1.0,"totalCanceledQty":0.0,"totalWeight":0.0,"totalVolume":0.0,"weightUnit":0,"lengthUnit":0,"volumeUnit":0,"invoiceNumber":"","salesOrderUuid":"8007858b-964f-45e0-9674-7b73da64315a","warehouseCode":"Main","orderDCAssignmentNum":0,"centralOrderNum":100003987,"channelOrderID":"99048364","shipmentType":0,"shipmentReferenceID":"","shipmentDateUtc":"2021-12-03T02:07:05.2543739","shipmentStatus":1},"packageItems":[{"shipmentPackage":{"packageID":"10000000665-Package","packageType":0,"packagePatternNum":0,"packageTrackingNumber":"FedEx-10000000665","packageReturnTrackingNumber":"","packageWeight":0.0,"packageLength":0.0,"packageWidth":0.0,"packageHeight":0.0,"packageVolume":0.0,"packageQty":1.0,"parentPackageNum":0,"hasChildPackage":false},"shippedItems":[{"salesOrderItemsUuid":"25bb9c9d-9f70-48b1-8571-af21e314ff6b","centralOrderLineNum":10004719,"orderDCAssignmentLineNum":0,"sku":"FA16553DMB-2886VINDISL-18-32","unitHandlingFee":0.0,"shippedQty":1.0,"lineHandlingFee":0.0,"enterDateUtc":"2021-12-03T03:07:01.2760035Z"}]}],"canceledItems":[]}



create table #wmsshipment(
shipmentID varchar(1000)
,shippingCarrier varchar(1000)
,shippingClass varchar(1000)
,shippingCost varchar(1000)
,mainTrackingNumber varchar(1000)
,channelOrderID varchar(1000)
,shipmentDateUtc varchar(100)
,salesOrderUuid varchar(100)
,centralOrderNum bigint
)

create table #ProcessData(id int identity(1,1),ProcessData varchar(max))

declare @n int,@rows int,@ProcessData varchar(max)
insert into  #ProcessData (ProcessData) 
select ProcessData from  EventProcessERP 
where ERPEventProcessType=4 
and ProcessStatus=1 
and ProfileNum=10003 
and MasterAccountNum=10002
 

select @rows =@@rowcount
set @n=1
while @n<=@rows
begin
  select @ProcessData=ProcessData from #ProcessData where id=@n 

--declare @processdata varchar(max)='{"shipmentHeader":{"shipmentID":"113-10000000515","shippingCarrier":"UPS","shippingClass":"UPS® Ground","shippingCost":20.0000,"mainTrackingNumber":"UPS-10000000515","mainReturnTrackingNumber":"","billOfLadingID":"","totalPackages":0,"totalHandlingFee":0.0,"totalShippedQty":2.0,"totalCanceledQty":0.0,"totalWeight":0.0,"totalVolume":0.0,"weightUnit":0,"lengthUnit":0,"volumeUnit":0,"invoiceNumber":"","salesOrderUuid":"38b047fa-34cf-4be4-b4ba-5cd90c7a1d33","warehouseCode":"Main","orderDCAssignmentNum":0,"centralOrderNum":100004286,"channelOrderID":"99308504","shipmentType":0,"shipmentReferenceID":"","shipmentDateUtc":"2021-12-03T03:39:09.5916512","shipmentStatus":1},"packageItems":[{"shipmentPackage":{"packageID":"10000000515-FEDEX_BOX","packageType":0,"packagePatternNum":0,"packageTrackingNumber":"UPS-10000000515","packageReturnTrackingNumber":"","packageWeight":0.0,"packageLength":0.0,"packageWidth":0.0,"packageHeight":0.0,"packageVolume":0.0,"packageQty":2.0,"parentPackageNum":0,"hasChildPackage":false},"shippedItems":[{"salesOrderItemsUuid":"0-597","centralOrderLineNum":10005075,"orderDCAssignmentLineNum":0,"sku":"FA16023WV-MIDNIGHTBLUE-XL-STD","unitHandlingFee":0.0,"shippedQty":1.0,"lineHandlingFee":0.0,"enterDateUtc":"2021-12-03T03:48:01.1633121Z"},{"salesOrderItemsUuid":"0-598","centralOrderLineNum":10005074,"orderDCAssignmentLineNum":0,"sku":"FA16023WV-SLATE-XL-STD","unitHandlingFee":0.0,"shippedQty":1.0,"lineHandlingFee":0.0,"enterDateUtc":"2021-12-03T03:48:01.1633207Z"}]}],"canceledItems":[]}'
declare @salesorderuuid varchar(100),@shipmentID varchar(100)
--put wms shipment header to tmp table
select @salesorderuuid=Json_Value (value,'$.salesOrderUuid'),@shipmentID=Json_Value (value,'$.shipmentID')
from openjson (@ProcessData) 
where [key]='shipmentHeader' 

--put wms shipment header to tmp table
insert into #wmsshipment(shipmentID,shippingCarrier,shippingClass,shippingCost,mainTrackingNumber,channelOrderID,shipmentDateUtc,salesOrderUuid,centralOrderNum)   
 select 
 trim(Json_Value (value,'$.shipmentID'))  as shipmentID
,Json_Value (value,'$.shippingCarrier')  as shippingCarrier
,Json_Value (value,'$.shippingClass')  as shippingClass
,Json_Value (value,'$.shippingCost')  as shippingCost  
,Json_Value (value,'$.mainTrackingNumber')  as mainTrackingNumber  
,Json_Value (value,'$.channelOrderID')  as channelOrderID
,Json_Value (value,'$.shipmentDateUtc')  as shipmentDateUtc 
,trim(Json_Value (value,'$.salesOrderUuid'))  as salesOrderUuid 
,Json_Value (value,'$.centralOrderNum')  as centralOrderNum 
from openjson (@ProcessData) 
where [key]='shipmentHeader'
 
  select @n=@n+1
end

 

select ProcessUuid  as salesOrderUuid into #salesordersDownloadedByWMS
from EventProcessERP  
where ERPEventProcessType=2 and MasterAccountNum=10002 and ProfileNum=10003
and  UpdateDateUtc>'2021-12-03'  and ActionStatus=1 and ProcessStatus=1

select orders.salesOrderUuid
into #shipmentNotUpload
from #salesordersDownloadedByWMS orders
left join #wmsshipment shipments on orders.salesOrderUuid=shipments.salesOrderUuid
where shipments.salesOrderUuid is null
--group by orders.salesOrderUuid 

--select * from #salesordersDownloadedByWMS

select count(1) as wmsuploadshipmentCount from #wmsshipment
select  salesOrderUuid from #wmsshipment group by salesOrderUuid--654
select shipmentID from #wmsshipment   group by shipmentID--654 +81

--select count(1) as salesordersDownloadedByWMSCount from #salesordersDownloadedByWMS
--select * from #salesordersDownloadedByWMS group by salesOrderUuid

select * from #shipmentNotUpload shipment
left join SalesOrderHeader header on header.SalesOrderUuid=shipment.salesOrderUuid
where header.SalesOrderUuid is null

--drop table #shipmentNotUpload
--drop table #wmsshipment  
--drop table #salesordersDownloadedByWMS
--drop table #ProcessData 
