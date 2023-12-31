import { Component } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { SubjectDetailsModel } from './../../../core/models/subject/subject.detail.model';
import { SubjectService } from './../../../core/service/subject.service';
import { SubjectFilterModel } from 'src/app/core/models/subject/subject.filter.model';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { SubjectDetailComponent } from './subject-detail/subject-detail.component';
import { SubjectMasterDataModel } from 'src/app/core/models/subject/subject.master.data.model';

@Component({
	selector: 'app-subject',
	templateUrl: './subject.component.html',
	styleUrls: ['./subject.component.sass'],
	providers: [ConfirmationService, DialogService],
})
export class SubjectComponent {
	//filter properties
	name: string = '';
	selectedSubjectStreamId: number = 0;

	//pagination meta data properties
	currentPage: number = 0;
	pageSize: number = 10;
	totalRecordCount: number = 0;

	//master data properties
	subjectMasterData: SubjectMasterDataModel;
	subjectTypes: DropDownModel[] = [];
	parentBasketSubjects: DropDownModel[] = [];
	subjectCategories: DropDownModel[] = [];
	subjectStreams: DropDownModel[] = [];
	academicLevels: DropDownModel[] = [];

	//core data properties
	listOfSubjects: SubjectDetailsModel[] = [];
	dialogRef: DynamicDialogRef | undefined;
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
		private _untypedFormBuilder: UntypedFormBuilder,
		private _router: Router,
		private _masterDataService: MasterDataService,
		private _spinner: NgxSpinnerService,
		private _spinnerMessageService: SpinnerMessageService,
		private _confirmationService: ConfirmationService,
		private _toastr: ToastrService,
		private _subjectService: SubjectService,
		private _dialogService: DialogService
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		await this.getSubjectMasterData();
	}

	/**
	 * @param {LazyLoadEvent}
	 * @Method loadSubjects
	 * @returns {Promise<void>}
	 */
	async loadSubjects(event: LazyLoadEvent): Promise<void> {
		try {
			this.currentPage = event.first / event.rows;
			this.pageSize = event.rows;

			await this.getSubjectsByFilter();
		} catch (error) {}
	}

	/**
	 * @param {}
	 * @Method getSubjectsByFilter
	 * @returns {Promise<void>}
	 */
	async getSubjectsByFilter(): Promise<void> {
		try {
			this._spinner.show();
			let subjectFilter = new SubjectFilterModel();

			subjectFilter.name = this.name;
			subjectFilter.subjectStreamId = this.selectedSubjectStreamId === null ? 0 : this.selectedSubjectStreamId;
			subjectFilter.currentPage = this.currentPage;
			subjectFilter.pageSize = this.pageSize;

			let response = await this._subjectService.getSubjectsByFilter(subjectFilter);

			this.listOfSubjects = response.items;
			this.totalRecordCount = response.totalCount;

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {}
	 * @Method getSubjectMasterData
	 * @returns {Promise<void>}
	 */
	async getSubjectMasterData(): Promise<void> {
		try {
			this._spinner.show();
			let response = await this._masterDataService.getSubjectMasterData();

			this.subjectMasterData = response;
			this.subjectTypes = response.subjectTypes;
			this.parentBasketSubjects = response.parentBasketSubjects;
			this.subjectCategories = response.subjectCategories;
			this.subjectStreams = response.subjectStreams;
			this.academicLevels = response.academicLevels;

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {}
	 * @Method openSubjectFormModal
	 * @returns {Promise<void>}
	 */
	async openSubjectFormModal(): Promise<void> {
		try {
			this.dialogData = {
				subjectId: 0,
				subjectMasterData: this.subjectMasterData,
				reLoadSubjects: this.getSubjectsByFilter.bind(this),
			};

			this.dialogRef = this._dialogService.open(SubjectDetailComponent, {
				header: `Save Subject`,
				data: this.dialogData,
				width: '50%',
				contentStyle: { overflow: 'auto' },
				baseZIndex: 10000,
				maximizable: true,
				showHeader: true,
				height: '60%',
			});
		} catch (error) {}
	}

	/**
	 * @param {SubjectDetailsModel}
	 * @Method openSubjectEditFormModal
	 * @returns {Promise<void>}
	 */
	async openSubjectEditFormModal(subjectDetail: SubjectDetailsModel): Promise<void> {
		try {
			this.dialogData = {
				subjectId: subjectDetail.id,
				subjectDetail: subjectDetail,
				subjectMasterData: this.subjectMasterData,
				reLoadSubjects: this.getSubjectsByFilter.bind(this),
			};

			this.dialogRef = this._dialogService.open(SubjectDetailComponent, {
				header: `Edit Subject`,
				data: this.dialogData,
				width: '50%',
				contentStyle: { overflow: 'auto' },
				baseZIndex: 10000,
				maximizable: true,
				showHeader: true,
				height: '50%',
			});
		} catch (error) {}
	}

	/**
	 * @param {number}
	 * @Method deleteSubject
	 * @returns {Promise<void>}
	 */
	async deleteSubject(id: number): Promise<void> {
		try {
			this._confirmationService.confirm({
				message: 'Are you sure that you want to proceed?',
				header: 'Confirmation',
				icon: 'pi pi-exclamation-triangle',
				accept: async () => {
					this._spinner.show();
					let response = await this._subjectService.deleteSubject(id);
					if (response.succeeded) {
						this._toastr.success(response.successMessage, 'Success');
						await this.getSubjectsByFilter();
					} else {
						response.errors.forEach((error) => {
							this._toastr.error(error, 'Error');
						});
					}

					this._spinner.hide();
				},
				reject: () => {
					this._spinner.hide();
				},
			});
		} catch (error) {}
	}
}
