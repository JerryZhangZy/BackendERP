select top 100* from SalesOrderHeader where MasterAccountNum=10002 and ProfileNum=10003 order by EnterDateUtc desc

select top 1000 oln.* from SalesOrderItems oln inner join (
	select * from SalesOrderHeader where MasterAccountNum=10002 and ProfileNum=10003
) ord on (ord.SalesOrderUuid = oln.SalesOrderUuid)

select * from OrderDCAssignmentHeader
select * from OrderDCAssignmentLine
select * from OrderHeader
select * from OrderLine

