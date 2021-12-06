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

--{"salesOrderItemsUuid":"0-597","centralOrderLineNum":10005075,"orderDCAssignmentLineNum":0,"sku":"FA16023WV-MIDNIGHTBLUE-XL-STD","unitHandlingFee":0.0,"shippedQty":1.0,"lineHandlingFee":0.0,"enterDateUtc":"2021-12-03T03:48:01.1633121Z"}
create table #wmsshipmentItem(
shipmentID varchar(1000) 
,salesOrderUuid varchar(100)
,shippedQty decimal
,centralOrderLineNum bigint
,salesOrderItemsUuid varchar(100)
,sku varchar(100)
)

--create table #wmsshipmentPackage
--(
--packageID varchar(1000)  
--packageTrackingNumber varchar(1000)  
--packageReturnTrackingNumber
--packageWeight
--packageLength
--packageWidth
--packageHeight
--packageVolume
--packageQty
--parentPackageNum
--hasChildPackage
--)


create table #ProcessData(id int identity(1,1),ProcessData varchar(max))

declare @n int,@rows int,@ProcessData varchar(max)
insert into  #ProcessData (ProcessData) 
select ProcessData from  EventProcessERP 
where ERPEventProcessType=4 and ProcessStatus=1 and ProfileNum=10003 and MasterAccountNum=10002
--and RowNum 
--not in (4756,
--4774,
--4775,
--4776,
--4777)

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
 Json_Value (value,'$.shipmentID')  as shipmentID
,Json_Value (value,'$.shippingCarrier')  as shippingCarrier
,Json_Value (value,'$.shippingClass')  as shippingClass
,Json_Value (value,'$.shippingCost')  as shippingCost  
,Json_Value (value,'$.mainTrackingNumber')  as mainTrackingNumber  
,Json_Value (value,'$.channelOrderID')  as channelOrderID
,Json_Value (value,'$.shipmentDateUtc')  as shipmentDateUtc 
,Json_Value (value,'$.salesOrderUuid')  as salesOrderUuid 
,Json_Value (value,'$.centralOrderNum')  as centralOrderNum 
from openjson (@ProcessData) 
where [key]='shipmentHeader'


--put wms shipment shippedItems to tmp table
insert into #wmsshipmentItem(shipmentID,salesOrderUuid,shippedQty,centralOrderLineNum,salesOrderItemsUuid,sku)
select  
 @shipmentID as shipmentID
 ,@salesorderuuid as salesorderuuid
,Json_Value (value,'$.shippedQty')  as shippedQty
,Json_Value (value,'$.centralOrderLineNum')  as centralOrderLineNum
,Json_Value (value,'$.salesOrderItemsUuid')  as salesOrderItemsUuid  
,Json_Value (value,'$.sku')  as sku
from openjson (@ProcessData,'$.packageItems[0].shippedItems') 




  print (@n)
  select @n=@n+1
end

drop table #ProcessData  

select  
shipment.InvoiceUuid
,shipment.OrderShipmentUuid 

,shipment.SalesOrderUuid as SalesOrderUuidInShipment
,wmsShipment.salesOrderUuid as SalesOrderUuidInWMSShipment

,wmsShipment.centralOrderNum as centralOrderNumInWMSShipment
,shipment.CentralOrderNum as CentralOrderNumInShipment 
,orderInfo.CentralOrderNum as CentralOrderNumInOrder
,invoiceInfo.CentralOrderNum as CentralOrderNumInInvoice

,shipment.ChannelAccountNum as ChannelAccountNumInShipment
,invoiceInfo.ChannelAccountNum as ChannelAccountNumInInvoice
,orderInfo.ChannelAccountNum as ChannelAccountNumInOrder

,shipment.ChannelNum as ChannelNumInShipment
,invoiceInfo.ChannelNum as ChannelNumInInvoice
,orderInfo.ChannelNum as ChannelNumInOrder

,wmsShipment.channelOrderID as ChannelOrderIDInWMSShipment
,shipment.ChannelOrderID as ChannelOrderIDInShipment
,invoiceInfo.ChannelOrderID as ChannelOrderIDInInvoice
,orderInfo.ChannelOrderID as ChannelOrderIDInOrder

,shipment.MasterAccountNum as MasterAccountNumInShipment
,invoice.MasterAccountNum as MasterAccountNumInInvoice
,orderHeader.MasterAccountNum as MasterAccountNumInOrder

,shipment.ProfileNum as ProfileNumInShipment
,invoice.ProfileNum as ProfileNumInInvoice
,orderHeader.ProfileNum as ProfileNumInOrder

,wmsShipment.MainTrackingNumber as MainTrackingNumberInWMSShipment
,shipment.MainTrackingNumber as MainTrackingNumberInShipment 


,wmsShipment.shippingCost as shippingCostInWMSShipment
,invoice.ShippingAmount as ShippingAmountInInvoice


,wmsShipment.shippingCarrier as shippingCarrierInWMSShipment
,invoiceInfo.shippingCarrier as shippingCarrierInInvoice


,wmsShipment.shippingClass as shippingClassInWMSShipment
,invoiceInfo.shippingClass as shippingClassInInvoice

,invoice.SubTotalAmount as SubTotalAmountInInvoice
,orderHeader.SubTotalAmount as SubTotalAmountInOrder

