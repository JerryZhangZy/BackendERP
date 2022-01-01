--reset salesorder to pending.
update ep set ActionStatus=0 
from EventProcessERP ep
join SalesOrderHeader so on so.SalesOrderUuid= ep.ProcessUuid
where ERPEventProcessType=2  and 
so.MasterAccountNum= 10002 and so.ProfileNum=10003 
and ep.MasterAccountNum= 10002 and ep.ProfileNum=10003 
and ActionStatus=1