import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class SpinnerMessageService {
	messageData$: BehaviorSubject<string> = new BehaviorSubject<string>('Loading..');

	constructor() {}

	sendData(term: any) {
		this.messageData$.next(term);
	}

	getData() {
		return this.messageData$.asObservable();
	}
}
