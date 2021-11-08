import * as util from '../../../util';
import { StoreMobx } from '../../../store';
import { setCurrentService, getService } from '../../../service';
import { VendorService } from './vendorService';

/**
* Set current service to current page service, 
* if current page service is not exist, create new page service. 
* @param {string} name - service object name
* @param {StoreMobx} store - screen data store object
* @param {StoreMobx} ui - screen ui design store object
* @return {VendorService} service object
*/
export const useService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): VendorService | null => {
    let service = getService(name) || createService(name, store, ui);
    setCurrentService(name, service);
    return service as VendorService;
};

/**
* Create new service object
* @return {VendorService} service object
*/
export const createService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): VendorService | null => {
    return new VendorService(name, store, ui);
};


