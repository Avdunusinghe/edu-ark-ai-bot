import { Component, Inject, Input } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SubjectService } from 'src/app/core/service/subject.service';

@Component({
	selector: 'app-subject-detail',
	templateUrl: './subject-detail.component.html',
	styleUrls: ['./subject-detail.component.sass'],
})
export class SubjectDetailComponent {
	@Input() data: any;
	public static readonly PARENT_BASKET_SUBJECT_ID: number = 3;
	subjectDetailsForm: UntypedFormGroup;

	subjectTypes: DropDownModel[] = [];
	parentBasketSubjects: DropDownModel[] = [];
	subjectCategories: DropDownModel[] = [];
	subjectStreams: DropDownModel[] = [];
	academicLevels: DropDownModel[] = [];

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
		private _masterDataService: MasterDataService,
		private _router: Router,
		private _toastr: ToastrService,
		private _subjectService: SubjectService,
		@Inject(DynamicDialogConfig) private _dialogConfig: DynamicDialogConfig
	) {
		this.data = this._dialogConfig.data;
		this.subjectTypes = this.data.subjectMasterData.subjectTypes;
		this.parentBasketSubjects = this.data.subjectMasterData.parentBasketSubjects;
		this.subjectCategories = this.data.subjectMasterData.subjectCategories;
		this.subjectStreams = this.data.subjectMasterData.subjectStreams;
		this.academicLevels = this.data.subjectMasterData.academicLevels;
	}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		await this.createSubjectForm();
	}

	/**
	 * @param {}
	 * @Method createSubjectForm
	 * @returns {Promise<void>}
	 */
	async createSubjectForm(): Promise<void> {
		this.subjectDetailsForm = this._untypedFormBuilder.group({
			id: [0],
			name: ['', [Validators.required]],
			subjectStreamId: [null, [Validators.required]],
			subjectCategory: [null, [Validators.required]],
			subjectCode: ['', [Validators.required]],
			subjectType: [null, [Validators.required]],
			subjectAcademicLevels: [null, [Validators.required]],
			parentBasketSubjectId: [null],
		});

		if (this.data.subjectId > 0) {
			this.subjectDetailsForm.patchValue(this.data.subjectDetail);

			this.subjectDetailsForm.get('id').disable();
		}
		try {
		} catch (error) {}
	}

	/**
	 * @param {}
	 * @Method saveSubject
	 * @returns {Promise<void>}
	 */
	async saveSubject(): Promise<void> {
		try {
			this._spinner.show();
			console.log(this.subjectDetailsForm.getRawValue());

			let response = await this._subjectService.saveSubject(this.subjectDetailsForm.getRawValue());

			if (response.succeeded) {
				this._toastr.success(response.successMessage, 'Success');
				this._spinner.hide();
				this._dialogRef.close();
				this._dialogConfig.data.reLoadSubjects();
			} else {
				this._spinner.hide();
				response.errors.forEach((error: any) => {
					this._toastr.error(error, 'Error');
				});
			}
		} catch (error) {}
	}

	//getters

	get subjectType(): number {
		return this.subjectDetailsForm.get('subjectType').value;
	}

	get subjectId(): number {
		return this.subjectDetailsForm.get('id').value;
	}
}
