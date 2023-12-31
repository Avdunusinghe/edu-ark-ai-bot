import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Column } from 'src/app/core/models/common/column';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { ExamMasterDataFilterModel } from 'src/app/core/models/exam/exam.master.data.filter.model';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { StudentTargetSettingService } from 'src/app/core/service/student-target-setting.service';
import { SubjectTargetSettingService } from 'src/app/core/service/subject-target-setting.service';
import { UserService } from 'src/app/core/service/user.service';
import { StudentMarksModel } from './../../../../core/models/exam/student.mark.model';
import { StudentExamMarksFilterModel } from './../../../../core/models/exam/student.exam.mark.filter';
import { ExamService } from './../../../../core/service/exam.service';
import { ExamMarkContainerModel } from './../../../../core/models/exam/exam.mark.container.model';

@Component({
	selector: 'app-student-exam-marks',
	templateUrl: './student-exam-marks.component.html',
	styleUrls: ['./student-exam-marks.component.sass'],
	providers: [ConfirmationService, DialogService],
})
export class StudentExamMarksComponent {
	@Input() classNameId: number = 0;
	@Input() academicYearId: number = 0;
	@Input() academicLevelId: number = 0;

	name: string = '';
	subjectId: number = 0;
	examTypeId: number = 0;
	semesterId: number = 0;
	examId: number = 0;
	//pagination meta data properties
	currentPage: number = 0;
	pageSize: number = 10;
	totalRecordCount: number = 0;

	//master data properties
	currentAcademicYear: number;
	academicYears: DropDownModel[] = [];
	academicLevels: DropDownModel[] = [];
	classNames: DropDownModel[] = [];
	classCategories: DropDownModel[] = [];
	languageStreams: DropDownModel[] = [];
	subjects: DropDownModel[] = [];
	semesters: DropDownModel[] = [];
	examTypes: DropDownModel[] = [];
	exams: DropDownModel[] = [];

	//core data properties
	columns!: Column[];
	listOfStudentMarks: StudentMarksModel[] = [];

	/**
	 * Constructor
	
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
		private _confirmationService: ConfirmationService,
		private _toastr: ToastrService,
		private _dialogService: DialogService,
		private _masterDataService: MasterDataService,
		private _userService: UserService,
		private _studentTargetSettingService: StudentTargetSettingService,
		private _subjectTargetSettingService: SubjectTargetSettingService,
		private _examService: ExamService
	) {
		this.getClassMasterData();
	}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		this.columns = [
			{ field: 'studentName', header: 'Student Name' },
			{ field: 'studentProfileImage', header: 'Profile' },
			{ field: 'marks', header: 'Mark' },
			{ field: 'grade', header: 'Grade' },
		];
	}

	/**
	 * @param {}
	 * @Method getClassMasterData
	 * @returns {Promise<void>}
	 */
	async getClassMasterData(): Promise<void> {
		try {
			this._spinner.show();

			let response = await this._masterDataService.getTeacherClassesMasterData();

			this.academicLevels = response.academicLevels.filter((x) => x.id == this.academicLevelId);
			this.academicYears = response.academicYears.filter((x) => x.id == this.academicYearId);
			this.classCategories = response.classCategories;
			this.languageStreams = response.languageStreams;
			this.classNames = response.classNames.filter((x) => x.id == this.classNameId);
			this.subjects = response.subjects;
			this.examTypes = response.examTypes;
			this.semesters = response.semesters;

			await this.getExamMasterData();

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	async getExamMasterData(): Promise<void> {
		try {
			this._spinner.show();
			let examFilter = new ExamMasterDataFilterModel();
			examFilter.academicYearId = this.academicYearId;
			examFilter.examTypeId = this.examTypeId;
			examFilter.semesterId = this.semesterId;
			let response = await this._masterDataService.getExamMasterData(examFilter);
			this.exams = response;

			//await this.getExamMarksByFilter();
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	async loadStudentMarksLazy(event: LazyLoadEvent): Promise<void> {
		try {
			this.currentPage = event.first / event.rows;
			this.pageSize = event.rows;

			await this.getExamMarksByFilter();
		} catch (error) {}
	}

	async getExamMarksByFilter(): Promise<void> {
		try {
			this._spinner.show();
			let studentExamMarksFilterModel = new StudentExamMarksFilterModel();

			studentExamMarksFilterModel.studentName = this.name ?? '';
			studentExamMarksFilterModel.academicYearId = this.academicYearId;
			studentExamMarksFilterModel.academicLevelId = this.academicLevelId;
			studentExamMarksFilterModel.classNameId = this.classNameId;
			studentExamMarksFilterModel.subjectId = this.subjectId;
			studentExamMarksFilterModel.examType = this.examTypeId;
			studentExamMarksFilterModel.semester = this.semesterId;
			studentExamMarksFilterModel.examId = this.examId;
			studentExamMarksFilterModel.currentPage = this.currentPage;
			studentExamMarksFilterModel.pageSize = this.pageSize;

			let response = await this._examService.getExamMarksByFilter(studentExamMarksFilterModel);

			this.listOfStudentMarks = response.items;
			this.totalRecordCount = response.totalCount;

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	async saveExamMarks(): Promise<void> {
		try {
			this._spinner.show();
			let examMarkContainerModel = new ExamMarkContainerModel();

			examMarkContainerModel.examId = this.examId;
			examMarkContainerModel.subjectId = this.subjectId;
			examMarkContainerModel.academicLevelId = this.academicLevelId;

			for (let item of this.listOfStudentMarks) {
				let studentMark = new StudentMarksModel();
				studentMark.id = item.id;
				studentMark.studentId = item.studentId;
				studentMark.marks = item.marks;

				examMarkContainerModel.studentMarks.push(studentMark);
			}

			let response = await this._examService.saveExamMarks(examMarkContainerModel);

			if (response.succeeded) {
				this._toastr.success(response.successMessage);
				await this.getExamMarksByFilter();
			} else {
				this._toastr.error(response.errors[0]);
				this._spinner.hide();
			}
		} catch (error) {
			this._spinner.hide();
			this._toastr.error('Error while saving exam marks. Please contact system administrator.');
		}
	}
}
