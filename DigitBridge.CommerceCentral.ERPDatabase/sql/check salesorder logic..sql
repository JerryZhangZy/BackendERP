-- get all TotalAmount=0 order 
select   dcHeader.CentralOrderUuid,tmp.SalesOrderUuid into #CentralOrderUuidList
from [OrderDCAssignmentHeader] dcHeader
join  
(
select  top 100  SUBSTRING(OrderSourceCode,22,4) as OrderDCAssignmentNum, SalesOrderUuid  
from SalesOrderHeader 
where MasterAccountNum=10002 and ProfileNum=10003 and TotalAmount=0  and OrderDate='2021-11-09'
order by RowNum desc
) tmp on tmp.OrderDCAssignmentNum=dcHeader.OrderDCAssignmentNum
order by dcHeader.OrderDCAssignmentNum desc



select 
uuid.CentralOrderUuid,
uuid.SalesOrderUuid,
abs(header.PromotionAmount) as headerDiscountAmount  
,header.TotalShippingAmount as headerShippingAmount --(coHeader.TotalShippingAmount ?? 0) * qtyRatio;
,header.TotalShippingAmount as headerShippingAmount --(coHeader.TotalShippingAmount ?? 0) * qtyRatio;
,header.TotalShippingTaxAmount as headerShippingTaxAmount --(coHeader.TotalShippingTaxAmount ?? 0) * qtyRatio;
,case when header.PaymentStatus=1 then header.TotalDueSellerAmount
      else 0 
 end as headerPaidAmount --(coHeader.PaymentStatus) ? coHeader.TotalDueSellerAmount : 0; 

,line.OrderQty 
,line.UnitDueSellerAmount as linePrice
,ABS(line.LinePromotionAmount) as lineDiscountAmount
,line.LineItemTaxAmount as lineTaxAmount
,line.LineShippingAmount as lineShippingAmount
,line.LineShippingTaxAmount as lineShippingTaxAmount
,line.LineGiftAmount as lineMiscAmount
,line.LineGiftTaxAmount as lineMiscTaxAmount
,line.LineItemTaxAmount as lineTaxable
,case when line.LineItemTaxAmount >0 then 1
      else 0 
 end as lineTaxable --(coHeader.PaymentStatus) ? coHeader.TotalDueSellerAmount : 0;  

from #CentralOrderUuidList uuid
join OrderHeader header on header.CentralOrderUuid=uuid.CentralOrderUuid
join OrderLine line on line.[CentralOrderUuid]=uuid.[CentralOrderUuid]  

drop table #CentralOrderUuidList


