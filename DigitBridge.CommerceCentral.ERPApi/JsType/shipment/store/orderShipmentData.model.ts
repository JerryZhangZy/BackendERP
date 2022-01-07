


import { types, getRoot, destroy, SnapshotIn, cast } from "mobx-state-tree";
import { createModelActions } from '../../../store';
              
/**
 * data model of OrderShipmentCanceledItem 
 */ 
const OrderShipmentCanceledItem = types
	.model('OrderShipmentCanceledItem', {
		orderShipmentCanceledItemNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		channelNum: types.optional(types.number, 0),
		channelAccountNum: types.optional(types.number, 0),
		orderShipmentNum: types.optional(types.number, 0),
		channelOrderID: types.optional(types.string, ''),
		orderDCAssignmentLineNum: types.optional(types.number, 0),
		sku: types.optional(types.string, ''),
		canceledQty: types.optional(types.number, 0),
		cancelCode: types.optional(types.string, ''),
		cancelOtherReason: types.optional(types.string, ''),
		dBChannelOrderLineRowID: types.optional(types.string, ''),
		orderShipmentUuid: types.optional(types.string, ''),
		orderShipmentCanceledItemUuid: types.optional(types.string, ''),
		rowNum: types.optional(types.number, 0),
		salesOrderItemsUuid: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of OrderShipmentHeader 
 */ 
const OrderShipmentHeader = types
	.model('OrderShipmentHeader', {
		orderShipmentNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		channelNum: types.optional(types.number, 0),
		channelAccountNum: types.optional(types.number, 0),
		orderDCAssignmentNum: types.optional(types.number, 0),
		distributionCenterNum: types.optional(types.number, 0),
		centralOrderNum: types.optional(types.number, 0),
		channelOrderID: types.optional(types.string, ''),
		shipmentID: types.optional(types.string, ''),
		warehouseCode: types.optional(types.string, ''),
		shipmentType: types.optional(types.number, 0),
		shipmentReferenceID: types.optional(types.string, ''),
		shipmentDateUtc: types.optional(types.string, ''),
		shippingCarrier: types.optional(types.string, ''),
		shippingClass: types.optional(types.string, ''),
		shippingCost: types.optional(types.number, 0),
		mainTrackingNumber: types.optional(types.string, ''),
		mainReturnTrackingNumber: types.optional(types.string, ''),
		billOfLadingID: types.optional(types.string, ''),
		totalPackages: types.optional(types.number, 0),
		totalShippedQty: types.optional(types.number, 0),
		totalCanceledQty: types.optional(types.number, 0),
		totalWeight: types.optional(types.number, 0),
		totalVolume: types.optional(types.number, 0),
		weightUnit: types.optional(types.number, 0),
		lengthUnit: types.optional(types.number, 0),
		volumeUnit: types.optional(types.number, 0),
		shipmentStatus: types.optional(types.number, 0),
		dBChannelOrderHeaderRowID: types.optional(types.string, ''),
		processStatus: types.optional(types.number, 0),
		processDateUtc: types.optional(types.string, ''),
		orderShipmentUuid: types.optional(types.string, ''),
		rowNum: types.optional(types.number, 0),
		invoiceNumber: types.optional(types.string, ''),
		invoiceUuid: types.optional(types.string, ''),
		salesOrderUuid: types.optional(types.string, ''),
		orderNumber: types.optional(types.string, ''),
	})
	.actions((self) => {
		return createModelActions(self);
	});



              
/**
 * data model of OrderShipmentPackage 
 */ 
const OrderShipmentPackage = types
	.model('OrderShipmentPackage', {
		orderShipmentPackageNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		channelNum: types.optional(types.number, 0),
		channelAccountNum: types.optional(types.number, 0),
		orderShipmentNum: types.optional(types.number, 0),
		packageID: types.optional(types.string, ''),
		packageType: types.optional(types.number, 0),
		packagePatternNum: types.optional(types.number, 0),
		packageTrackingNumber: types.optional(types.string, ''),
		packageReturnTrackingNumber: types.optional(types.string, ''),
		packageWeight: types.optional(types.number, 0),
		packageLength: types.optional(types.number, 0),
		packageWidth: types.optional(types.number, 0),
		packageHeight: types.optional(types.number, 0),
		packageVolume: types.optional(types.number, 0),
		packageQty: types.optional(types.number, 0),
		parentPackageNum: types.optional(types.number, 0),
		hasChildPackage: types.optional(types.boolean, false),
		orderShipmentUuid: types.optional(types.string, ''),
		orderShipmentPackageUuid: types.optional(types.string, ''),
		rowNum: types.optional(types.number, 0),

			orderShipmentShippedItem: types.array(types.map(types.frozen())),
		})
		.actions((self) => {
			return createModelActions(self);
		});



              
/**
 * data model of OrderShipmentShippedItem 
 */ 
const OrderShipmentShippedItem = types
	.model('OrderShipmentShippedItem', {
		orderShipmentShippedItemNum: types.optional(types.number, 0),
		databaseNum: types.optional(types.number, 0),
		masterAccountNum: types.optional(types.number, 0),
		profileNum: types.optional(types.number, 0),
		channelNum: types.optional(types.number, 0),
		channelAccountNum: types.optional(types.number, 0),
		orderShipmentNum: types.optional(types.number, 0),
		orderShipmentPackageNum: types.optional(types.number, 0),
		channelOrderID: types.optional(types.string, ''),
		orderDCAssignmentLineNum: types.optional(types.number, 0),
		sku: types.optional(types.string, ''),
		shippedQty: types.optional(types.number, 0),
		dBChannelOrderLineRowID: types.optional(types.string, ''),
		orderShipmentUuid: types.optional(types.string, ''),
		orderShipmentPackageUuid: types.optional(types.string, ''),
		orderShipmentShippedItemUuid: types.optional(types.string, ''),
		rowNum: types.optional(types.number, 0),
		salesOrderItemsUuid: types.optional(types.string, ''),
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
		orderShipmentCanceledItem: types.optional(types.number, 0),
		orderShipmentPackage: types.optional(types.number, 0),
	})
	.actions((self) => {
		return createModelActions(self);
	});
const dataVersionInit = {
	data: 0,
	orderShipmentCanceledItem: 0,
	orderShipmentPackage: 0,
};

export const OrderShipmentDataModel = types
	.model('OrderShipmentData', {
		dataVersion: types.optional(DataVersion, dataVersionInit),
		orderShipmentHeader: types.optional(OrderShipmentHeader, {}),
		orderShipmentCanceledItem: types.array(OrderShipmentCanceledItem),
		orderShipmentPackage: types.array(OrderShipmentPackage),
	})
	.actions((self) => {
		return createModelActions(self);
	});



/**
 * init data of OrderShipmentData 
 */ 
export const orderShipmentDataInit = {
	dataVersion: dataVersionInit,
	orderShipmentHeader: {
		orderShipmentNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		channelNum: 0,
		channelAccountNum: 0,
		orderDCAssignmentNum: 0,
		distributionCenterNum: 0,
		centralOrderNum: 0,
		channelOrderID: '',
		shipmentID: '',
		warehouseCode: '',
		shipmentType: 0,
		shipmentReferenceID: '',
		shipmentDateUtc: '',
		shippingCarrier: '',
		shippingClass: '',
		shippingCost: 0,
		mainTrackingNumber: '',
		mainReturnTrackingNumber: '',
		billOfLadingID: '',
		totalPackages: 0,
		totalShippedQty: 0,
		totalCanceledQty: 0,
		totalWeight: 0,
		totalVolume: 0,
		weightUnit: 0,
		lengthUnit: 0,
		volumeUnit: 0,
		shipmentStatus: 0,
		dBChannelOrderHeaderRowID: '',
		processStatus: 0,
		processDateUtc: '',
		orderShipmentUuid: '',
		rowNum: 0,
		invoiceNumber: '',
		invoiceUuid: '',
		salesOrderUuid: '',
		orderNumber: '',



	},
	orderShipmentCanceledItem: [{
		orderShipmentCanceledItemNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		channelNum: 0,
		channelAccountNum: 0,
		orderShipmentNum: 0,
		channelOrderID: '',
		orderDCAssignmentLineNum: 0,
		sku: '',
		canceledQty: 0,
		cancelCode: '',
		cancelOtherReason: '',
		dBChannelOrderLineRowID: '',
		orderShipmentUuid: '',
		orderShipmentCanceledItemUuid: '',
		rowNum: 0,
		salesOrderItemsUuid: '',



	}], 
	orderShipmentPackage: [{
		orderShipmentPackageNum: 0,
		databaseNum: 0,
		masterAccountNum: 0,
		profileNum: 0,
		channelNum: 0,
		channelAccountNum: 0,
		orderShipmentNum: 0,
		packageID: '',
		packageType: 0,
		packagePatternNum: 0,
		packageTrackingNumber: '',
		packageReturnTrackingNumber: '',
		packageWeight: 0,
		packageLength: 0,
		packageWidth: 0,
		packageHeight: 0,
		packageVolume: 0,
		packageQty: 0,
		parentPackageNum: 0,
		hasChildPackage: false,
		orderShipmentUuid: '',
		orderShipmentPackageUuid: '',
		rowNum: 0,

			orderShipmentShippedItem: [],



		}], 
};



