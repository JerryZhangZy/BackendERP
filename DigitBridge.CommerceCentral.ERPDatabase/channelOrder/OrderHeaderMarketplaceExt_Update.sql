
-- 07/28/20201 By Yunman Li
IF COL_LENGTH('OrderHeaderMarketplaceExt', 'CentralOrderUuid') IS NULL					
BEGIN					
    ALTER TABLE OrderHeaderMarketplaceExt ADD [CentralOrderUuid] VARCHAR(50) NOT NULL DEFAULT ('')
    ALTER TABLE OrderHeaderMarketplaceExt ADD [CentralOrderHeaderMarketplaceExtUuid] VARCHAR(50) NOT NULL DEFAULT (CAST(newid() AS NVARCHAR(50)))
	CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderHeader_CentralOrderHeaderMarketplaceExtUuid] ON [dbo].[OrderHeaderMarketplaceExt]
	(
		[CentralOrderHeaderMarketplaceExtUuid] ASC
	) 
END					

/*
    UPDATE spp
    SET spp.CentralOrderUuid = sph.CentralOrderUuid
    FROM OrderHeaderMarketplaceExt spp
    INNER JOIN OrderHeader sph ON (sph.CentralOrderNum = spp.CentralOrderNum);
*/