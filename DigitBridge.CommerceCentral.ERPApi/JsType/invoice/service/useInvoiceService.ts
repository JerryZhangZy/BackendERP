import * as util from '../../../util';
import { StoreMobx } from '../../../store';
import { InvoiceService } from './invoiceService';

const services: any = { current: {} };

export const useService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): InvoiceService | null => {
    if (!name) return getCurrent();
    if (!services[name] || !services[name][name]) {
        services[name] = createService(name, store, ui);
    }
    services.current = services[name];
    return services.current;
};

export const getCurrent = (): InvoiceService | null => {
    return (services.current.name) ? services.current as InvoiceService : null;
};

/**
* Create new service object
* @return {SalesOrderService} service object
*/
export const createService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): InvoiceService | null => {
    return new InvoiceService(name, store, ui);
};
