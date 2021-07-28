

              
 /**
 * @typedef {Object} SalesOrderHeaderDto
 * 
 * @property {number} DatabaseNum 
 * - Integer 
 * - (Readonly) Database Number. 
 * - Display: false, Editable: false. 
 * 
 * @property {number} MasterAccountNum 
 * - Integer 
 * - (Readonly) Login user account. 
 * - Display: false, Editable: false. 
 * 
 * @property {number} ProfileNum 
 * - Integer 
 * - (Readonly) Login user profile. 
 * - Display: false, Editable: false. 
 * 
 * @property {string} SalesOrderUuid 
 * - MaxLength: 50 
 * - Order uuid. 
 * - Display: false, Editable: false. 
 * 
 * @property {string} OrderNumber 
 * - MaxLength: 50 
 * - Readable order number, unique in same database and profile. 
 * - Parameter should pass ProfileNum-OrderNumber. 
 * - Title: Order Number, Display: true, Editable: true 
 * 
 * @property {number} OrderType 
 * - Integer 
 * - Order type. 
 * - Title: Type, Display: true, Editable: true 
 * 
 * @property {number} OrderStatus 
 * - Integer 
 * - Order status. 
 * - Title: Status, Display: true, Editable: true 
 * 
 * @property {Date} OrderDate 
 * - Order date. 
 * - Title: Date, Display: true, Editable: true 
 * 
 * @property {Date} OrderTime 
 * - Order time. 
 * - Title: Time, Display: true, Editable: true 
 * 
 * @property {Date} DueDate 
 * - (Ignore) Order due date. 
 * - Display: false, Editable: false 
 * 
 * @property {Date} BillDate 
 * - (Ignore) Order bill date. 
 * - Display: false, Editable: false 
 * 
 * @property {string} CustomerUuid 
 * - MaxLength: 50 
 * - Customer uuid, load from customer data. 
 * - Display: false, Editable: false 
 * 
 * @property {string} CustomerCode 
 * - MaxLength: 50 
 * - Customer number. use DatabaseNum-CustomerCode too load customer data. 
 * - Title: Customer Number, Display: true, Editable: true 
 * 
 * @property {string} CustomerName 
 * - MaxLength: 200 
 * - (Readonly) Customer name, load from customer data. 
 * - Title: Customer Name, Display: true, Editable: false 
 * 
 * @property {string} Terms 
 * - MaxLength: 50 
 * - Payment terms, default from customer data. 
 * - Title: Terms, Display: true, Editable: true 
 * 
 * @property {number} TermsDays 
 * - Integer 
 * - Payment terms days, default from customer data. 
 * - Title: Days, Display: true, Editable: true 
 * 
 * @property {string} Currency 
 * - MaxLength: 10 
 * - Currency code. 
 * - Title: Currency, Display: true, Editable: true 
 * 
 * @property {number} SubTotalAmount 
 * - (Readonly) Sub total amount of items. Sales amount without discount, tax and other charge. 
 * - Title: Subtotal, Display: true, Editable: false 
 * 
 * @property {number} SalesAmount 
 * - (Readonly) Sub Total amount deduct discount, but not include tax and other charge. 
 * - Title: Sales Amount, Display: true, Editable: false 
 * 
 * @property {number} TotalAmount 
 * - (Readonly) Total amount. Include every charge (tax, shipping, misc...). 
 * - Title: Total, Display: true, Editable: false 
 * 
 * @property {number} TaxableAmount 
 * - (Readonly) Amount should apply tax. 
 * - Title: Taxable Amount, Display: true, Editable: false 
 * 
 * @property {number} NonTaxableAmount 
 * - (Readonly) Amount should not apply tax. 
 * - Title: NonTaxable, Display: true, Editable: false 
 * 
 * @property {number} TaxRate 
 * - Order Tax rate. 
 * - Title: Tax, Display: true, Editable: true 
 * 
 * @property {number} TaxAmount 
 * - Order tax amount (include shipping tax and misc tax). 
 * - Title: Tax Amount, Display: true, Editable: false 
 * 
 * @property {number} DiscountRate 
 * - Order discount rate base on SubTotalAmount. If user enter discount rate, should recalculate discount amount. 
 * - Title: Discount, Display: true, Editable: true 
 * 
 * @property {number} DiscountAmount 
 * - Order discount amount base on SubTotalAmount. If user enter discount amount, should set discount rate to zero. 
 * - Title: Discount Amount, Display: true, Editable: true 
 * 
 * @property {number} ShippingAmount 
 * - Order shipping fee. 
 * - Title: Shipping, Display: true, Editable: true 
 * 
 * @property {number} ShippingTaxAmount 
 * - (Readonly) tax amount for shipping fee. 
 * - Title: Shipping Tax, Display: true, Editable: false 
 * 
 * @property {number} MiscAmount 
 * - Order handling charge. 
 * - Title: Handling, Display: true, Editable: true 
 * 
 * @property {number} MiscTaxAmount 
 * - (Readonly) tax amount for handling charge. 
 * - Title: Handling Tax, Display: true, Editable: false 
 * 
 * @property {number} ChargeAndAllowanceAmount 
 * - Order other Charg and Allowance Amount. Positive is charge, Negative is Allowance. 
 * - Title: Charge&Allowance, Display: true, Editable: true 
 * 
 * @property {number} PaidAmount 
 * - (Ignore) Total Paid amount. 
 * - Display: false, Editable: false 
 * 
 * @property {number} CreditAmount 
 * - (Ignore) Total Credit amount. 
 * - Display: false, Editable: false 
 * 
 * @property {number} Balance 
 * - (Ignore) Current balance of Order. 
 * - Display: false, Editable: false 
 * 
 * @property {number} UnitCost 
 * - (Ignore) Total Unit Cost. 
 * - Display: false, Editable: false 
 * 
 * @property {number} AvgCost 
 * - (Ignore) Total Avg.Cost. 
 * - Display: false, Editable: false 
 * 
 * @property {number} LotCost 
 * - (Ignore) Total Lot Cost. 
 * - Display: false, Editable: false 
 * 
 * @property {string} OrderSourceCode 
 * - MaxLength: 100 
 * - (Readonly) Order created from other entity number, use to prevent import duplicate order. 
 * - Title: Source Number, Display: false, Editable: false 
 * 
 * @property {Date} UpdateDateUtc 
 * - (Readonly) Last update date time. 
 * - Title: Update At, Display: true, Editable: false 
 * 
 * @property {string} EnterBy 
 * - MaxLength: 100 
 * - (Readonly) User who created this order. 
 * - Title: Created By, Display: true, Editable: false 
 * 
 * @property {string} UpdateBy 
 * - MaxLength: 100 
 * - (Readonly) Last updated user. 
 * - Title: Update By, Display: true, Editable: false 
 * 
 */



