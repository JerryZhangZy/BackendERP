


import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of InvoiceHeader 
 */ 
const InvoiceHeader = types
	.model('InvoiceHeader', {
		rowNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		invoiceUuid: types.optional(types.string, ''),
		invoiceNumber: types.optional(types.string, ''),
		qboDocNumber: types.optional(types.string, ''),
		salesOrderUuid: types.optional(types.string, ''),
		orderNumber: types.optional(types.string, ''),
		invoiceType: types.optional(types.number, 0),
		invoiceStatus: types.optional(types.number, 0),
		invoiceDate: types.optional(types.string, ''),
		invoiceTime: types.optional(types.string, ''),
		dueDate: types.optional(types.string, ''),
		billDate: types.optional(types.string, ''),
		shipDate: types.optional(types.string, ''),
		customerUuid: types.optional(types.string, ''),
		customerCode: types.optional(types.string, ''),
		customerName: types.optional(types.string, ''),
		terms: types.optional(types.string, ''),
		termsDays: types.optional(types.number, 0),
		currency: types.optional(types.string, ''),
		subTotalAmount: types.optional(types.number, 0),
		salesAmount: types.optional(types.number, 0),
		totalAmount: types.optional(types.number, 0),
		taxableAmount: types.optional(types.number, 0),
		nonTaxableAmount: types.optional(types.number, 0),
		taxRate: types.optional(types.number, 0),
		taxAmount: types.optional(types.number, 0),
		discountRate: types.optional(types.number, 0),
		discountAmount: types.optional(types.number, 0),
		shippingAmount: types.optional(types.number, 0),
		shippingTaxAmount: types.optional(types.number, 0),
		miscAmount: types.optional(types.number, 0),
		miscTaxAmount: types.optional(types.number, 0),
		chargeAndAllowanceAmount: types.optional(types.number, 0),
		paidAmount: types.optional(types.number, 0),
		creditAmount: types.optional(types.number, 0),
		balance: types.optional(types.number, 0),
		unitCost: types.optional(types.number, 0),
		avgCost: types.optional(types.number, 0),
		lotCost: types.optional(types.number, 0),
		invoiceSourceCode: types.optional(types.string, ''),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of InvoiceHeaderAttributes 
 */ 
const InvoiceHeaderAttributes = types
	.model('InvoiceHeaderAttributes', {
		rowNum: types.optional(types.number, 0),
		invoiceUuid: types.optional(types.string, ''),
		jsonFields: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of InvoiceHeaderInfo 
 */ 
const InvoiceHeaderInfo = types
	.model('InvoiceHeaderInfo', {
		rowNum: types.optional(types.number, 0),
		invoiceUuid: types.optional(types.string, ''),
		centralFulfillmentNum: types.optional(types.number, 0),
		orderShipmentNum: types.optional(types.number, 0),
		orderShipmentUuid: types.optional(types.string, ''),
		shippingCarrier: types.optional(types.string, ''),
		shippingClass: types.optional(types.string, ''),
		distributionCenterNum: types.optional(types.number, 0),
		centralOrderNum: types.optional(types.number, 0),
		channelNum: types.optional(types.number, 0),
		channelAccountNum: types.optional(types.number, 0),
		channelOrderID: types.optional(types.string, ''),
		secondaryChannelOrderID: types.optional(types.string, ''),
		shippingAccount: types.optional(types.string, ''),
		warehouseUuid: types.optional(types.string, ''),
		warehouseCode: types.optional(types.string, ''),
		refNum: types.optional(types.string, ''),
		customerPoNum: types.optional(types.string, ''),
		endBuyerUserId: types.optional(types.string, ''),
		endBuyerName: types.optional(types.string, ''),
		endBuyerEmail: types.optional(types.string, ''),
		shipToName: types.optional(types.string, ''),
		shipToFirstName: types.optional(types.string, ''),
		shipToLastName: types.optional(types.string, ''),
		shipToSuffix: types.optional(types.string, ''),
		shipToCompany: types.optional(types.string, ''),
		shipToCompanyJobTitle: types.optional(types.string, ''),
		shipToAttention: types.optional(types.string, ''),
		shipToAddressLine1: types.optional(types.string, ''),
		shipToAddressLine2: types.optional(types.string, ''),
		shipToAddressLine3: types.optional(types.string, ''),
		shipToCity: types.optional(types.string, ''),
		shipToState: types.optional(types.string, ''),
		shipToStateFullName: types.optional(types.string, ''),
		shipToPostalCode: types.optional(types.string, ''),
		shipToPostalCodeExt: types.optional(types.string, ''),
		shipToCounty: types.optional(types.string, ''),
		shipToCountry: types.optional(types.string, ''),
		shipToEmail: types.optional(types.string, ''),
		shipToDaytimePhone: types.optional(types.string, ''),
		shipToNightPhone: types.optional(types.string, ''),
		billToName: types.optional(types.string, ''),
		billToFirstName: types.optional(types.string, ''),
		billToLastName: types.optional(types.string, ''),
		billToSuffix: types.optional(types.string, ''),
		billToCompany: types.optional(types.string, ''),
		billToCompanyJobTitle: types.optional(types.string, ''),
		billToAttention: types.optional(types.string, ''),
		billToAddressLine1: types.optional(types.string, ''),
		billToAddressLine2: types.optional(types.string, ''),
		billToAddressLine3: types.optional(types.string, ''),
		billToCity: types.optional(types.string, ''),
		billToState: types.optional(types.string, ''),
		billToStateFullName: types.optional(types.string, ''),
		billToPostalCode: types.optional(types.string, ''),
		billToPostalCodeExt: types.optional(types.string, ''),
		billToCounty: types.optional(types.string, ''),
		billToCountry: types.optional(types.string, ''),
		billToEmail: types.optional(types.string, ''),
		billToDaytimePhone: types.optional(types.string, ''),
		billToNightPhone: types.optional(types.string, ''),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of InvoiceItems 
 */ 
const InvoiceItems = types
	.model('InvoiceItems', {
		rowNum: types.optional(types.number, 0),
		invoiceItemsUuid: types.optional(types.string, ''),
		invoiceUuid: types.optional(types.string, ''),
		seq: types.optional(types.number, 0),
		invoiceItemType: types.optional(types.number, 0),
		invoiceItemStatus: types.optional(types.number, 0),
		itemDate: types.optional(types.string, ''),
		itemTime: types.optional(types.string, ''),
		shipDate: types.optional(types.string, ''),
		etaArrivalDate: types.optional(types.string, ''),
		sku: types.optional(types.string, ''),
		productUuid: types.optional(types.string, ''),
		inventoryUuid: types.optional(types.string, ''),
		warehouseUuid: types.optional(types.string, ''),
		warehouseCode: types.optional(types.string, ''),
		lotNum: types.optional(types.string, ''),
		description: types.optional(types.string, ''),
		notes: types.optional(types.string, ''),
		currency: types.optional(types.string, ''),
		uom: types.optional(types.string, ''),
		packType: types.optional(types.string, ''),
		packQty: types.optional(types.number, 0),
		orderPack: types.optional(types.number, 0),
		shipPack: types.optional(types.number, 0),
		cancelledPack: types.optional(types.number, 0),
		openPack: types.optional(types.number, 0),
		orderQty: types.optional(types.number, 0),
		shipQty: types.optional(types.number, 0),
		cancelledQty: types.optional(types.number, 0),
		openQty: types.optional(types.number, 0),
		priceRule: types.optional(types.string, ''),
		price: types.optional(types.number, 0),
		discountRate: types.optional(types.number, 0),
		discountAmount: types.optional(types.number, 0),
		discountPrice: types.optional(types.number, 0),
		extAmount: types.optional(types.number, 0),
		taxableAmount: types.optional(types.number, 0),
		nonTaxableAmount: types.optional(types.number, 0),
		taxRate: types.optional(types.number, 0),
		taxAmount: types.optional(types.number, 0),
		shippingAmount: types.optional(types.number, 0),
		shippingTaxAmount: types.optional(types.number, 0),
		miscAmount: types.optional(types.number, 0),
		miscTaxAmount: types.optional(types.number, 0),
		chargeAndAllowanceAmount: types.optional(types.number, 0),
		itemTotalAmount: types.optional(types.number, 0),
		orderAmount: types.optional(types.number, 0),
		cancelledAmount: types.optional(types.number, 0),
		openAmount: types.optional(types.number, 0),
		stockable: types.optional(types.boolean, false),
		isAr: types.optional(types.boolean, false),
		taxable: types.optional(types.boolean, false),
		costable: types.optional(types.boolean, false),
		isProfit: types.optional(types.boolean, false),
		unitCost: types.optional(types.number, 0),
		avgCost: types.optional(types.number, 0),
		lotCost: types.optional(types.number, 0),
		lotInDate: types.optional(types.string, ''),
		lotExpDate: types.optional(types.string, ''),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),

		invoiceItemsAttributes: types.map(types.frozen()),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of InvoiceItemsAttributes 
 */ 
const InvoiceItemsAttributes = types
	.model('InvoiceItemsAttributes', {
		rowNum: types.optional(types.number, 0),
		invoiceItemsUuid: types.optional(types.string, ''),
		invoiceUuid: types.optional(types.string, ''),
		jsonFields: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of DataVersion
 */
const DataVersion = types
	.model('DataVersion', {
		data: types.optional(types.number, 0),
		invoiceItems: types.optional(types.number, 0),
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
	invoiceItems: 0,
};

export const InvoiceDataModel = types
	.model('InvoiceData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		invoiceHeader: types.optional(InvoiceHeader, {}),
		invoiceHeaderInfo: types.optional(InvoiceHeaderInfo, {}),
		invoiceHeaderAttributes: types.optional(InvoiceHeaderAttributes, {}),
		invoiceItems: types.array(InvoiceItems),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of InvoiceData 
 */ 
export const invoiceDataInit = {
	dataVersion: dataVersionInit,
	invoiceHeader: {
		rowNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		invoiceUuid: '',
		invoiceNumber: '',
		qboDocNumber: '',
		salesOrderUuid: '',
		orderNumber: '',
		invoiceType: 0,
		invoiceStatus: 0,
		invoiceDate: '',
		invoiceTime: '',
		dueDate: '',
		billDate: '',
		shipDate: '',
		customerUuid: '',
		customerCode: '',
		customerName: '',
		terms: '',
		termsDays: 0,
		currency: '',
		subTotalAmount: 0,
		salesAmount: 0,
		totalAmount: 0,
		taxableAmount: 0,
		nonTaxableAmount: 0,
		taxRate: 0,
		taxAmount: 0,
		discountRate: 0,
		discountAmount: 0,
		shippingAmount: 0,
		shippingTaxAmount: 0,
		miscAmount: 0,
		miscTaxAmount: 0,
		chargeAndAllowanceAmount: 0,
		paidAmount: 0,
		creditAmount: 0,
		balance: 0,
		unitCost: 0,
		avgCost: 0,
		lotCost: 0,
		invoiceSourceCode: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
	invoiceHeaderInfo: {
		rowNum: 0,
		invoiceUuid: '',
		centralFulfillmentNum: 0,
		orderShipmentNum: 0,
		orderShipmentUuid: '',
		shippingCarrier: '',
		shippingClass: '',
		distributionCenterNum: 0,
		centralOrderNum: 0,
		channelNum: 0,
		channelAccountNum: 0,
		channelOrderID: '',
		secondaryChannelOrderID: '',
		shippingAccount: '',
		warehouseUuid: '',
		warehouseCode: '',
		refNum: '',
		customerPoNum: '',
		endBuyerUserId: '',
		endBuyerName: '',
		endBuyerEmail: '',
		shipToName: '',
		shipToFirstName: '',
		shipToLastName: '',
		shipToSuffix: '',
		shipToCompany: '',
		shipToCompanyJobTitle: '',
		shipToAttention: '',
		shipToAddressLine1: '',
		shipToAddressLine2: '',
		shipToAddressLine3: '',
		shipToCity: '',
		shipToState: '',
		shipToStateFullName: '',
		shipToPostalCode: '',
		shipToPostalCodeExt: '',
		shipToCounty: '',
		shipToCountry: '',
		shipToEmail: '',
		shipToDaytimePhone: '',
		shipToNightPhone: '',
		billToName: '',
		billToFirstName: '',
		billToLastName: '',
		billToSuffix: '',
		billToCompany: '',
		billToCompanyJobTitle: '',
		billToAttention: '',
		billToAddressLine1: '',
		billToAddressLine2: '',
		billToAddressLine3: '',
		billToCity: '',
		billToState: '',
		billToStateFullName: '',
		billToPostalCode: '',
		billToPostalCodeExt: '',
		billToCounty: '',
		billToCountry: '',
		billToEmail: '',
		billToDaytimePhone: '',
		billToNightPhone: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
	invoiceHeaderAttributes: {
		rowNum: 0,
		invoiceUuid: '',
		jsonFields: '',



	},
	invoiceItems: [{
		rowNum: 0,
		invoiceItemsUuid: '',
		invoiceUuid: '',
		seq: 0,
		invoiceItemType: 0,
		invoiceItemStatus: 0,
		itemDate: '',
		itemTime: '',
		shipDate: '',
		etaArrivalDate: '',
		sku: '',
		productUuid: '',
		inventoryUuid: '',
		warehouseUuid: '',
		warehouseCode: '',
		lotNum: '',
		description: '',
		notes: '',
		currency: '',
		uom: '',
		packType: '',
		packQty: 0,
		orderPack: 0,
		shipPack: 0,
		cancelledPack: 0,
		openPack: 0,
		orderQty: 0,
		shipQty: 0,
		cancelledQty: 0,
		openQty: 0,
		priceRule: '',
		price: 0,
		discountRate: 0,
		discountAmount: 0,
		discountPrice: 0,
		extAmount: 0,
		taxableAmount: 0,
		nonTaxableAmount: 0,
		taxRate: 0,
		taxAmount: 0,
		shippingAmount: 0,
		shippingTaxAmount: 0,
		miscAmount: 0,
		miscTaxAmount: 0,
		chargeAndAllowanceAmount: 0,
		itemTotalAmount: 0,
		orderAmount: 0,
		cancelledAmount: 0,
		openAmount: 0,
		stockable: false,
		isAr: false,
		taxable: false,
		costable: false,
		isProfit: false,
		unitCost: 0,
		avgCost: 0,
		lotCost: 0,
		lotInDate: '',
		lotExpDate: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',

		invoiceItemsAttributes: {},



	}], 
};



