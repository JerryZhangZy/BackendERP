insert into EventProcessERP (DatabaseNum,MasterAccountNum,ProfileNum,ChannelNum,ChannelAccountNum,ERPEventProcessType,processuuid,actionstatus)
select  
header.DatabaseNum,
header.MasterAccountNum,header.ProfileNum,
info.ChannelNum,info.ChannelAccountNum,
2 as ERPEventProcessType,
header.SalesOrderUuid as processuuid,
0 as actionstatus

from SalesOrderHeader header
left join SalesOrderHeaderInfo info on header.SalesOrderUuid=info.SalesOrderUuid
where OrderStatus=1 and ProfileNum=10001 and MasterAccountNum=10001 and info.ChannelNum is not null 
order by header.RowNum desc 
 
select * from EventProcessERP