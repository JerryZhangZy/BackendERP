--select * from InvoiceTransaction where InvoiceUuid IN('69df82dd-1ee4-4759-8d7c-b9381c9f9a18', 'afc62d87-3c54-43e8-869f-5af8effb89fb', 'c74969c9-0cd9-49ed-ba0e-7c04ccc4474b')

select * from InvoiceTransaction where paymentNUmber = 25

select balance, * from InvoiceHeader where invoiceUuid IN ('69df82dd-1ee4-4759-8d7c-b9381c9f9a18', 'cfb3e181-7c20-4829-b83b-e44ec000c8ed')
select TotalAmount, PaidBy, * from InvoiceTransaction where InvoiceUuid IN ('69df82dd-1ee4-4759-8d7c-b9381c9f9a18')
select TotalAmount, PaidBy, * from InvoiceTransaction where InvoiceUuid IN ('cfb3e181-7c20-4829-b83b-e44ec000c8ed')

select TotalAmount, PaidBy, * from MiscInvoiceTransaction where MiscInvoiceUuid IN ('f0530be1-bde3-47ca-8a65-9e30e307b32b')


select balance, * from InvoiceHeader 
where Balance < 0 and CustomerCode = 'cumque'
order by RowNum desc

select balance, * from MiscInvoiceHeader
--update MiscInvoiceHeader set customerCode = 'cumque', CustomerUuid = '4fba6d7a-cd45-4a1a-9da1-8de390ffaeb1' where rowNum = 39
where CustomerCode = 'cumque'
order by RowNum desc


SELECT trs.RowNum
FROM InvoiceTransaction trs
WHERE Exists (SELECT RowNum FROM InvoiceTransaction WHERE TransUuid = '2b70d681-f282-428a-8366-20f49b48b5e3' AND PaidBy=51)
AND trs.AuthCode = '2b70d681-f282-428a-8366-20f49b48b5e3'

