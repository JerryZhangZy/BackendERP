CREATE TABLE [dbo].[OrderLineMerchantExt]
(
	[RowNum]	BIGINT IDENTITY(1,1) NOT NULL,
	[CentralOrderLineNum]	bigint NOT NULL,
    [DatabaseNum] INT NOT NULL, --Each database has its own default value.
	[CentralOrderNum]	bigint NOT NULL,
	[MasterAccountNum]	int NOT NULL,
	[ProfileNum]	int NOT NULL,
	[ChannelNum] int NOT NULL, --The channel which sells the item. Refer to Master Account Channel Setting
	[ChannelAccountNum] Int NOT NULL, --The unique number of this profile’s channel account
	[ChannelOrderID]	varchar(130) NULL,
	[ChannelOrderLineNum]	int NULL, --sequentially ordered I 4 number assigned to each line item included in the purchase order.
	[MerchantLineNumber]	int NULL, --This field will contain a non-sequentially ordered number assigned by the retailer to each line Item in the purchase order.
	[CustomerOrderLineNumber]	int NULL, --This field will contain a non-sequentially ordered I 4 number assigned to each line in the customer order.
	[HubLineID] varchar(50) NULL,
	[MerchantNRProductID]	varchar(50) NULL, --The merchant’s product ID for the product on the line item.
	[MerchantSKU]	varchar(50) NULL, --The merchant’s SKU for the product on the line item.
	[VendorSKU] varchar(50) NULL,
	[ManufacturerSKU]	varchar(50) NULL,
	[ShoppingCartSKU] varchar(50) NULL,
	[VendorDescription]	Varchar(255) NULL, --Vendor oriented description of the product
	[VendorStyleNumber]	Varchar(255) NULL, --A vendor assigned style number for the product on the line item.
	[VendorColorDescription]	varchar(50) NULL, --A vendor description / color for the color of the product on the line item.
	[VendorSizeDescription]	varchar(20) NULL, --A vendor description / code for the size of the product on the line item.
	[MerchantColorCode]	varchar(20) NULL,
	[MerchantSizeCode]	varchar(20) NULL,
	[MerchantSetCode]	varchar(50) NULL,
	[MerchantDescription]	Varchar(255) NULL,
	[MerchantDescription2]	Varchar(255) NULL,
	[MerchantDescription3]	Varchar(255) NULL,
	[MerchantColorSizeDescription]	varchar(20) NULL,
	[FullRetailPrice]	money NULL,
	[EncodedPrice]	VARCHAR(50) NULL,
	[UnitShippingWeight]	decimal(6,2) NULL,
	[WeightUnit]	varchar(20) NULL,
	[CustomerUnitPrice]	money NULL, --The price, per unit, charged to the end customer (this will be the price that the customer sees on the website).
	[UnitCostToMerchant]	money NULL,
	[LineMerchandiseCost]	money NULL, --The wholesale extended cost, what the vendor is expected to charge the merchant for the line item.
	[CustomerMerchandiseAmount]	money NULL,
	[CustomerLineShippingAmount]	money NULL, --The shipping cost charged to the end customer for the line item.
	[CustomerLineHandlingAmount]	money NULL, --The handling cost charged to the end customer for the line item.
	[CustomerLineSubTotalAmount]	money NULL, --The taxable total of the merchandise, shipping and handling for the line item.
	[CustomerLineTaxAmount]	money NULL, --The tax charged to the end customer for the line item.
	[CustomerLineTotalAmount]	money NULL, --The end customers line total, excluding any credits.
	[CustomerLineCredits]	money NULL, --All credits applied to the line item.
	[CustomerLineBalanceDue]	money NULL, --The end customers line total after any credits have been applied
	[LineDiscountAmount]	money NULL, --The line item discount allowed by the retailer.
	[VendorWareshoueID]	varchar(20) NULL,
	[ExpectedShipDateUtc]	DATETIME NULL,
	[PackingSlipLineMessage]	Varchar(255) NULL, --A line item message that will appear on the packing slip.
	[VendorLineNotes]	NVarchar(255) NULL, --Special instructions to the vendor regarding the line item
	[StoreName]	Varchar(255) NULL,
	[PersonalizationData]	Varchar(400) NULL, --End customer provided personalization parameters
	[FacgtoryOrderNum]	Varchar(50) NULL, --The merchant’s unique ID for the lineitem.
	[SubUnitQty]	decimal(24,6) NULL, --The quantity of sub units or components per unit ordered.
	[GiftWrapIndicator]	Varchar(10) NULL, --This field contains the do not fulfill until date.
	[HoldUntilDateUtc]	DATETIME NULL,
	[RequiredShipDateUtc]	DATETIME NULL, --The date on which the merchant expects the vendor to ship the goods includ
	[RequiredDeliveryDateUtc]	DATETIME NULL, --Must be delivered to the end customer by this date.
	[CustomerRequestedArrivalDateUtc]	DATETIME NULL, --This field contains the end customers requested delivery date.
	[ShipperHubCode]	Varchar(20) NULL, --A merchant specified code used for pick sorting.
	[ShippingHub]	Varchar(50) NULL, --This field is used to identify which of the shippers ‘hub’ a particular package should be processed through. When this element is provided, packing slips can be sorted by Shipping Hub to facilitate ‘staging’ operations for pick-up by the shipping carrier.
	[SerializedProduct]	Varchar(20) NULL, --This field is used to declare if a product is serialized (i.e., is assigned a serial number) and if the serial number must be reported as part of a shipment confirmation message.
	[CustomerSKU]	Varchar(50) NULL, --The SKU by which the drop-ship customer identifies the product.
	[VendorQuoteNumber]	Varchar(30) NULL, --This field references a quote document on which he Unit Cost value is predicated
	[GiftRegistryID]	Varchar(30) NULL, --This field will contain the ID of a gift registry list to which the line item belongs, used for merchandise return processing.
	[MerchantDepartment]	Varchar(50) NULL, --The merchant assigned department ID.
	[NdcNumber]	Varchar(20) NULL, --National Drug Code number.
	[VendorPatternCode]	Varchar(50) NULL,
	[VendorFinishCode]	Varchar(50) NULL, 
    [EnterDateUtc] DATETIME NULL, 
    [DigitBridgeGuid] uniqueidentifier NOT NULL, 
   
    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [CentralOrderLineUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line
    [CentralOrderLineMerchantExtUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder line

 CONSTRAINT [PK_OrderLineMerchantExt] PRIMARY KEY CLUSTERED 
(
	[RowNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderLineMerchantExt] ADD  CONSTRAINT [DF_OrderLineMerchantExt_EnterDateUtc]  DEFAULT (getutcdate()) FOR [EnterDateUtc]
GO

ALTER TABLE [dbo].[OrderLineMerchantExt] ADD  CONSTRAINT [DF_OrderLineMerchantExt_CentralOrderUuid]  DEFAULT ('') FOR [CentralOrderUuid]
GO

ALTER TABLE [dbo].[OrderLineMerchantExt] ADD  CONSTRAINT [DF_OrderLineMerchantExt_CentralOrderLineUuid]  DEFAULT ('') FOR [CentralOrderLineUuid]
GO

ALTER TABLE [dbo].[OrderLineMerchantExt] ADD  CONSTRAINT [DF_OrderLineMerchantExt_CentralOrderLineMerchantExtUuid]  DEFAULT (CONVERT([nvarchar](50),newid())) FOR [CentralOrderLineMerchantExtUuid]
GO




