select OrderSourceCode, * from SalesOrderHeader where OrderSourceCode != '' AND SalesOrderUuid = '716bcd22-3779-45db-a443-1b274995c394'
select * from SalesOrderHeaderInfo where SalesOrderUuid = '716bcd22-3779-45db-a443-1b274995c394'
select * from SalesOrderItems where SalesOrderUuid = '716bcd22-3779-45db-a443-1b274995c394'

select * 
--delete
from OrderShipmentHeader where OrderShipmentUuid = '6c77ced1-9154-4050-8ed4-4934cebec8ef'
select * 
--delete
from OrderShipmentPackage where OrderShipmentUuid = '6c77ced1-9154-4050-8ed4-4934cebec8ef'
select *
--delete
from OrderShipmentShippedItem  where OrderShipmentUuid = '6c77ced1-9154-4050-8ed4-4934cebec8ef'

select * from InvoiceHeader where InvoiceUuid = '8e5aa85d-09bd-44d8-a8b7-acbb44930fc6'
select * from InvoiceHeaderInfo where InvoiceUuid = '8e5aa85d-09bd-44d8-a8b7-acbb44930fc6'
select * from InvoiceItems where InvoiceUuid = '8e5aa85d-09bd-44d8-a8b7-acbb44930fc6'

select OrderSourceCode, * from SalesOrderHeader where OrderSourceCode = 'OrderDCAssignmentNum:8114'
select CentralOrderNum, * from SalesOrderHeaderInfo where CentralOrderNum > 0


select channelAccount.*, * from OrderHeader ord
left join Setting_Channel chanel ON (ord.MasterAccountNum = chanel.MasterAccountNum AND ord.ProfileNum = chanel.ProfileNum AND ord.ChannelNum = chanel.ChannelNum)
left Join Setting_ChannelAccount channelAccount ON (ord.MasterAccountNum = channelAccount.MasterAccountNum AND ord.ProfileNum = channelAccount.ProfileNum AND ord.ChannelNum = channelAccount.ChannelNum AND ord.ChannelAccountNum = channelAccount.ChannelAccountNum)
where ord.CentralOrderNum = 100015225

select soi.*, * from SalesOrderHeader so
left join SalesOrderItems soi on (soi.SalesOrderUuid = so.SalesOrderUuid) 
where so.ProfileNum = 10003
