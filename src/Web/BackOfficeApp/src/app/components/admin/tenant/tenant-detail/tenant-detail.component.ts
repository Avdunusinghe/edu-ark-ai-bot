import { Component, OnInit } from '@angular/core';
import { AbstractControl, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, PrimeNGConfig } from 'primeng/api';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { ServerDetailsDropDownModel } from 'src/app/core/models/common/server.details.drop.down.model';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { TenantService } from 'src/app/core/service/tenant.service';
import { TenantDomainValidator } from './../../../../core/validators/tenant.domain.validator';

@Component({
	selector: 'app-tenant-detail',
	templateUrl: './tenant-detail.component.html',
	styleUrls: ['./tenant-detail.component.sass'],
	providers: [ConfirmationService],
})
export class TenantDetailComponent implements OnInit {
	tenantId = 0;
	tenantRegistrationForm: UntypedFormGroup;
	tenantTypeStatus: DropDownModel[] = [];
	smtpServerDetails: ServerDetailsDropDownModel[] = [];
	serverDetails: ServerDetailsDropDownModel[] = [];

	/**
	 * Constructor
	 *
	 * @param {UntypedFormBuilder} _untypedFormBuilder
	 * @param {NgxSpinnerService} _spinner
	 * @param {ToastrService} _toastr
	 * @param {ActivatedRoute} _route
	 * @param {ConfirmationService} _confirmationService
	 * @param {PrimeNGConfig} _primengConfig
	 * @param {Router} _router
	 * @param {TenantService} _tenantService
	 * @param {MasterDataService} _masterDataService
	 *
	 */

	constructor(
		private _untypedFormBuilder: UntypedFormBuilder,
		private _spinner: NgxSpinnerService,
		private _toastr: ToastrService,
		private _route: ActivatedRoute,
		private _confirmationService: ConfirmationService,
		private _primengConfig: PrimeNGConfig,
		private _router: Router,
		private _tenantService: TenantService,
		private _masterDataService: MasterDataService,
		private _tenantDomainValidator: TenantDomainValidator
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		this._route.paramMap.subscribe((params: ParamMap) => {
			this.tenantId = +(params.get('id') ?? 0);
		});

		this.getTenantMasterData();
	}

	// -----------------------------------------------------------------------------------------------------

	/**
	 * TenantDetailComponent
	 * @param {}
	 * @service getTenantMasterData
	 * @returns {Promise<void>}
	 */
	async getTenantMasterData(): Promise<void> {
		try {
			this._spinner.show();
			let response = await this._masterDataService.getTenantMasterData();
			this.serverDetails = response.serverDetails;
			this.tenantTypeStatus = response.tenantTypeStatus;

			this.smtpServerDetails.push({ id: 0, serverName: 'SendGrid', name: 'SendGrid' });
			this._spinner.hide();

			await this.createTenantRegistrationForm();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * TenantDetailComponent
	 * @param {}
	 * @service createTenantRegistrationForm
	 * @returns {Promise<void>}
	 */
	async createTenantRegistrationForm(): Promise<void> {
		try {
			this.tenantRegistrationForm = this._untypedFormBuilder.group({
				id: [0],
				name: ['', [Validators.required]],
				domain: ['', [Validators.required]],
				databaseServer: ['', [Validators.required]],
				connectionString: [''],
				sMTPServer: [''],
				sMTPUsername: [''],
				sMTPPort: [0],
				sMTPPassword: [''],
				sMTPFrom: [''],
				isSMTPUseSSL: [false, [Validators.required]],
				isGovernmentSchool: [false, [Validators.required]],
				specialNotes: [''],
			});

			if (this.tenantId > 0) {
				await this.getTenantById();
			}
		} catch (error) {}
	}

	/**
	 * TenantDetailComponent
	 * @param {}
	 * @service saveTenant
	 * @returns {Promise<void>}
	 */
	async saveTenant(): Promise<void> {
		try {
			this._spinner.show();
			let response = await this._tenantService.saveTenant(this.tenantRegistrationForm.getRawValue());

			if (response.succeeded) {
				this._toastr.success(response.successMessage, 'Success');
				this._router.navigate(['/admin/tenant']);
			} else {
				response.errors.forEach((error) => {
					this._toastr.error(error, 'Error');
				});
			}

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * TenantDetailComponent
	 * @param {}
	 * @service getTenantById
	 * @returns {Promise<void>}
	 */
	async getTenantById(): Promise<void> {
		try {
			this._spinner.show();
			let response = await this._tenantService.getTenantById(this.tenantId);
			this.tenantRegistrationForm.patchValue(response);
			this.tenantRegistrationForm.controls['databaseServer'].disable();
			this.tenantRegistrationForm.controls['domain'].disable();
			this.tenantRegistrationForm.controls['connectionString'].disable();
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * TenantDetailComponent
	 * @param {}
	 * @service validateTenantDomain
	 * @returns {Promise<void>}
	 */
	async validateTenantDomain() {
		try {
			if (this.domainFormControl.value) {
				let response = await this._tenantService.validateTenantDomain(this.domain);
				if (!response.succeeded) {
					this.domainFormControl.setErrors({ isExist: true, required: false });
				} else {
					this.domainFormControl.setErrors(null);
				}
			} else {
				this.domainFormControl.setErrors({ required: true });
			}
		} catch (error) {}
	}

	//getters
	get domain(): string {
		return this.tenantRegistrationForm.get('domain').value;
	}

	get domainFormControl(): AbstractControl {
		return this.tenantRegistrationForm.get('domain');
	}
}
