import { ScreenType, ProcessMode } from '../../types/enums';
import { DbComponentEnums } from '../../types';
import { IconNames } from '../../components/icon';
import { btnSave, btnEdit, btnList, btnDelete } from '../default';
import * as util from '../../util';

import { inventoryGrid } from './inventory.grid';
export const inventoryUi = { 
    page: {
        screenType: ScreenType.PROCESSING,
        screenid: 1001,
        processMode: ProcessMode.LIST,
        title: 'Inventory',
        subTitle: '',
        readonly: false,
    },
    header: {
        title: 'Inventory',
    },
    navbar: {},
    section: {},
    buttonGroup: {},
    buttons: {
        btnSave,
        btnEdit, 
        btnList, 
        btnDelete,
    },
	ui: {
		//#region UI control for ProductBasic
		centralProductNum: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'centralProductNum',
			label: 'centralProductNum',
			placeholder: 'centralProductNum',
			format: 'number',
			textStyle: {},
		},
		sku: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'sku',
			label: 'sku',
			placeholder: 'sku',
			maxLength: 100,
			textStyle: {},
		},
		fNSku: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'fNSku',
			label: 'fNSku',
			placeholder: 'fNSku',
			maxLength: 10,
			textStyle: {},
		},
		condition: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productBasic',
			name: 'condition',
			label: 'condition',
			placeholder: 'condition',
			textStyle: {},
		},
		brand: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'brand',
			label: 'brand',
			placeholder: 'brand',
			maxLength: 150,
			textStyle: {},
		},
		manufacturer: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'manufacturer',
			label: 'manufacturer',
			placeholder: 'manufacturer',
			maxLength: 255,
			textStyle: {},
		},
		productTitle: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'productTitle',
			label: 'productTitle',
			placeholder: 'productTitle',
			maxLength: 500,
			textStyle: {},
		},
		longDescription: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'longDescription',
			label: 'longDescription',
			placeholder: 'longDescription',
			maxLength: 2000,
			textStyle: {},
		},
		shortDescription: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'shortDescription',
			label: 'shortDescription',
			placeholder: 'shortDescription',
			maxLength: 100,
			textStyle: {},
		},
		subtitle: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'subtitle',
			label: 'subtitle',
			placeholder: 'subtitle',
			maxLength: 50,
			textStyle: {},
		},
		aSIN: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'aSIN',
			label: 'aSIN',
			placeholder: 'aSIN',
			maxLength: 10,
			textStyle: {},
		},
		uPC: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'uPC',
			label: 'uPC',
			placeholder: 'uPC',
			maxLength: 20,
			textStyle: {},
		},
		eAN: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'eAN',
			label: 'eAN',
			placeholder: 'eAN',
			maxLength: 20,
			textStyle: {},
		},
		iSBN: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'iSBN',
			label: 'iSBN',
			placeholder: 'iSBN',
			maxLength: 20,
			textStyle: {},
		},
		mPN: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'mPN',
			label: 'mPN',
			placeholder: 'mPN',
			maxLength: 50,
			textStyle: {},
		},
		price: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'price',
			label: 'price',
			placeholder: 'price',
			format: 'price',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		cost: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'cost',
			label: 'cost',
			placeholder: 'cost',
			format: 'cost',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		avgCost: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'avgCost',
			label: 'avgCost',
			placeholder: 'avgCost',
			format: 'cost',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		mAPPrice: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'mAPPrice',
			label: 'mAPPrice',
			placeholder: 'mAPPrice',
			format: 'price',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		mSRP: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'mSRP',
			label: 'mSRP',
			placeholder: 'mSRP',
			format: 'decimalNumber',
			textStyle: {},
		},
		bundleType: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productBasic',
			name: 'bundleType',
			label: 'bundleType',
			placeholder: 'bundleType',
			textStyle: {},
		},
		productType: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productBasic',
			name: 'productType',
			label: 'productType',
			placeholder: 'productType',
			textStyle: {},
		},
		variationVaryBy: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'variationVaryBy',
			label: 'variationVaryBy',
			placeholder: 'variationVaryBy',
			maxLength: 80,
			textStyle: {},
		},
		copyToChildren: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productBasic',
			name: 'copyToChildren',
			label: 'copyToChildren',
			placeholder: 'copyToChildren',
			textStyle: {},
		},
		multipackQuantity: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'multipackQuantity',
			label: 'multipackQuantity',
			placeholder: 'multipackQuantity',
			format: 'number',
			textStyle: {},
		},
		variationParentSKU: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'variationParentSKU',
			label: 'variationParentSKU',
			placeholder: 'variationParentSKU',
			maxLength: 50,
			textStyle: {},
		},
		isInRelationship: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productBasic',
			name: 'isInRelationship',
			label: 'isInRelationship',
			placeholder: 'isInRelationship',
			textStyle: {},
		},
		netWeight: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'netWeight',
			label: 'netWeight',
			placeholder: 'netWeight',
			format: 'decimalNumber',
			textStyle: {},
		},
		grossWeight: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'grossWeight',
			label: 'grossWeight',
			placeholder: 'grossWeight',
			format: 'decimalNumber',
			textStyle: {},
		},
		weightUnit: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productBasic',
			name: 'weightUnit',
			label: 'weightUnit',
			placeholder: 'weightUnit',
			textStyle: {},
		},
		productHeight: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'productHeight',
			label: 'productHeight',
			placeholder: 'productHeight',
			format: 'decimalNumber',
			textStyle: {},
		},
		productLength: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'productLength',
			label: 'productLength',
			placeholder: 'productLength',
			format: 'decimalNumber',
			textStyle: {},
		},
		productWidth: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'productWidth',
			label: 'productWidth',
			placeholder: 'productWidth',
			format: 'decimalNumber',
			textStyle: {},
		},
		boxHeight: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'boxHeight',
			label: 'boxHeight',
			placeholder: 'boxHeight',
			format: 'decimalNumber',
			textStyle: {},
		},
		boxLength: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'boxLength',
			label: 'boxLength',
			placeholder: 'boxLength',
			format: 'decimalNumber',
			textStyle: {},
		},
		boxWidth: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'boxWidth',
			label: 'boxWidth',
			placeholder: 'boxWidth',
			format: 'decimalNumber',
			textStyle: {},
		},
		dimensionUnit: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productBasic',
			name: 'dimensionUnit',
			label: 'dimensionUnit',
			placeholder: 'dimensionUnit',
			textStyle: {},
		},
		harmonizedCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'harmonizedCode',
			label: 'harmonizedCode',
			placeholder: 'harmonizedCode',
			maxLength: 20,
			textStyle: {},
		},
		taxProductCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'taxProductCode',
			label: 'taxProductCode',
			placeholder: 'taxProductCode',
			maxLength: 25,
			textStyle: {},
		},
		isBlocked: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productBasic',
			name: 'isBlocked',
			label: 'isBlocked',
			placeholder: 'isBlocked',
			textStyle: {},
		},
		warranty: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'warranty',
			label: 'warranty',
			placeholder: 'warranty',
			maxLength: 255,
			textStyle: {},
		},
		createBy: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'createBy',
			label: 'createBy',
			placeholder: 'createBy',
			maxLength: 100,
			textStyle: {},
		},
		updateBy: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'updateBy',
			label: 'updateBy',
			placeholder: 'updateBy',
			maxLength: 100,
			textStyle: {},
		},
		createDate: {
			type: DbComponentEnums.inputTypeEnum.date,
			parentName: 'productBasic',
			name: 'createDate',
			label: 'createDate',
			placeholder: 'createDate',
			format: 'date',
			textStyle: {},
		},
		updateDate: {
			type: DbComponentEnums.inputTypeEnum.date,
			parentName: 'productBasic',
			name: 'updateDate',
			label: 'updateDate',
			placeholder: 'updateDate',
			format: 'date',
			textStyle: {},
		},
		classificationNum: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productBasic',
			name: 'classificationNum',
			label: 'classificationNum',
			placeholder: 'classificationNum',
			format: 'number',
			textStyle: {},
		},
		originalUPC: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productBasic',
			name: 'originalUPC',
			label: 'originalUPC',
			placeholder: 'originalUPC',
			maxLength: 20,
			textStyle: {},
		},
		//#endregion UI control for ProductBasic



		//#region UI control for ProductExt
		styleCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'styleCode',
			label: 'styleCode',
			placeholder: 'styleCode',
			maxLength: 100,
			textStyle: {},
		},
		colorPatternCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'colorPatternCode',
			label: 'colorPatternCode',
			placeholder: 'colorPatternCode',
			maxLength: 50,
			textStyle: {},
		},
		sizeType: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'sizeType',
			label: 'sizeType',
			placeholder: 'sizeType',
			maxLength: 50,
			textStyle: {},
		},
		sizeCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'sizeCode',
			label: 'sizeCode',
			placeholder: 'sizeCode',
			maxLength: 50,
			textStyle: {},
		},
		widthCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'widthCode',
			label: 'widthCode',
			placeholder: 'widthCode',
			maxLength: 30,
			textStyle: {},
		},
		lengthCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'lengthCode',
			label: 'lengthCode',
			placeholder: 'lengthCode',
			maxLength: 30,
			textStyle: {},
		},
		classCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'classCode',
			label: 'classCode',
			placeholder: 'classCode',
			maxLength: 50,
			textStyle: {},
		},
		subClassCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'subClassCode',
			label: 'subClassCode',
			placeholder: 'subClassCode',
			maxLength: 50,
			textStyle: {},
		},
		departmentCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'departmentCode',
			label: 'departmentCode',
			placeholder: 'departmentCode',
			maxLength: 50,
			textStyle: {},
		},
		divisionCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'divisionCode',
			label: 'divisionCode',
			placeholder: 'divisionCode',
			maxLength: 50,
			textStyle: {},
		},
		oEMCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'oEMCode',
			label: 'oEMCode',
			placeholder: 'oEMCode',
			maxLength: 50,
			textStyle: {},
		},
		alternateCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'alternateCode',
			label: 'alternateCode',
			placeholder: 'alternateCode',
			maxLength: 50,
			textStyle: {},
		},
		remark: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'remark',
			label: 'remark',
			placeholder: 'remark',
			maxLength: 50,
			textStyle: {},
		},
		model: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'model',
			label: 'model',
			placeholder: 'model',
			maxLength: 50,
			textStyle: {},
		},
		catalogPage: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'catalogPage',
			label: 'catalogPage',
			placeholder: 'catalogPage',
			maxLength: 50,
			textStyle: {},
		},
		categoryCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'categoryCode',
			label: 'categoryCode',
			placeholder: 'categoryCode',
			maxLength: 50,
			textStyle: {},
		},
		groupCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'groupCode',
			label: 'groupCode',
			placeholder: 'groupCode',
			maxLength: 50,
			textStyle: {},
		},
		subGroupCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'subGroupCode',
			label: 'subGroupCode',
			placeholder: 'subGroupCode',
			maxLength: 50,
			textStyle: {},
		},
		priceRule: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'priceRule',
			label: 'priceRule',
			placeholder: 'priceRule',
			maxLength: 50,
			textStyle: {},
		},
		stockable: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productExt',
			name: 'stockable',
			label: 'stockable',
			placeholder: 'stockable',
			textStyle: {},
		},
		isAr: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productExt',
			name: 'isAr',
			label: 'isAr',
			placeholder: 'isAr',
			textStyle: {},
		},
		isAp: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productExt',
			name: 'isAp',
			label: 'isAp',
			placeholder: 'isAp',
			textStyle: {},
		},
		taxable: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productExt',
			name: 'taxable',
			label: 'taxable',
			placeholder: 'taxable',
			textStyle: {},
		},
		costable: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productExt',
			name: 'costable',
			label: 'costable',
			placeholder: 'costable',
			textStyle: {},
		},
		isProfit: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productExt',
			name: 'isProfit',
			label: 'isProfit',
			placeholder: 'isProfit',
			textStyle: {},
		},
		release: {
			type: DbComponentEnums.inputTypeEnum.checkbox,
			parentName: 'productExt',
			name: 'release',
			label: 'release',
			placeholder: 'release',
			textStyle: {},
		},
		currency: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'currency',
			label: 'currency',
			placeholder: 'currency',
			maxLength: 10,
			textStyle: {},
		},
		uom: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'uom',
			label: 'uom',
			placeholder: 'uom',
			maxLength: 50,
			textStyle: {},
		},
		qtyPerPallot: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productExt',
			name: 'qtyPerPallot',
			label: 'qtyPerPallot',
			placeholder: 'qtyPerPallot',
			format: 'decimalNumber',
			textStyle: {},
		},
		qtyPerCase: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productExt',
			name: 'qtyPerCase',
			label: 'qtyPerCase',
			placeholder: 'qtyPerCase',
			format: 'decimalNumber',
			textStyle: {},
		},
		qtyPerBox: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productExt',
			name: 'qtyPerBox',
			label: 'qtyPerBox',
			placeholder: 'qtyPerBox',
			format: 'decimalNumber',
			textStyle: {},
		},
		packType: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'packType',
			label: 'packType',
			placeholder: 'packType',
			maxLength: 50,
			textStyle: {},
		},
		packQty: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productExt',
			name: 'packQty',
			label: 'packQty',
			placeholder: 'packQty',
			format: 'qty',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		defaultPackType: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'defaultPackType',
			label: 'defaultPackType',
			placeholder: 'defaultPackType',
			maxLength: 50,
			textStyle: {},
		},
		defaultWarehouseCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'defaultWarehouseCode',
			label: 'defaultWarehouseCode',
			placeholder: 'defaultWarehouseCode',
			maxLength: 50,
			textStyle: {},
		},
		defaultVendorCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'defaultVendorCode',
			label: 'defaultVendorCode',
			placeholder: 'defaultVendorCode',
			maxLength: 50,
			textStyle: {},
		},
		poSize: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productExt',
			name: 'poSize',
			label: 'poSize',
			placeholder: 'poSize',
			format: 'decimalNumber',
			textStyle: {},
		},
		minStock: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productExt',
			name: 'minStock',
			label: 'minStock',
			placeholder: 'minStock',
			format: 'decimalNumber',
			textStyle: {},
		},
		salesCost: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productExt',
			name: 'salesCost',
			label: 'salesCost',
			placeholder: 'salesCost',
			format: 'cost',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		leadTimeDay: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'productExt',
			name: 'leadTimeDay',
			label: 'leadTimeDay',
			placeholder: 'leadTimeDay',
			format: 'number',
			textStyle: {},
		},
		productYear: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'productYear',
			label: 'productYear',
			placeholder: 'productYear',
			maxLength: 50,
			textStyle: {},
		},
		updateDateUtc: {
			type: DbComponentEnums.inputTypeEnum.date,
			parentName: 'productExt',
			name: 'updateDateUtc',
			label: 'updateDateUtc',
			placeholder: 'updateDateUtc',
			format: 'date',
			textStyle: {},
		},
		enterBy: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'productExt',
			name: 'enterBy',
			label: 'enterBy',
			placeholder: 'enterBy',
			maxLength: 100,
			textStyle: {},
		},
		//#endregion UI control for ProductExt



		//#region UI control for ProductExtAttributes
		//#endregion UI control for ProductExtAttributes



	},

    grid: {
		inventory: inventoryGrid,
    },
    modal: {},
    menu: {},
};
