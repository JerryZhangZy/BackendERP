select* from EventProcessERP
where MasterAccountNum = 10002 and ProfileNum = 10003
AND ActionStatus = 1 --and ProcessStatus != 0

select* from Event_ERP
where MasterAccountNum = 10002 and ProfileNum = 10003
and actionStatus = 0
