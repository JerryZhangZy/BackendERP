              
 /**
 * @typedef {Object} SalesOrderHeaderInfoDto
 * 
 * @property {string} SalesOrderUuid 
 * - MaxLength: 50 
 * - Order uuid. 
 * - Display: false, Editable: false. 
 * 
 * @property {number} CentralFulfillmentNum 
 * - Integer 
 * - (Ignore) Reference to CentralFulfillmentNum. 
 * - Display: false, Editable: false 
 * 
 * @property {string} ShippingCarrier 
 * - MaxLength: 50 
 * - Shipping Carrier. 
 * - Title: Shipping Carrier: Display: true, Editable: true 
 * 
 * @property {string} ShippingClass 
 * - MaxLength: 50 
 * - Shipping Method. 
 * - Title: Shipping Method: Display: true, Editable: true 
 * 
 * @property {number} DistributionCenterNum 
 * - Integer 
 * - (Readonly) Original DC number. 
 * - Title: DC number: Display: false, Editable: false 
 * 
 * @property {number} CentralOrderNum 
 * - Integer 
 * - (Readonly) CentralOrderNum. 
 * - Title: Central Order: Display: true, Editable: false 
 * 
 * @property {number} ChannelNum 
 * - Integer 
 * - (Readonly) The channel which sells the item. Refer to Master Account Channel Setting. 
 * - Title: Channel: Display: true, Editable: false 
 * 
 * @property {number} ChannelAccountNum 
 * - Integer 
 * - (Readonly) The unique number of this profile’s channel account. 
 * - Title: Shipping Carrier: Display: false, Editable: false 
 * 
 * @property {string} ChannelOrderID 
 * - MaxLength: 130 
 * - (Readonly) This usually is the marketplace order ID, or merchant PO Number. 
 * - Title: Channel Order: Display: true, Editable: false 
 * 
 * @property {string} SecondaryChannelOrderID 
 * - MaxLength: 200 
 * - (Readonly) Secondary identifier provided by the channel. This is a secondary marketplace-generated Order ID. It is not populated most of the time. 
 * - Title: Other Channel Order: Display: true, Editable: false 
 * 
 * @property {string} ShippingAccount 
 * - MaxLength: 100 
 * - (Readonly) requested Vendor use Account to ship. 
 * - Title: Shipping Account: Display: false, Editable: false 
 * 
 * @property {string} WarehouseUuid 
 * - MaxLength: 50 
 * - (Readonly) Warehouse uuid. 
 * - Display: false, Editable: false 
 * 
 * @property {string} WarehouseCode 
 * - MaxLength: 50 
 * - Readable warehouse code. 
 * - Title: Warehouse Code: Display: true, Editable: true 
 * 
 * @property {string} RefNum 
 * - MaxLength: 100 
 * - Reference Number. 
 * - Title: Reference Number: Display: true, Editable: true 
 * 
 * @property {string} CustomerPoNum 
 * - MaxLength: 100 
 * - Customer P/O Number. 
 * - Title: Customer PO: Display: true, Editable: true 
 * 
 * @property {string} EndBuyerUserID 
 * - MaxLength: 255 
 * - (Ignore) The marketplace user ID of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department. 
 * - Display: false, Editable: false 
 * 
 * @property {string} EndBuyerName 
 * - MaxLength: 255 
 * - The marketplace name of the customer. Don’t use “Buyer” alone to avoid confusion with retailer buyer from the purchase department. 
 * - Title: Buyer Name : Display: true, Editable: false 
 * 
 * @property {string} EndBuyerEmail 
 * - MaxLength: 255 
 * - The email of the end customer. 
 * - Title: Buyer Email: Display: true, Editable: false 
 * 
 * @property {string} ShipToName 
 * - MaxLength: 100 
 * - Ship to name 
 * - Title: Ship to name: Display: true, Editable: true 
 * 
 * @property {string} ShipToFirstName 
 * - MaxLength: 50 
 * - (Ignore) 
 * 
 * @property {string} ShipToLastName 
 * - MaxLength: 50 
 * - (Ignore) 
 * 
 * @property {string} ShipToSuffix 
 * - MaxLength: 50 
 * - (Ignore) 
 * 
 * @property {string} ShipToCompany 
 * - MaxLength: 100 
 * - Ship to company name. 
 * - Title: Ship to company: Display: true, Editable: true 
 * 
 * @property {string} ShipToCompanyJobTitle 
 * - MaxLength: 100 
 * - (Ignore) 
 * 
 * @property {string} ShipToAttention 
 * - MaxLength: 100 
 * - Ship to contact 
 * - Title: Ship to contact: Display: true, Editable: true 
 * 
 * @property {string} ShipToAddressLine1 
 * - MaxLength: 200 
 * - Ship to address 1 
 * - Title: Ship to address 1: Display: true, Editable: true 
 * 
 * @property {string} ShipToAddressLine2 
 * - MaxLength: 200 
 * - Ship to address 2 
 * - Title: Ship to address 2: Display: true, Editable: true 
 * 
 * @property {string} ShipToAddressLine3 
 * - MaxLength: 200 
 * - Ship to address 3 
 * - Title: Ship to address 3: Display: true, Editable: true 
 * 
 * @property {string} ShipToCity 
 * - MaxLength: 100 
 * - Ship to city 
 * - Title: Ship to city: Display: true, Editable: true 
 * 
 * @property {string} ShipToState 
 * - MaxLength: 50 
 * - Ship to state 
 * - Title: Ship to state: Display: true, Editable: true 
 * 
 * @property {string} ShipToStateFullName 
 * - MaxLength: 100 
 * - (Ignore) 
 * 
 * @property {string} ShipToPostalCode 
 * - MaxLength: 50 
 * - Ship to zip code 
 * - Title: Ship to zip Display: true, Editable: true 
 * 
 * @property {string} ShipToPostalCodeExt 
 * - MaxLength: 50 
 * - (Ignore) 
 * 
 * @property {string} ShipToCounty 
 * - MaxLength: 100 
 * - Ship to county 
 * - Title: Ship to county: Display: true, Editable: true 
 * 
 * @property {string} ShipToCountry 
 * - MaxLength: 100 
 * - Ship to country 
 * - Title: Ship to country: Display: true, Editable: true 
 * 
 * @property {string} ShipToEmail 
 * - MaxLength: 100 
 * - Ship to email 
 * - Title: Ship to email: Display: true, Editable: true 
 * 
 * @property {string} ShipToDaytimePhone 
 * - MaxLength: 50 
 * - Ship to phone 
 * - Title: Ship to phone: Display: true, Editable: true 
 * 
 * @property {string} ShipToNightPhone 
 * - MaxLength: 50 
 * - (Ignore) 
 * 
 * @property {string} BillToName 
 * - MaxLength: 100 
 * - Bill to name 
 * - Title: Bill to name: Display: true, Editable: true 
 * 
 * @property {string} BillToFirstName 
 * - MaxLength: 50 
 * - (Ignore) 
 * 
 * @property {string} BillToLastName 
 * - MaxLength: 50 
 * - (Ignore) 
 * 
 * @property {string} BillToSuffix 
 * - MaxLength: 50 
 * - (Ignore) 
 * 
 * @property {string} BillToCompany 
 * - MaxLength: 100 
 * - Bill to company name. 
 * - Title: Bill to company: Display: true, Editable: true 
 * 
 * @property {string} BillToCompanyJobTitle 
 * - MaxLength: 100 
 * - (Ignore) 
 * 
 * @property {string} BillToAttention 
 * - MaxLength: 100 
 * - Bill to contact 
 * - Title: Bill to contact: Display: true, Editable: true 
 * 
 * @property {string} BillToAddressLine1 
 * - MaxLength: 200 
 * - Bill to address 1 
 * - Title: Bill to address 1: Display: true, Editable: true 
 * 
 * @property {string} BillToAddressLine2 
 * - MaxLength: 200 
 * - Bill to address 2 
 * - Title: Bill to address 2: Display: true, Editable: true 
 * 
 * @property {string} BillToAddressLine3 
 * - MaxLength: 200 
 * - Bill to address 3 
 * - Title: Bill to address 3: Display: true, Editable: true 
 * 
 * @property {string} BillToCity 
 * - MaxLength: 100 
 * - Bill to city 
 * - Title: Bill to city: Display: true, Editable: true 
 * 
 * @property {string} BillToState 
 * - MaxLength: 50 
 * - Bill to state 
 * - Title: Bill to state: Display: true, Editable: true 
 * 
 * @property {string} BillToStateFullName 
 * - MaxLength: 100 
 * - (Ignore) 
 * 
 * @property {string} BillToPostalCode 
 * - MaxLength: 50 
 * - Bill to zip code 
 * - Title: Bill to zip Display: true, Editable: true 
 * 
 * @property {string} BillToPostalCodeExt 
 * - MaxLength: 50 
 * - (Ignore) 
 * 
 * @property {string} BillToCounty 
 * - MaxLength: 50 
 * - Bill to county 
 * - Title: Bill to county: Display: true, Editable: true 
 * 
 * @property {string} BillToCountry 
 * - MaxLength: 100 
 * - Bill to country 
 * - Title: Bill to country: Display: true, Editable: true 
 * 
 * @property {string} BillToEmail 
 * - MaxLength: 100 
 * - Bill to email 
 * - Title: Bill to email: Display: true, Editable: true 
 * 
 * @property {string} BillToDaytimePhone 
 * - MaxLength: 50 
 * - Bill to phone 
 * - Title: Bill to phone: Display: true, Editable: true 
 * 
 * @property {string} BillToNightPhone 
 * - MaxLength: 50 
 * - (Ignore) 
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
 */



