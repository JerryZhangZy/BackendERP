-- get all TotalAmount=0 order 
select   dcHeader.CentralOrderUuid,tmp.SalesOrderUuid into #CentralOrderUuidList
from [OrderDCAssignmentHeader] dcHeader
join  
(
select  top 100  SUBSTRING(OrderSourceCode,22,4) as OrderDCAssignmentNum, SalesOrderUuid  
from SalesOrderHeader header
where MasterAccountNum=10002 and ProfileNum=10003 
and not exists (select 1 from SalesOrderItems item where  item.SalesOrderUuid =header.SalesOrderUuid)
--and TotalAmount=0  and OrderDate='2021-11-09'
--and SalesOrderUuid='d5dd5bd5-74e6-407c-9301-2579c7b2bb10'
order by RowNum desc
) tmp on tmp.OrderDCAssignmentNum=dcHeader.OrderDCAssignmentNum
order by dcHeader.OrderDCAssignmentNum desc

select * from #CentralOrderUuidList

select * from SalesOrderHeader  header
join #CentralOrderUuidList tmp on tmp.SalesOrderUuid=header.SalesOrderUuid


select * from SalesOrderItems  line
join #CentralOrderUuidList tmp on tmp.SalesOrderUuid=line.SalesOrderUuid

select * from OrderHeader  header
join #CentralOrderUuidList tmp on tmp.CentralOrderUuid=header.CentralOrderUuid


select * from OrderLine  line
join #CentralOrderUuidList tmp on tmp.CentralOrderUuid=line.CentralOrderUuid

select SalesOrderUuid,OrderNumber from SalesOrderHeader header
where OrderNumber like '%duplicate'
and not exists (select 1 from SalesOrderItems item where  item.SalesOrderUuid =header.SalesOrderUuid)