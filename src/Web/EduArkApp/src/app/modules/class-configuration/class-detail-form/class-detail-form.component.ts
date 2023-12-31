import { Component, Input } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { ClassService } from 'src/app/core/service/class.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { ClassMasterDataModel } from './../../../core/models/class/class.master.data.model';
import { ClassSubjectTeacherModel } from 'src/app/core/models/class/class.subject.teacher.model';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { ClassModel } from 'src/app/core/models/class/class.model';

@Component({
	selector: 'app-class-detail-form',
	templateUrl: './class-detail-form.component.html',
	styleUrls: ['./class-detail-form.component.scss'],
	providers: [ConfirmationService, DialogService],
})
export class ClassDetailFormComponent {
	//core data properties
	classMasterData: ClassMasterDataModel;
	classDetailForm: UntypedFormGroup;
	classSubjectTeachers: ClassSubjectTeacherModel[] = [];
	allClassSubjectTeacherSelected: boolean = false;

	//master data properties
	classNames: DropDownModel[] = [];
	academicLevels: DropDownModel[] = [];
	academicYears: DropDownModel[] = [];
	classCategories: DropDownModel[] = [];
	languageStreams: DropDownModel[] = [];
	allTeachers: DropDownModel[] = [];
	currentAcadmicYearId: number = 0;

	@Input() selectedClassAcademicLevelId: number = 0;
	@Input() selectedClassNameId: number = 0;
	@Input() selectedAcademicYearId: number = 0;
	@Input() isClassTeacher: boolean = false;

	classDetails: ClassModel;

	activeTabIndex = 0;

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
	 * @param {DialogService} _dialogService
	 * @param {ActivatedRoute} _route
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
		private _classService: ClassService,
		private _dialogService: DialogService,
		private _route: ActivatedRoute
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		await this.createClassDetailForm();
	}

	async createClassDetailForm(): Promise<void> {
		try {
			this.classDetailForm = this._untypedFormBuilder.group({
				academicYearId: [null, Validators.required],
				academicLevelId: [null, Validators.required],
				classNameId: [null, Validators.required],
				name: ['', Validators.required],
				classCategoryId: [null, Validators.required],
				languageStreamId: [null, Validators.required],
				classTeacherId: [null, Validators.required],
				classSubjectTeachers: [null],
			});

			await this.getClassMasterData();
		} catch (error) {}
	}

	/**
	 * @param {LazyLoadEvent}
	 * @Method getClassMasterData
	 * @returns {Promise<void>}
	 */
	async getClassMasterData(): Promise<void> {
		try {
			this._spinner.show();

			this.classMasterData = await this._classService.getClassMasterData();

			this.academicLevels = this.classMasterData.academicLevels;
			this.academicYears = this.classMasterData.academicYears;
			this.classCategories = this.classMasterData.classCategories;
			this.languageStreams = this.classMasterData.languageStreams;
			this.allTeachers = this.classMasterData.allTeachers;
			this.currentAcadmicYearId = this.classMasterData.currentAcademicYear;
			this.classNames = this.classMasterData.classNames;

			if (this.selectedAcademicYearId > 0 && this.selectedClassAcademicLevelId > 0 && this.selectedClassNameId > 0) {
				await this.getClassDetails();
			}
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	async onAcademicLevelChanged() {
		this._spinner.show();
		await this.getSubjectClassTeacherDetails();
		await this.configureClassName();
	}

	async getSubjectClassTeacherDetails() {
		try {
			this._spinner.show();
			let response = await this._classService.getClassSubjectsForSelectedAcademicLevel(
				this.academicYearId,
				this.academicLevelId
			);
			this.classSubjectTeachers = response;
			await this.checkAllClassSubjectTeacherSelected();

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}
	async onClassNameChanged() {
		await this.configureClassName();
	}

	async configureClassName() {
		let selectedAcademicLevel: string = '';
		let selectedClassName: string = '';

		for (let index = 0; index < this.academicLevels.length; index++) {
			if (this.academicLevels[index].id == this.academicLevelId) {
				selectedAcademicLevel = this.academicLevels[index].name;
			}
		}

		for (let index = 0; index < this.classNames.length; index++) {
			if (this.classNames[index].id == this.classNameId) {
				selectedClassName = this.classNames[index].name;
			}
		}

		this.classDetailForm.get('name').setValue(selectedAcademicLevel + ' ' + selectedClassName);
	}

	async onSubjectTeacherChange(item: any) {
		await this.checkAllClassSubjectTeacherSelected();
	}

	async checkAllClassSubjectTeacherSelected() {
		if (this.classSubjectTeachers.length <= 0) {
			this.allClassSubjectTeacherSelected = false;
			return;
		}

		for (let index = 0; index < this.classSubjectTeachers.length; index++) {
			if (!this.classSubjectTeachers[index].subjectTeacherId) {
				this.allClassSubjectTeacherSelected = false;
				return;
			}
		}

		this.allClassSubjectTeacherSelected = true;
	}

	async saveClassDetails() {
		try {
			this._spinner.show();
			var classModel = this.classDetailForm.getRawValue();
			classModel.classSubjectTeachers = this.classSubjectTeachers;

			let response = await this._classService.saveClass(classModel);
			if (response.succeeded) {
				this._spinner.hide();
				this._toastr.success(response.successMessage, 'Success');
				this._router.navigate(['/admin/class']);
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

	async getClassDetails() {
		try {
			this._spinner.show();
			let response = await this._classService.getClassDetails(
				this.selectedAcademicYearId,
				this.selectedClassAcademicLevelId,
				this.selectedClassNameId
			);

			this.classDetails = response;
			this.classDetailForm.patchValue(this.classDetails);
			this.classSubjectTeachers = this.classDetails.classSubjectTeachers;

			this.classDetailForm.disable();

			this._spinner.hide();
		} catch (error) {
			console.log(error);

			this._spinner.hide();
		}
	}

	//Getters

	// get AcademicYear Id
	get academicYearId(): number {
		return this.classDetailForm.get('academicYearId').value;
	}

	// get AcademicLevel Id
	get academicLevelId(): number {
		return this.classDetailForm.get('academicLevelId').value;
	}
	// get ClassNameId Id
	get classNameId(): number {
		return this.classDetailForm.get('classNameId').value;
	}
}
