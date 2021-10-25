import { ScreenType, ProcessMode } from '../../types/enums';
import { DbComponentEnums } from '../../types';
import { IconNames } from '../../components/icon';
import { btnSave, btnEdit, btnList, btnDelete } from '../default';
import * as util from '../../util';

import { invoiceItemsGrid } from './invoiceItems.grid';
export const invoiceUi = { 
    page: {
        screenType: ScreenType.PROCESSING,
        screenid: 1001,
        processMode: ProcessMode.LIST,
        title: 'Invoice',
        subTitle: '',
        readonly: false,
    },
    header: {
        title: 'Invoice',
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
		//#region UI control for InvoiceHeader
		invoiceNumber: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'invoiceNumber',
			label: 'invoiceNumber',
			placeholder: 'invoiceNumber',
			maxLength: 50,
			textStyle: {},
		},
		qboDocNumber: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'qboDocNumber',
			label: 'qboDocNumber',
			placeholder: 'qboDocNumber',
			maxLength: 50,
			textStyle: {},
		},
		orderNumber: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'orderNumber',
			label: 'orderNumber',
			placeholder: 'orderNumber',
			maxLength: 50,
			textStyle: {},
		},
		invoiceType: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'invoiceType',
			label: 'invoiceType',
			placeholder: 'invoiceType',
			format: 'number',
			textStyle: {},
		},
		invoiceStatus: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'invoiceStatus',
			label: 'invoiceStatus',
			placeholder: 'invoiceStatus',
			format: 'number',
			textStyle: {},
		},
		invoiceDate: {
			type: DbComponentEnums.inputTypeEnum.date,
			parentName: 'invoiceHeader',
			name: 'invoiceDate',
			label: 'invoiceDate',
			placeholder: 'invoiceDate',
			format: 'date',
			textStyle: {},
		},
		invoiceTime: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'invoiceTime',
			label: 'invoiceTime',
			placeholder: 'invoiceTime',
			format: 'time',
			textStyle: {},
		},
		dueDate: {
			type: DbComponentEnums.inputTypeEnum.date,
			parentName: 'invoiceHeader',
			name: 'dueDate',
			label: 'dueDate',
			placeholder: 'dueDate',
			format: 'date',
			textStyle: {},
		},
		billDate: {
			type: DbComponentEnums.inputTypeEnum.date,
			parentName: 'invoiceHeader',
			name: 'billDate',
			label: 'billDate',
			placeholder: 'billDate',
			format: 'date',
			textStyle: {},
		},
		shipDate: {
			type: DbComponentEnums.inputTypeEnum.date,
			parentName: 'invoiceHeader',
			name: 'shipDate',
			label: 'shipDate',
			placeholder: 'shipDate',
			format: 'date',
			textStyle: {},
		},
		customerCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'customerCode',
			label: 'customerCode',
			placeholder: 'customerCode',
			maxLength: 50,
			textStyle: {},
		},
		customerName: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'customerName',
			label: 'customerName',
			placeholder: 'customerName',
			maxLength: 200,
			textStyle: {},
		},
		terms: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'terms',
			label: 'terms',
			placeholder: 'terms',
			maxLength: 50,
			textStyle: {},
		},
		termsDays: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'termsDays',
			label: 'termsDays',
			placeholder: 'termsDays',
			format: 'number',
			textStyle: {},
		},
		currency: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'currency',
			label: 'currency',
			placeholder: 'currency',
			maxLength: 10,
			textStyle: {},
		},
		subTotalAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'subTotalAmount',
			label: 'subTotalAmount',
			placeholder: 'subTotalAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		salesAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'salesAmount',
			label: 'salesAmount',
			placeholder: 'salesAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		totalAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'totalAmount',
			label: 'totalAmount',
			placeholder: 'totalAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		taxableAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'taxableAmount',
			label: 'taxableAmount',
			placeholder: 'taxableAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		nonTaxableAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'nonTaxableAmount',
			label: 'nonTaxableAmount',
			placeholder: 'nonTaxableAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		taxRate: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'taxRate',
			label: 'taxRate',
			placeholder: 'taxRate',
			format: 'taxRate',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		taxAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'taxAmount',
			label: 'taxAmount',
			placeholder: 'taxAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		discountRate: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'discountRate',
			label: 'discountRate',
			placeholder: 'discountRate',
			format: 'rate',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		discountAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'discountAmount',
			label: 'discountAmount',
			placeholder: 'discountAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		shippingAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'shippingAmount',
			label: 'shippingAmount',
			placeholder: 'shippingAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		shippingTaxAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'shippingTaxAmount',
			label: 'shippingTaxAmount',
			placeholder: 'shippingTaxAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		miscAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'miscAmount',
			label: 'miscAmount',
			placeholder: 'miscAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		miscTaxAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'miscTaxAmount',
			label: 'miscTaxAmount',
			placeholder: 'miscTaxAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		chargeAndAllowanceAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'chargeAndAllowanceAmount',
			label: 'chargeAndAllowanceAmount',
			placeholder: 'chargeAndAllowanceAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		paidAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'paidAmount',
			label: 'paidAmount',
			placeholder: 'paidAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		creditAmount: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'creditAmount',
			label: 'creditAmount',
			placeholder: 'creditAmount',
			format: 'amount',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		balance: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'balance',
			label: 'balance',
			placeholder: 'balance',
			format: 'decimalNumber',
			textStyle: {},
		},
		unitCost: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'unitCost',
			label: 'unitCost',
			placeholder: 'unitCost',
			format: 'cost',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		avgCost: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'avgCost',
			label: 'avgCost',
			placeholder: 'avgCost',
			format: 'cost',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		lotCost: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeader',
			name: 'lotCost',
			label: 'lotCost',
			placeholder: 'lotCost',
			format: 'cost',
			align: DbComponentEnums.alignEnum.right,
			textStyle: {},
		},
		invoiceSourceCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'invoiceSourceCode',
			label: 'invoiceSourceCode',
			placeholder: 'invoiceSourceCode',
			maxLength: 100,
			textStyle: {},
		},
		updateDateUtc: {
			type: DbComponentEnums.inputTypeEnum.date,
			parentName: 'invoiceHeader',
			name: 'updateDateUtc',
			label: 'updateDateUtc',
			placeholder: 'updateDateUtc',
			format: 'date',
			textStyle: {},
		},
		enterBy: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'enterBy',
			label: 'enterBy',
			placeholder: 'enterBy',
			maxLength: 100,
			textStyle: {},
		},
		updateBy: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeader',
			name: 'updateBy',
			label: 'updateBy',
			placeholder: 'updateBy',
			maxLength: 100,
			textStyle: {},
		},
		//#endregion UI control for InvoiceHeader



		//#region UI control for InvoiceHeaderInfo
		centralFulfillmentNum: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeaderInfo',
			name: 'centralFulfillmentNum',
			label: 'centralFulfillmentNum',
			placeholder: 'centralFulfillmentNum',
			format: 'number',
			textStyle: {},
		},
		orderShipmentNum: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeaderInfo',
			name: 'orderShipmentNum',
			label: 'orderShipmentNum',
			placeholder: 'orderShipmentNum',
			format: 'number',
			textStyle: {},
		},
		shippingCarrier: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shippingCarrier',
			label: 'shippingCarrier',
			placeholder: 'shippingCarrier',
			maxLength: 50,
			textStyle: {},
		},
		shippingClass: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shippingClass',
			label: 'shippingClass',
			placeholder: 'shippingClass',
			maxLength: 50,
			textStyle: {},
		},
		centralOrderNum: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'invoiceHeaderInfo',
			name: 'centralOrderNum',
			label: 'centralOrderNum',
			placeholder: 'centralOrderNum',
			format: 'number',
			textStyle: {},
		},
		channelOrderID: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'channelOrderID',
			label: 'channelOrderID',
			placeholder: 'channelOrderID',
			maxLength: 130,
			textStyle: {},
		},
		secondaryChannelOrderID: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'secondaryChannelOrderID',
			label: 'secondaryChannelOrderID',
			placeholder: 'secondaryChannelOrderID',
			maxLength: 200,
			textStyle: {},
		},
		shippingAccount: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shippingAccount',
			label: 'shippingAccount',
			placeholder: 'shippingAccount',
			maxLength: 100,
			textStyle: {},
		},
		warehouseCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'warehouseCode',
			label: 'warehouseCode',
			placeholder: 'warehouseCode',
			maxLength: 50,
			textStyle: {},
		},
		refNum: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'refNum',
			label: 'refNum',
			placeholder: 'refNum',
			maxLength: 100,
			textStyle: {},
		},
		customerPoNum: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'customerPoNum',
			label: 'customerPoNum',
			placeholder: 'customerPoNum',
			maxLength: 100,
			textStyle: {},
		},
		endBuyerUserId: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'endBuyerUserId',
			label: 'endBuyerUserId',
			placeholder: 'endBuyerUserId',
			maxLength: 255,
			textStyle: {},
		},
		endBuyerName: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'endBuyerName',
			label: 'endBuyerName',
			placeholder: 'endBuyerName',
			maxLength: 255,
			textStyle: {},
		},
		endBuyerEmail: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'endBuyerEmail',
			label: 'endBuyerEmail',
			placeholder: 'endBuyerEmail',
			maxLength: 255,
			textStyle: {},
		},
		shipToName: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToName',
			label: 'shipToName',
			placeholder: 'shipToName',
			maxLength: 100,
			textStyle: {},
		},
		shipToCompany: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToCompany',
			label: 'shipToCompany',
			placeholder: 'shipToCompany',
			maxLength: 100,
			textStyle: {},
		},
		shipToAttention: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToAttention',
			label: 'shipToAttention',
			placeholder: 'shipToAttention',
			maxLength: 100,
			textStyle: {},
		},
		shipToAddressLine1: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToAddressLine1',
			label: 'shipToAddressLine1',
			placeholder: 'shipToAddressLine1',
			maxLength: 200,
			textStyle: {},
		},
		shipToAddressLine2: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToAddressLine2',
			label: 'shipToAddressLine2',
			placeholder: 'shipToAddressLine2',
			maxLength: 200,
			textStyle: {},
		},
		shipToCity: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToCity',
			label: 'shipToCity',
			placeholder: 'shipToCity',
			maxLength: 100,
			textStyle: {},
		},
		shipToState: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToState',
			label: 'shipToState',
			placeholder: 'shipToState',
			maxLength: 50,
			textStyle: {},
		},
		shipToStateFullName: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToStateFullName',
			label: 'shipToStateFullName',
			placeholder: 'shipToStateFullName',
			maxLength: 100,
			textStyle: {},
		},
		shipToPostalCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToPostalCode',
			label: 'shipToPostalCode',
			placeholder: 'shipToPostalCode',
			maxLength: 50,
			textStyle: {},
		},
		shipToPostalCodeExt: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToPostalCodeExt',
			label: 'shipToPostalCodeExt',
			placeholder: 'shipToPostalCodeExt',
			maxLength: 50,
			textStyle: {},
		},
		shipToCounty: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToCounty',
			label: 'shipToCounty',
			placeholder: 'shipToCounty',
			maxLength: 100,
			textStyle: {},
		},
		shipToCountry: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToCountry',
			label: 'shipToCountry',
			placeholder: 'shipToCountry',
			maxLength: 100,
			textStyle: {},
		},
		shipToEmail: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToEmail',
			label: 'shipToEmail',
			placeholder: 'shipToEmail',
			maxLength: 100,
			textStyle: {},
		},
		shipToDaytimePhone: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToDaytimePhone',
			label: 'shipToDaytimePhone',
			placeholder: 'shipToDaytimePhone',
			maxLength: 50,
			textStyle: {},
		},
		shipToNightPhone: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'shipToNightPhone',
			label: 'shipToNightPhone',
			placeholder: 'shipToNightPhone',
			maxLength: 50,
			textStyle: {},
		},
		billToName: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToName',
			label: 'billToName',
			placeholder: 'billToName',
			maxLength: 100,
			textStyle: {},
		},
		billToCompany: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToCompany',
			label: 'billToCompany',
			placeholder: 'billToCompany',
			maxLength: 100,
			textStyle: {},
		},
		billToAttention: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToAttention',
			label: 'billToAttention',
			placeholder: 'billToAttention',
			maxLength: 100,
			textStyle: {},
		},
		billToAddressLine1: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToAddressLine1',
			label: 'billToAddressLine1',
			placeholder: 'billToAddressLine1',
			maxLength: 200,
			textStyle: {},
		},
		billToAddressLine2: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToAddressLine2',
			label: 'billToAddressLine2',
			placeholder: 'billToAddressLine2',
			maxLength: 200,
			textStyle: {},
		},
		billToCity: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToCity',
			label: 'billToCity',
			placeholder: 'billToCity',
			maxLength: 100,
			textStyle: {},
		},
		billToState: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToState',
			label: 'billToState',
			placeholder: 'billToState',
			maxLength: 50,
			textStyle: {},
		},
		billToStateFullName: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToStateFullName',
			label: 'billToStateFullName',
			placeholder: 'billToStateFullName',
			maxLength: 100,
			textStyle: {},
		},
		billToPostalCode: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToPostalCode',
			label: 'billToPostalCode',
			placeholder: 'billToPostalCode',
			maxLength: 50,
			textStyle: {},
		},
		billToPostalCodeExt: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToPostalCodeExt',
			label: 'billToPostalCodeExt',
			placeholder: 'billToPostalCodeExt',
			maxLength: 50,
			textStyle: {},
		},
		billToCounty: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToCounty',
			label: 'billToCounty',
			placeholder: 'billToCounty',
			maxLength: 50,
			textStyle: {},
		},
		billToCountry: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToCountry',
			label: 'billToCountry',
			placeholder: 'billToCountry',
			maxLength: 100,
			textStyle: {},
		},
		billToEmail: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToEmail',
			label: 'billToEmail',
			placeholder: 'billToEmail',
			maxLength: 100,
			textStyle: {},
		},
		billToDaytimePhone: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToDaytimePhone',
			label: 'billToDaytimePhone',
			placeholder: 'billToDaytimePhone',
			maxLength: 50,
			textStyle: {},
		},
		billToNightPhone: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'invoiceHeaderInfo',
			name: 'billToNightPhone',
			label: 'billToNightPhone',
			placeholder: 'billToNightPhone',
			maxLength: 50,
			textStyle: {},
		},
		//#endregion UI control for InvoiceHeaderInfo



		//#region UI control for InvoiceHeaderAttributes
		//#endregion UI control for InvoiceHeaderAttributes



	},

    grid: {
		invoiceItems: invoiceItemsGrid,
    },
    modal: {},
    menu: {},
};
