


import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of InventoryUpdateHeader 
 */ 
const InventoryUpdateHeader = types
	.model('InventoryUpdateHeader', {
		rowNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		inventoryUpdateUuid: types.optional(types.string, ''),
		batchNumber: types.optional(types.string, ''),
		inventoryUpdateType: types.optional(types.number, 0),
		inventoryUpdateStatus: types.optional(types.number, 0),
		updateDate: types.optional(types.string, ''),
		updateTime: types.optional(types.string, ''),
		processor: types.optional(types.string, ''),
		warehouseUuid: types.optional(types.string, ''),
		warehouseCode: types.optional(types.string, ''),
		customerUuid: types.optional(types.string, ''),
		customerCode: types.optional(types.string, ''),
		customerName: types.optional(types.string, ''),
		vendorUuid: types.optional(types.string, ''),
		vendorCode: types.optional(types.string, ''),
		vendorName: types.optional(types.string, ''),
		referenceType: types.optional(types.number, 0),
		referenceUuid: types.optional(types.string, ''),
		referenceNum: types.optional(types.string, ''),
		inventoryUpdateSourceCode: types.optional(types.string, ''),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of InventoryUpdateItems 
 */ 
const InventoryUpdateItems = types
	.model('InventoryUpdateItems', {
		rowNum: types.optional(types.number, 0),
		inventoryUpdateItemsUuid: types.optional(types.string, ''),
		inventoryUpdateUuid: types.optional(types.string, ''),
		seq: types.optional(types.number, 0),
		itemDate: types.optional(types.string, ''),
		itemTime: types.optional(types.string, ''),
		sku: types.optional(types.string, ''),
		productUuid: types.optional(types.string, ''),
		inventoryUuid: types.optional(types.string, ''),
		warehouseUuid: types.optional(types.string, ''),
		warehouseCode: types.optional(types.string, ''),
		lotNum: types.optional(types.string, ''),
		description: types.optional(types.string, ''),
		notes: types.optional(types.string, ''),
		uom: types.optional(types.string, ''),
		packType: types.optional(types.string, ''),
		packQty: types.optional(types.number, 0),
		updatePack: types.optional(types.number, 0),
		countPack: types.optional(types.number, 0),
		beforeInstockPack: types.optional(types.number, 0),
		updateQty: types.optional(types.number, 0),
		countQty: types.optional(types.number, 0),
		beforeInstockQty: types.optional(types.number, 0),
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
		inventoryUpdateItems: types.optional(types.number, 0),
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
	inventoryUpdateItems: 0,
};

export const InventoryUpdateDataModel = types
	.model('InventoryUpdateData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		inventoryUpdateHeader: types.optional(InventoryUpdateHeader, {}),
		inventoryUpdateItems: types.array(InventoryUpdateItems),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of InventoryUpdateData 
 */ 
export const inventoryUpdateDataInit = {
	dataVersion: dataVersionInit,
	inventoryUpdateHeader: {
		rowNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		inventoryUpdateUuid: '',
		batchNumber: '',
		inventoryUpdateType: 0,
		inventoryUpdateStatus: 0,
		updateDate: '',
		updateTime: '',
		processor: '',
		warehouseUuid: '',
		warehouseCode: '',
		customerUuid: '',
		customerCode: '',
		customerName: '',
		vendorUuid: '',
		vendorCode: '',
		vendorName: '',
		referenceType: 0,
		referenceUuid: '',
		referenceNum: '',
		inventoryUpdateSourceCode: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
	inventoryUpdateItems: [{
		rowNum: 0,
		inventoryUpdateItemsUuid: '',
		inventoryUpdateUuid: '',
		seq: 0,
		itemDate: '',
		itemTime: '',
		sku: '',
		productUuid: '',
		inventoryUuid: '',
		warehouseUuid: '',
		warehouseCode: '',
		lotNum: '',
		description: '',
		notes: '',
		uom: '',
		packType: '',
		packQty: 0,
		updatePack: 0,
		countPack: 0,
		beforeInstockPack: 0,
		updateQty: 0,
		countQty: 0,
		beforeInstockQty: 0,
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



