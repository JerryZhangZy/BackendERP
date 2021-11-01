import { serviceBase } from '../../../service';
import { StoreMobx } from '../../../store';
import * as util from '../../../util';

/**
 * Class representing a MiscInvoiceService service object. 
 * @param {string} name - service name
 * @param {StoreMobx} store - data store object
 * @param {StoreMobx} ui - ui store object
*/
export class MiscInvoiceService extends serviceBase {

    constructor(name: string, store: StoreMobx | null, ui: StoreMobx | null) {
        super(name, store, ui);
    }
}

