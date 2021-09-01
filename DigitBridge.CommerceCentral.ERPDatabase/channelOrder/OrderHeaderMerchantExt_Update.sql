
-- 07/28/20201 By Yunman Li
IF COL_LENGTH('OrderHeaderMerchantExt', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderHeaderMerchantExt ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ('')
    ALTER TABLE OrderHeaderMerchantExt ADD [CentralOrderHeaderMerchantExtUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderHeaderMerchantExt_CentralOrderHeaderMerchantExtUuid] ON [dbo].OrderHeaderMerchantExt
	(
		CentralOrderHeaderMerchantExtUuid ASC
	) 
END					

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderHeaderMerchantExt spp
    INNER JOIN OrderHeader sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/