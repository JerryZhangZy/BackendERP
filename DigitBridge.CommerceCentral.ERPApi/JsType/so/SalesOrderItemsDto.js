              
 /**
 * @typedef {Object} SalesOrderItemsDto
 * 
 * @property {string} SalesOrderItemsUuid 
 * - MaxLength: 50 
 * - (Readonly) Order Item Line uuid. 
 * - Display: false, Editable: false 
 * 
 * @property {string} SalesOrderUuid 
 * - MaxLength: 50 
 * - Order uuid. 
 * - Display: false, Editable: false. 
 * 
 * @property {number} Seq 
 * - Integer 
 * - Order Item Line sequence number. 
 * - Title: Line#, Display: true, Editable: false 
 * 
 * @property {number} OrderItemType 
 * - Integer 
 * - Order item type. 
 * - Title: Type, Display: true, Editable: true 
 * 
 * @property {number} SalesOrderItemstatus 
 * - Integer 
 * - Order item status. 
 * - Title: Status, Display: true, Editable: true 
 * 
 * @property {Date} ItemDate 
 * - (Ignore) Order date 
 * 
 * @property {Date} ItemTime 
 * - (Ignore) Order time 
 * 
 * @property {Date} ShipDate 
 * - Estimated vendor ship date. 
 * - Title: Ship Date, Display: true, Editable: true 
 * 
 * @property {Date} EtaArrivalDate 
 * - Estimated date when item arrival to buyer. 
 * - Title: Delivery Date, Display: true, Editable: true 
 * 
 * @property {string} SKU 
 * - MaxLength: 100 
 * - Product SKU. 
 * - Title: SKU, Display: true, Editable: true 
 * 
 * @property {string} ProductUuid 
 * - MaxLength: 50 
 * - (Readonly) Product uuid. load from ProductBasic data. 
 * - Display: false, Editable: false 
 * 
 * @property {string} InventoryUuid 
 * - MaxLength: 50 
 * - (Readonly) Inventory Item Line uuid, load from inventory data. 
 * - Display: false, Editable: false 
 * 
 * @property {string} WarehouseUuid 
 * - MaxLength: 50 
 * - (Readonly) Warehouse uuid, load from inventory data. 
 * - Display: false, Editable: false 
 * 
 * @property {string} WarehouseCode 
 * - MaxLength: 50 
 * - Readable warehouse code, load from inventory data. 
 * - Title: Warehouse Code, Display: true, Editable: true 
 * 
 * @property {string} LotNum 
 * - MaxLength: 100 
 * - Lot Number. 
 * - Title: Lot Number, Display: true, Editable: true 
 * 
 * @property {string} Description 
 * - MaxLength: 200 
 * - Item line description, default from ProductBasic data. 
 * - Title: Description, Display: true, Editable: true 
 * 
 * @property {string} Notes 
 * - MaxLength: 500 
 * - Order item line notes. 
 * - Title: Notes, Display: true, Editable: true 
 * 
 * @property {string} Currency 
 * - MaxLength: 10 
 * - (Ignore) 
 * 
 * @property {string} UOM 
 * - MaxLength: 50 
 * - (Readonly) Product unit of measure, load from ProductBasic data. 
 * - Title: UOM, Display: true, Editable: false 
 * 
 * @property {string} PackType 
 * - MaxLength: 50 
 * - (Ignore) Product SKU Qty pack type, for example: Case, Box, Each 
 * 
 * @property {number} PackQty 
 * - (Ignore) Item Qty each per pack. 
 * 
 * @property {number} OrderPack 
 * - (Ignore) Item Order number of pack. 
 * 
 * @property {number} ShipPack 
 * - (Ignore) Item Shipped number of pack. 
 * 
 * @property {number} CancelledPack 
 * - (Ignore) Item Cancelled number of pack. 
 * 
 * @property {number} OpenPack 
 * - (Ignore) Item Cancelled number of pack. 
 * 
 * @property {number} OrderQty 
 * - Item Order Qty. 
 * - Title: Order Qty, Display: true, Editable: true 
 * 
 * @property {number} ShipQty 
 * - Item Shipped Qty. 
 * - Title: Shipped Qty, Display: true, Editable: false 
 * 
 * @property {number} CancelledQty 
 * - Item Cancelled Qty. 
 * - Title: Cancelled Qty, Display: true, Editable: false 
 * 
 * @property {number} OpenQty 
 * - Item Open Qty. 
 * 
 * @property {string} PriceRule 
 * - MaxLength: 50 
 * - Item price rule. 
 * - Title: Price Type, Display: true, Editable: true 
 * 
 * @property {number} Price 
 * - Item unit price. 
 * - Title: Unit Price, Display: true, Editable: true 
 * 
 * @property {number} DiscountRate 
 * - Item level discount rate. 
 * - Title: Discount Rate, Display: true, Editable: true 
 * 
 * @property {number} DiscountAmount 
 * - Item level discount amount. 
 * - Title: Discount Amount, Display: true, Editable: true 
 * 
 * @property {number} DiscountPrice 
 * - Item after discount price. 
 * - Title: Discount Price, Display: true, Editable: false 
 * 
 * @property {number} ExtAmount 
 * - Item total amount. 
 * - Title: Ext.Amount, Display: true, Editable: false 
 * 
 * @property {number} TaxableAmount 
 * - Amount should apply tax. 
 * - Display: false, Editable: false 
 * 
 * @property {number} NonTaxableAmount 
 * - Amount should not apply tax. 
 * - Display: false, Editable: false 
 * 
 * @property {number} TaxRate 
 * - Default Tax rate for item. 
 * - Display: false, Editable: false 
 * 
 * @property {number} TaxAmount 
 * - Item level tax amount (include shipping tax and misc tax). 
 * - Display: false, Editable: false 
 * 
 * @property {number} ShippingAmount 
 * - shipping fee for this item. 
 * - Display: false, Editable: false 
 * 
 * @property {number} ShippingTaxAmount 
 * - Item level tax amount of shipping fee. 
 * - Display: false, Editable: false 
 * 
 * @property {number} MiscAmount 
 * - Item level handling charge. 
 * - Display: false, Editable: false 
 * 
 * @property {number} MiscTaxAmount 
 * - Item level tax amount of handling charge. 
 * - Display: false, Editable: false 
 * 
 * @property {number} ChargeAndAllowanceAmount 
 * - Item level Charge and Allowance Amount. 
 * - Display: false, Editable: false 
 * 
 * @property {number} ItemTotalAmount 
 * - Item total amount include all. 
 * - Display: false, Editable: false 
 * 
 * @property {number} ShipAmount 
 * - Item shipped amount. 
 * - Display: false, Editable: false 
 * 
 * @property {number} CancelledAmount 
 * - Item cancelled amount. 
 * - Display: false, Editable: false 
 * 
 * @property {number} OpenAmount 
 * - Item open amount. 
 * - Display: false, Editable: false 
 * 
 * @property {number} Stockable 
 * - Integer 
 * - item will update inventory instock qty. 
 * - Title: Stockable, Display: true, Editable: true 
 * 
 * @property {number} IsAr 
 * - Integer 
 * - item will add to Order total amount. 
 * - Title: A/R, Display: true, Editable: true 
 * 
 * @property {number} Taxable 
 * - Integer 
 * - item will apply tax. 
 * - Title: Taxable, Display: true, Editable: true 
 * 
 * @property {number} Costable 
 * - Integer 
 * - item will calculate total cost. 
 * - Title: Apply Cost, Display: true, Editable: true 
 * 
 * @property {number} IsProfit 
 * - Integer 
 * - item will calculate profit. 
 * - Title: Apply Profit, Display: true, Editable: true 
 * 
 * @property {number} UnitCost 
 * - (Ignore) Item Unit Cost. 
 * 
 * @property {number} AvgCost 
 * - (Ignore) Item Avg.Cost. 
 * 
 * @property {number} LotCost 
 * - (Ignore) Item Lot Cost. 
 * 
 * @property {Date} LotInDate 
 * - (Ignore) Lot receive Date 
 * 
 * @property {Date} LotExpDate 
 * - (Ignore) Lot Expiration date 
 * 
 * @property {Date} UpdateDateUtc 
 * - (Ignore) 
 * 
 * @property {string} EnterBy 
 * - MaxLength: 100 
 * - (Ignore) 
 * 
 * @property {string} UpdateBy 
 * - MaxLength: 100 
 * - (Ignore) 
 * 
 * @property {SalesOrderItemsAttributesDto} SalesOrderItemsAttributes - {@link SalesOrderItemsAttributesDto}
 * 
 */



