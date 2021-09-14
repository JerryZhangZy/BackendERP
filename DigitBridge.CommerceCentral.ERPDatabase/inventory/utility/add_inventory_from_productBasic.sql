
INSERT INTO [dbo].[Inventory]
           (
		   [DatabaseNum]
           ,[MasterAccountNum]
           ,[ProfileNum]
           ,[ProductUuid]
           ,[InventoryUuid]
           ,[StyleCode]
           ,[ColorPatternCode]
           ,[SizeType]
           ,[SizeCode]
           ,[WidthCode]
           ,[LengthCode]
           ,[PriceRule]
           ,[LeadTimeDay]
           ,[PoSize]
           ,[MinStock]
           ,[SKU]
           ,[WarehouseUuid]
           ,[WarehouseCode]
           ,[WarehouseName]
           ,[LotNum]
           ,[LotInDate]
           ,[LotExpDate]
           ,[LotDescription]
           ,[LpnNum]
           ,[LpnDescription]
           ,[Notes]
           ,[Currency]
           ,[UOM]
           ,[QtyPerPallot]
           ,[QtyPerCase]
           ,[QtyPerBox]
           ,[PackType]
           ,[PackQty]
           ,[DefaultPackType]
           ,[Instock]
           ,[OnHand]
           ,[OpenSoQty]
           ,[OpenFulfillmentQty]
           ,[AvaQty]
           ,[OpenPoQty]
           ,[OpenInTransitQty]
           ,[OpenWipQty]
           ,[ProjectedQty]
           ,[BaseCost]
           ,[TaxRate]
           ,[TaxAmount]
           ,[ShippingAmount]
           ,[MiscAmount]
           ,[ChargeAndAllowanceAmount]
           ,[UnitCost]
           ,[AvgCost]
           ,[SalesCost]
           ,[UpdateDateUtc]
           ,[EnterBy]
           ,[UpdateBy]
           --,[EnterDateUtc]
           --,[DigitBridgeGuid]
		   )
SELECT
           prd.DatabaseNum
           ,prd.MasterAccountNum
           ,prd.ProfileNum
           ,prd.ProductUuid
           ,CAST(newid() AS NVARCHAR(50))
           ,prd.SKU
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,0
           ,100
           ,1000
           ,prd.SKU
           ,COALESCE(whs.DistributionCenterUuid, '')
           ,COALESCE(whs.DistributionCenterCode, '')
           ,COALESCE(whs.DistributionCenterName, '') --<WarehouseName, nvarchar(200),>
           ,''
           ,null
           ,null
           ,''
           ,''
           ,''
           ,''
           ,''
           ,'EA'
           ,1
           ,1
           ,1
           ,1
           ,1
           ,0
           ,COALESCE(qty.AvailableQuantity,0)	--<Instock, decimal(24,6),>
           ,0
           ,0
           ,0
           ,COALESCE(qty.AvailableQuantity,0)
           ,0
           ,0
           ,0
           ,COALESCE(qty.AvailableQuantity,0)
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,GETDATE()
           ,'SYSTEM'
           ,'SYSTEM'
FROM ProductBasic prd
LEFT JOIN DistributionCenter whs 
ON (whs.MasterAccountNum = prd.MasterAccountNum AND whs.ProfileNum = prd.ProfileNum AND whs.DatabaseNum = prd.DatabaseNum)
LEFT JOIN ProductDistributionCenterQuantity qty
ON (qty.MasterAccountNum = prd.MasterAccountNum AND qty.ProfileNum = prd.ProfileNum AND qty.DatabaseNum = prd.DatabaseNum 
AND qty.CentralProductNum = prd.CentralProductNum AND qty.DistributionCenterNum = whs.DistributionCenterNum)
where prd.ProductUuid != ''
AND NOT EXISTS (SELECT * FROM Inventory i WHERE i.ProductUuid = prd.ProductUuid)





