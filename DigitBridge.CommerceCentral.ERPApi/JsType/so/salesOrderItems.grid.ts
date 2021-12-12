import { DbComponentEnums } from '../../types';
import { IconNames } from '../../components/icon';
import * as util from '../../util';

//#region all grid columns for SalesOrderItems
/** copy column's define object to gridColumns will display column in screen.
const allColumns = [
	{
		id: 'rid',
		name: 'rid',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.hiden,
		defaultFlex: 1,
		systemHide: true,
	},
	{
		id: 'rno',
		name: 'rno',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.rowNo,
		header: '#',
		align: DbComponentEnums.alignEnum.center,
		defaultFlex: 1,
		textStyle: {},
		sortable: false,
		hide: true,
	},
	{
		id: 'seq',
		name: 'seq',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'seq',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'number',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'orderItemType',
		name: 'orderItemType',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'orderItemType',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'number',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'salesOrderItemstatus',
		name: 'salesOrderItemstatus',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'salesOrderItemstatus',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'number',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'itemDate',
		name: 'itemDate',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'itemDate',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.date,
		format: 'date',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'itemTime',
		name: 'itemTime',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'itemTime',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		format: 'time',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'shipDate',
		name: 'shipDate',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'shipDate',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.date,
		format: 'date',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'etaArrivalDate',
		name: 'etaArrivalDate',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'etaArrivalDate',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.date,
		format: 'date',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'earliestShipDate',
		name: 'earliestShipDate',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'earliestShipDate',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.date,
		format: 'date',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'latestShipDate',
		name: 'latestShipDate',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'latestShipDate',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.date,
		format: 'date',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'signatureFlag',
		name: 'signatureFlag',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'signatureFlag',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'sku',
		name: 'sku',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'sku',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'warehouseCode',
		name: 'warehouseCode',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'warehouseCode',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'lotNum',
		name: 'lotNum',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'lotNum',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'description',
		name: 'description',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'description',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'notes',
		name: 'notes',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'notes',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'currency',
		name: 'currency',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'currency',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'uom',
		name: 'uom',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'uom',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'packType',
		name: 'packType',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'packType',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'packQty',
		name: 'packQty',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'packQty',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'qty',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'orderPack',
		name: 'orderPack',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'orderPack',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'shipPack',
		name: 'shipPack',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'shipPack',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'cancelledPack',
		name: 'cancelledPack',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'cancelledPack',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'openPack',
		name: 'openPack',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'openPack',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'orderQty',
		name: 'orderQty',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'orderQty',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'qty',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'shipQty',
		name: 'shipQty',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'shipQty',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'qty',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'cancelledQty',
		name: 'cancelledQty',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'cancelledQty',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'qty',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'openQty',
		name: 'openQty',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'openQty',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'qty',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'priceRule',
		name: 'priceRule',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'priceRule',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'price',
		name: 'price',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'price',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'price',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'discountRate',
		name: 'discountRate',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'discountRate',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'rate',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'discountAmount',
		name: 'discountAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'discountAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'discountPrice',
		name: 'discountPrice',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'discountPrice',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'price',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'extAmount',
		name: 'extAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'extAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'taxableAmount',
		name: 'taxableAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'taxableAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'nonTaxableAmount',
		name: 'nonTaxableAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'nonTaxableAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'taxRate',
		name: 'taxRate',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'taxRate',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'taxRate',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'taxAmount',
		name: 'taxAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'taxAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'shippingAmount',
		name: 'shippingAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'shippingAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'shippingTaxAmount',
		name: 'shippingTaxAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'shippingTaxAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'miscAmount',
		name: 'miscAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'miscAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'miscTaxAmount',
		name: 'miscTaxAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'miscTaxAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'chargeAndAllowanceAmount',
		name: 'chargeAndAllowanceAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'chargeAndAllowanceAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'itemTotalAmount',
		name: 'itemTotalAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'itemTotalAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'shipAmount',
		name: 'shipAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'shipAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'cancelledAmount',
		name: 'cancelledAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'cancelledAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'openAmount',
		name: 'openAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'openAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'stockable',
		name: 'stockable',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'stockable',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'isAr',
		name: 'isAr',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'isAr',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'taxable',
		name: 'taxable',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'taxable',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'costable',
		name: 'costable',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'costable',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'isProfit',
		name: 'isProfit',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'isProfit',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'unitCost',
		name: 'unitCost',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'unitCost',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'cost',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'avgCost',
		name: 'avgCost',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'avgCost',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'cost',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'lotCost',
		name: 'lotCost',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'lotCost',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'cost',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'lotInDate',
		name: 'lotInDate',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'lotInDate',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.date,
		format: 'date',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'lotExpDate',
		name: 'lotExpDate',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'lotExpDate',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.date,
		format: 'date',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'dBChannelOrderLineRowID',
		name: 'dBChannelOrderLineRowID',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'dBChannelOrderLineRowID',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'updateDateUtc',
		name: 'updateDateUtc',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'updateDateUtc',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.date,
		format: 'date',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'enterBy',
		name: 'enterBy',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'enterBy',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'updateBy',
		name: 'updateBy',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'updateBy',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'orderDCAssignmentLineNum',
		name: 'orderDCAssignmentLineNum',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'orderDCAssignmentLineNum',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'number',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'commissionRate',
		name: 'commissionRate',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'commissionRate',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'rate',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'commissionAmount',
		name: 'commissionAmount',
		parentName: 'salesOrderItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'commissionAmount',
		align: DbComponentEnums.alignEnum.right,
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'amount',
		enableEdit: false,
		hide: true,
	},
];
*/
//#endregion all grid columns for SalesOrderItems
// display columns in screen grid
const gridColumns: any[] = [
];

