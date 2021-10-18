

import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of InvoiceReturnItems 
 */ 
const InvoiceReturnItems = types
	.model('InvoiceReturnItems', {
		returnItemUuid: types.optional(types.string, ''),
		transUuid: types.optional(types.string, ''),
		seq: types.optional(types.number, 0),
		invoiceUuid: types.optional(types.string, ''),
		invoiceItemsUuid: types.optional(types.string, ''),
		returnItemType: types.optional(types.number, 0),
		returnItemStatus: types.optional(types.number, 0),
		returnDate: types.optional(types.string, ''),
		returnTime: types.optional(types.string, ''),
		receiveDate: types.optional(types.string, ''),
		stockDate: types.optional(types.string, ''),
		sku: types.optional(types.string, ''),
		productUuid: types.optional(types.string, ''),
		inventoryUuid: types.optional(types.string, ''),
		invoiceWarehouseUuid: types.optional(types.string, ''),
		invoiceWarehouseCode: types.optional(types.string, ''),
		warehouseUuid: types.optional(types.string, ''),
		warehouseCode: types.optional(types.string, ''),
		lotNum: types.optional(types.string, ''),
		reason: types.optional(types.string, ''),
		description: types.optional(types.string, ''),
		notes: types.optional(types.string, ''),
		currency: types.optional(types.string, ''),
		uom: types.optional(types.string, ''),
		packType: types.optional(types.string, ''),
		packQty: types.optional(types.number, 0),
		returnPack: types.optional(types.number, 0),
		receivePack: types.optional(types.number, 0),
		stockPack: types.optional(types.number, 0),
		nonStockPack: types.optional(types.number, 0),
		returnQty: types.optional(types.number, 0),
		receiveQty: types.optional(types.number, 0),
		stockQty: types.optional(types.number, 0),
		nonStockQty: types.optional(types.number, 0),
		putBackWarehouseUuid: types.optional(types.string, ''),
		putBackWarehouseCode: types.optional(types.string, ''),
		damageWarehouseUuid: types.optional(types.string, ''),
		damageWarehouseCode: types.optional(types.string, ''),
		invoiceDiscountPrice: types.optional(types.number, 0),
		invoiceDiscountAmount: types.optional(types.number, 0),
		returnDiscountAmount: types.optional(types.number, 0),
		price: types.optional(types.number, 0),
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
		stockable: types.optional(types.boolean, false),
		isAr: types.optional(types.boolean, false),
		taxable: types.optional(types.boolean, false),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of InvoiceTransaction 
 */ 
const InvoiceTransaction = types
	.model('InvoiceTransaction', {
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		transUuid: types.optional(types.string, ''),
		transNum: types.optional(types.number, 0),
		invoiceUuid: types.optional(types.string, ''),
		invoiceNumber: types.optional(types.string, ''),
		transType: types.optional(types.number, 0),
		transStatus: types.optional(types.number, 0),
		transDate: types.optional(types.string, ''),
		transTime: types.optional(types.string, ''),
		description: types.optional(types.string, ''),
		notes: types.optional(types.string, ''),
		paidBy: types.optional(types.number, 0),
		bankAccountUuid: types.optional(types.string, ''),
		bankAccountCode: types.optional(types.string, ''),
		checkNum: types.optional(types.string, ''),
		authCode: types.optional(types.string, ''),
		currency: types.optional(types.string, ''),
		exchangeRate: types.optional(types.number, 0),
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
		creditAccount: types.optional(types.number, 0),
		debitAccount: types.optional(types.number, 0),
		transSourceCode: types.optional(types.string, ''),
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
		invoiceReturnItems: types.optional(types.number, 0),
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
	invoiceReturnItems: 0,
};

export const InvoiceTransactionDataModel = types
	.model('InvoiceTransactionData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		invoiceTransaction: types.optional(InvoiceTransaction, {}),
		invoiceReturnItems: types.array(InvoiceReturnItems),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of InvoiceTransactionData 
 */ 
export const invoiceTransactionDataInit = {
	dataVersion: dataVersionInit,
	invoiceTransaction: {
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		transUuid: '',
		transNum: 0,
		invoiceUuid: '',
		invoiceNumber: '',
		transType: 0,
		transStatus: 0,
		transDate: '',
		transTime: '',
		description: '',
		notes: '',
		paidBy: 0,
		bankAccountUuid: '',
		bankAccountCode: '',
		checkNum: '',
		authCode: '',
		currency: '',
		exchangeRate: 0,
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
		creditAccount: 0,
		debitAccount: 0,
		transSourceCode: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
	invoiceReturnItems: [{
		returnItemUuid: '',
		transUuid: '',
		seq: 0,
		invoiceUuid: '',
		invoiceItemsUuid: '',
		returnItemType: 0,
		returnItemStatus: 0,
		returnDate: '',
		returnTime: '',
		receiveDate: '',
		stockDate: '',
		sku: '',
		productUuid: '',
		inventoryUuid: '',
		invoiceWarehouseUuid: '',
		invoiceWarehouseCode: '',
		warehouseUuid: '',
		warehouseCode: '',
		lotNum: '',
		reason: '',
		description: '',
		notes: '',
		currency: '',
		uom: '',
		packType: '',
		packQty: 0,
		returnPack: 0,
		receivePack: 0,
		stockPack: 0,
		nonStockPack: 0,
		returnQty: 0,
		receiveQty: 0,
		stockQty: 0,
		nonStockQty: 0,
		putBackWarehouseUuid: '',
		putBackWarehouseCode: '',
		damageWarehouseUuid: '',
		damageWarehouseCode: '',
		invoiceDiscountPrice: 0,
		invoiceDiscountAmount: 0,
		returnDiscountAmount: 0,
		price: 0,
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
		stockable: false,
		isAr: false,
		taxable: false,
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	}], 
};



