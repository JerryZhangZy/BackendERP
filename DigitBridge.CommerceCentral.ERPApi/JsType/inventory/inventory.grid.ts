import { DbComponentEnums } from '../../types';
import { IconNames } from '../../components/icon';
import * as util from '../../util';

//#region all grid columns for Inventory
/** copy column's define object to gridColumns will display column in screen.
const allColumns = [
	{
		id: 'rid',
		name: 'rid',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.hiden,
		defaultFlex: 1,
		systemHide: true,
	},
	{
		id: 'rno',
		name: 'rno',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.rowNo,
		header: '#',
		align: DbComponentEnums.alignEnum.center,
		defaultFlex: 1,
		textStyle: {},
		sortable: false,
		hide: true,
	},
	{
		id: 'styleCode',
		name: 'styleCode',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'styleCode',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'colorPatternCode',
		name: 'colorPatternCode',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'colorPatternCode',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'sizeType',
		name: 'sizeType',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'sizeType',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'sizeCode',
		name: 'sizeCode',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'sizeCode',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'widthCode',
		name: 'widthCode',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'widthCode',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'lengthCode',
		name: 'lengthCode',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'lengthCode',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'priceRule',
		name: 'priceRule',
		parentName: 'inventory',
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
		id: 'leadTimeDay',
		name: 'leadTimeDay',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'leadTimeDay',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'number',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'poSize',
		name: 'poSize',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'poSize',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'minStock',
		name: 'minStock',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'minStock',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'sku',
		name: 'sku',
		parentName: 'inventory',
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
		parentName: 'inventory',
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
		id: 'warehouseName',
		name: 'warehouseName',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'warehouseName',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'lotNum',
		name: 'lotNum',
		parentName: 'inventory',
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
		id: 'lotInDate',
		name: 'lotInDate',
		parentName: 'inventory',
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
		parentName: 'inventory',
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
		id: 'lotDescription',
		name: 'lotDescription',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'lotDescription',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'lpnNum',
		name: 'lpnNum',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'lpnNum',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'lpnDescription',
		name: 'lpnDescription',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'lpnDescription',
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
		parentName: 'inventory',
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
		parentName: 'inventory',
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
		parentName: 'inventory',
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
		id: 'qtyPerPallot',
		name: 'qtyPerPallot',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'qtyPerPallot',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'qtyPerCase',
		name: 'qtyPerCase',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'qtyPerCase',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'qtyPerBox',
		name: 'qtyPerBox',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'qtyPerBox',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'packType',
		name: 'packType',
		parentName: 'inventory',
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
		parentName: 'inventory',
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
		id: 'defaultPackType',
		name: 'defaultPackType',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'defaultPackType',
		defaultFlex: 2,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'instock',
		name: 'instock',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'instock',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'onHand',
		name: 'onHand',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'onHand',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'decimalNumber',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'openSoQty',
		name: 'openSoQty',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'openSoQty',
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
		id: 'openFulfillmentQty',
		name: 'openFulfillmentQty',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'openFulfillmentQty',
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
		id: 'avaQty',
		name: 'avaQty',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'avaQty',
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
		id: 'openPoQty',
		name: 'openPoQty',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'openPoQty',
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
		id: 'openInTransitQty',
		name: 'openInTransitQty',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'openInTransitQty',
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
		id: 'openWipQty',
		name: 'openWipQty',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'openWipQty',
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
		id: 'projectedQty',
		name: 'projectedQty',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'projectedQty',
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
		id: 'baseCost',
		name: 'baseCost',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'baseCost',
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
		id: 'taxRate',
		name: 'taxRate',
		parentName: 'inventory',
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
		parentName: 'inventory',
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
		parentName: 'inventory',
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
		id: 'miscAmount',
		name: 'miscAmount',
		parentName: 'inventory',
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
		id: 'chargeAndAllowanceAmount',
		name: 'chargeAndAllowanceAmount',
		parentName: 'inventory',
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
		id: 'unitCost',
		name: 'unitCost',
		parentName: 'inventory',
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
		parentName: 'inventory',
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
		id: 'salesCost',
		name: 'salesCost',
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'salesCost',
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
		id: 'updateDateUtc',
		name: 'updateDateUtc',
		parentName: 'inventory',
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
		parentName: 'inventory',
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
		parentName: 'inventory',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'updateBy',
		defaultFlex: 4,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
];
*/
//#endregion all grid columns for Inventory
// display columns in screen grid
const gridColumns: any[] = [
];

