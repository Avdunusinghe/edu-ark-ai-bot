import { Component } from '@angular/core';
import { UserService } from './../../../core/service/user.service';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MasterDataService } from './../../../core/service/master-data.service';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserDetailsModel } from 'src/app/core/models/user/user.details.model';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { UserDetailsFilterModel } from 'src/app/core/models/user/user.details.filter.model';
import { ToastrService } from 'ngx-toastr';
import { emailValidator } from './../../../core/validators/app.validators';
import { AuthenticationService } from './../../../core/service/authentication.service';

@Component({
	selector: 'app-user',
	templateUrl: './user.component.html',
	styleUrls: ['./user.component.sass'],
	providers: [ConfirmationService],
})
export class UserComponent {
	//master data properties
	roles: DropDownModel[] = [];
	userActiveStatus: DropDownModel[] = [];

	//filter properties
	name: string = '';
	selectedUserRole: number = 0;
	selectedUserActiveStatus: number = 0;

	//pagination meta data properties
	currentPage: number = 0;
	pageSize: number = 10;
	totalRecordCount: number = 0;

	//core data properties
	listOfUsers: UserDetailsModel[] = [];
	userDetailForm: UntypedFormGroup;
	visibleUserDetailFormDialog: boolean = false;
	headerText: string;

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
		private _userService: UserService,
		private _router: Router,
		private _masterDataService: MasterDataService,
		private _spinner: NgxSpinnerService,
		private _spinnerMessageService: SpinnerMessageService,
		private _confirmationService: ConfirmationService,
		private _toastr: ToastrService,
		private _authService: AuthenticationService
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		await this.createUserAuthenticationForm();
	}

	async createUserAuthenticationForm() {
		this._spinner.show();
		try {
			this.userDetailForm = this._untypedFormBuilder.group({
				id: [0],
				firstName: ['', [Validators.required]],
				lastName: ['', [Validators.required]],
				userName: ['', [Validators.required]],
				email: ['', [Validators.required, emailValidator]],
				phoneNumber: ['', [Validators.required]],
				password: ['', [Validators.required]],
				roles: [null, [Validators.required]],
			});
			this._spinner.hide();
			await this.getUserMasterData();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {LazyLoadEvent}
	 * @service getUserMasterData
	 * @returns {Promise<void>}
	 */
	async loadUsers(event: LazyLoadEvent) {
		this._spinner.show();
		this.currentPage = event.first / event.rows;
		this.pageSize = event.rows;

		await this.getAllUsersByFilter();
	}

	/**
	 * @param {}
	 * @service getUserMasterData
	 * @returns {Promise<void>}
	 */
	async getUserMasterData() {
		try {
			this._spinner.show();
			let response = await this._masterDataService.getUserMasterData();
			let defaultDropDownModel: DropDownModel = {
				id: 0,
				name: '_All_',
			};

			this.roles = [defaultDropDownModel, ...response.roles];
			this.userActiveStatus = response.userActiveStatus;

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {}
	 * @service getAllUsersByFilter
	 * @returns {Promise<void>}
	 */
	async getAllUsersByFilter() {
		try {
			this._spinner.show();
			let filter = new UserDetailsFilterModel();

			filter.name = this.name;
			filter.selectedRole = this.selectedUserRole;
			filter.userActiveStatus = this.selectedUserActiveStatus;
			filter.currentPage = this.currentPage;
			filter.pageSize = this.pageSize;

			let response = await this._userService.getAllUsersByFilter(filter);
			this.listOfUsers = response.items;
			this.totalRecordCount = response.totalCount;

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {number}
	 * @service deleteUser
	 * @returns {Promise<void>}
	 */
	async deleteUser(id: number) {
		try {
			this._confirmationService.confirm({
				message: 'Are you sure that you want to proceed?',
				header: 'Confirmation',
				icon: 'pi pi-exclamation-triangle',
				accept: async () => {
					this._spinner.show();
					let response = await this._userService.deleteUser(id);
					if (response.succeeded) {
						this._toastr.success(response.successMessage, 'Success');
						await this.getAllUsersByFilter();
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

	/**
	 * @param {}
	 * @service saveUser
	 * @returns {Promise<void>}
	 */
	async saveUser() {
		try {
			this._spinner.show();
			let response = await this._userService.saveUser(this.userDetailForm.value);

			if (response.succeeded) {
				this._toastr.success(response.successMessage, 'Success');
				this.closeUserDetailFormDialog();

				await this.getAllUsersByFilter();
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
	 * @param {}
	 * @service openUserDetailFormDialog
	 * @returns {Promise<void>}
	 */
	async openUserDetailFormDialog() {
		try {
			this.visibleUserDetailFormDialog = true;
			this.headerText = 'Add New User';
		} catch (error) {}
	}

	/**
	 * @param {}
	 * @service openUserDetailEditDialog
	 * @returns {Promise<void>}
	 */
	async openUserDetailEditDialog(userDetails: UserDetailsModel) {
		try {
			this.userDetailForm.patchValue(userDetails);
			this.headerText = 'Edit User';
			this.visibleUserDetailFormDialog = true;
		} catch (error) {}
	}

	/**
	 * @param {}
	 * @service closeUserDetailFormDialog
	 * @returns {<void>}
	 */
	closeUserDetailFormDialog() {
		try {
			this.visibleUserDetailFormDialog = false;
			this.userDetailForm.reset();
			this.userDetailForm.get('id').setValue(0);
		} catch (error) {}
	}

	//getters

	get userId(): number {
		return this.userDetailForm.value.id;
	}
}
