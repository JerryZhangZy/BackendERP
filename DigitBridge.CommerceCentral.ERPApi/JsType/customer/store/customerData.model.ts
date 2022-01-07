


import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of Customer 
 */ 
const Customer = types
	.model('Customer', {
		rowNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		digit_seller_id: types.optional(types.string, ''),
		customerUuid: types.optional(types.string, ''),
		customerCode: types.optional(types.string, ''),
		customerName: types.optional(types.string, ''),
		contact: types.optional(types.string, ''),
		contact2: types.optional(types.string, ''),
		contact3: types.optional(types.string, ''),
		phone1: types.optional(types.string, ''),
		phone2: types.optional(types.string, ''),
		phone3: types.optional(types.string, ''),
		phone4: types.optional(types.string, ''),
		email: types.optional(types.string, ''),
		webSite: types.optional(types.string, ''),
		channelNum: types.optional(types.number, 0),
		channelAccountNum: types.optional(types.number, 0),
		customerType: types.optional(types.number, 0),
		customerStatus: types.optional(types.number, 0),
		businessType: types.optional(types.string, ''),
		priceRule: types.optional(types.string, ''),
		firstDate: types.optional(types.string, ''),
		currency: types.optional(types.string, ''),
		creditLimit: types.optional(types.number, 0),
		taxRate: types.optional(types.number, 0),
		discountRate: types.optional(types.number, 0),
		shippingCarrier: types.optional(types.string, ''),
		shippingClass: types.optional(types.string, ''),
		shippingAccount: types.optional(types.string, ''),
		priority: types.optional(types.string, ''),
		area: types.optional(types.string, ''),
		region: types.optional(types.string, ''),
		districtn: types.optional(types.string, ''),
		zone: types.optional(types.string, ''),
		taxId: types.optional(types.string, ''),
		resaleLicense: types.optional(types.string, ''),
		classCode: types.optional(types.string, ''),
		departmentCode: types.optional(types.string, ''),
		divisionCode: types.optional(types.string, ''),
		sourceCode: types.optional(types.string, ''),
		terms: types.optional(types.string, ''),
		termsDays: types.optional(types.number, 0),
		salesRep: types.optional(types.string, ''),
		salesRep2: types.optional(types.string, ''),
		salesRep3: types.optional(types.string, ''),
		salesRep4: types.optional(types.string, ''),
		commissionRate: types.optional(types.number, 0),
		commissionRate2: types.optional(types.number, 0),
		commissionRate3: types.optional(types.number, 0),
		commissionRate4: types.optional(types.number, 0),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of CustomerAddress 
 */ 
const CustomerAddress = types
	.model('CustomerAddress', {
		rowNum: types.optional(types.number, 0),
		addressUuid: types.optional(types.string, ''),
		customerUuid: types.optional(types.string, ''),
		addressCode: types.optional(types.string, ''),
		addressType: types.optional(types.number, 0),
		description: types.optional(types.string, ''),
		name: types.optional(types.string, ''),
		firstName: types.optional(types.string, ''),
		lastName: types.optional(types.string, ''),
		suffix: types.optional(types.string, ''),
		company: types.optional(types.string, ''),
		companyJobTitle: types.optional(types.string, ''),
		attention: types.optional(types.string, ''),
		addressLine1: types.optional(types.string, ''),
		addressLine2: types.optional(types.string, ''),
		addressLine3: types.optional(types.string, ''),
		city: types.optional(types.string, ''),
		state: types.optional(types.string, ''),
		stateFullName: types.optional(types.string, ''),
		postalCode: types.optional(types.string, ''),
		postalCodeExt: types.optional(types.string, ''),
		county: types.optional(types.string, ''),
		country: types.optional(types.string, ''),
		email: types.optional(types.string, ''),
		daytimePhone: types.optional(types.string, ''),
		nightPhone: types.optional(types.string, ''),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of CustomerAttributes 
 */ 
const CustomerAttributes = types
	.model('CustomerAttributes', {
		rowNum: types.optional(types.number, 0),
		customerUuid: types.optional(types.string, ''),
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
		customerAddress: types.optional(types.number, 0),
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
	customerAddress: 0,
};

export const CustomerDataModel = types
	.model('CustomerData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		customer: types.optional(Customer, {}),
		customerAddress: types.array(CustomerAddress),
		customerAttributes: types.optional(CustomerAttributes, {}),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of CustomerData 
 */ 
export const customerDataInit = {
	dataVersion: dataVersionInit,
	customer: {
		rowNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		digit_seller_id: '',
		customerUuid: '',
		customerCode: '',
		customerName: '',
		contact: '',
		contact2: '',
		contact3: '',
		phone1: '',
		phone2: '',
		phone3: '',
		phone4: '',
		email: '',
		webSite: '',
		channelNum: 0,
		channelAccountNum: 0,
		customerType: 0,
		customerStatus: 0,
		businessType: '',
		priceRule: '',
		firstDate: '',
		currency: '',
		creditLimit: 0,
		taxRate: 0,
		discountRate: 0,
		shippingCarrier: '',
		shippingClass: '',
		shippingAccount: '',
		priority: '',
		area: '',
		region: '',
		districtn: '',
		zone: '',
		taxId: '',
		resaleLicense: '',
		classCode: '',
		departmentCode: '',
		divisionCode: '',
		sourceCode: '',
		terms: '',
		termsDays: 0,
		salesRep: '',
		salesRep2: '',
		salesRep3: '',
		salesRep4: '',
		commissionRate: 0,
		commissionRate2: 0,
		commissionRate3: 0,
		commissionRate4: 0,
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
	customerAddress: [{
		rowNum: 0,
		addressUuid: '',
		customerUuid: '',
		addressCode: '',
		addressType: 0,
		description: '',
		name: '',
		firstName: '',
		lastName: '',
		suffix: '',
		company: '',
		companyJobTitle: '',
		attention: '',
		addressLine1: '',
		addressLine2: '',
		addressLine3: '',
		city: '',
		state: '',
		stateFullName: '',
		postalCode: '',
		postalCodeExt: '',
		county: '',
		country: '',
		email: '',
		daytimePhone: '',
		nightPhone: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	}], 
	customerAttributes: {
		rowNum: 0,
		customerUuid: '',
		jsonFields: '',



	},
};



