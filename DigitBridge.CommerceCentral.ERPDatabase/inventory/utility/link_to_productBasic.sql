--update inv set ProductUuid = prd.ProductUuid
select * 
from inventory inv
inner join ProductBasic prd on prd.sku = inv.sku and prd.ProfileNum = inv.ProfileNum

--update inv set ProductUuid = prd.ProductUuid
select * 
from productExt inv
inner join ProductBasic prd on prd.sku = inv.sku and prd.ProfileNum = inv.ProfileNum and prd.productUuid != inv.productUuid


--update ProductBasic set MasterAccountNum = 10001, ProfileNum = 10001, DatabaseNum = 1
update ProductExt set MasterAccountNum = 10001, ProfileNum = 10001, DatabaseNum = 1
update Inventory set MasterAccountNum = 10001, ProfileNum = 10001, DatabaseNum = 1
