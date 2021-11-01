


import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of MiscInvoiceHeader 
 */ 
const MiscInvoiceHeader = types
	.model('MiscInvoiceHeader', {
		rowNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		miscInvoiceUuid: types.optional(types.string, ''),
		miscInvoiceNumber: types.optional(types.string, ''),
		qboDocNumber: types.optional(types.string, ''),
		miscInvoiceType: types.optional(types.number, 0),
		miscInvoiceStatus: types.optional(types.number, 0),
		miscInvoiceDate: types.optional(types.string, ''),
		miscInvoiceTime: types.optional(types.string, ''),
		customerUuid: types.optional(types.string, ''),
		customerCode: types.optional(types.string, ''),
		customerName: types.optional(types.string, ''),
		notes: types.optional(types.string, ''),
		paidBy: types.optional(types.number, 0),
		bankAccountUuid: types.optional(types.string, ''),
		bankAccountCode: types.optional(types.string, ''),
		checkNum: types.optional(types.string, ''),
		authCode: types.optional(types.string, ''),
		currency: types.optional(types.string, ''),
		totalAmount: types.optional(types.number, 0),
		paidAmount: types.optional(types.number, 0),
		creditAmount: types.optional(types.number, 0),
		balance: types.optional(types.number, 0),
		invoiceSourceCode: types.optional(types.string, ''),
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
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
};

export const MiscInvoiceDataModel = types
	.model('MiscInvoiceData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		miscInvoiceHeader: types.optional(MiscInvoiceHeader, {}),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of MiscInvoiceData 
 */ 
export const miscInvoiceDataInit = {
	dataVersion: dataVersionInit,
	miscInvoiceHeader: {
		rowNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		miscInvoiceUuid: '',
		miscInvoiceNumber: '',
		qboDocNumber: '',
		miscInvoiceType: 0,
		miscInvoiceStatus: 0,
		miscInvoiceDate: '',
		miscInvoiceTime: '',
		customerUuid: '',
		customerCode: '',
		customerName: '',
		notes: '',
		paidBy: 0,
		bankAccountUuid: '',
		bankAccountCode: '',
		checkNum: '',
		authCode: '',
		currency: '',
		totalAmount: 0,
		paidAmount: 0,
		creditAmount: 0,
		balance: 0,
		invoiceSourceCode: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
};



