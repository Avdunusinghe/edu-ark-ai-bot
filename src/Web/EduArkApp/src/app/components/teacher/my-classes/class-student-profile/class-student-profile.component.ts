import { Component, Inject, Input } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { StudentAcademicBehaviorModel } from 'src/app/core/models/student/student.academic.behavior.model';
import { StudentBasicDataModel } from 'src/app/core/models/student/student.basic.data.model';
import { UserService } from 'src/app/core/service/user.service';
@Component({
	selector: 'app-class-student-profile',
	templateUrl: './class-student-profile.component.html',
	styleUrls: ['./class-student-profile.component.sass'],
})
export class ClassStudentProfileComponent {
	@Input() data: any;
	studentBasicDataModel: StudentBasicDataModel;
	studentAcademicBehaviorModel: StudentAcademicBehaviorModel;
	studyHours: number = 0;
	/**
	 * Constructor
	 * @param {DynamicDialogRef} _dialogRef
	 * @param {NgxSpinnerService} _spinner
	 
	 *
	 */
	constructor(
		private _dialogRef: DynamicDialogRef,
		private _spinner: NgxSpinnerService,
		private _userService: UserService,

		@Inject(DynamicDialogConfig) private _dialogConfig: DynamicDialogConfig
	) {
		this.data = this._dialogConfig.data;

		this.getStudentDataById();
	}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {}

	async getStudentDataById() {
		try {
			let response = await this._userService.getStudentDataById(this.data.studentId);
			this.studentBasicDataModel = response;
			this.studentAcademicBehaviorModel = response.studentAcademicBehavior;
			this.studyHours = this.studentAcademicBehaviorModel.studyHours;
		} catch (error) {}
	}
}
