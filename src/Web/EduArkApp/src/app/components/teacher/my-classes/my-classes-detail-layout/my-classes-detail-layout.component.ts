import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';

@Component({
	selector: 'app-my-classes-detail-layout',
	templateUrl: './my-classes-detail-layout.component.html',
	styleUrls: ['./my-classes-detail-layout.component.sass'],
})
export class MyClassesDetailLayoutComponent {
	selectedClassAcademicLevelId: number = 0;
	selectedClassNameId: number = 0;
	selectedAcademicYearId: number = 0;
	isClassTeacher: boolean = false;

	activeTabIndex = 0;

	/**
	 * Constructor
	 * @param {Router} _router
	 * @param {NgxSpinnerService} _spinner
	 * @param {SpinnerMessageService} _spinnerMessageService
	 * @param {ActivatedRoute} _route
	 *
	 */

	constructor(
		private _router: Router,
		private _spinner: NgxSpinnerService,
		private _spinnerMessageService: SpinnerMessageService,
		private _route: ActivatedRoute
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		this._route.params.subscribe((params: ParamMap) => {
			this.selectedAcademicYearId = params['academicYearId'];
			this.selectedClassAcademicLevelId = params['academicLevelId'];
			this.selectedClassNameId = params['classNameId'];
			this.isClassTeacher = params['isClassTeacher'];
		});
	}
}