export const inventoryGrid = { 
	grid: {
        name: 'inventory',
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
	editer: {
	//#region grid editor for Inventory
	styleCode: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'styleCode',
		placeholder: 'styleCode',
		maxLength: 100,
		textStyle: {},
	},
	colorPatternCode: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'colorPatternCode',
		placeholder: 'colorPatternCode',
		maxLength: 50,
		textStyle: {},
	},
	sizeType: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'sizeType',
		placeholder: 'sizeType',
		maxLength: 50,
		textStyle: {},
	},
	sizeCode: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'sizeCode',
		placeholder: 'sizeCode',
		maxLength: 50,
		textStyle: {},
	},
	widthCode: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'widthCode',
		placeholder: 'widthCode',
		maxLength: 30,
		textStyle: {},
	},
	lengthCode: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'lengthCode',
		placeholder: 'lengthCode',
		maxLength: 30,
		textStyle: {},
	},
	priceRule: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'priceRule',
		placeholder: 'priceRule',
		maxLength: 50,
		textStyle: {},
	},
	leadTimeDay: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'leadTimeDay',
		placeholder: 'leadTimeDay',
		format: 'number',
		textStyle: {},
	},
	poSize: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'poSize',
		placeholder: 'poSize',
		format: 'decimalNumber',
		textStyle: {},
	},
	minStock: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'minStock',
		placeholder: 'minStock',
		format: 'decimalNumber',
		textStyle: {},
	},
	sku: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'sku',
		placeholder: 'sku',
		maxLength: 100,
		textStyle: {},
	},
	warehouseCode: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'warehouseCode',
		placeholder: 'warehouseCode',
		maxLength: 50,
		textStyle: {},
	},
	warehouseName: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'warehouseName',
		placeholder: 'warehouseName',
		maxLength: 200,
		textStyle: {},
	},
	lotNum: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'lotNum',
		placeholder: 'lotNum',
		maxLength: 100,
		textStyle: {},
	},
	lotInDate: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'inventory',
		name: 'lotInDate',
		placeholder: 'lotInDate',
		format: 'date',
		textStyle: {},
	},
	lotExpDate: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'inventory',
		name: 'lotExpDate',
		placeholder: 'lotExpDate',
		format: 'date',
		textStyle: {},
	},
	lotDescription: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'lotDescription',
		placeholder: 'lotDescription',
		maxLength: 200,
		textStyle: {},
	},
	lpnNum: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'lpnNum',
		placeholder: 'lpnNum',
		maxLength: 100,
		textStyle: {},
	},
	lpnDescription: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'lpnDescription',
		placeholder: 'lpnDescription',
		maxLength: 200,
		textStyle: {},
	},
	notes: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'notes',
		placeholder: 'notes',
		maxLength: 500,
		textStyle: {},
	},
	currency: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'currency',
		placeholder: 'currency',
		maxLength: 10,
		textStyle: {},
	},
	uom: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'uom',
		placeholder: 'uom',
		maxLength: 50,
		textStyle: {},
	},
	qtyPerPallot: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'qtyPerPallot',
		placeholder: 'qtyPerPallot',
		format: 'decimalNumber',
		textStyle: {},
	},
	qtyPerCase: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'qtyPerCase',
		placeholder: 'qtyPerCase',
		format: 'decimalNumber',
		textStyle: {},
	},
	qtyPerBox: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'qtyPerBox',
		placeholder: 'qtyPerBox',
		format: 'decimalNumber',
		textStyle: {},
	},
	packType: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'packType',
		placeholder: 'packType',
		maxLength: 50,
		textStyle: {},
	},
	packQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'packQty',
		placeholder: 'packQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	defaultPackType: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'defaultPackType',
		placeholder: 'defaultPackType',
		maxLength: 50,
		textStyle: {},
	},
	instock: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'instock',
		placeholder: 'instock',
		format: 'decimalNumber',
		textStyle: {},
	},
	onHand: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'onHand',
		placeholder: 'onHand',
		format: 'decimalNumber',
		textStyle: {},
	},
	openSoQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'openSoQty',
		placeholder: 'openSoQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	openFulfillmentQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'openFulfillmentQty',
		placeholder: 'openFulfillmentQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	avaQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'avaQty',
		placeholder: 'avaQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	openPoQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'openPoQty',
		placeholder: 'openPoQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	openInTransitQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'openInTransitQty',
		placeholder: 'openInTransitQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	openWipQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'openWipQty',
		placeholder: 'openWipQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	projectedQty: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'projectedQty',
		placeholder: 'projectedQty',
		format: 'qty',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	baseCost: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'baseCost',
		placeholder: 'baseCost',
		format: 'cost',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	taxRate: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'taxRate',
		placeholder: 'taxRate',
		format: 'taxRate',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	taxAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'taxAmount',
		placeholder: 'taxAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	shippingAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'shippingAmount',
		placeholder: 'shippingAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	miscAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'miscAmount',
		placeholder: 'miscAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	chargeAndAllowanceAmount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'chargeAndAllowanceAmount',
		placeholder: 'chargeAndAllowanceAmount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	unitCost: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'unitCost',
		placeholder: 'unitCost',
		format: 'cost',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	avgCost: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'avgCost',
		placeholder: 'avgCost',
		format: 'cost',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	salesCost: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'inventory',
		name: 'salesCost',
		placeholder: 'salesCost',
		format: 'cost',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	updateDateUtc: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'inventory',
		name: 'updateDateUtc',
		placeholder: 'updateDateUtc',
		format: 'date',
		textStyle: {},
	},
	enterBy: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'enterBy',
		placeholder: 'enterBy',
		maxLength: 100,
		textStyle: {},
	},
	updateBy: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'inventory',
		name: 'updateBy',
		placeholder: 'updateBy',
		maxLength: 100,
		textStyle: {},
	},
	//#endregion grid editor for Inventory
	}
};


