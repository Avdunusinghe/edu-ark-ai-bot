import { Component } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { ClassService } from 'src/app/core/service/class.service';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { UserService } from 'src/app/core/service/user.service';
import { Column } from './../../../core/models/common/column';
import { BasicTeacherClassModel } from './../../../core/models/class/basic.teacher.class.model';
import { TeacherClassFilterModel } from 'src/app/core/models/class/teacher.class.filter.model';

@Component({
	selector: 'app-my-classes',
	templateUrl: './my-classes.component.html',
	styleUrls: ['./my-classes.component.sass'],
	providers: [ConfirmationService, DialogService],
})
export class MyClassesComponent {
	//filter properties
	name: string = '';
	academiclLevelId: number = 0;
	academicYearId: number = 0;
	classNameId: number = 0;
	classCategoryId: number = 0;
	languageStreamId: number = 0;
	showMySubjectClasses: boolean = false;
	subjectId: number = 0;

	//pagination meta data properties
	currentPage: number = 0;
	pageSize: number = 10;
	totalRecordCount: number = 0;

	//master data properties
	classNames: DropDownModel[] = [];
	academicLevels: DropDownModel[] = [];
	academicYears: DropDownModel[] = [];
	classCategories: DropDownModel[] = [];
	languageStreams: DropDownModel[] = [];
	subjects: DropDownModel[] = [];
	currentAcadmicYearId: number = 0;

	//core data properties
	headerText: string = 'My Classes';
	columns!: Column[];
	listOfClasses: BasicTeacherClassModel[] = [];

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
		private _classService: ClassService,
		private _dialogService: DialogService,
		private _masterDataService: MasterDataService,
		private _userService: UserService
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		this.columns = [
			{ field: 'className', header: 'Class Name' },
			{ field: 'academicLevelName', header: 'Academic Level' },
			{ field: 'languageStreamName', header: 'Language Stream' },
			{ field: 'classCategoryName', header: 'Class Category' },
		];
		await this.getClassMasterData();
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

			this.academicLevels = response.academicLevels;
			this.academicYears = response.academicYears;
			this.classCategories = response.classCategories;
			this.languageStreams = response.languageStreams;

			this.classNames = response.classNames;
			this.subjects = response.subjects;
			this.subjectId = response.subjects[0].id;

			this.academicYears.unshift(defaultItem);
			this.academicYearId = response.currentAcademicYear;
			this.academicLevels.unshift(defaultItem);
			this.classCategories.unshift(defaultItem);
			this.languageStreams.unshift(defaultItem);

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {LazyLoadEvent}
	 * @Method loadClassesLazy
	 * @returns {Promise<void>}
	 */
	async loadClassesLazy(event: LazyLoadEvent): Promise<void> {
		try {
			try {
				this.currentPage = event.first / event.rows;
				this.pageSize = event.rows;
				await this.getTeacherClassesByFilter();
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
	async getTeacherClassesByFilter(): Promise<void> {
		try {
			this._spinner.show();
			let filter = new TeacherClassFilterModel();

			filter.name = this.name == '' ? '' : this.name;
			filter.academicLevelId = this.academiclLevelId;
			filter.academicYearId = this.academicYearId;
			filter.classNameId = this.classNameId;
			filter.classCategoryId = this.classCategoryId;
			filter.languageStreamId = this.languageStreamId;
			filter.subJectId = this.subjectId;
			filter.showMySubjectClasses = this.showMySubjectClasses;
			filter.currentPage = this.currentPage;
			filter.pageSize = this.pageSize;

			let response = await this._classService.getTeacherClassesByFilter(filter);

			this.listOfClasses = response.items;
			this.totalRecordCount = response.totalCount;
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {}
	 * @Method onShowMySubjectClassesChanged
	 * @returns {Promise<void>}
	 */
	async onShowMySubjectClassesChanged() {
		if (this.showMySubjectClasses) {
			this.headerText = 'My Subject Classes';
		} else {
			this.headerText = 'My Classes';
		}
		await this.getTeacherClassesByFilter();
	}

	/**
	 * @param {}
	 * @Method onFilterDropDownChanged
	 * @returns {Promise<void>}
	 */
	async onFilterDropDownChanged() {
		await this.getTeacherClassesByFilter();
	}

	/**
	 * @param {}
	 * @Method onFilterTextChanged
	 * @returns {Promise<void>}
	 */
	async onFilterTextChanged() {
		await this.getTeacherClassesByFilter();
	}

	async viewClassDetails(classRowData: any): Promise<void> {
		console.log(classRowData);
		let isClassTeacher = false;

		if (this.showMySubjectClasses) {
			isClassTeacher = false;
		} else {
			isClassTeacher = true;
		}

		this._router.navigate([
			'/teacher/my-classes/layout/',
			this.academicYearId,
			classRowData.academicLevelId,
			classRowData.classNameId,
			isClassTeacher,
		]);
	}
}
