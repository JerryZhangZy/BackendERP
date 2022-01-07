


import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of WarehouseTransferHeader 
 */ 
const WarehouseTransferHeader = types
	.model('WarehouseTransferHeader', {
		rowNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		warehouseTransferUuid: types.optional(types.string, ''),
		batchNumber: types.optional(types.string, ''),
		warehouseTransferType: types.optional(types.number, 0),
		warehouseTransferStatus: types.optional(types.number, 0),
		transferDate: types.optional(types.string, ''),
		transferTime: types.optional(types.string, ''),
		processor: types.optional(types.string, ''),
		receiveDate: types.optional(types.string, ''),
		receiveTime: types.optional(types.string, ''),
		receiveProcessor: types.optional(types.string, ''),
		fromWarehouseUuid: types.optional(types.string, ''),
		fromWarehouseCode: types.optional(types.string, ''),
		toWarehouseUuid: types.optional(types.string, ''),
		toWarehouseCode: types.optional(types.string, ''),
		inTransitToWarehouseCode: types.optional(types.string, ''),
		referenceType: types.optional(types.number, 0),
		referenceUuid: types.optional(types.string, ''),
		referenceNum: types.optional(types.string, ''),
		warehouseTransferSourceCode: types.optional(types.string, ''),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of WarehouseTransferItems 
 */ 
const WarehouseTransferItems = types
	.model('WarehouseTransferItems', {
		rowNum: types.optional(types.number, 0),
		warehouseTransferItemsUuid: types.optional(types.string, ''),
		referWarehouseTransferItemsUuid: types.optional(types.string, ''),
		warehouseTransferUuid: types.optional(types.string, ''),
		seq: types.optional(types.number, 0),
		itemDate: types.optional(types.string, ''),
		itemTime: types.optional(types.string, ''),
		sku: types.optional(types.string, ''),
		productUuid: types.optional(types.string, ''),
		fromInventoryUuid: types.optional(types.string, ''),
		fromWarehouseUuid: types.optional(types.string, ''),
		fromWarehouseCode: types.optional(types.string, ''),
		lotNum: types.optional(types.string, ''),
		toInventoryUuid: types.optional(types.string, ''),
		toWarehouseUuid: types.optional(types.string, ''),
		toWarehouseCode: types.optional(types.string, ''),
		toLotNum: types.optional(types.string, ''),
		description: types.optional(types.string, ''),
		notes: types.optional(types.string, ''),
		uom: types.optional(types.string, ''),
		packType: types.optional(types.string, ''),
		packQty: types.optional(types.number, 0),
		transferPack: types.optional(types.number, 0),
		transferQty: types.optional(types.number, 0),
		fromBeforeInstockPack: types.optional(types.number, 0),
		fromBeforeInstockQty: types.optional(types.number, 0),
		toBeforeInstockPack: types.optional(types.number, 0),
		toBeforeInstockQty: types.optional(types.number, 0),
		unitCost: types.optional(types.number, 0),
		avgCost: types.optional(types.number, 0),
		lotCost: types.optional(types.number, 0),
		lotInDate: types.optional(types.string, ''),
		lotExpDate: types.optional(types.string, ''),
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
		warehouseTransferItems: types.optional(types.number, 0),
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
	warehouseTransferItems: 0,
};

export const WarehouseTransferDataModel = types
	.model('WarehouseTransferData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		warehouseTransferHeader: types.optional(WarehouseTransferHeader, {}),
		warehouseTransferItems: types.array(WarehouseTransferItems),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of WarehouseTransferData 
 */ 
export const warehouseTransferDataInit = {
	dataVersion: dataVersionInit,
	warehouseTransferHeader: {
		rowNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		warehouseTransferUuid: '',
		batchNumber: '',
		warehouseTransferType: 0,
		warehouseTransferStatus: 0,
		transferDate: '',
		transferTime: '',
		processor: '',
		receiveDate: '',
		receiveTime: '',
		receiveProcessor: '',
		fromWarehouseUuid: '',
		fromWarehouseCode: '',
		toWarehouseUuid: '',
		toWarehouseCode: '',
		inTransitToWarehouseCode: '',
		referenceType: 0,
		referenceUuid: '',
		referenceNum: '',
		warehouseTransferSourceCode: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
	warehouseTransferItems: [{
		rowNum: 0,
		warehouseTransferItemsUuid: '',
		referWarehouseTransferItemsUuid: '',
		warehouseTransferUuid: '',
		seq: 0,
		itemDate: '',
		itemTime: '',
		sku: '',
		productUuid: '',
		fromInventoryUuid: '',
		fromWarehouseUuid: '',
		fromWarehouseCode: '',
		lotNum: '',
		toInventoryUuid: '',
		toWarehouseUuid: '',
		toWarehouseCode: '',
		toLotNum: '',
		description: '',
		notes: '',
		uom: '',
		packType: '',
		packQty: 0,
		transferPack: 0,
		transferQty: 0,
		fromBeforeInstockPack: 0,
		fromBeforeInstockQty: 0,
		toBeforeInstockPack: 0,
		toBeforeInstockQty: 0,
		unitCost: 0,
		avgCost: 0,
		lotCost: 0,
		lotInDate: '',
		lotExpDate: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	}], 
};



