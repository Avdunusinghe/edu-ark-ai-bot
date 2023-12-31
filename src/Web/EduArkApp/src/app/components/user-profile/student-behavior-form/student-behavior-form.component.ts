import { Component, Input, SimpleChanges } from '@angular/core';
import { StudentAcademicBehaviorModel } from './../../../core/models/student/student.academic.behavior.model';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { ConfirmationService } from 'primeng/api';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/core/service/user.service';

@Component({
	selector: 'app-student-behavior-form',
	templateUrl: './student-behavior-form.component.html',
	styleUrls: ['./student-behavior-form.component.sass'],
})
export class StudentBehaviorFormComponent {
	@Input() studentId: number = 0;
	//core data properties
	studentAcademicBehaviorModel: StudentAcademicBehaviorModel;
	studyHours: number = 0;

	/**
	 * Constructor
	 * @param {NgxSpinnerService} _spinner
	 * @param {SpinnerMessageService} _spinnerMessageService
	 * @param {ConfirmationService} _confirmationService
	 * @param {ToastrService} _toastr
	 * @param {UserService} _userService
	 */

	constructor(
		private _spinner: NgxSpinnerService,
		private _spinnerMessageService: SpinnerMessageService,
		private _confirmationService: ConfirmationService,
		private _toastr: ToastrService,
		private _userService: UserService
	) {
		this.studentAcademicBehaviorModel = new StudentAcademicBehaviorModel();
	}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		await this.getStudentAcademicBehavior();
	}

	async ngOnChanges(changes: SimpleChanges) {}

	async getStudentAcademicBehavior() {
		try {
			this._spinner.show();
			this.studentAcademicBehaviorModel = await this._userService.getStudentAcademicBehavior(this.studentId);
			this.studyHours = this.studentAcademicBehaviorModel.studyHours;

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	async saveStudentAcademicBehavior() {
		try {
			this._spinner.show();
			this.studentAcademicBehaviorModel.studyHours = this.studyHours;
			let response = await this._userService.saveStudentAcademicBehavior(this.studentAcademicBehaviorModel);

			if (response.succeeded) {
				this._toastr.success(response.successMessage);
			} else {
				this._toastr.error(response.errors[0]);
			}
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}
}
