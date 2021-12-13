import { setCurrentStore } from '../../../store/dataStorePool';
import { WarehouseTransferDataModel, warehouseTransferDataInit } from './warehouseTransferData.model';
import { isObject, isEqual } from '../../../util';
import { createStoreMobx, StoreMobx } from '../../../store/StoreMobx';

const storeName = 'globalWarehouseTransferDataStore';
const store: StoreMobx = createStoreMobx(storeName, WarehouseTransferDataModel, warehouseTransferDataInit);

/**
* Get global store
* @param {string} name - if ignore then return current global store, otherwise will check store name is match
* @return {StoreMobx} global store
*/
export const getStore = (name?: string): StoreMobx | null => {
    if (!name) return store;
    return (!isObject(store) || !isEqual(store.name, name)) 
        ? null
        : store;
};

/**
* Set global current store
* @return {StoreMobx} global store
*/
export const useCurrentStore = (): StoreMobx | null => {
    setCurrentStore(storeName, store);
    return store;
};


