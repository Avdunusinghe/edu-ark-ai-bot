import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { AcademicLevelDetailsModel } from './../../../core/models/academic-level/academic.level.details.model';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { AcademicLevelFilterModel } from './../../../core/models/academic-level/academic.level.filter.model';
import { AcademicLevelService } from './../../../core/service/academic-level.service';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AcademicLevelDetailComponent } from './academic-level-detail/academic-level-detail.component';
import { UserDetailsMasterDataFilterModel } from 'src/app/core/models/user/user.details.master.data.model';

@Component({
	selector: 'app-academic-level',
	templateUrl: './academic-level.component.html',
	styleUrls: ['./academic-level.component.sass'],
	providers: [ConfirmationService, DialogService],
})
export class AcademicLevelComponent {
	//pagination meta data properties
	currentPage: number = 0;
	pageSize: number = 10;
	totalRecordCount: number = 0;
	name: string = '';
	levelHeadId: number = 0;

	//core data properties
	listOfAcademicLevels: AcademicLevelDetailsModel[] = [];
	levelHeads: DropDownModel[] = [];
	ref: DynamicDialogRef | undefined;
	dialogData: any;

	/**
	 * Constructor
	 * @param {Router} _router
	 * @param {MasterDataService} _masterDataService
	 * @param {NgxSpinnerService} _spinner
	 * @param {SpinnerMessageService} _spinnerMessageService
	 * @param {ConfirmationService} _confirmationService
	 * @param {ToastrService} _toastr
	 * @param {AcademicLevelService} _academicLevelService
	 * @param {DialogService} _dialogService
	 *
	 *
	 */
	constructor(
		private _router: Router,
		private _masterDataService: MasterDataService,
		private _spinner: NgxSpinnerService,
		private _spinnerMessageService: SpinnerMessageService,
		private _confirmationService: ConfirmationService,
		private _toastr: ToastrService,
		private _academicLevelService: AcademicLevelService,
		private _dialogService: DialogService
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		await this.getLevelHeadsByFilter({ filter: '' });
	}

	/**
	 * @param {}
	 * @Method addNewAcademicLevel
	 * @returns {Promise<void>}
	 */
	async addNewAcademicLevel() {
		try {
			this.dialogData = {
				academicLevelId: 0,
				reLoadAcademicLevels: this.gellAllAcademicLevels.bind(this),
			};
			this.ref = this._dialogService.open(AcademicLevelDetailComponent, {
				header: `Add new academic level`,
				data: this.dialogData,
				width: '50%',
				contentStyle: { overflow: 'auto' },
				baseZIndex: 10000,
				maximizable: true,
				showHeader: true,
				height: '30%',
			});
		} catch (error) {}
	}

	/**
	 * @param {AcademicLevelDetailsModel}
	 * @Method editAcademicLevel
	 * @returns {Promise<void>}
	 */
	async editAcademicLevel(academicLevel: AcademicLevelDetailsModel) {
		try {
			this.dialogData = {
				academicLevelId: academicLevel.id,
				academicLevelData: academicLevel,
				reLoadAcademicLevels: this.gellAllAcademicLevels.bind(this),
			};

			this.ref = this._dialogService.open(AcademicLevelDetailComponent, {
				header: `Edit academic level`,
				data: this.dialogData,
				width: '50%',
				contentStyle: { overflow: 'auto' },
				baseZIndex: 10000,
				maximizable: true,
				showHeader: true,
				height: '30%',
			});
		} catch (error) {}
	}

	/**
	 * @param {number}
	 * @Method deleteAcademicLevel
	 * @returns {Promise<void>}
	 */
	async deleteAcademicLevel(id: number) {
		this._confirmationService.confirm({
			message: 'Are you sure that you want to proceed?',
			header: 'Confirmation',
			icon: 'pi pi-exclamation-triangle',
			accept: async () => {
				this._spinner.show();
				let response = await this._academicLevelService.deleteAcademicLevel(id);
				if (response.succeeded) {
					this._toastr.success(response.successMessage, 'Success');
					await this.gellAllAcademicLevels();
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
	}

	/**
	 * @param {LazyLoadEvent}
	 * @Method loadAcademicLevels
	 * @returns {Promise<void>}
	 */
	async loadAcademicLevels(event: LazyLoadEvent) {
		this._spinner.show();
		this.currentPage = event.first / event.rows;
		this.pageSize = event.rows;

		await this.gellAllAcademicLevels();
	}

	/**
	 * @param {}
	 * @Method gellAllAcademicLevels
	 * @returns {Promise<void>}
	 */
	async gellAllAcademicLevels() {
		try {
			this._spinner.show();
			let academicLevelFilter = new AcademicLevelFilterModel();

			academicLevelFilter.name = this.name ? this.name : '';
			academicLevelFilter.levelHeadId = this.levelHeadId ? this.levelHeadId : 0;
			academicLevelFilter.currentPage = this.currentPage;
			academicLevelFilter.pageSize = this.pageSize;

			let response = await this._academicLevelService.getAllAcademicLevels(academicLevelFilter);

			this.listOfAcademicLevels = response.items;
			this.totalRecordCount = response.totalCount;
			await this.getLevelHeadsByFilter({ event: '' });
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {any}
	 * @Method getLevelHeadsByFilter
	 * @returns {Promise<void>}
	 */
	async getLevelHeadsByFilter(event: any) {
		let filter = new UserDetailsMasterDataFilterModel();

		filter.name = event.filter === null ? '' : event.filter;
		filter.roleId = 3;
		let response = await this._masterDataService.getUserDetailMasterDataByFilter(filter);

		this.levelHeads = response;

		let defaultItem: DropDownModel = {
			id: 0,
			name: '--All--',
		};

		this.levelHeads.unshift(defaultItem);
	}
}
