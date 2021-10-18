import * as util from '../../../util';
import { StoreMobx } from '../../../store';
import { CustomerService } from './customerService';

const services: any = { current: {} };

export const useService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): CustomerService | null => {
    if (!name) return getCurrent();
    if (!services[name] || !services[name][name]) {
        services[name] = createService(name, store, ui);
    }
    services.current = services[name];
    return services.current;
};

export const getCurrent = (): CustomerService | null => {
    return (services.current.name) ? services.current as CustomerService : null;
};

/**
* Create new service object
* @return {SalesOrderService} service object
*/
export const createService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): CustomerService | null => {
    return new CustomerService(name, store, ui);
};

