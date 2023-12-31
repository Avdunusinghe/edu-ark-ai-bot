import { Component } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ClassNameFilterModel } from 'src/app/core/models/class/class.name.filter.model';
import { ClassNameModel } from 'src/app/core/models/class/class.name.model';
import { ClassService } from 'src/app/core/service/class.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { ClassNameDetailComponent } from './class-name-detail/class-name-detail.component';

@Component({
	selector: 'app-class-name',
	templateUrl: './class-name.component.html',
	styleUrls: ['./class-name.component.sass'],
	providers: [ConfirmationService, DialogService],
})
export class ClassNameComponent {
	//filter properties
	name: string = '';

	//pagination meta data properties
	currentPage: number = 0;
	pageSize: number = 10;
	totalRecordCount: number = 0;

	//core data properties
	listOfClassNames: ClassNameModel[] = [];
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
		private _spinner: NgxSpinnerService,
		private _spinnerMessageService: SpinnerMessageService,
		private _confirmationService: ConfirmationService,
		private _toastr: ToastrService,
		private _classService: ClassService,
		private _dialogService: DialogService
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {}

	/**
	 * @param {LazyLoadEvent}
	 * @Method loadClassNames
	 * @returns {Promise<void>}
	 */
	async loadClassNames(event: LazyLoadEvent): Promise<void> {
		try {
			this.currentPage = event.first / event.rows;
			this.pageSize = event.rows;

			await this.getClassNamesByFilter();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {}
	 * @Method getClassNamesByFilter
	 * @returns {Promise<void>}
	 */
	async getClassNamesByFilter(): Promise<void> {
		try {
			let classNameFilter = new ClassNameFilterModel();

			classNameFilter.name = this.name;
			classNameFilter.currentPage = this.currentPage;
			classNameFilter.pageSize = this.pageSize;

			this._spinner.show();

			let response = await this._classService.getClassNamesByFilter(classNameFilter);

			this.listOfClassNames = response.items;
			this.totalRecordCount = response.totalCount;

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {}
	 * @Method openClassNameFormModal
	 * @returns {Promise<void>}
	 */
	async openClassNameFormModal(): Promise<void> {
		try {
			this.dialogData = {
				classNameId: 0,
				reLoadClassNames: this.getClassNamesByFilter.bind(this),
			};

			this.dialogRef = this._dialogService.open(ClassNameDetailComponent, {
				header: `Save Class Name`,
				data: this.dialogData,
				width: '50%',
				contentStyle: { overflow: 'auto' },
				baseZIndex: 10000,
				maximizable: true,
				showHeader: true,
				height: '60%',
			});
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {ClassNameModel}
	 * @Method openClassNameEditFormModal
	 * @returns {Promise<void>}
	 */
	async openClassNameEditFormModal(classNameDetail: ClassNameModel): Promise<void> {
		try {
			this.dialogData = {
				classNameId: classNameDetail.id,
				classNameDetail: classNameDetail,
				reLoadClassNames: this.getClassNamesByFilter.bind(this),
			};

			this.dialogRef = this._dialogService.open(ClassNameDetailComponent, {
				header: `Edit Class Name`,
				data: this.dialogData,
				width: '50%',
				contentStyle: { overflow: 'auto' },
				baseZIndex: 10000,
				maximizable: true,
				showHeader: true,
				height: '60%',
			});
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {number}
	 * @Method deleteClassName
	 * @returns {Promise<void>}
	 */
	async deleteClassName(classNameId: number): Promise<void> {
		try {
			this._confirmationService.confirm({
				message: 'Are you sure that you want to proceed?',
				header: 'Confirmation',
				icon: 'pi pi-exclamation-triangle',
				accept: async () => {
					this._spinner.show();
					let response = await this._classService.deleteClassName(classNameId);
					if (response.succeeded) {
						this._toastr.success(response.successMessage, 'Success');
						await this.getClassNamesByFilter();
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
		} catch (error) {
			this._spinner.hide();
		}
	}
}
