import { Component, Inject, Input } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ClassService } from 'src/app/core/service/class.service';

@Component({
	selector: 'app-class-name-detail',
	templateUrl: './class-name-detail.component.html',
	styleUrls: ['./class-name-detail.component.sass'],
})
export class ClassNameDetailComponent {
	@Input() data: any;

	classNameDetailForm: UntypedFormGroup;
	/**
	 * Constructor
	 * @param {DynamicDialogRef} _dialogRef
	 * @param {NgxSpinnerService} _spinner
	 * @param {UntypedFormBuilder} _untypedFormBuilder
	 * @param {MasterDataService} _masterDataService
	 * @param {Router} _router
	 *
	 */
	constructor(
		private _dialogRef: DynamicDialogRef,
		private _spinner: NgxSpinnerService,
		private _untypedFormBuilder: UntypedFormBuilder,
		private _router: Router,
		private _toastr: ToastrService,
		private _classService: ClassService,
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
		await this.createClssName();
	}

	/**
	 * @param {}
	 * @Method createClssName
	 * @returns {Promise<void>}
	 */
	async createClssName(): Promise<void> {
		try {
			this.classNameDetailForm = this._untypedFormBuilder.group({
				id: [0],
				name: ['', [Validators.required]],
				description: ['', [Validators.required]],
			});

			if (this.data.classNameId > 0) {
				this.classNameDetailForm.patchValue(this.data.classNameDetail);
			}
		} catch (error) {}
	}

	async saveClassName(): Promise<void> {
		try {
			this._spinner.show();
			let response = await this._classService.saveClassName(this.classNameDetailForm.value);

			if (response.succeeded) {
				this._toastr.success(response.successMessage, 'Success');
				this._spinner.hide();
				this._dialogRef.close();
				this._dialogConfig.data.reLoadClassNames();
			} else {
				this._spinner.hide();
				response.errors.forEach((error: any) => {
					this._toastr.error(error, 'Error');
				});
			}
		} catch (error) {
			this._spinner.hide();
		}
	}
}
