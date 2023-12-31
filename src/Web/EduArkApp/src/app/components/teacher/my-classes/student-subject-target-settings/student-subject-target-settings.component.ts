import { Component, Input, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Column } from 'src/app/core/models/common/column';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { UserService } from 'src/app/core/service/user.service';
import { StudentTargetSettingDetailModel } from './../../../../core/models/student-target-settings/student.target.setting.detail.model';
import { StuddentTargetSettingFilterModel } from './../../../../core/models/student-target-settings/student.target.settings.filter.model';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { StudentTargetSettingService } from './../../../../core/service/student-target-setting.service';
import { SubjectTargetSettingService } from 'src/app/core/service/subject-target-setting.service';
import { ClassStudentProfileComponent } from '../class-student-profile/class-student-profile.component';
import { ReportService } from 'src/app/core/service/report.service';
import { HttpEventType } from '@angular/common/http';
import { formatDate } from '@angular/common';
import { TeacherTargetScoreModel } from 'src/app/core/models/student-target-settings/teacher.target.score.model';
import { TeacherTargetScoreContainerModel } from 'src/app/core/models/student-target-settings/teacher.target.score.container.model';

@Component({
	selector: 'app-student-subject-target-settings',
	templateUrl: './student-subject-target-settings.component.html',
	styleUrls: ['./student-subject-target-settings.component.sass'],
	providers: [ConfirmationService, DialogService],
})
export class StudentSubjectTargetSettingsComponent {
	//filter properties
	@Input() classNameId: number = 0;
	@Input() academicYearId: number = 0;
	@Input() academicLevelId: number = 0;
	@Input() subjectId: number = 0;
	@Input() isClassTeacher: boolean = false;

	semesterId: number = 0;
	currentAcadmicYearId: number = 0;
	searchText: string = '';

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

	//core data properties
	columns!: Column[];
	listOfStudentTargetSettings: StudentTargetSettingDetailModel[] = [];
	dialogRef: DynamicDialogRef | undefined;
	dialogData: any;

