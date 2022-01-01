import { ScreenType, ProcessMode } from '../../types/enums';
import { DbComponentEnums, Enums } from '../../types';
import { IconNames } from '../../components/icon';
import { btnSave, btnEdit, btnList, btnDelete } from '../default';
import * as util from '../../util';

export const customIOFormatUi = { 
    page: {
        screenType: ScreenType.PROCESSING,
        screenid: 1001,
        processMode: ProcessMode.LIST,
        processModeList: [Enums.ProcessMode.LIST],
        title: 'CustomIOFormat',
        subTitle: '',
        readonly: false,
    },
    header: {
        title: 'CustomIOFormat',
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
		//#region UI control for CustomIOFormat
		formatType: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'customIOFormat',
			name: 'formatType',
			label: 'formatType',
			placeholder: 'formatType',
			maxLength: 50,
			textStyle: {},
		},
		formatNumber: {
			type: DbComponentEnums.inputTypeEnum.number,
			parentName: 'customIOFormat',
			name: 'formatNumber',
			label: 'formatNumber',
			placeholder: 'formatNumber',
			format: 'number',
			textStyle: {},
		},
		formatName: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'customIOFormat',
			name: 'formatName',
			label: 'formatName',
			placeholder: 'formatName',
			maxLength: 50,
			textStyle: {},
		},
		description: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'customIOFormat',
			name: 'description',
			label: 'description',
			placeholder: 'description',
			maxLength: 200,
			textStyle: {},
		},
		formatObject: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'customIOFormat',
			name: 'formatObject',
			label: 'formatObject',
			placeholder: 'formatObject',
			textStyle: {},
		},
		updateDateUtc: {
			type: DbComponentEnums.inputTypeEnum.date,
			parentName: 'customIOFormat',
			name: 'updateDateUtc',
			label: 'updateDateUtc',
			placeholder: 'updateDateUtc',
			format: 'date',
			textStyle: {},
		},
		enterBy: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'customIOFormat',
			name: 'enterBy',
			label: 'enterBy',
			placeholder: 'enterBy',
			maxLength: 100,
			textStyle: {},
		},
		updateBy: {
			type: DbComponentEnums.inputTypeEnum.input,
			parentName: 'customIOFormat',
			name: 'updateBy',
			label: 'updateBy',
			placeholder: 'updateBy',
			maxLength: 100,
			textStyle: {},
		},
		//#endregion UI control for CustomIOFormat



	},

    grid: {
    },
    modal: {},
    menu: {},
};

