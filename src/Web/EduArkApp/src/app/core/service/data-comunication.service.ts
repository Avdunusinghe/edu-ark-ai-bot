import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root',
})
export class DataComunicationService {
	constructor() {}

	private objectStore: any;

	setObject(object: any) {
		this.objectStore = object;
	}

	getObject(): any {
		return this.objectStore;
	}
}
