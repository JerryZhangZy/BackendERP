

INSERT INTO [dbo].[ProductExt]
           (
		   [DatabaseNum]
           ,[MasterAccountNum]
           ,[ProfileNum]
           ,[ProductUuid]
           ,[CentralProductNum]
           ,[SKU]
           ,[StyleCode]
           ,[ColorPatternCode]
           ,[SizeType]
           ,[SizeCode]
           ,[WidthCode]
           ,[LengthCode]
           ,[ClassCode]
           ,[SubClassCode]
           ,[DepartmentCode]
           ,[DivisionCode]
           ,[OEMCode]
           ,[AlternateCode]
           ,[Remark]
           ,[Model]
           ,[CatalogPage]
           ,[CategoryCode]
           ,[GroupCode]
           ,[SubGroupCode]
           ,[PriceRule]
           ,[Stockable]
           ,[IsAr]
           ,[IsAp]
           ,[Taxable]
           ,[Costable]
           ,[IsProfit]
           ,[Release]
           ,[Currency]
           ,[UOM]
           ,[QtyPerPallot]
           ,[QtyPerCase]
           ,[QtyPerBox]
           ,[PackType]
           ,[PackQty]
           ,[DefaultPackType]
           ,[DefaultWarehouseCode]
           ,[DefaultVendorCode]
           ,[PoSize]
           ,[MinStock]
           ,[SalesCost]
           ,[LeadTimeDay]
           ,[ProductYear]
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
           ,prd.CentralProductNum
           ,prd.SKU
           ,prd.SKU
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,''
           ,'EA'
           ,1
           ,1
           ,1
           ,''
           ,1
           ,''
           ,COALESCE(whs.DistributionCenterCode, '')
           ,''
           ,100
           ,1000
           ,0
           ,0
           ,''
           ,GETDATE()
           ,'SYSTEM'
           ,'SYSTEM'
FROM ProductBasic prd
OUTER APPLY (
SELECT TOP 1 *
FROM DistributionCenter dc
WHERE dc.MasterAccountNum = prd.MasterAccountNum AND dc.ProfileNum = prd.ProfileNum AND dc.DatabaseNum = prd.DatabaseNum
ORDER BY dc.DistributionCenterCode
) whs 
where prd.ProductUuid != ''
AND NOT EXISTS (SELECT * FROM ProductExt i WHERE i.ProductUuid = prd.ProductUuid)



