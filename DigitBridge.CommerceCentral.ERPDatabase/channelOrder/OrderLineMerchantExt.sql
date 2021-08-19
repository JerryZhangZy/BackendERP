CREATE TABLE [dbo].[OrderLineMerchantExt]
(
	[RowNum] BIGINT IDENTITY(1,1) NOT NULL,
	[CentralOrderLineNum] bigint NOT NULL DEFAULT 0,
    [DatabaseNum] INT NOT NULL DEFAULT 0, --Each database has its own default value.
	[CentralOrderNum] bigint NOT NULL DEFAULT 0,
	[MasterAccountNum] int NOT NULL DEFAULT 0,
	[ProfileNum] int NOT NULL DEFAULT 0,
	[ChannelNum] int NOT NULL DEFAULT 0, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL DEFAULT 0, --The unique number of this profile’s channel account
	[ChannelOrderID] varchar(130) NOT NULL DEFAULT '',
	[ChannelOrderLineNum] int NOT NULL DEFAULT 0, --sequentially ordered I 4 number assigned to each line item included in the purchase order.
	[MerchantLineNumber] int NOT NULL DEFAULT 0, --This field will contain a non-sequentially ordered number assigned by the retailer to each line Item in the purchase order.
	[CustomerOrderLineNumber] int NOT NULL DEFAULT 0, --This field will contain a non-sequentially ordered I 4 number assigned to each line in the customer order.
	[HubLineID] varchar(50) NOT NULL DEFAULT '',
	[MerchantNRProductID] varchar(50) NOT NULL DEFAULT '', --The merchant’s product ID for the product on the line item.
	[MerchantSKU] varchar(50) NOT NULL DEFAULT '', --The merchant’s SKU for the product on the line item.
	[VendorSKU] varchar(50) NOT NULL DEFAULT '',
	[ManufacturerSKU] varchar(50) NOT NULL DEFAULT '',
	[ShoppingCartSKU] varchar(50) NOT NULL DEFAULT '',
	[VendorDescription]	Varchar(255) NOT NULL DEFAULT '', --Vendor oriented description of the product
	[VendorStyleNumber]	Varchar(255) NOT NULL DEFAULT '', --A vendor assigned style number for the product on the line item.
	[VendorColorDescription] varchar(50) NOT NULL DEFAULT '', --A vendor description / color for the color of the product on the line item.
	[VendorSizeDescription]	varchar(20) NOT NULL DEFAULT '', --A vendor description / code for the size of the product on the line item.
	[MerchantColorCode]	varchar(20) NOT NULL DEFAULT '',
	[MerchantSizeCode] varchar(20) NOT NULL DEFAULT '',
	[MerchantSetCode] varchar(50) NOT NULL DEFAULT '',
	[MerchantDescription] Varchar(255) NOT NULL DEFAULT '',
	[MerchantDescription2] Varchar(255) NOT NULL DEFAULT '',
	[MerchantDescription3] Varchar(255) NOT NULL DEFAULT '',
	[MerchantColorSizeDescription] varchar(20) NOT NULL DEFAULT '',
	[FullRetailPrice] money NOT NULL DEFAULT 0,
	[EncodedPrice] VARCHAR(50) NOT NULL DEFAULT '',
	[UnitShippingWeight] decimal(6,2) NOT NULL DEFAULT 0,
	[WeightUnit] varchar(20) NOT NULL DEFAULT '',
	[CustomerUnitPrice]	money NOT NULL DEFAULT 0, --The price, per unit, charged to the end customer (this will be the price that the customer sees on the website).
	[UnitCostToMerchant] money NOT NULL DEFAULT 0,
	[LineMerchandiseCost] money NOT NULL DEFAULT 0, --The wholesale extended cost, what the vendor is expected to charge the merchant for the line item.
	[CustomerMerchandiseAmount]	money NOT NULL DEFAULT 0,
	[CustomerLineShippingAmount] money NOT NULL DEFAULT 0, --The shipping cost charged to the end customer for the line item.
	[CustomerLineHandlingAmount] money NOT NULL DEFAULT 0, --The handling cost charged to the end customer for the line item.
	[CustomerLineSubTotalAmount] money NOT NULL DEFAULT 0, --The taxable total of the merchandise, shipping and handling for the line item.
	[CustomerLineTaxAmount]	money NOT NULL DEFAULT 0, --The tax charged to the end customer for the line item.
	[CustomerLineTotalAmount] money NOT NULL DEFAULT 0, --The end customers line total, excluding any credits.
	[CustomerLineCredits] money NOT NULL DEFAULT 0, --All credits applied to the line item.
	[CustomerLineBalanceDue] money NOT NULL DEFAULT 0, --The end customers line total after any credits have been applied
	[LineDiscountAmount] money NOT NULL DEFAULT 0, --The line item discount allowed by the retailer.
	[VendorWareshoueID]	varchar(20) NOT NULL DEFAULT '',
	[ExpectedShipDateUtc] DATETIME NULL,
	[PackingSlipLineMessage] Varchar(255) NOT NULL DEFAULT '', --A line item message that will appear on the packing slip.
	[VendorLineNotes] NVarchar(255) NOT NULL DEFAULT '', --Special instructions to the vendor regarding the line item
	[StoreName]	Varchar(255) NOT NULL DEFAULT '',
	[PersonalizationData] Varchar(400) NOT NULL DEFAULT '', --End customer provided personalization parameters
	[FacgtoryOrderNum] Varchar(50) NOT NULL DEFAULT '', --The merchant’s unique ID for the lineitem.
	[SubUnitQty] decimal(24,6) NOT NULL DEFAULT 0, --The quantity of sub units or components per unit ordered.
	[GiftWrapIndicator]	Varchar(10) NOT NULL DEFAULT '', --This field contains the do not fulfill until date.
	[HoldUntilDateUtc] DATETIME NULL,
	[RequiredShipDateUtc] DATETIME NULL, --The date on which the merchant expects the vendor to ship the goods includ
	[RequiredDeliveryDateUtc] DATETIME NULL, --Must be delivered to the end customer by this date.
	[CustomerRequestedArrivalDateUtc] DATETIME NULL, --This field contains the end customers requested delivery date.
	[ShipperHubCode] Varchar(20) NOT NULL DEFAULT '', --A merchant specified code used for pick sorting.
	[ShippingHub] Varchar(50) NOT NULL DEFAULT '', --This field is used to identify which of the shippers ‘hub’ a particular package should be processed through. When this element is provided, packing slips can be sorted by Shipping Hub to facilitate ‘staging’ operations for pick-up by the shipping carrier.
	[SerializedProduct]	Varchar(20) NOT NULL DEFAULT '', --This field is used to declare if a product is serialized (i.e., is assigned a serial number) and if the serial number must be reported as part of a shipment confirmation message.
	[CustomerSKU] Varchar(50) NOT NULL DEFAULT '', --The SKU by which the drop-ship customer identifies the product.
	[VendorQuoteNumber]	Varchar(30) NOT NULL DEFAULT '', --This field references a quote document on which he Unit Cost value is predicated
	[GiftRegistryID] Varchar(30) NOT NULL DEFAULT '', --This field will contain the ID of a gift registry list to which the line item belongs, used for merchandise return processing.
	[MerchantDepartment] Varchar(50) NOT NULL DEFAULT '', --The merchant assigned department ID.
	[NdcNumber]	Varchar(20) NOT NULL DEFAULT '', --National Drug Code number.
	[VendorPatternCode]	Varchar(50) NOT NULL DEFAULT '',
	[VendorFinishCode] Varchar(50) NOT NULL DEFAULT '', 
    [EnterDateUtc] DATETIME NULL, 
    [DigitBridgeGuid] uniqueidentifier NOT NULL, 
   
    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderLineUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line
    [CentralOrderLineMerchantExtUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line
	CONSTRAINT [PK_OrderLineMerchantExt] PRIMARY KEY CLUSTERED ([RowNum] ASC)
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderLineMerchantExt_CentralOrderLineMerchantExtUuid] ON [dbo].[OrderLineMerchantExt]
(
	[CentralOrderLineMerchantExtUuid] ASC
);
GO

CREATE NONCLUSTERED INDEX [FK_OrderLineMerchantExt_CentralOrderUuid] ON [dbo].[OrderLineMerchantExt]
(
	[CentralOrderUuid] ASC, 
	[CentralOrderLineNum] ASC
);
GO

CREATE NONCLUSTERED INDEX [FK_OrderLineMerchantExt_CentralOrderLineUuid] ON [dbo].[OrderLineMerchantExt]
(
	[CentralOrderLineUuid] ASC
);
GO






