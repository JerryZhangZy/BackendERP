insert into EventProcessERP (DatabaseNum,MasterAccountNum,ProfileNum,ChannelNum,ChannelAccountNum,ERPEventProcessType,processuuid,actionstatus,ActionDate,ProcessStatus)
select  
header.DatabaseNum,
header.MasterAccountNum,header.ProfileNum,
isnull(info.ChannelNum,0)
,isnull(info.ChannelAccountNum,0)
,2 as ERPEventProcessType
,header.SalesOrderUuid as processuuid,
0 as actionstatus
,getdate() as ActionDate
,0 as  ProcessStatus

from SalesOrderHeader header
left join SalesOrderHeaderInfo info on header.SalesOrderUuid=info.SalesOrderUuid
where OrderStatus=1 and ProfileNum=10001 and MasterAccountNum=10001 and info.ChannelNum is not null 
order by header.RowNum desc 
 
 
 
select * from EventProcessERP