import { Contact } from './contact';
import { KeyValuePair } from './keyValuePair';

export interface Vehicle {
    id: number;
    model: KeyValuePair;
    make: KeyValuePair;
    isRegistered: boolean;
    features: KeyValuePair[],
    contact: Contact,
    isDeleted: boolean;
    deletedOn: string,
    createdOn: string,
    modifiedOn: string,
}