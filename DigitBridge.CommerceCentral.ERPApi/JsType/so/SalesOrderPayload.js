
 // link to Dto data type
 /**
 * @typedef { import('./SalesOrderDataDto') } SalesOrderDataDto
 */
 
 
 // Filter Object Type
 /**
 * @typedef {Object} FilterObject
 * 
 * @property {string} value 
 * - filter value 
 * 
 * @property {number} operator 
 * - Integer 
 * - Optional 
 * - filter match operator 
 * - 1 = equal 
 * - 2 = not equal 
 * - 3 = less than 
 * - 4 = less or equal 
 * - 5 = greater 
 * - 6 = greater or equal 
 * - 7 = begin with 
 * - 8 = not begin 
 * - 9 = ends with 
 * - 10 = not end 
 * - 11 = contains 
 * - 12 = not contain 
 * 
 */
 
 
 // Payload Type
 /**
 * @typedef {Object} SalesOrderPayload
 * 
 * @property {number} $top 
 * - Integer 
 * - Optional 
 * - Page size 
 * - Default value is 100 
 * - Maximum value is 500 
 * 
 * @property {number} $skip 
 * - Integer 
 * - Optional 
 * - Records to skip 
 * - Default value is 0 
 * 
 * @property {bool} $count 
 * - Optional 
 * - true: query totalcount and paging data
 * - false: only query paging data 
 * - Default value is true 
 * 
 * @property {string} $sortBy 
 * - Optional 
 * - Sort by fields. (multiple fields use comma delimited)
 * - Suffix ASC and DESC control sorting direction 
 * - For Example: 'OrderDate DESC, OrderNumber ASC' 
 * - Default value is set by backend 
 * 
 * @property {bool} $loadAll 
 * - Optional 
 * - true: load all query result 
 * - false: only only load paging data 
 * - Default value is false 
 * 
 * @property {SalesOrderPayloadQueryFilter} $filter - {@link SalesOrderPayloadQueryFilter} 
 * - Optional 
 * - Filter json object 
 *
 *
 * @property {string[]} SalesOrderUuids
 * - Request Data
 * - API:  GET /SalesOrder
 * - Get multiple SalesOrder Data by SalesOrderUuid array.
 * 
 * @property {SalesOrderDataDto[]} SalesOrders {@link SalesOrderDataDto}
 * - Responce Data
 * - API:  GET /SalesOrder
 * - Multiple SalesOrder Data by SalesOrderUuid array.
 *
 * @property {SalesOrderDataDto} SalesOrder {@link SalesOrderDataDto}
 * - Request data or Responce Data
 * - API:  GET /SalesOrder/SalesOrderNumber
 * - One SalesOrder Data Object.
 *
 * @property {Array} SalesOrderList
 * - Responce Data
 * - API:  POST /SalesOrder/Find
 * - Load SalesOrder List Data by Filter.
 *
 * @property {number} SalesOrderListCount
 * - Integer
 * - Responce Data
 * - API:  POST /SalesOrder/Find
 * - SalesOrder List Row count.
 */


// API Payload Type
 /**
 * API: POST /SalesOrder/
 * @typedef {Object} Add SalesOrder Payload
 *
 * @property {SalesOrderDataDto} SalesOrder {@link SalesOrderDataDto}
 * - One salesorder data object.
 *
 */


 /**
 * API: PATCH /SalesOrder/
 * @typedef {Object} Update SalesOrder Payload
 *
 * @property {SalesOrderDataDto} SalesOrder {@link SalesOrderDataDto}
 * - Request data
 * - one salesorder data object.
 *
 */


/**
 * API: GET /SalesOrder/SalesOrderNumber
 * @typedef {Object} Get single SalesOrder Payload
 *
 * @property {SalesOrderDataDto} SalesOrder {@link SalesOrderDataDto}
 * - Responce data
 * - one salesorder data object.
 *
 */


/**
 * API: DELETE /SalesOrder/SalesOrderUuids
 * @typedef {Object} Delete single SalesOrder Payload
 *
 */


/**
 * API: GET /SalesOrder/
 * @typedef {Object} Get single SalesOrder Payload
 *
 * @property {string[]} SalesOrderUuids
 * - Request query parameter
 * - Get multiple SalesOrder Data by SalesOrderUuid array.
 *
 * @property {SalesOrderDataDto[]} SalesOrders {@link SalesOrderDataDto}
 * - Responce Data
 * - Multiple SalesOrder Data by SalesOrderUuid array.
 *
 */


/**
 * API: POST /SalesOrder/Find
 * @typedef {Object} Load SalesOrder List Payload
 *
 * @property {number} $top
 * - Integer
 * - Optional
 * - Page size
 * - Default value is 100
 * - Maximum value is 500
 *
 * @property {number} $skip
 * - Integer
 * - Optional
 * - Records to skip
 * - Default value is 0
 *
 * @property {bool} $count
 * - Optional
 * - true: query totalcount and paging data
 * - false: only query paging data
 * - Default value is true
 *
 * @property {string} $sortBy
 * - Optional
 * - Sort by fields. (multiple fields use comma delimited)
 * - Suffix ASC and DESC control sorting direction
 * - For Example: 'OrderDate DESC, OrderNumber ASC'
 * - Default value is set by backend
 *
 * @property {bool} $loadAll
 * - Optional
 * - true: load all query result
 * - false: only only load paging data
 * - Default value is false
 *
 * @property {SalesOrderPayloadQueryFilter} $filter - {@link SalesOrderPayloadQueryFilter}
 * - Optional
 * - Filter json object
 *
 * @property {Array} SalesOrderList
 * - Responce Data
 * - API:  POST /SalesOrder/Find
 * - Load SalesOrder List Data by Filter.
 *
 * @property {number} SalesOrderListCount
 * - Integer
 * - Responce Data
 * - API:  POST /SalesOrder/Find
 * - SalesOrder List Row count.
 */


 // Query Filter Type
 /**
 * @typedef {Object} SalesOrderPayloadQueryFilter
 *
 * @property {FilterObject} OrderNumberFrom - {@link FilterObject}
 * - >=
 * - default: ""
 * 
 * @property {FilterObject} OrderNumberTo - {@link FilterObject}
 * - <=
 * - default: ""
 *
 * @property {FilterObject} CustomerCode - {@link FilterObject}
 * - =
 * - default: ""
 *
 * @property {FilterObject} CustomerName - {@link FilterObject}
 * - Begin With
 * - default: ""
 *
 * @property {FilterObject} OrderDateFrom - {@link FilterObject}
 * - >=
 * - default: ""
 *
 * @property {FilterObject} OrderDateTo - {@link FilterObject}
 * - <=
 * - default: ""
 *
 * @property {FilterObject} OrderStatus - {@link FilterObject}
 * - =
 * - default: All
 *
 * @property {FilterObject} OrderType - {@link FilterObject}
 * - =
 * - default: All
 *
 */