export const salesOrderItemsGrid = { 
	grid: {
        name: 'salesOrderItems',
	    idProperty: 'rid',
        // className: '',
        // bgColor: '',
        // color: '',
        // width: '100%',
        // minWidth: null,
        height: 500,
        // minHeight: 500,

        // hide: false,
        // systemHide?: false,
        // readOnly?: false,
        // reverseScreenReadOnly?: false,
        // disabled?: false,

        // enableEip: true,
        // enableSort: true,
        // enableSelect: true,
        // enableInsert: true,
        // enableDelete: true,
        // showRowNo: true,
        // enableRowNoButton: true,      // Add row no as button. { name: `btnRowNo_${gridName}`, moreData: { gridName, rid } }

        columns: gridColumns,
	},
	editor: {
	//#region grid editor for SalesOrderItems
	seq: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'seq',
		placeholder: 'seq',
		format: 'number',
		textStyle: {},
	},
	orderItemType: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'orderItemType',
		placeholder: 'orderItemType',
		format: 'number',
		textStyle: {},
	},
	salesOrderItemstatus: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'salesOrderItemstatus',
		placeholder: 'salesOrderItemstatus',
		format: 'number',
		textStyle: {},
	},
	itemDate: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'salesOrderItems',
		name: 'itemDate',
		placeholder: 'itemDate',
		format: 'date',
		textStyle: {},
	},
	itemTime: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'itemTime',
		placeholder: 'itemTime',
		format: 'time',
		textStyle: {},
	},
	shipDate: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'salesOrderItems',
		name: 'shipDate',
		placeholder: 'shipDate',
		format: 'date',
		textStyle: {},
	},
	etaArrivalDate: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'salesOrderItems',
		name: 'etaArrivalDate',
		placeholder: 'etaArrivalDate',
		format: 'date',
		textStyle: {},
	},
	earliestShipDate: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'salesOrderItems',
		name: 'earliestShipDate',
		placeholder: 'earliestShipDate',
		format: 'date',
		textStyle: {},
	},
	latestShipDate: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'salesOrderItems',
		name: 'latestShipDate',
		placeholder: 'latestShipDate',
		format: 'date',
		textStyle: {},
	},
	signatureFlag: {
		type: DbComponentEnums.inputTypeEnum.checkbox,
		parentName: 'salesOrderItems',
		name: 'signatureFlag',
		placeholder: 'signatureFlag',
		textStyle: {},
	},
	sku: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'sku',
		placeholder: 'sku',
		maxLength: 100,
		textStyle: {},
	},
	warehouseCode: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'warehouseCode',
		placeholder: 'warehouseCode',
		maxLength: 50,
		textStyle: {},
	},
	lotNum: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'lotNum',
		placeholder: 'lotNum',
		maxLength: 100,
		textStyle: {},
	},
	description: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'description',
		placeholder: 'description',
		maxLength: 200,
		textStyle: {},
	},
	notes: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'notes',
		placeholder: 'notes',
		maxLength: 500,
		textStyle: {},
	},
	currency: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'currency',
		placeholder: 'currency',
		maxLength: 10,
		textStyle: {},
	},
	uom: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'uom',
		placeholder: 'uom',
		maxLength: 50,
		textStyle: {},
	},
	packType: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'packType',
		placeholder: 'packType',
		maxLength: 50,
		textStyle: {},
	},
	packQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'packQty',
		placeholder: 'packQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	orderPack: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'orderPack',
		placeholder: 'orderPack',
		format: 'decimalNumber',
		textStyle: {},
	},
	shipPack: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'shipPack',
		placeholder: 'shipPack',
		format: 'decimalNumber',
		textStyle: {},
	},
	cancelledPack: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'cancelledPack',
		placeholder: 'cancelledPack',
		format: 'decimalNumber',
		textStyle: {},
	},
	openPack: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'openPack',
		placeholder: 'openPack',
		format: 'decimalNumber',
		textStyle: {},
	},
	orderQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'orderQty',
		placeholder: 'orderQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	shipQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'shipQty',
		placeholder: 'shipQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	cancelledQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'cancelledQty',
		placeholder: 'cancelledQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	openQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'openQty',
		placeholder: 'openQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	priceRule: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'priceRule',
		placeholder: 'priceRule',
		maxLength: 50,
		textStyle: {},
	},
	price: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'price',
		placeholder: 'price',
		format: 'price',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	discountRate: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'discountRate',
		placeholder: 'discountRate',
		format: 'rate',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	discountAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'discountAmount',
		placeholder: 'discountAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	discountPrice: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'discountPrice',
		placeholder: 'discountPrice',
		format: 'price',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	extAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'extAmount',
		placeholder: 'extAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	taxableAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'taxableAmount',
		placeholder: 'taxableAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	nonTaxableAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'nonTaxableAmount',
		placeholder: 'nonTaxableAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	taxRate: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'taxRate',
		placeholder: 'taxRate',
		format: 'taxRate',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	taxAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'taxAmount',
		placeholder: 'taxAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	shippingAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'shippingAmount',
		placeholder: 'shippingAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	shippingTaxAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'shippingTaxAmount',
		placeholder: 'shippingTaxAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	miscAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'miscAmount',
		placeholder: 'miscAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	miscTaxAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'miscTaxAmount',
		placeholder: 'miscTaxAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	chargeAndAllowanceAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'chargeAndAllowanceAmount',
		placeholder: 'chargeAndAllowanceAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	itemTotalAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'itemTotalAmount',
		placeholder: 'itemTotalAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	shipAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'shipAmount',
		placeholder: 'shipAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	cancelledAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'cancelledAmount',
		placeholder: 'cancelledAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	openAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'openAmount',
		placeholder: 'openAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	stockable: {
		type: DbComponentEnums.inputTypeEnum.checkbox,
		parentName: 'salesOrderItems',
		name: 'stockable',
		placeholder: 'stockable',
		textStyle: {},
	},
	isAr: {
		type: DbComponentEnums.inputTypeEnum.checkbox,
		parentName: 'salesOrderItems',
		name: 'isAr',
		placeholder: 'isAr',
		textStyle: {},
	},
	taxable: {
		type: DbComponentEnums.inputTypeEnum.checkbox,
		parentName: 'salesOrderItems',
		name: 'taxable',
		placeholder: 'taxable',
		textStyle: {},
	},
	costable: {
		type: DbComponentEnums.inputTypeEnum.checkbox,
		parentName: 'salesOrderItems',
		name: 'costable',
		placeholder: 'costable',
		textStyle: {},
	},
	isProfit: {
		type: DbComponentEnums.inputTypeEnum.checkbox,
		parentName: 'salesOrderItems',
		name: 'isProfit',
		placeholder: 'isProfit',
		textStyle: {},
	},
	unitCost: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'unitCost',
		placeholder: 'unitCost',
		format: 'cost',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	avgCost: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'avgCost',
		placeholder: 'avgCost',
		format: 'cost',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	lotCost: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'lotCost',
		placeholder: 'lotCost',
		format: 'cost',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	lotInDate: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'salesOrderItems',
		name: 'lotInDate',
		placeholder: 'lotInDate',
		format: 'date',
		textStyle: {},
	},
	lotExpDate: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'salesOrderItems',
		name: 'lotExpDate',
		placeholder: 'lotExpDate',
		format: 'date',
		textStyle: {},
	},
	dBChannelOrderLineRowID: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'dBChannelOrderLineRowID',
		placeholder: 'dBChannelOrderLineRowID',
		maxLength: 50,
		textStyle: {},
	},
	updateDateUtc: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'salesOrderItems',
		name: 'updateDateUtc',
		placeholder: 'updateDateUtc',
		format: 'date',
		textStyle: {},
	},
	enterBy: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'enterBy',
		placeholder: 'enterBy',
		maxLength: 100,
		textStyle: {},
	},
	updateBy: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'salesOrderItems',
		name: 'updateBy',
		placeholder: 'updateBy',
		maxLength: 100,
		textStyle: {},
	},
	orderDCAssignmentLineNum: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'orderDCAssignmentLineNum',
		placeholder: 'orderDCAssignmentLineNum',
		format: 'number',
		textStyle: {},
	},
	commissionRate: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'commissionRate',
		placeholder: 'commissionRate',
		format: 'rate',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	commissionAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'salesOrderItems',
		name: 'commissionAmount',
		placeholder: 'commissionAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	//#endregion grid editor for SalesOrderItems
	}
};



