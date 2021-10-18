import * as util from '../../../util';
import { StoreMobx } from '../../../store';
import { PurchaseOrderService } from './purchaseOrderService';

const services: any = { current: {} };

export const useService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): PurchaseOrderService | null => {
    if (!name) return getCurrent();
    if (!services[name] || !services[name][name]) {
        services[name] = createService(name, store, ui);
    }
    services.current = services[name];
    return services.current;
};

export const getCurrent = (): PurchaseOrderService | null => {
    return (services.current.name) ? services.current as PurchaseOrderService : null;
};

/**
* Create new service object
* @return {SalesOrderService} service object
*/
export const createService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): PurchaseOrderService | null => {
    return new PurchaseOrderService(name, store, ui);
};

