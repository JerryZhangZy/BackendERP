

import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of PoHeader 
 */ 
const PoHeader = types
	.model('PoHeader', {
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		poUuid: types.optional(types.string, ''),
		poNum: types.optional(types.string, ''),
		poType: types.optional(types.number, 0),
		poStatus: types.optional(types.number, 0),
		poDate: types.optional(types.string, ''),
		poTime: types.optional(types.string, ''),
		etaShipDate: types.optional(types.string, ''),
		etaArrivalDate: types.optional(types.string, ''),
		cancelDate: types.optional(types.string, ''),
		vendorUuid: types.optional(types.string, ''),
		vendorNum: types.optional(types.string, ''),
		vendorName: types.optional(types.string, ''),
		currency: types.optional(types.string, ''),
		subTotalAmount: types.optional(types.number, 0),
		totalAmount: types.optional(types.number, 0),
		taxRate: types.optional(types.number, 0),
		taxAmount: types.optional(types.number, 0),
		taxableAmount: types.optional(types.number, 0),
		nonTaxableAmount: types.optional(types.number, 0),
		discountRate: types.optional(types.number, 0),
		discountAmount: types.optional(types.number, 0),
		shippingAmount: types.optional(types.number, 0),
		shippingTaxAmount: types.optional(types.number, 0),
		miscAmount: types.optional(types.number, 0),
		miscTaxAmount: types.optional(types.number, 0),
		chargeAndAllowanceAmount: types.optional(types.number, 0),
		poSourceCode: types.optional(types.string, ''),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of PoHeaderAttributes 
 */ 
const PoHeaderAttributes = types
	.model('PoHeaderAttributes', {
		poUuid: types.optional(types.string, ''),
		jsonFields: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of PoHeaderInfo 
 */ 
const PoHeaderInfo = types
	.model('PoHeaderInfo', {
		poUuid: types.optional(types.string, ''),
		centralFulfillmentNum: types.optional(types.number, 0),
		shippingCarrier: types.optional(types.string, ''),
		shippingClass: types.optional(types.string, ''),
		distributionCenterNum: types.optional(types.number, 0),
		centralOrderNum: types.optional(types.number, 0),
		channelNum: types.optional(types.number, 0),
		channelAccountNum: types.optional(types.number, 0),
		channelOrderID: types.optional(types.string, ''),
		secondaryChannelOrderID: types.optional(types.string, ''),
		shippingAccount: types.optional(types.string, ''),
		refNum: types.optional(types.string, ''),
		customerPoNum: types.optional(types.string, ''),
		warehouseUuid: types.optional(types.string, ''),
		customerUuid: types.optional(types.string, ''),
		endBuyerUserID: types.optional(types.string, ''),
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
 * data model of PoItems 
 */ 
const PoItems = types
	.model('PoItems', {
		poItemUuid: types.optional(types.string, ''),
		poUuid: types.optional(types.string, ''),
		seq: types.optional(types.number, 0),
		poItemType: types.optional(types.number, 0),
		poItemStatus: types.optional(types.number, 0),
		poDate: types.optional(types.string, ''),
		poTime: types.optional(types.string, ''),
		etaShipDate: types.optional(types.string, ''),
		etaArrivalDate: types.optional(types.string, ''),
		cancelDate: types.optional(types.string, ''),
		productUuid: types.optional(types.string, ''),
		inventoryUuid: types.optional(types.string, ''),
		sku: types.optional(types.string, ''),
		description: types.optional(types.string, ''),
		notes: types.optional(types.string, ''),
		itemTotalAmount: types.optional(types.number, 0),
		currency: types.optional(types.string, ''),
		poQty: types.optional(types.number, 0),
		receivedQty: types.optional(types.number, 0),
		cancelledQty: types.optional(types.number, 0),
		priceRule: types.optional(types.string, ''),
		price: types.optional(types.number, 0),
		extAmount: types.optional(types.number, 0),
		taxRate: types.optional(types.number, 0),
		taxAmount: types.optional(types.number, 0),
		discountPrice: types.optional(types.number, 0),
		discountRate: types.optional(types.number, 0),
		discountAmount: types.optional(types.number, 0),
		shippingAmount: types.optional(types.number, 0),
		shippingTaxAmount: types.optional(types.number, 0),
		miscAmount: types.optional(types.number, 0),
		miscTaxAmount: types.optional(types.number, 0),
		chargeAndAllowanceAmount: types.optional(types.number, 0),
		stockable: types.optional(types.boolean, false),
		costable: types.optional(types.boolean, false),
		taxable: types.optional(types.boolean, false),
		taxableAmount: types.optional(types.number, 0),
		nonTaxableAmount: types.optional(types.number, 0),
		isAp: types.optional(types.boolean, false),
		warehouseUuid: types.optional(types.string, ''),
		warehouseCode: types.optional(types.string, ''),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),

		poItemsAttributes: types.map(types.frozen()),

		poItemsRef: types.map(types.frozen()),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of PoItemsAttributes 
 */ 
const PoItemsAttributes = types
	.model('PoItemsAttributes', {
		poItemUuid: types.optional(types.string, ''),
		poUuid: types.optional(types.string, ''),
		jsonFields: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of PoItemsRef 
 */ 
const PoItemsRef = types
	.model('PoItemsRef', {
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		poUuid: types.optional(types.string, ''),
		poItemUuid: types.optional(types.string, ''),
		centralFulfillmentNum: types.optional(types.number, 0),
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
		customerUuid: types.optional(types.string, ''),
		endBuyerUserID: types.optional(types.string, ''),
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
 * data model of DataVersion
 */
const DataVersion = types
	.model('DataVersion', {
		data: types.optional(types.number, 0),
		poItems: types.optional(types.number, 0),
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
	poItems: 0,
};

export const PurchaseOrderDataModel = types
	.model('PurchaseOrderData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		poHeader: types.optional(PoHeader, {}),
		poHeaderInfo: types.optional(PoHeaderInfo, {}),
		poHeaderAttributes: types.optional(PoHeaderAttributes, {}),
		poItems: types.array(PoItems),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of PurchaseOrderData 
 */ 
export const purchaseOrderDataInit = {
	dataVersion: dataVersionInit,
	poHeader: {
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		poUuid: '',
		poNum: '',
		poType: 0,
		poStatus: 0,
		poDate: '',
		poTime: '',
		etaShipDate: '',
		etaArrivalDate: '',
		cancelDate: '',
		vendorUuid: '',
		vendorNum: '',
		vendorName: '',
		currency: '',
		subTotalAmount: 0,
		totalAmount: 0,
		taxRate: 0,
		taxAmount: 0,
		taxableAmount: 0,
		nonTaxableAmount: 0,
		discountRate: 0,
		discountAmount: 0,
		shippingAmount: 0,
		shippingTaxAmount: 0,
		miscAmount: 0,
		miscTaxAmount: 0,
		chargeAndAllowanceAmount: 0,
		poSourceCode: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
	poHeaderInfo: {
		poUuid: '',
		centralFulfillmentNum: 0,
		shippingCarrier: '',
		shippingClass: '',
		distributionCenterNum: 0,
		centralOrderNum: 0,
		channelNum: 0,
		channelAccountNum: 0,
		channelOrderID: '',
		secondaryChannelOrderID: '',
		shippingAccount: '',
		refNum: '',
		customerPoNum: '',
		warehouseUuid: '',
		customerUuid: '',
		endBuyerUserID: '',
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
	poHeaderAttributes: {
		poUuid: '',
		jsonFields: '',



	},
	poItems: [{
		poItemUuid: '',
		poUuid: '',
		seq: 0,
		poItemType: 0,
		poItemStatus: 0,
		poDate: '',
		poTime: '',
		etaShipDate: '',
		etaArrivalDate: '',
		cancelDate: '',
		productUuid: '',
		inventoryUuid: '',
		sku: '',
		description: '',
		notes: '',
		itemTotalAmount: 0,
		currency: '',
		poQty: 0,
		receivedQty: 0,
		cancelledQty: 0,
		priceRule: '',
		price: 0,
		extAmount: 0,
		taxRate: 0,
		taxAmount: 0,
		discountPrice: 0,
		discountRate: 0,
		discountAmount: 0,
		shippingAmount: 0,
		shippingTaxAmount: 0,
		miscAmount: 0,
		miscTaxAmount: 0,
		chargeAndAllowanceAmount: 0,
		stockable: false,
		costable: false,
		taxable: false,
		taxableAmount: 0,
		nonTaxableAmount: 0,
		isAp: false,
		warehouseUuid: '',
		warehouseCode: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',

		poItemsAttributes: {},

		poItemsRef: {},



	}], 
};



