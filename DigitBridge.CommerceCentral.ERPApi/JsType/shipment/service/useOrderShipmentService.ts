import * as util from '../../../util';
import { StoreMobx } from '../../../store';
import { setCurrentService, getService } from '../../../service';
import { OrderShipmentService } from './orderShipmentService';

/**
* Set current service to current page service, 
* if current page service is not exist, create new page service. 
* @param {string} name - service object name
* @param {StoreMobx} store - screen data store object
* @param {StoreMobx} ui - screen ui design store object
* @return {OrderShipmentService} service object
*/
export const useService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): OrderShipmentService | null => {
    let service = getService(name) || createService(name, store, ui);
    setCurrentService(name, service);
    return service as OrderShipmentService;
};

/**
* Create new service object
* @return {OrderShipmentService} service object
*/
export const createService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): OrderShipmentService | null => {
    return new OrderShipmentService(name, store, ui);
};


