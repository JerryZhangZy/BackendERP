insert into EventProcessERP (DatabaseNum,MasterAccountNum,ProfileNum,ChannelNum,ChannelAccountNum,ERPEventProcessType,processuuid,actionstatus,ActionDate,ProcessStatus)
select 
top 10
header.DatabaseNum,
header.MasterAccountNum,header.ProfileNum,
info.ChannelNum,info.ChannelAccountNum,
1 as ERPEventProcessType,
header.InvoiceUuid as processuuid,
0 as actionstatus
,getdate() as ActionDate
,0 as  ProcessStatus

from InvoiceHeader header
left join InvoiceHeaderInfo info on header.InvoiceUuid=info.InvoiceUuid
order by header.RowNum desc 
 
select * from EventProcessERP