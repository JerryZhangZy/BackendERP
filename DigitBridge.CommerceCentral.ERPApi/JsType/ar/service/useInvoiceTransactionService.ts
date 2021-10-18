import * as util from '../../../util';
import { StoreMobx } from '../../../store';
import { InvoiceTransactionService } from './invoiceTransactionService';

const services: any = { current: {} };

export const useService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): InvoiceTransactionService | null => {
    if (!name) return getCurrent();
    if (!services[name] || !services[name][name]) {
        services[name] = createService(name, store, ui);
    }
    services.current = services[name];
    return services.current;
};

export const getCurrent = (): InvoiceTransactionService | null => {
    return (services.current.name) ? services.current as InvoiceTransactionService : null;
};

/**
* Create new service object
* @return {SalesOrderService} service object
*/
export const createService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): InvoiceTransactionService | null => {
    return new InvoiceTransactionService(name, store, ui);
};