--SELECT [OrderHeader].[DatabaseNum], [OrderHeader].[CentralOrderNum], [OrderHeader].[MasterAccountNum], [OrderHeader].[ProfileNum], [OrderHeader].[ChannelNum], [OrderHeader].[ChannelAccountNum], [OrderHeader].[UserDataPresent], [OrderHeader].[UserDataRemoveDateUtc], [OrderHeader].[ChannelOrderID], [OrderHeader].[SecondaryChannelOrderID], [OrderHeader].[SellerOrderID], [OrderHeader].[Currency], [OrderHeader].[OriginalOrderDateUtc], [OrderHeader].[SellerPublicNote], [OrderHeader].[SellerPrivateNote], [OrderHeader].[EndBuyerInstruction], [OrderHeader].[TotalOrderAmount], [OrderHeader].[TotalTaxAmount], [OrderHeader].[TotalShippingAmount], [OrderHeader].[TotalShippingTaxAmount], [OrderHeader].[TotalShippingDiscount], [OrderHeader].[TotalShippingDiscountTaxAmount], [OrderHeader].[TotalInsuranceAmount], [OrderHeader].[TotalGiftOptionAmount], [OrderHeader].[TotalGiftOptionTaxAmount], [OrderHeader].[AdditionalCostOrDiscount], [OrderHeader].[PromotionAmount], [OrderHeader].[EstimatedShipDateUtc], [OrderHeader].[DeliverByDateUtc], [OrderHeader].[RequestedShippingCarrier], [OrderHeader].[RequestedShippingClass], [OrderHeader].[ResellerID], [OrderHeader].[FlagNum], [OrderHeader].[FlagDesc], [OrderHeader].[PaymentStatus], [OrderHeader].[PaymentUpdateUtc], [OrderHeader].[ShippingUpdateUtc], [OrderHeader].[EndBuyerUserID], [OrderHeader].[EndBuyerEmail], [OrderHeader].[PaymentMethod], [OrderHeader].[ShipToName], [OrderHeader].[ShipToFirstName], [OrderHeader].[ShipToLastName], [OrderHeader].[ShipToSuffix], [OrderHeader].[ShipToCompany], [OrderHeader].[ShipToCompanyJobTitle], [OrderHeader].[ShipToAttention], [OrderHeader].[ShipToDaytimePhone], [OrderHeader].[ShipToNightPhone], [OrderHeader].[ShipToAddressLine1], [OrderHeader].[ShipToAddressLine2], [OrderHeader].[ShipToAddressLine3], [OrderHeader].[ShipToCity], [OrderHeader].[ShipToState], [OrderHeader].[ShipToStateFullName], [OrderHeader].[ShipToPostalCode], [OrderHeader].[ShipToPostalCodeExt], [OrderHeader].[ShipToCounty], [OrderHeader].[ShipToCountry], [OrderHeader].[ShipToEmail], [OrderHeader].[BillToName], [OrderHeader].[BillToFirstName], [OrderHeader].[BillToLastName], [OrderHeader].[BillToSuffix], [OrderHeader].[BillToCompany], [OrderHeader].[BillToCompanyJobTitle], [OrderHeader].[BillToAttention], [OrderHeader].[BillToAddressLine1], [OrderHeader].[BillToAddressLine2], [OrderHeader].[BillToAddressLine3], [OrderHeader].[BillToCity], [OrderHeader].[BillToState], [OrderHeader].[BillToStateFullName], [OrderHeader].[BillToPostalCode], [OrderHeader].[BillToPostalCodeExt], [OrderHeader].[BillToCounty], [OrderHeader].[BillToCountry], [OrderHeader].[BillToEmail], [OrderHeader].[BillToDaytimePhone], [OrderHeader].[BillToNightPhone], [OrderHeader].[SignatureFlag], [OrderHeader].[PickupFlag], [OrderHeader].[MerchantDivision], [OrderHeader].[MerchantBatchNumber], [OrderHeader].[MerchantDepartmentSiteID], [OrderHeader].[ReservationNumber], [OrderHeader].[MerchantShipToAddressType], [OrderHeader].[CustomerOrganizationType], [OrderHeader].[OrderMark], [OrderHeader].[OrderMark2], [OrderHeader].[OrderStatus], [OrderHeader].[OrderStatusUpdateDateUtc], [OrderHeader].[DBChannelOrderHeaderRowID], [OrderHeader].[DCAssignmentStatus], [OrderHeader].[DCAssignmentDateUtc], [OrderHeader].[CentralOrderUuid], [OrderHeader].[TotalDueSellerAmount], [OrderHeader].[RowNum], [OrderHeader].[EnterDateUtc], [OrderHeader].[DigitBridgeGuid] 
--FROM [OrderHeader] 
--WHERE [CentralOrderUuid] = 'd856b0a4-f94c-4b37-9f28-8b8577de437d'

--select  * from OrderDCAssignmentLine
--WHERE [CentralOrderUuid] = 'd856b0a4-f94c-4b37-9f28-8b8577de437d'


select * from OrderLine WHERE [CentralOrderUuid] = '040898d5-441d-4b4a-aef4-10c5e3fdf812'
select * from OrderHeader WHERE [CentralOrderUuid] = '040898d5-441d-4b4a-aef4-10c5e3fdf812'

--select  
--line.UnitDueSellerAmount as linePrice
--,ABS(line.LinePromotionAmount) as lineDiscountAmount
--,line.LineItemTaxAmount as lineTaxAmount
--,line.LineShippingAmount as lineShippingAmount
--,line.LineShippingTaxAmount as lineShippingTaxAmount
--,line.LineGiftAmount as lineMiscAmount
--,line.LineGiftTaxAmount as lineMiscTaxAmount
--,line.LineItemTaxAmount as lineTaxable
--from OrderLine line
--WHERE [CentralOrderUuid] = 'd856b0a4-f94c-4b37-9f28-8b8577de437d'

select * from SalesOrderHeader where SalesOrderUuid='746f232b-6d6a-4507-92da-64f0aa2632a0'