

import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of Inventory 
 */ 
const Inventory = types
	.model('Inventory', {
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		productUuid: types.optional(types.string, ''),
		inventoryUuid: types.optional(types.string, ''),
		styleCode: types.optional(types.string, ''),
		colorPatternCode: types.optional(types.string, ''),
		sizeType: types.optional(types.string, ''),
		sizeCode: types.optional(types.string, ''),
		widthCode: types.optional(types.string, ''),
		lengthCode: types.optional(types.string, ''),
		priceRule: types.optional(types.string, ''),
		leadTimeDay: types.optional(types.number, 0),
		poSize: types.optional(types.number, 0),
		minStock: types.optional(types.number, 0),
		sku: types.optional(types.string, ''),
		warehouseUuid: types.optional(types.string, ''),
		warehouseCode: types.optional(types.string, ''),
		warehouseName: types.optional(types.string, ''),
		lotNum: types.optional(types.string, ''),
		lotInDate: types.optional(types.string, ''),
		lotExpDate: types.optional(types.string, ''),
		lotDescription: types.optional(types.string, ''),
		lpnNum: types.optional(types.string, ''),
		lpnDescription: types.optional(types.string, ''),
		notes: types.optional(types.string, ''),
		currency: types.optional(types.string, ''),
		uom: types.optional(types.string, ''),
		qtyPerPallot: types.optional(types.number, 0),
		qtyPerCase: types.optional(types.number, 0),
		qtyPerBox: types.optional(types.number, 0),
		packType: types.optional(types.string, ''),
		packQty: types.optional(types.number, 0),
		defaultPackType: types.optional(types.string, ''),
		instock: types.optional(types.number, 0),
		onHand: types.optional(types.number, 0),
		openSoQty: types.optional(types.number, 0),
		openFulfillmentQty: types.optional(types.number, 0),
		avaQty: types.optional(types.number, 0),
		openPoQty: types.optional(types.number, 0),
		openInTransitQty: types.optional(types.number, 0),
		openWipQty: types.optional(types.number, 0),
		projectedQty: types.optional(types.number, 0),
		baseCost: types.optional(types.number, 0),
		taxRate: types.optional(types.number, 0),
		taxAmount: types.optional(types.number, 0),
		shippingAmount: types.optional(types.number, 0),
		miscAmount: types.optional(types.number, 0),
		chargeAndAllowanceAmount: types.optional(types.number, 0),
		unitCost: types.optional(types.number, 0),
		avgCost: types.optional(types.number, 0),
		salesCost: types.optional(types.number, 0),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),

		inventoryAttributes: types.map(types.frozen()),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of InventoryAttributes 
 */ 
const InventoryAttributes = types
	.model('InventoryAttributes', {
		productUuid: types.optional(types.string, ''),
		inventoryUuid: types.optional(types.string, ''),
		jsonFields: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of ProductBasic 
 */ 
const ProductBasic = types
	.model('ProductBasic', {
		centralProductNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		sku: types.optional(types.string, ''),
		fNSku: types.optional(types.string, ''),
		condition: types.optional(types.boolean, false),
		brand: types.optional(types.string, ''),
		manufacturer: types.optional(types.string, ''),
		productTitle: types.optional(types.string, ''),
		longDescription: types.optional(types.string, ''),
		shortDescription: types.optional(types.string, ''),
		subtitle: types.optional(types.string, ''),
		aSIN: types.optional(types.string, ''),
		uPC: types.optional(types.string, ''),
		eAN: types.optional(types.string, ''),
		iSBN: types.optional(types.string, ''),
		mPN: types.optional(types.string, ''),
		price: types.optional(types.number, 0),
		cost: types.optional(types.number, 0),
		avgCost: types.optional(types.number, 0),
		mAPPrice: types.optional(types.number, 0),
		mSRP: types.optional(types.number, 0),
		bundleType: types.optional(types.boolean, false),
		productType: types.optional(types.boolean, false),
		variationVaryBy: types.optional(types.string, ''),
		copyToChildren: types.optional(types.boolean, false),
		multipackQuantity: types.optional(types.number, 0),
		variationParentSKU: types.optional(types.string, ''),
		isInRelationship: types.optional(types.boolean, false),
		netWeight: types.optional(types.number, 0),
		grossWeight: types.optional(types.number, 0),
		weightUnit: types.optional(types.boolean, false),
		productHeight: types.optional(types.number, 0),
		productLength: types.optional(types.number, 0),
		productWidth: types.optional(types.number, 0),
		boxHeight: types.optional(types.number, 0),
		boxLength: types.optional(types.number, 0),
		boxWidth: types.optional(types.number, 0),
		dimensionUnit: types.optional(types.boolean, false),
		harmonizedCode: types.optional(types.string, ''),
		taxProductCode: types.optional(types.string, ''),
		isBlocked: types.optional(types.boolean, false),
		warranty: types.optional(types.string, ''),
		createBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
		createDate: types.optional(types.string, ''),
		updateDate: types.optional(types.string, ''),
		classificationNum: types.optional(types.number, 0),
		originalUPC: types.optional(types.string, ''),
		productUuid: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of ProductExt 
 */ 
const ProductExt = types
	.model('ProductExt', {
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		productUuid: types.optional(types.string, ''),
		centralProductNum: types.optional(types.number, 0),
		sku: types.optional(types.string, ''),
		styleCode: types.optional(types.string, ''),
		colorPatternCode: types.optional(types.string, ''),
		sizeType: types.optional(types.string, ''),
		sizeCode: types.optional(types.string, ''),
		widthCode: types.optional(types.string, ''),
		lengthCode: types.optional(types.string, ''),
		classCode: types.optional(types.string, ''),
		subClassCode: types.optional(types.string, ''),
		departmentCode: types.optional(types.string, ''),
		divisionCode: types.optional(types.string, ''),
		oEMCode: types.optional(types.string, ''),
		alternateCode: types.optional(types.string, ''),
		remark: types.optional(types.string, ''),
		model: types.optional(types.string, ''),
		catalogPage: types.optional(types.string, ''),
		categoryCode: types.optional(types.string, ''),
		groupCode: types.optional(types.string, ''),
		subGroupCode: types.optional(types.string, ''),
		priceRule: types.optional(types.string, ''),
		stockable: types.optional(types.boolean, false),
		isAr: types.optional(types.boolean, false),
		isAp: types.optional(types.boolean, false),
		taxable: types.optional(types.boolean, false),
		costable: types.optional(types.boolean, false),
		isProfit: types.optional(types.boolean, false),
		release: types.optional(types.boolean, false),
		currency: types.optional(types.string, ''),
		uom: types.optional(types.string, ''),
		qtyPerPallot: types.optional(types.number, 0),
		qtyPerCase: types.optional(types.number, 0),
		qtyPerBox: types.optional(types.number, 0),
		packType: types.optional(types.string, ''),
		packQty: types.optional(types.number, 0),
		defaultPackType: types.optional(types.string, ''),
		defaultWarehouseCode: types.optional(types.string, ''),
		defaultVendorCode: types.optional(types.string, ''),
		poSize: types.optional(types.number, 0),
		minStock: types.optional(types.number, 0),
		salesCost: types.optional(types.number, 0),
		leadTimeDay: types.optional(types.number, 0),
		productYear: types.optional(types.string, ''),
		updateDateUtc: types.optional(types.string, ''),
		enterBy: types.optional(types.string, ''),
		updateBy: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of ProductExtAttributes 
 */ 
const ProductExtAttributes = types
	.model('ProductExtAttributes', {
		productUuid: types.optional(types.string, ''),
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
		inventory: types.optional(types.number, 0),
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
	inventory: 0,
};

export const InventoryDataModel = types
	.model('InventoryData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		productBasic: types.optional(ProductBasic, {}),
		productExt: types.optional(ProductExt, {}),
		productExtAttributes: types.optional(ProductExtAttributes, {}),
		inventory: types.array(Inventory),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of InventoryData 
 */ 
export const inventoryDataInit = {
	dataVersion: dataVersionInit,
	productBasic: {
		centralProductNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		sku: '',
		fNSku: '',
		condition: false,
		brand: '',
		manufacturer: '',
		productTitle: '',
		longDescription: '',
		shortDescription: '',
		subtitle: '',
		aSIN: '',
		uPC: '',
		eAN: '',
		iSBN: '',
		mPN: '',
		price: 0,
		cost: 0,
		avgCost: 0,
		mAPPrice: 0,
		mSRP: 0,
		bundleType: false,
		productType: false,
		variationVaryBy: '',
		copyToChildren: false,
		multipackQuantity: 0,
		variationParentSKU: '',
		isInRelationship: false,
		netWeight: 0,
		grossWeight: 0,
		weightUnit: false,
		productHeight: 0,
		productLength: 0,
		productWidth: 0,
		boxHeight: 0,
		boxLength: 0,
		boxWidth: 0,
		dimensionUnit: false,
		harmonizedCode: '',
		taxProductCode: '',
		isBlocked: false,
		warranty: '',
		createBy: '',
		updateBy: '',
		createDate: '',
		updateDate: '',
		classificationNum: 0,
		originalUPC: '',
		productUuid: '',



	},
	productExt: {
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		productUuid: '',
		centralProductNum: 0,
		sku: '',
		styleCode: '',
		colorPatternCode: '',
		sizeType: '',
		sizeCode: '',
		widthCode: '',
		lengthCode: '',
		classCode: '',
		subClassCode: '',
		departmentCode: '',
		divisionCode: '',
		oEMCode: '',
		alternateCode: '',
		remark: '',
		model: '',
		catalogPage: '',
		categoryCode: '',
		groupCode: '',
		subGroupCode: '',
		priceRule: '',
		stockable: false,
		isAr: false,
		isAp: false,
		taxable: false,
		costable: false,
		isProfit: false,
		release: false,
		currency: '',
		uom: '',
		qtyPerPallot: 0,
		qtyPerCase: 0,
		qtyPerBox: 0,
		packType: '',
		packQty: 0,
		defaultPackType: '',
		defaultWarehouseCode: '',
		defaultVendorCode: '',
		poSize: 0,
		minStock: 0,
		salesCost: 0,
		leadTimeDay: 0,
		productYear: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
	productExtAttributes: {
		productUuid: '',
		jsonFields: '',



	},
	inventory: [{
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		productUuid: '',
		inventoryUuid: '',
		styleCode: '',
		colorPatternCode: '',
		sizeType: '',
		sizeCode: '',
		widthCode: '',
		lengthCode: '',
		priceRule: '',
		leadTimeDay: 0,
		poSize: 0,
		minStock: 0,
		sku: '',
		warehouseUuid: '',
		warehouseCode: '',
		warehouseName: '',
		lotNum: '',
		lotInDate: '',
		lotExpDate: '',
		lotDescription: '',
		lpnNum: '',
		lpnDescription: '',
		notes: '',
		currency: '',
		uom: '',
		qtyPerPallot: 0,
		qtyPerCase: 0,
		qtyPerBox: 0,
		packType: '',
		packQty: 0,
		defaultPackType: '',
		instock: 0,
		onHand: 0,
		openSoQty: 0,
		openFulfillmentQty: 0,
		avaQty: 0,
		openPoQty: 0,
		openInTransitQty: 0,
		openWipQty: 0,
		projectedQty: 0,
		baseCost: 0,
		taxRate: 0,
		taxAmount: 0,
		shippingAmount: 0,
		miscAmount: 0,
		chargeAndAllowanceAmount: 0,
		unitCost: 0,
		avgCost: 0,
		salesCost: 0,
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',

		inventoryAttributes: {},



	}], 
};



