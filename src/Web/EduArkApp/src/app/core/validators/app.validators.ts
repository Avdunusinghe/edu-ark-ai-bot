import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';

export function emailValidator(control: FormControl): { [key: string]: any } {
	var emailRegexp = /[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$/;
	if (control.value && !emailRegexp.test(control.value)) {
		return { invalidEmail: true };
	}
}

export function matchingPasswords(passwordKey: string, passwordConfirmationKey: string) {
	return (group: FormGroup) => {
		let password = group.controls[passwordKey];
		let passwordConfirmation = group.controls[passwordConfirmationKey];
		if (password.value !== passwordConfirmation.value) {
			return passwordConfirmation.setErrors({ mismatchedPasswords: true });
		}
	};
}

export function confirmedValidator(controlName: string, matchingControlName: string) {
	return (formGroup: FormGroup) => {
		const control = formGroup.controls[controlName];
		const matchingControl = formGroup.controls[matchingControlName];
		if (matchingControl.errors && !matchingControl.errors.confirmedValidator) {
			return;
		}
		if (control.value !== matchingControl.value) {
			matchingControl.setErrors({ confirmedValidator: true });
		} else {
			matchingControl.setErrors(null);
		}
	};
}
