import { Component, Inject, Input } from '@angular/core';
import { UntypedFormArray, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { UserDetailsMasterDataFilterModel } from 'src/app/core/models/user/user.details.master.data.model';
import { AcademicLevelService } from 'src/app/core/service/academic-level.service';
import { MasterDataService } from 'src/app/core/service/master-data.service';

@Component({
	selector: 'app-academic-level-detail',
	templateUrl: './academic-level-detail.component.html',
	styleUrls: ['./academic-level-detail.component.sass'],
})
export class AcademicLevelDetailComponent {
	@Input() data: any;

	academicLevelDetailForm: UntypedFormGroup;
	levelHeads: DropDownModel[] = [];

	/**
	 * Constructor
	 * @param {DynamicDialogRef} _dialogRef
	 * @param {NgxSpinnerService} _spinner
	 * @param {UntypedFormBuilder} _untypedFormBuilder
	 * @param {MasterDataService} _masterDataService
	 * @param {AcademicLevelService} _academicLevelService
	 * @param {Router} _router
	 *
	 */
	constructor(
		private _dialogRef: DynamicDialogRef,
		private _spinner: NgxSpinnerService,
		private _untypedFormBuilder: UntypedFormBuilder,
		private _masterDataService: MasterDataService,
		private _academicLevelService: AcademicLevelService,
		private _router: Router,
		private _toastr: ToastrService,
		@Inject(DynamicDialogConfig) private _dialogConfig: DynamicDialogConfig
	) {
		this.data = this._dialogConfig.data;
	}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		await this.createAcademicLevelForm();
	}

	/**
	 * @param {any}
	 * @Method createAcademicLevelForm
	 * @returns {Promise<void>}
	 */
	async createAcademicLevelForm() {
		try {
			this.academicLevelDetailForm = this._untypedFormBuilder.group({
				id: [0],
				name: ['', [Validators.required]],
				levelHeadId: [0],
			});

			if (this.data.academicLevelId > 0) {
				let filter = new UserDetailsMasterDataFilterModel();

				filter.name = this.data.academicLevelData.levelHeadName;
				filter.roleId = 3;
				let response = await this._masterDataService.getUserDetailMasterDataByFilter(filter);

				this.levelHeads = response;
				this.academicLevelDetailForm.patchValue(this.data.academicLevelData);
			} else {
				await this.getLevelHeadsByFilter({ filter: '' });
			}
		} catch (error) {}
	}

	/**
	 * @param {any}
	 * @Method getLevelHeadsByFilter
	 * @returns {Promise<void>}
	 */
	/* async getLevelHeadsByFilter(event: any) {
		try {
			let name = event.filter === null ? '' : event.filter;

			name = name === undefined ? '' : name;
			let response = await this._masterDataService.getLevelHeadsByFilter(name);
			this.levelHeads = response;
		} catch (error) {}
	} */

	/**
	 * @param {any}
	 * @Method saveAcademicLevel
	 * @returns {Promise<void>}
	 */
	async saveAcademicLevel() {
		try {
			this._spinner.show();
			let response = await this._academicLevelService.saveAcademicLevel(this.academicLevelDetailForm.value);
			if (response.succeeded) {
				this._toastr.success(response.successMessage, 'Success');
				this._spinner.hide();
				this._dialogRef.close();
				this._dialogConfig.data.reLoadAcademicLevels();
			} else {
				this._spinner.hide();
				response.errors.forEach((error: any) => {
					this._toastr.error(error, 'Error');
				});
			}
		} catch (error) {}
	}
	/**
	 * @param {any}
	 * @Method getLevelHeadsByFilter
	 * @returns {Promise<void>}
	 */
	async getLevelHeadsByFilter(event: any) {
	
		let filter = new UserDetailsMasterDataFilterModel();

		filter.name = event.filter === null ? '' : event.filter;
		filter.roleId = 3;
		let response = await this._masterDataService.getUserDetailMasterDataByFilter(filter);

		this.levelHeads = response;
	}
}
