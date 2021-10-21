import * as util from '../../../util';
import { StoreMobx } from '../../../store';
import { InventoryManagerService } from './inventoryManagerService';

const services: any = { current: {} };

export const useService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): InventoryManagerService | null => {
    if (!name) return getCurrent();
    if (!services[name] || !services[name][name]) {
        services[name] = createService(name, store, ui);
    }
    services.current = services[name];
    return services.current;
};

export const getCurrent = (): InventoryManagerService | null => {
    return (services.current.name) ? services.current as InventoryManagerService : null;
};

/**
* Create new service object
* @return {SalesOrderService} service object
*/
export const createService = (name: string, store: StoreMobx | null, ui: StoreMobx | null): InventoryManagerService | null => {
    return new InventoryManagerService(name, store, ui);
};

