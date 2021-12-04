--totoal upload.
select count(1) from EventProcessERP ep 
where ep.ERPEventProcessType=4 

-- transfer succeed
select count(1) from EventProcessERP ep 
where ep.ERPEventProcessType=4 
and ep.ProcessStatus=2

--transfer failed.
select count(1) from EventProcessERP ep 
where ep.ERPEventProcessType=4 
and ep.ProcessStatus=1

--only upload, not transfer
select count(1) from EventProcessERP ep 
where ep.ERPEventProcessType=4 
and ep.ProcessStatus=0
 