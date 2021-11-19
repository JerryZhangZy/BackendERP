select * from Event_ERP order by RowNum desc

select * from SalesOrderHeader ord
inner join SalesOrderHeaderInfo soi ON (soi.SalesOrderUuid = ord.SalesOrderUuid)
where soi.CentralOrderUuid IN ('1daac816-0eef-4c3e-90de-b7fa3e7269bb', '28917ee1-3ab2-438a-a0f3-552e8865fce8', 'b302d57f-1da9-4328-b46b-132222419fcf')

select * from SalesOrderItems olg
inner join SalesOrderHeaderInfo soi ON (soi.SalesOrderUuid = olg.SalesOrderUuid)
where soi.CentralOrderUuid IN ('1daac816-0eef-4c3e-90de-b7fa3e7269bb', '28917ee1-3ab2-438a-a0f3-552e8865fce8', 'b302d57f-1da9-4328-b46b-132222419fcf')


select * from OrderHeader ord
where ord.CentralOrderUuid IN ('1daac816-0eef-4c3e-90de-b7fa3e7269bb', '28917ee1-3ab2-438a-a0f3-552e8865fce8', 'b302d57f-1da9-4328-b46b-132222419fcf')
