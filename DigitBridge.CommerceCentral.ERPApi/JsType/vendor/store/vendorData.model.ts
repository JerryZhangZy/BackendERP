


import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of Vendor 
 */ 
const Vendor = types
	.model('Vendor', {
		rowNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		digit_supplier_id: types.optional(types.string, ''),
		vendorUuid: types.optional(types.string, ''),
		vendorCode: types.optional(types.string, ''),
		vendorName: types.optional(types.string, ''),
		contact: types.optional(types.string, ''),
		phone1: types.optional(types.string, ''),
		phone2: types.optional(types.string, ''),
		phone3: types.optional(types.string, ''),
		phone4: types.optional(types.string, ''),
		email: types.optional(types.string, ''),
		vendorType: types.optional(types.number, 0),
		vendorStatus: types.optional(types.number, 0),
		businessType: types.optional(types.string, ''),
		priceRule: types.optional(types.string, ''),
		firstDate: types.optional(types.string, ''),
		currency: types.optional(types.string, ''),
		taxRate: types.optional(types.number, 0),
		discountRate: types.optional(types.number, 0),
		shippingCarrier: types.optional(types.string, ''),
		shippingClass: types.optional(types.string, ''),
		shippingAccount: types.optional(types.string, ''),
		priority: types.optional(types.string, ''),
		area: types.optional(types.string, ''),
		taxId: types.optional(types.string, ''),
		resaleLicense: types.optional(types.string, ''),
		classCode: types.optional(types.string, ''),
		departmentCode: types.optional(types.string, ''),
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
 * data model of VendorAddress 
 */ 
const VendorAddress = types
	.model('VendorAddress', {
		rowNum: types.optional(types.number, 0),
		addressUuid: types.optional(types.string, ''),
		vendorUuid: types.optional(types.string, ''),
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
 * data model of VendorAttributes 
 */ 
const VendorAttributes = types
	.model('VendorAttributes', {
		rowNum: types.optional(types.number, 0),
		vendorUuid: types.optional(types.string, ''),
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
		vendorAddress: types.optional(types.number, 0),
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
	vendorAddress: 0,
};

export const VendorDataModel = types
	.model('VendorData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		vendor: types.optional(Vendor, {}),
		vendorAddress: types.array(VendorAddress),
		vendorAttributes: types.optional(VendorAttributes, {}),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of VendorData 
 */ 
export const vendorDataInit = {
	dataVersion: dataVersionInit,
	vendor: {
		rowNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		digit_supplier_id: '',
		vendorUuid: '',
		vendorCode: '',
		vendorName: '',
		contact: '',
		phone1: '',
		phone2: '',
		phone3: '',
		phone4: '',
		email: '',
		vendorType: 0,
		vendorStatus: 0,
		businessType: '',
		priceRule: '',
		firstDate: '',
		currency: '',
		taxRate: 0,
		discountRate: 0,
		shippingCarrier: '',
		shippingClass: '',
		shippingAccount: '',
		priority: '',
		area: '',
		taxId: '',
		resaleLicense: '',
		classCode: '',
		departmentCode: '',
		creditAccount: 0,
		debitAccount: 0,
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
	vendorAddress: [{
		rowNum: 0,
		addressUuid: '',
		vendorUuid: '',
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
	vendorAttributes: {
		rowNum: 0,
		vendorUuid: '',
		jsonFields: '',



	},
};