	percentage: any;
	progressBarVisible: boolean = false;

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
		private _reportService: ReportService
	) {}

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
			{ field: 'predictedMark', header: 'Predicted Mark' },
			{ field: 'teacherTaergetScore', header: 'Teacher Target Score' },
		];
	}

	async ngOnChanges(changes: SimpleChanges) {
		await this.getClassMasterData();
	}

	/**
	 * @param {LazyLoadEvent}
	 * @Method loadStudentTargetSettingsLazy
	 * @returns {Promise<void>}
	 */
	async loadStudentTargetSettingsLazy(event: LazyLoadEvent): Promise<void> {
		try {
			try {
				this.currentPage = event.first / event.rows;
				this.pageSize = event.rows;
				await this.getStudentTargetSettingsByFilter();
			} catch (error) {
				this._spinner.hide();
			}
		} catch (error) {}
	}

	/**
	 * @param {}
	 * @Method getTeacherClassesByFilter
	 * @returns {Promise<void>}
	 */

	async getStudentTargetSettingsByFilter(): Promise<void> {
		try {
			this._spinner.show();
			let filter = new StuddentTargetSettingFilterModel();
			filter.searchText = this.searchText ? '' : this.searchText;
			filter.classNameId = this.classNameId;
			filter.academicYearId = this.academicYearId;
			filter.academicLevelId = this.academicLevelId;
			filter.subjectId = this.subjectId;
			filter.semesterId = 3;
			filter.currentPage = this.currentPage;
			filter.pageSize = this.pageSize;

			let response = await this._subjectTargetSettingService.getStudentsTargetSettingsByFilter(filter);

			this.listOfStudentTargetSettings = response.items;
			this.totalRecordCount = response.totalCount;
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
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
			let defaultItem: DropDownModel = {
				id: 0,
				name: 'All',
			};
			this.academicLevels = response.academicLevels.filter((x) => x.id == this.academicLevelId);
			this.academicYears = response.academicYears.filter((x) => x.id == this.academicYearId);
			this.classCategories = response.classCategories;
			this.languageStreams = response.languageStreams;

			this.classNames = response.classNames.filter((x) => x.id == this.classNameId);
			this.subjects = response.subjects;
			this.subjectId = response.subjects[0].id;
			await this.getStudentTargetSettingsByFilter();
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	async filterStudentByName(event: any): Promise<void> {
		this.searchText = event;
		await this.getStudentTargetSettingsByFilter();
	}

	async viewStudentDetails(studentId: number): Promise<void> {
		try {
			this.dialogData = {
				studentId: studentId,
			};

			this.dialogRef = this._dialogService.open(ClassStudentProfileComponent, {
				header: `Class Student Profile`,
				data: this.dialogData,
				width: '80%',
				contentStyle: { overflow: 'auto' },
				baseZIndex: 10000,
				maximizable: true,
				showHeader: true,
				height: '80%',
			});
		} catch (error) {}
	}

	async downloadStudentTargetSettingReport(studentId: number): Promise<void> {
		try {
			this._spinner.show();
			this._spinnerMessageService.sendData('Generating report, please wait...');
			let filter = {
				studentId: studentId,
				classNameId: this.classNameId,
				academicYearId: this.academicYearId,
				academicLevelId: this.academicLevelId,
				subjectId: this.subjectId,
				semesterId: this.semesterId,
			};

			this._reportService.downloadStudentTargetSettingReport(filter).subscribe(
				(response) => {
					if (response.type === HttpEventType.DownloadProgress) {
						this.percentage = Math.round((100 * response.loaded) / response.total);
					}

					if (response.type === HttpEventType.Response) {
						if (response.status == 204) {
							this.progressBarVisible = false;
							this.percentage = 0;
							this._spinner.hide();
						} else {
							const objectUrl: string = URL.createObjectURL(response.body);
							const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;

							a.href = objectUrl;
							a.download = 'student-report' + this.dateTimeCovert(new Date()) + '.pdf';
							document.body.appendChild(a);
							a.click();

							document.body.removeChild(a);
							URL.revokeObjectURL(objectUrl);
							this.progressBarVisible = false;
							this.percentage = 0;
							this._spinnerMessageService.sendData('');
							this._spinnerMessageService.sendData('Loading...');
							this._spinner.hide();
						}
					}
				},
				(error) => {
					this._spinner.hide();
					this.progressBarVisible = false;
					this._spinnerMessageService.sendData('');
					this._spinnerMessageService.sendData('Loading...');
					this.percentage = 0;
				}
			);
		} catch (error) {}
	}

	dateTimeCovert(convertedDateTime: Date) {
		return formatDate(convertedDateTime, 'yyyy-MM-dd', 'en-US');
	}

	getSeverity(grade: string) {
		switch (grade) {
			case 'A+':
			case 'A':
				return 'success';
			case 'B':
				return 'info';
			case 'C+':
			case 'C':
				return 'warning';
			case 'F':
				return 'danger';
			default:
				return 'primary';
		}
	}

	async saveTeacherTargetScore(): Promise<void> {
		try {
			let teacherTargetScores = new TeacherTargetScoreContainerModel();

			this.listOfStudentTargetSettings.forEach((x) => {
				let teacherTargetScore = new TeacherTargetScoreModel();
				teacherTargetScore.id = x.id;
				teacherTargetScore.studentId = x.studentId;
				teacherTargetScore.teacherTargetScore = x.teacherTaergetScore;

				teacherTargetScores.teacherTargetScores.push(teacherTargetScore);
			});

			let response = await this._subjectTargetSettingService.saveTeacherTargetScore(teacherTargetScores);

			if (response.succeeded) {
				this._toastr.success(response.successMessage, 'Success');
				await this.getStudentTargetSettingsByFilter();
			} else {
				this._toastr.error(response.errors[0], 'Error');
			}
		} catch (error) {
			this._toastr.error('Error has been occurred please try again ', 'Error');
			this._spinner.hide();
		}
	}
}
