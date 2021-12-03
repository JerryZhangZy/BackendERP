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
select ProcessData from EventProcessERP where ERPEventProcessType=4 and ProcessStatus=2
select @rows =@@rowcount
set @n=1
while @n<=@rows
begin
  select @ProcessData=ProcessData from #ProcessData where id=@n


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
from openjson
( 
@ProcessData
)  as a
where a.[key]='shipmentHeader'
  print (@n)
  select @n=@n+1
end

drop table #ProcessData 

--select * from #wmsshipment
--drop table #wmsshipment


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



from EventProcessERP ep
join #wmsshipment wmsShipment on wmsShipment.shipmentID=ep.ProcessUuid  
join OrderShipmentHeader  shipment on shipment.ShipmentID=ep.ProcessUuid
join SalesOrderHeader  orderHeader on orderHeader.SalesOrderUuid=shipment.SalesOrderUuid
left join SalesOrderHeaderInfo orderInfo on orderInfo.SalesOrderUuid=orderHeader.SalesOrderUuid
join InvoiceHeader invoice on invoice.InvoiceUuid=shipment.InvoiceUuid 
left join InvoiceHeaderInfo invoiceInfo on invoiceInfo.InvoiceUuid=invoice.InvoiceUuid

where ep.ERPEventProcessType=4  
and ep.ProcessStatus=2
--and shipment.ShipmentID in 
--(
-- select * from #tmpProcessUuid
--)

drop table #wmsshipment