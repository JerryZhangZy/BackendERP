


import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of CustomIOFormat 
 */ 
const CustomIOFormat = types
	.model('CustomIOFormat', {
		rowNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		customIOFormatUuid: types.optional(types.string, ''),
		formatType: types.optional(types.string, ''),
		formatNumber: types.optional(types.number, 0),
		formatName: types.optional(types.string, ''),
		description: types.optional(types.string, ''),
		formatObject: types.optional(types.string, ''),
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
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
};

export const CustomIOFormatDataModel = types
	.model('CustomIOFormatData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		customIOFormat: types.optional(CustomIOFormat, {}),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of CustomIOFormatData 
 */ 
export const customIOFormatDataInit = {
	dataVersion: dataVersionInit,
	customIOFormat: {
		rowNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		customIOFormatUuid: '',
		formatType: '',
		formatNumber: 0,
		formatName: '',
		description: '',
		formatObject: '',
		updateDateUtc: '',
		enterBy: '',
		updateBy: '',



	},
};



