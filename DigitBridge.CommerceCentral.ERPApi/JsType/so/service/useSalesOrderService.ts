import * as util from '../../../util';
import { StoreMobx } from '../../../store';
import { SalesOrderService } from './salesOrderService';

const services: any = { current: {} };

export const useService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): SalesOrderService | null => {
    if (!name) return getCurrent();
    if (!services[name] || !services[name][name]) {
        services[name] = createService(name, store, ui);
    }
    services.current = services[name];
    return services.current;
};

export const getCurrent = (): SalesOrderService | null => {
    return (services.current.name) ? services.current as SalesOrderService : null;
};

/**
* Create new service object
* @return {SalesOrderService} service object
*/
export const createService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): SalesOrderService | null => {
    return new SalesOrderService(name, store, ui);
};

