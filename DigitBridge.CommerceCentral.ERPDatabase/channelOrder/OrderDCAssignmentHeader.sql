CREATE TABLE [dbo].[OrderDCAssignmentHeader](
	[OrderDCAssignmentNum] BIGINT IDENTITY(1,1) NOT NULL, --(Readonly) Record Number. Required, <br> Display: false, Editable: false.
	[DatabaseNum] INT NOT NULL, --(Readonly) Database Number. <br> Display: false, Editable: false.
	[MasterAccountNum] INT NOT NULL, --(Readonly) Login user account. <br> Display: false, Editable: false.
	[ProfileNum] INT NOT NULL, --(Readonly) Login user profile. <br> Display: false, Editable: false.

	[ChannelNum] INT NOT NULL DEFAULT 0, --(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
	[ChannelAccountNum] INT  NOT NULL DEFAULT 0, --(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
	[CentralOrderNum] BIGINT NOT NULL DEFAULT 0, --(Readonly) CentralOrderNum. <br> Title: Central Order: Display: true, Editable: false
	[ChannelOrderID] VARCHAR(50) NOT NULL DEFAULT '', --(Readonly) This usually is the marketplace order ID, or merchant PO Number. <br> Title: Channel Order: Display: true, Editable: false
	[ShippingCost] MONEY NOT NULL DEFAULT 0, --Shipping fee. <br> Title: Shipping, Display: true, Editable: true
	[InsuranceCost] MONEY NOT NULL DEFAULT 0, --Insurance cost. <br> Title: Insurance, Display: true, Editable: true
	[TaxCost] MONEY NOT NULL DEFAULT 0, --Tax Amount. <br> Title: Tax, Display: true, Editable: true
	[FulfillmentType] INT NOT NULL DEFAULT 0, --Fulfillment Type. <br> Title: Fulfillment Type, Display: true, Editable: true
	[DistributionCenterNum] INT NOT NULL DEFAULT 0, --Distribution Center Number. <br> Title: DC#, Display: true, Editable: true
	[SellerWarehouseID] VARCHAR(50) NOT NULL DEFAULT '', --Seller Warehouse ID. <br> Title: Seller Warehouse, Display: true, Editable: true
	[UseSystemShippingLabel] INT NOT NULL DEFAULT 0, --Use System Shipping Label. <br> Title: Use System Shipping Label, Display: true, Editable: true
	[UseChannelPackingSlip] INT NOT NULL DEFAULT 0, --Use Channel Packing Slip Format. <br> Title: Use Channel Packing Slip, Display: true, Editable: true
	[UseSystemReturnLabel] INT NOT NULL DEFAULT 0, --Use System Return Label. <br> Title: Use System Return Label, Display: true, Editable: true
	[ShippingLabelFormat] INT NOT NULL DEFAULT 0, --Shipping Label Format. <br> Title: Shipping Label Format, Display: true, Editable: true
	[ReturnLabelFormat] INT NOT NULL DEFAULT 0, --Return Label Format. <br> Title: Return Label Format, Display: true, Editable: true
	[DBChannelOrderHeaderRowID] VARCHAR(50) NOT NULL DEFAULT '', --DB Channel Order Header RowID. <br> Title: Header RowID, Display: true, Editable: true
	[FulfillmentProcessStatus] INT NOT NULL DEFAULT 0, --Fulfillment Process Status. <br> Title: Fulfillment Status, Display: true, Editable: true
	[IntegrationStatus] INT NOT NULL DEFAULT 0, --Integration Status. <br> Title: Integration Status, Display: true, Editable: true
	[IntegrationDateUtc] DATETIME NULL, --Integration Date Utc. <br> Title: Integration Date, Display: true, Editable: true

	[EnterDateUtc] DATETIME NOT NULL DEFAULT (getutcdate()), --(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false

    [CentralOrderUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [OrderDCAssignmentUuid] VARCHAR(50) NOT NULL, --Global Unique Guid for CentralOrder
    [RowNum]      BIGINT NOT NULL DEFAULT 0,
    [DigitBridgeGuid] uniqueidentifier NOT NULL DEFAULT (newid()), --(Ignore)
	CONSTRAINT [PK_OrderDCAssignmentHeader] PRIMARY KEY CLUSTERED ([OrderDCAssignmentNum] ASC)
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [DI_OrderDCAssignmentHeader_DatabaseNum_CentralOrderNum_DistributionCenterNum]
    ON [dbo].[OrderDCAssignmentHeader]([DatabaseNum] ASC, [CentralOrderNum] ASC)
    INCLUDE([DistributionCenterNum]);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_OrderDCAssignmentHeader_OrderDCAssignmentUuid] ON [dbo].[OrderDCAssignmentHeader]
(
	[OrderDCAssignmentUuid] ASC
);
GO

CREATE NONCLUSTERED INDEX [FK_OrderDCAssignmentHeader_CentralOrderUuid] ON [dbo].[OrderDCAssignmentHeader]
(
	[CentralOrderUuid] ASC
);
GO