into #compareHeader 
from #wmsshipment wmsShipment  
left join OrderShipmentHeader  shipment on shipment.ShipmentID=wmsShipment.shipmentID
left join SalesOrderHeader  orderHeader on orderHeader.SalesOrderUuid=shipment.SalesOrderUuid
left join SalesOrderHeaderInfo orderInfo on orderInfo.SalesOrderUuid=orderHeader.SalesOrderUuid
left join InvoiceHeader invoice on invoice.InvoiceUuid=shipment.InvoiceUuid 
left join InvoiceHeaderInfo invoiceInfo on invoiceInfo.InvoiceUuid=invoice.InvoiceUuid 

select * from  #compareHeader
where isnull(SubTotalAmountInInvoice,0)!=isnull(SubTotalAmountInOrder,1)

 -- no result means all data matched.
select * from #compareHeader c
where 
c.centralOrderNumInWMSShipment!=c.CentralOrderNumInShipment or c.CentralOrderNumInShipment != c.CentralOrderNumInInvoice or c.CentralOrderNumInShipment != c.CentralOrderNumInOrder
or c.ChannelAccountNumInShipment!=c.ChannelAccountNumInOrder or c.ChannelAccountNumInShipment!=c.ChannelAccountNumInInvoice
or c.ChannelNumInShipment!=c.ChannelNumInOrder or c.ChannelNumInShipment!=c.ChannelNumInInvoice
or c.ChannelOrderIDInWMSShipment!=c.ChannelOrderIDInShipment or c.ChannelOrderIDInShipment!=c.ChannelOrderIDInOrder or c.ChannelOrderIDInShipment!=c.ChannelOrderIDInInvoice
or c.MainTrackingNumberInWMSShipment!=c.MainTrackingNumberInShipment
or c.SalesOrderUuidInWMSShipment!=c.SalesOrderUuidInShipment
or c.MasterAccountNumInShipment!=c.MasterAccountNumInOrder or c.MasterAccountNumInShipment!=c.MasterAccountNumInInvoice
or c.ProfileNumInShipment!=c.ProfileNumInOrder or c.ProfileNumInShipment!=c.ProfileNumInInvoice
or c.shippingCostInWMSShipment!= c.ShippingAmountInInvoice
or c.shippingCarrierInWMSShipment!=c.shippingCarrierInInvoice
or c.shippingClassInWMSShipment!=c.shippingClassInInvoice 


 --compare wms shipment item & erp shipment item
 select wmsItem.sku as skuInWMS, erpShipmentItem.SKU as skuInErp
 ,wmsItem.shippedQty as shippedQtyInWMS, erpShipmentItem.ShippedQty as shippedQtyInErp   
 into #comparewmsShipmentAndErpShiment
 from #wmsshipmentItem wmsItem
 left join OrderShipmentHeader erpShipmentHeader on wmsItem.shipmentID=erpShipmentHeader.ShipmentID
 left join OrderShipmentShippedItem erpShipmentItem on wmsItem.sku= erpShipmentItem.SKU  and  erpShipmentItem.OrderShipmentUuid=erpShipmentHeader.OrderShipmentUuid

 select * from #comparewmsShipmentAndErpShiment
 where 
 skuInErp is null or shippedQtyInWMS is null or shippedQtyInWMS is null
 or skuInWMS!=skuInErp 
 or shippedQtyInWMS!=shippedQtyInErp  

 --compare wms item & salesorder item
 select wmsItem.sku as skuInWMS, orderItem.SKU as skuInOrder
 ,wmsItem.shippedQty as shippedQtyInWMS, orderItem.ShipQty as shippedQtyInOrder
 ,wmsItem.salesOrderItemsUuid salesOrderItemsUuidInWMS,orderItem.SalesOrderItemsUuid as  SalesOrderItemsUuidInOrder
 ,wmsItem.shipmentID
 --,wmsItem.centralOrderLineNum as centralOrderLineNumInWMS
 into #comparewmsShipmentAndOrder
 from #wmsshipmentItem wmsItem 
 left join SalesOrderItems orderItem on wmsItem.salesOrderUuid= orderItem.SalesOrderUuid and wmsItem.salesOrderItemsUuid=orderItem.SalesOrderItemsUuid 

 select 
 shipmentID 
 ,*
 from #comparewmsShipmentAndOrder
 where  
    SalesOrderItemsUuidInOrder is null or skuInOrder is null or shippedQtyInWMS is null-- item in wms shipment ,not in erp s/o
 or skuInOrder!=skuInWMS 
 or shippedQtyInWMS!=shippedQtyInOrder
 or salesOrderItemsUuidInWMS!=SalesOrderItemsUuidInOrder 

 --check invoie count is match wms shipment
 select wmsshipment.shipmentID
 from #wmsshipment wmsshipment 
 left join OrderShipmentHeader erpshipment on wmsshipment.shipmentID=erpshipment.ShipmentID
 left join InvoiceHeader invoice on invoice.InvoiceUuid=erpshipment.InvoiceUuid
 where invoice.InvoiceUuid is null
 
 --select * from #wmsshipment

 --select * from #comparewmsShipmentAndOrder
drop table #wmsshipment
drop table #wmsshipmentItem 
drop table #compareHeader
drop table #comparewmsShipmentAndErpShiment
drop table  #comparewmsShipmentAndOrder