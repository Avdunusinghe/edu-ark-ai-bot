import { Component } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { MasterDataService } from './../../../core/service/master-data.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { ToastrService } from 'ngx-toastr';
import { TenantDetailModel } from './../../../core/models/tenant/tenant.detail.model';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { TenantService } from './../../../core/service/tenant.service';
import { TenantFilterModel } from 'src/app/core/models/tenant/tenant.filter.moel';

@Component({
	selector: 'app-tenant',
	templateUrl: './tenant.component.html',
	styleUrls: ['./tenant.component.scss'],
	providers: [ConfirmationService],
})
export class TenantComponent {
	//master data properties
	roles: DropDownModel[] = [];
	tenantTypeStatus: DropDownModel[] = [];

	//filter properties
	name: string = '';
	selectedTenantType: number = 0;
	selectedTenantActiveStatus: number = 0;

	//pagination meta data properties
	currentPage: number = 0;
	pageSize: number = 5;
	totalRecordCount: number = 0;

	//core data properties
	listOfTenants: TenantDetailModel[] = [];

	/**
	 * Constructor
	 * @param {UntypedFormBuilder} _untypedFormBuilder
	 * @param {UserService} _userService
	 * @param {Router} _router
	 * @param {MasterDataService} _masterDataService
	 * @param {SpinnerMessageService} _spinnerMessageService
	 * @param {NgxSpinnerService} _spinner
	 * @param {ConfirmationService} _confirmationService
	 * @param {ToastrService} _toastr
	 *
	 */

	constructor(
		private _untypedFormBuilder: UntypedFormBuilder,
		private _router: Router,
		private _masterDataService: MasterDataService,
		private _spinner: NgxSpinnerService,
		private _confirmationService: ConfirmationService,
		private _toastr: ToastrService,
		private _tentantService: TenantService
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		await this.getTenantMasterData();
	}

	/**
	 * @param {}
	 * @service getTenantsByFilter
	 * @returns {Promise<void>}
	 */
	async getTenantsByFilter() {
		try {
			this._spinner.show();
			let filter = new TenantFilterModel();

			filter.name = this.name;
			filter.tenantTypeStatus = this.selectedTenantType;
			filter.currentPage = this.currentPage;
			filter.pageSize = this.pageSize;

			let response = await this._tentantService.getTenantsByFilter(filter);

			this.listOfTenants = response.items;

			this.totalRecordCount = response.totalCount;
			console.log(response);

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {LazyLoadEvent}
	 * @service loadTenants
	 * @returns {Promise<void>}
	 */
	async loadTenants(event: LazyLoadEvent) {
		this._spinner.show();
		this.currentPage = event.first / event.rows;
		this.pageSize = event.rows;

		await this.getTenantsByFilter();
	}

	async getTenantMasterData() {
		try {
			this._spinner.show();
			let response = await this._masterDataService.getTenantMasterData();
			let defualtFilter: DropDownModel = {
				id: 0,
				name: '_All_',
			};

			this.tenantTypeStatus = response.tenantTypeStatus;

			this.tenantTypeStatus.unshift(defualtFilter);
			this.selectedTenantActiveStatus = this.tenantTypeStatus[0].id;

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	async routeTenantDetailsModule(number: number) {
		if (number == 0) {
			this._router.navigate(['/admin/tenant/tenant-detail']);
		} else {
			this._router.navigate(['/admin/tenant/tenant-detail/' + number]);
		}
	}

	async deleteTenant(id: number) {
		try {
			this._confirmationService.confirm({
				message: 'Are you sure that you want to proceed?',
				header: 'Confirmation',
				icon: 'pi pi-exclamation-triangle',
				accept: async () => {
					this._spinner.show();
					this._spinner.show();
					let response = await this._tentantService.deleteTenant(id);

					if (response.succeeded) {
						this._toastr.success(response.successMessage, 'Success');
					} else {
						this._toastr.error(response.errors[0], 'Error');
					}
					await this.getTenantsByFilter();
					this._spinner.hide();

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
