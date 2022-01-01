


import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of ApInvoiceHeader 
 */ 
const ApInvoiceHeader = types
	.model('ApInvoiceHeader', {
		rowNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		apInvoiceUuid: types.optional(types.string, ''),
		apInvoiceNum: types.optional(types.string, ''),
		apInvoiceType: types.optional(types.number, 0),
		apInvoiceStatus: types.optional(types.number, 0),
		apInvoiceDate: types.optional(types.string, ''),
		apInvoiceTime: types.optional(types.string, ''),
		vendorUuid: types.optional(types.string, ''),
		vendorNum: types.optional(types.string, ''),
		vendorName: types.optional(types.string, ''),
		vendorInvoiceNum: types.optional(types.string, ''),
		vendorInvoiceDate: types.optional(types.string, ''),
		dueDate: types.optional(types.string, ''),
		billDate: types.optional(types.string, ''),
		currency: types.optional(types.string, ''),
		totalAmount: types.optional(types.number, 0),
		paidAmount: types.optional(types.number, 0),
		creditAmount: types.optional(types.number, 0),
		balance: types.optional(types.number, 0),
		creditAccount: types.optional(types.number, 0),
		debitAccount: types.optional(types.number, 0),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of ApInvoiceHeaderAttributes 
 */ 
const ApInvoiceHeaderAttributes = types
	.model('ApInvoiceHeaderAttributes', {
		rowNum: types.optional(types.number, 0),
		apInvoiceUuid: types.optional(types.string, ''),
		jsonFields: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of ApInvoiceHeaderInfo 
 */ 
const ApInvoiceHeaderInfo = types
	.model('ApInvoiceHeaderInfo', {
		rowNum: types.optional(types.number, 0),
		apInvoiceUuid: types.optional(types.string, ''),
		poUuid: types.optional(types.string, ''),
		receiveUuid: types.optional(types.string, ''),
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
 * data model of ApInvoiceItems 
 */ 
const ApInvoiceItems = types
	.model('ApInvoiceItems', {
		rowNum: types.optional(types.number, 0),
		apInvoiceItemsUuid: types.optional(types.string, ''),
		apInvoiceUuid: types.optional(types.string, ''),
		seq: types.optional(types.number, 0),
		apInvoiceItemType: types.optional(types.number, 0),
		apInvoiceItemStatus: types.optional(types.number, 0),
		itemDate: types.optional(types.string, ''),
		itemTime: types.optional(types.string, ''),
		apDistributionNum: types.optional(types.string, ''),
		description: types.optional(types.string, ''),
		notes: types.optional(types.string, ''),
		currency: types.optional(types.string, ''),
		amount: types.optional(types.number, 0),
		isAp: types.optional(types.boolean, false),
		creditAccount: types.optional(types.number, 0),
		debitAccount: types.optional(types.number, 0),
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
		apInvoiceItems: types.optional(types.number, 0),
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
	apInvoiceItems: 0,
};

export const ApInvoiceDataModel = types
	.model('ApInvoiceData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		apInvoiceHeader: types.optional(ApInvoiceHeader, {}),
		apInvoiceHeaderInfo: types.optional(ApInvoiceHeaderInfo, {}),
		apInvoiceHeaderAttributes: types.optional(ApInvoiceHeaderAttributes, {}),
		apInvoiceItems: types.array(ApInvoiceItems),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of ApInvoiceData 
 */ 
export const apInvoiceDataInit = {
	dataVersion: dataVersionInit,
	apInvoiceHeader: {
		rowNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		apInvoiceUuid: '',
		apInvoiceNum: '',
		apInvoiceType: 0,
		apInvoiceStatus: 0,
		apInvoiceDate: '',
		apInvoiceTime: '',
		vendorUuid: '',
		vendorNum: '',
		vendorName: '',
		vendorInvoiceNum: '',
		vendorInvoiceDate: '',
		dueDate: '',
		billDate: '',
		currency: '',
		totalAmount: 0,
		paidAmount: 0,
		creditAmount: 0,
		balance: 0,
		creditAccount: 0,
		debitAccount: 0,
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
	apInvoiceHeaderInfo: {
		rowNum: 0,
		apInvoiceUuid: '',
		poUuid: '',
		receiveUuid: '',
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
	apInvoiceHeaderAttributes: {
		rowNum: 0,
		apInvoiceUuid: '',
		jsonFields: '',



	},
	apInvoiceItems: [{
		rowNum: 0,
		apInvoiceItemsUuid: '',
		apInvoiceUuid: '',
		seq: 0,
		apInvoiceItemType: 0,
		apInvoiceItemStatus: 0,
		itemDate: '',
		itemTime: '',
		apDistributionNum: '',
		description: '',
		notes: '',
		currency: '',
		amount: 0,
		isAp: false,
		creditAccount: 0,
		debitAccount: 0,
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	}], 
};



