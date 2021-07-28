
-- 07/28/20201 By Yunman Li
IF COL_LENGTH('OrderHeaderPromotion', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderHeaderPromotion ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ('')
    ALTER TABLE OrderHeaderPromotion ADD [CentralOrderHeaderPromotionUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderHeaderPromotion_CentralOrderHeaderPromotionUuid] ON [dbo].OrderHeaderPromotion
	(
		CentralOrderHeaderPromotionUuid ASC
	) 
END					

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderHeaderPromotion spp
    INNER JOIN OrderHeader sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/