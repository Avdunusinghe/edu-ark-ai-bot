import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/core/service/authentication.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';

@Component({
	selector: 'app-sign-in',
	templateUrl: './sign-in.component.html',
	styleUrls: ['./sign-in.component.sass'],
})
export class SignInComponent {
	userAuthenticationForm: UntypedFormGroup;
	loginBtnText: string = 'Login';
	isLoading: boolean = false;

	/**
	 * Constructor
	 * @param {UntypedFormBuilder} _untypedFormBuilder
	 * @param {AuthenticationService} _authenticationService
	 * @param {Rotuter} _router
	 * @param {NgxSpinnerService} _spinner
	 * @param {SpinnerMessageService} _spinnerMessageService
	 * @param {ToastrService} _toastr
	 */

	constructor(
		private _untypedFormBuilder: UntypedFormBuilder,
		private _authenticationService: AuthenticationService,
		private _router: Router,
		private _spinner: NgxSpinnerService,
		private _spinnerMessageService: SpinnerMessageService,
		private _toastr: ToastrService
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
		this.userAuthenticationForm = this._untypedFormBuilder.group({
			domain: ['', [Validators.required]],
			userName: ['', [Validators.required]],
			password: ['', [Validators.required]],
		});
	}

	login() {
		this.loginBtnText = 'Authenticating...';
		this.isLoading = true;
		this._authenticationService.login(this.userAuthenticationForm.value).subscribe(
			(response) => {
				console.log(response);

				if (response.isLoginSuccess) {
					const roles = this._authenticationService.currentUserValue.roles;

					if (roles.includes('Admin')) {
						this._router.navigate(['/admin']);
					}
					if (roles.includes('Student')) {
						this._router.navigate(['/admin']);
					}
					if (roles.includes('Teacher')) {
						this._router.navigate(['/teacher']);
					}
				} else {
					this._spinner.hide();
					this._toastr.error(response.message, 'Error');
					this.loginBtnText = 'Login';
					this.isLoading = false;
				}
			},
			(error) => {
				this._toastr.error('Network Error Please Contact Admin', 'Error');
				this.loginBtnText = 'Login';
				this.isLoading = false;
				this._spinner.hide();
			}
		);
	}
}
