--totoal upload.
select count(1) as total_uploaded from EventProcessERP ep 
where ep.ERPEventProcessType=4 

-- transfer succeed
select count(1) as succeed_Transfered from EventProcessERP ep 
where ep.ERPEventProcessType=4 
and ep.ProcessStatus=1

--transfer failed.
select count(1) failed_transfered from EventProcessERP ep 
where ep.ERPEventProcessType=4 
and ep.ProcessStatus=2

--only upload, not transfer
select count(1) as uploaded_not_transfer from EventProcessERP ep 
where ep.ERPEventProcessType=4 
and ep.ProcessStatus=0
 