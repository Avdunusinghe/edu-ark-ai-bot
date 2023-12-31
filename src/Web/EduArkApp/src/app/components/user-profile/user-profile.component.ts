import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { UserService } from 'src/app/core/service/user.service';
import { AuthenticationResponseModel } from './../../core/models/authentication/authentication.response.model';
import { AuthenticationService } from 'src/app/core/service/authentication.service';
import { UserDetailsModel } from 'src/app/core/models/user/user.details.model';
import { emailValidator } from 'src/app/core/validators/app.validators';
import { EMPTY, Observable } from 'rxjs';
import { Upload } from './../../core/models/common/upload';

@Component({
	selector: 'app-user-profile',
	templateUrl: './user-profile.component.html',
	styleUrls: ['./user-profile.component.sass'],
	providers: [ConfirmationService],
})
export class UserProfileComponent {
	currentUser: AuthenticationResponseModel;
	currentUserDetails: UserDetailsModel;
	currentUserProfileEditForm: UntypedFormGroup;

	activeTabIndex = 0;
	/**
	 * Constructor
	 * @param {UntypedFormBuilder} _untypedFormBuilder
	 * @param {UserService} _userService
	 * @param {NgxSpinnerService} _spinner
	 * @param {SpinnerMessageService} _spinnerMessageService
	 * @param {ConfirmationService} _confirmationService
	 * @param {ToastrService} _toastr
	 * @param {AuthenticationService} _authenticationService
	 *
	 */

	constructor(
		private _untypedFormBuilder: UntypedFormBuilder,
		private _userService: UserService,
		private _spinner: NgxSpinnerService,
		private _spinnerMessageService: SpinnerMessageService,
		private _confirmationService: ConfirmationService,
		private _toastr: ToastrService,
		private _authenticationService: AuthenticationService
	) {
		this.currentUser = this._authenticationService.currentUserValue;
	}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		await this.createUserProfileEditForm();
	}

	async createUserProfileEditForm() {
		try {
			this.currentUserProfileEditForm = this._untypedFormBuilder.group({
				id: [0],
				firstName: ['', [Validators.required]],
				lastName: ['', [Validators.required]],
				userName: ['', [Validators.required]],
				email: ['', [Validators.required, emailValidator]],
				phoneNumber: ['', [Validators.required]],
				roles: [null, [Validators.required]],
			});

			await this.getUserByCurrentUserId();
		} catch (error) {}
	}

	async getUserByCurrentUserId() {
		try {
			this._spinnerMessageService.sendData('Loading user details...');
			this._spinner.show();
			this.currentUserDetails = await this._userService.getUserByCurrentUserId();
			this.currentUserProfileEditForm.patchValue(this.currentUserDetails);
			this._spinnerMessageService.sendData('Loading...');

			this._spinner.hide();
		} catch (error) {
			this._spinnerMessageService.sendData('Loading...');
			this._spinner.hide();
		}
	}

	async saveProfile() {}

	/**
	 * Method of get user profile image file upload
	 *
	 * @param {any} event
	 * @returns {Observable<Upload>}
	 */

	upload$: Observable<Upload> = EMPTY;
	precentage: any;

	onFileChange(event: any) {
		let file = event.srcElement;
		const formData = new FormData();

		formData.set('id', this.currentUserDetails.id.toString());

		if (file.files.length > 0) {
			this._spinnerMessageService.sendData('Uploading Profile Image..');
			this._spinner.show();

			for (let index = 0; index < file.files.length; index++) {
				formData.append('file', file.files[index], file.files[index].name);
			}
			console.log('====================================');
			console.log(formData);
			console.log('====================================');
			this._userService.uploadUserProfileImage(formData).subscribe(
				(response) => {
					this.precentage = response;

					if (response.state === 'DONE') {
						this._spinner.hide();
						this._spinnerMessageService.sendData('');
						this._toastr.success('Product Image has been uploaded successfully.');
						this.getUserByCurrentUserId();
					}
				},
				(error) => {
					console.log(error);

					this._spinner.hide();
					this._spinnerMessageService.sendData('');
					this._toastr.error('Network error has been occured. Please try again.');
				}
			);
		}
	}
}
