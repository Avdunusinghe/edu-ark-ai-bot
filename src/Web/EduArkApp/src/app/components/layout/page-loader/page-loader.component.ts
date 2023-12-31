import { Component, OnInit } from '@angular/core';
import { SpinnerMessageService } from './../../../core/service/spinner-message.service';
@Component({
	selector: 'app-page-loader',
	templateUrl: './page-loader.component.html',
	styleUrls: ['./page-loader.component.sass'],
})
export class PageLoaderComponent implements OnInit {
	messageData$: any = {};

	/**
	 * Constructor
	 *
	 * @param {SpinnerMessageService} _spinnerMessageService
	 *
	 */
	constructor(private _spinnerMessageService: SpinnerMessageService) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	ngOnInit() {
		this.messageData$ = this._spinnerMessageService.getData();
	}
}
