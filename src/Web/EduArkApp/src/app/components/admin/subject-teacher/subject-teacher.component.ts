import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { SubjectTeacherFilterModel } from 'src/app/core/models/subject-teacher/subject.teacher.filter.model';
import { SubjectTeacherModel } from 'src/app/core/models/subject-teacher/subject.teacher.model';
import { ClassService } from 'src/app/core/service/class.service';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { SubjectService } from 'src/app/core/service/subject.service';
import { DataComunicationService } from './../../../core/service/data-comunication.service';
import { SubjectDetailComponent } from '../subject/subject-detail/subject-detail.component';
import { SubjectTeacherDetailComponent } from './subject-teacher-detail/subject-teacher-detail.component';

@Component({
	selector: 'app-subject-teacher',
	templateUrl: './subject-teacher.component.html',
	styleUrls: ['./subject-teacher.component.sass'],
	providers: [ConfirmationService, DialogService],
})
export class SubjectTeacherComponent {
	//filter properties
	name: string = '';
	academiclLevelId: number = 0;
	academicYearId: number = 0;

	//core data properties
	currentAcadmicYearId: number = 0;
	academicLevels: DropDownModel[] = [];
	academicYears: DropDownModel[] = [];
	subjects: DropDownModel[] = [];
	listOfSubjectTeachers: SubjectTeacherModel[] = [];
	ref: DynamicDialogRef | undefined;
	dialogData: any;

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
		private _router: Router,
		private _spinner: NgxSpinnerService,
		private _spinnerMessageService: SpinnerMessageService,
		private _toastr: ToastrService,
		private _classService: ClassService,
		//private _dialogService: DialogService,
		private _masterDataService: MasterDataService,
		private _subjectService: SubjectService,
		private _dataComunicationService: DataComunicationService,
		private _dialogService: DialogService
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit(): Promise<void> {
		this.getBaseAcademicMasterData();
	}

	/**
	 * @param {}
	 * @Method getBaseAcademicMasterData
	 * @returns {Promise<void>}
	 */
	async getBaseAcademicMasterData(): Promise<void> {
		try {
			this._spinner.show();

			let response = await this._masterDataService.getBaseAcademicMasterData();
			this.academicLevels = response.academicLevels;
			this.academicYears = response.academicYears;
			this.currentAcadmicYearId = response.currentAcademicYear;
			this.academiclLevelId = this.academicLevels[0].id;
			await this.getSubjectsForSelectedAcademicLevel();
			await this.getAllSubjectTeachersByFilter();
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {}
	 * @Method getAllSubjectTeachersByFilter
	 * @returns {Promise<void>}
	 */
	async getAllSubjectTeachersByFilter(): Promise<void> {
		try {
			this._spinner.show();
			let subjectTeacherFilter: SubjectTeacherFilterModel = {
				subjectName: this.name,
				academicLevelId: this.academiclLevelId,
				academicYearId: this.currentAcadmicYearId,
			};

			console.log('====================================');
			console.log(subjectTeacherFilter);
			console.log('====================================');
			let response = await this._subjectService.getAllSubjectTeachersByFilter(subjectTeacherFilter);
			this.listOfSubjectTeachers = response;
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {SubjectTeacherModel}
	 * @Method setSubjectTeacherDetails
	 * @returns {Promise<void>}
	 */
	async setSubjectTeacherDetails(subjectTeacher: SubjectTeacherModel): Promise<void> {
		try {
			this.dialogData = {
				subjectTeacher: subjectTeacher,
				reLoadSubjectTeacherList: this.getAllSubjectTeachersByFilter.bind(this),
				currentAcadmicYearId: this.currentAcadmicYearId,
				academicLevels: this.academicLevels,
				academicYears: this.academicYears,
				subjects: this.subjects,
				teachers: subjectTeacher.allTeachers,
			};

			this.ref = this._dialogService.open(SubjectTeacherDetailComponent, {
				header: `Subject Teacher Details`,
				data: this.dialogData,
				width: '50%',
				contentStyle: { overflow: 'auto' },
				baseZIndex: 10000,
				maximizable: true,
				showHeader: true,
				height: '80%',
			});
		} catch (error) {
			console.log(error);
		}
	}

	/**
	 * @param {}
	 * @Method getSubjectsForSelectedAcademicLevel
	 * @returns {Promise<void>}
	 */
	async getSubjectsForSelectedAcademicLevel(): Promise<void> {
		try {
			this._spinner.show();
			let response = await this._masterDataService.getSubjectMasterDataByAcademicLevelId(this.academiclLevelId);
			this.subjects = response;
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}
}
