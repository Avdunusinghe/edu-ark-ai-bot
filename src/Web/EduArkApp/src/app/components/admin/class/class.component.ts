import { Component, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EMPTY, Observable, Subject } from 'rxjs';
import { ClassDetailModel } from 'src/app/core/models/class/class.detail.model';
import { ClassFilterModel } from 'src/app/core/models/class/class.filter.model';
import { ClassMasterDataModel } from 'src/app/core/models/class/class.master.data.model';
import { BaseAcademicMasterDataModel } from 'src/app/core/models/common/base.academic.master.data.model';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { Upload } from 'src/app/core/models/common/upload';
import { ClassService } from 'src/app/core/service/class.service';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { UserService } from 'src/app/core/service/user.service';

@Component({
	selector: 'app-class',
	templateUrl: './class.component.html',
	styleUrls: ['./class.component.sass'],
	providers: [ConfirmationService, DialogService],
})
export class ClassComponent {
	//filter properties
	name: string = '';
	academiclLevelId: number = 0;
	academicYearId: number = 0;

	//pagination meta data properties
	currentPage: number = 0;
	pageSize: number = 10;
	totalRecordCount: number = 0;

	//core data properties
	public static readonly DEFAULT_CLASS_ID: number = 0;

	listOfClasses: ClassDetailModel[] = [];
	dialogRef: DynamicDialogRef | undefined;
	dialogData: any;

	//master data properties
	academicLevels: DropDownModel[] = [];
	academicYears: DropDownModel[] = [];
	classCategories: DropDownModel[] = [];
	languageStreams: DropDownModel[] = [];
	allTeachers: DropDownModel[] = [];
	currentAcadmicYearId: number = 0;

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
		await this.getBaseAcademicMasterData();
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
			this.currentAcadmicYearId = response.currentAcademicYear;
			this.academicYears = response.academicYears.filter((x) => x.id == this.currentAcadmicYearId);
			let defaultItem: DropDownModel = {
				id: 0,
				name: '--All--',
			};
			this.academicLevels.unshift(defaultItem);
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {LazyLoadEvent}
	 * @Method loadClasses
	 * @returns {Promise<void>}
	 */
	async loadClasses(event: LazyLoadEvent): Promise<void> {
		try {
			this.currentPage = event.first / event.rows;
			this.pageSize = event.rows;
			await this.getClassesByFilter();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {}
	 * @Method getClassesByFilter
	 * @returns {Promise<void>}
	 */
	async getClassesByFilter(): Promise<void> {
		try {
			let classFilter = new ClassFilterModel();

			classFilter.name = this.name;
			classFilter.academiclLevelId = this.academiclLevelId;
			classFilter.academicYearId = this.currentAcadmicYearId;
			classFilter.currentPage = this.currentPage;
			classFilter.pageSize = this.pageSize;

			this._spinner.show();

			let response = await this._classService.getClassesByFilter(classFilter);

			this.listOfClasses = response.items;
			this.totalRecordCount = response.totalCount;

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	async editClass(classRowData: any): Promise<void> {
		this._router.navigate([
			'/admin/class/class-detail/',
			this.academicYearId,
			classRowData.academicLevelId,
			classRowData.classNameId,
		]);
	}

	async addClass(): Promise<void> {
		this._router.navigate(['/admin/class/class-detail']);
	}

	upload$: Observable<Upload> = EMPTY;
	precentage: any;
	progressBarVisible: boolean = false;
	onFileChange(event: any) {
		this.progressBarVisible = true;
		let file = event.srcElement;
		const formData = new FormData();
		//formData.set("id",this.quotationId.toString());

		if (file.files.length > 0) {
			for (let index = 0; index < file.files.length; index++) {
				formData.append('file', file.files[index], file.files[index].name);
			}

			this._spinner.show();
			this._userService.uploadClassStudents(formData).subscribe(
				(response) => {
					this.precentage = response;

					//progress
				},
				(error) => {
					this._spinner.hide();
				}
			);
		}
	}
}
