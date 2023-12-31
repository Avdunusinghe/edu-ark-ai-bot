import { Component, Inject, Input } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { SubjectTeacherModel } from 'src/app/core/models/subject-teacher/subject.teacher.model';
import { SubjectDetailsModel } from 'src/app/core/models/subject/subject.detail.model';
import { DataComunicationService } from 'src/app/core/service/data-comunication.service';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { SubjectService } from 'src/app/core/service/subject.service';

@Component({
	selector: 'app-subject-teacher-detail',
	templateUrl: './subject-teacher-detail.component.html',
	styleUrls: ['./subject-teacher-detail.component.sass'],
	providers: [ConfirmationService, DialogService],
})
export class SubjectTeacherDetailComponent {
	@Input() data: any;

	//core data properties
	subjectTeacherForm: UntypedFormGroup;
	subjectTeacherDetail: SubjectTeacherModel;

	//core data properties
	currentAcadmicYearId: number = 0;
	academicLevels: DropDownModel[] = [];
	academicYears: DropDownModel[] = [];
	teachers: DropDownModel[] = [];
	subjects: DropDownModel[] = [];

	/**
	 * Constructor
	 * @param {UntypedFormBuilder} _untypedFormBuilder
	 * @param {Router} _router
	 * @param {MasterDataService} _masterDataService
	 * @param {NgxSpinnerService} _spinner
	 * @param {SpinnerMessageService} _spinnerMessageService
	 * @param {ConfirmationService} _confirmationService
	 * @param {ToastrService} _toastr
	 * @param {SubjectService} _subjectService
	 *
	 *
	 */

	constructor(
		private _untypedFormBuilder: UntypedFormBuilder,
		private _router: Router,
		private _spinner: NgxSpinnerService,
		private _spinnerMessageService: SpinnerMessageService,
		private _confirmationService: ConfirmationService,
		private _toastr: ToastrService,
		private _dialogService: DialogService,
		private _masterDataService: MasterDataService,
		private _dataCommunicationService: DataComunicationService,
		private _subjectService: SubjectService,
		private _dialogRef: DynamicDialogRef,
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
		await this.createSubjectTeacherForm();
	}

	async createSubjectTeacherForm() {
		try {
			this.subjectTeacherForm = this._untypedFormBuilder.group({
				id: [0, Validators.required],
				academicYearId: [0, Validators.required],
				academicLevelId: [null, Validators.required],
				subjectId: [null, Validators.required],
				assignedTeacherIds: [[], Validators.required],
			});

			this.subjectTeacherDetail = this.data.subjectTeacher;
			this.academicLevels = this.data.academicLevels;
			this.academicYears = this.data.academicYears;
			this.subjects = this.data.subjects;
			this.teachers = this.data.teachers;

			this.subjectTeacherForm.patchValue(this.data.subjectTeacher);
		} catch (error) {}
	}

	async saveSubjectTeacher() {
		try {
			console.log(this.subjectTeacherForm.getRawValue());

			let response = await this._subjectService.saveSubjectTeachers(this.subjectTeacherForm.getRawValue());
			if (response.succeeded) {
				this._toastr.success(response.successMessage, 'Success');
				this._spinner.hide();
				this._dialogRef.close();
				this._dialogConfig.data.reLoadSubjectTeacherList();
			} else {
				this._spinner.hide();
				response.errors.forEach((error: any) => {
					this._toastr.error(error, 'Error');
				});
			}
		} catch (error) {}
	}
}
