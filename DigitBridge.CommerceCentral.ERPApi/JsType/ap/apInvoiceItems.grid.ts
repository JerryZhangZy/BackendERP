import { DbComponentEnums } from '../../types';
import { IconNames } from '../../components/icon';
import * as util from '../../util';

//#region all grid columns for ApInvoiceItems
/** copy column's define object to gridColumns will display column in screen.
const allColumns = [
	{
		id: 'rid',
		name: 'rid',
		parentName: 'apInvoiceItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.hiden,
		defaultFlex: 1,
		systemHide: true,
	},
	{
		id: 'rno',
		name: 'rno',
		parentName: 'apInvoiceItems',
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
		parentName: 'apInvoiceItems',
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
		id: 'apInvoiceItemType',
		name: 'apInvoiceItemType',
		parentName: 'apInvoiceItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'apInvoiceItemType',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'number',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'apInvoiceItemStatus',
		name: 'apInvoiceItemStatus',
		parentName: 'apInvoiceItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'apInvoiceItemStatus',
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
		parentName: 'apInvoiceItems',
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
		parentName: 'apInvoiceItems',
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
		id: 'apDistributionNum',
		name: 'apDistributionNum',
		parentName: 'apInvoiceItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'apDistributionNum',
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
		parentName: 'apInvoiceItems',
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
		parentName: 'apInvoiceItems',
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
		parentName: 'apInvoiceItems',
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
		id: 'amount',
		name: 'amount',
		parentName: 'apInvoiceItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'amount',
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
		id: 'isAp',
		name: 'isAp',
		parentName: 'apInvoiceItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'isAp',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.string,
		enableEdit: false,
		hide: true,
	},
	{
		id: 'creditAccount',
		name: 'creditAccount',
		parentName: 'apInvoiceItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'creditAccount',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'number',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'debitAccount',
		name: 'debitAccount',
		parentName: 'apInvoiceItems',
		columnType: DbComponentEnums.gridColumnTypeEnum.text,
		header: 'debitAccount',
		defaultFlex: 1,
		textStyle: {},
		sortable: true,
		sortType: DbComponentEnums.gridSortEnum.number,
		format: 'number',
		enableEdit: false,
		hide: true,
	},
	{
		id: 'updateDateUtc',
		name: 'updateDateUtc',
		parentName: 'apInvoiceItems',
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
		parentName: 'apInvoiceItems',
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
		parentName: 'apInvoiceItems',
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
//#endregion all grid columns for ApInvoiceItems
// display columns in screen grid
const gridColumns: any[] = [
];

export const apInvoiceItemsGrid = { 
	grid: {
        name: 'apInvoiceItems',
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
	//#region grid editor for ApInvoiceItems
	seq: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'apInvoiceItems',
		name: 'seq',
		placeholder: 'seq',
		format: 'number',
		textStyle: {},
	},
	apInvoiceItemType: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'apInvoiceItems',
		name: 'apInvoiceItemType',
		placeholder: 'apInvoiceItemType',
		format: 'number',
		textStyle: {},
	},
	apInvoiceItemStatus: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'apInvoiceItems',
		name: 'apInvoiceItemStatus',
		placeholder: 'apInvoiceItemStatus',
		format: 'number',
		textStyle: {},
	},
	itemDate: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'apInvoiceItems',
		name: 'itemDate',
		placeholder: 'itemDate',
		format: 'date',
		textStyle: {},
	},
	itemTime: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'apInvoiceItems',
		name: 'itemTime',
		placeholder: 'itemTime',
		format: 'time',
		textStyle: {},
	},
	apDistributionNum: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'apInvoiceItems',
		name: 'apDistributionNum',
		placeholder: 'apDistributionNum',
		maxLength: 100,
		textStyle: {},
	},
	description: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'apInvoiceItems',
		name: 'description',
		placeholder: 'description',
		maxLength: 200,
		textStyle: {},
	},
	notes: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'apInvoiceItems',
		name: 'notes',
		placeholder: 'notes',
		maxLength: 500,
		textStyle: {},
	},
	currency: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'apInvoiceItems',
		name: 'currency',
		placeholder: 'currency',
		maxLength: 10,
		textStyle: {},
	},
	amount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'apInvoiceItems',
		name: 'amount',
		placeholder: 'amount',
		format: 'amount',
		align: DbComponentEnums.alignEnum.right,
		textStyle: {},
	},
	isAp: {
		type: DbComponentEnums.inputTypeEnum.checkbox,
		parentName: 'apInvoiceItems',
		name: 'isAp',
		placeholder: 'isAp',
		textStyle: {},
	},
	creditAccount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'apInvoiceItems',
		name: 'creditAccount',
		placeholder: 'creditAccount',
		format: 'number',
		textStyle: {},
	},
	debitAccount: {
		type: DbComponentEnums.inputTypeEnum.number,
		parentName: 'apInvoiceItems',
		name: 'debitAccount',
		placeholder: 'debitAccount',
		format: 'number',
		textStyle: {},
	},
	updateDateUtc: {
		type: DbComponentEnums.inputTypeEnum.date,
		parentName: 'apInvoiceItems',
		name: 'updateDateUtc',
		placeholder: 'updateDateUtc',
		format: 'date',
		textStyle: {},
	},
	enterBy: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'apInvoiceItems',
		name: 'enterBy',
		placeholder: 'enterBy',
		maxLength: 100,
		textStyle: {},
	},
	updateBy: {
		type: DbComponentEnums.inputTypeEnum.input,
		parentName: 'apInvoiceItems',
		name: 'updateBy',
		placeholder: 'updateBy',
		maxLength: 100,
		textStyle: {},
	},
	//#endregion grid editor for ApInvoiceItems
	}
};


