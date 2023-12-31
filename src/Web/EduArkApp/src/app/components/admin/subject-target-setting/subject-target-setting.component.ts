import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SubjectTargetSettingService } from './../../../core/service/subject-target-setting.service';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { AuthService } from 'src/app/core/service/auth.service';
import { AuthenticationService } from 'src/app/core/service/authentication.service';

@Component({
	selector: 'app-subject-target-setting',
	templateUrl: './subject-target-setting.component.html',
	styleUrls: ['./subject-target-setting.component.scss'],
	animations: [
		trigger('fadeInOut', [
			state('in', style({ opacity: 1 })),
			state('out', style({ opacity: 0, display: 'none' })),
			transition('in <=> out', animate('30000ms ease-in-out')),
		]),
	],
	providers: [ConfirmationService, DialogService],
})
export class SubjectTargetSettingComponent {
	//master data properties
	subjects: DropDownModel[] = [];
	currentAcademicYear: number = 0;
	academicYears: DropDownModel[] = [];
	academicLevels: DropDownModel[] = [];
	semesters: DropDownModel[] = [];
	examTypes: DropDownModel[] = [];
	//core data properties
	subjectTargetSettingForm: UntypedFormGroup;
	isSubjectsVisible = false;
	precentage: any;
	progressBarVisible: boolean = false;
	currentUserName: string = '';
	/**
	 * Constructot
	 * @param {NgxSpinnerService} _spinner
	 * @param {UntypedFormBuilder} _untypedFormBuilder
	 * @param {MasterDataService} _masterDataService
	 * @param {Router} _router
	 * @param {ToastrService} _toastr
	 * @param {SubjectTargetSettingService} _subjectTargetSettingService
	 *
	 *
	 */
	constructor(
		private _spinner: NgxSpinnerService,
		private _untypedFormBuilder: UntypedFormBuilder,
		private _masterDataService: MasterDataService,
		private _confirmationService: ConfirmationService,
		private _router: Router,
		private _toastr: ToastrService,
		private _subjectTargetSettingService: SubjectTargetSettingService,
		private _spinnerMessageService: SpinnerMessageService,
		private _authService: AuthenticationService
	) {
		this.currentUserName = this._authService.currentUserValue.displayName;
	}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		await this.getBaseAcademicMasterData();
	}

	/**
	 * @param {}
	 * @Method getBaseAcademicMasterData
	 * @returns {Promise<void>}
	 */
	async getBaseAcademicMasterData(): Promise<void> {
		try {
			this._spinner.show();

			let response = await this._masterDataService.getBaseAcademicMasterData();

			this.academicLevels = response.academicLevels;
			this.currentAcademicYear = response.currentAcademicYear;
			this.academicYears = response.academicYears;
			this.semesters = response.semesters;
			this.examTypes = response.examTypes;
			await this.createSubjectTargetSettingForm();

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	/**
	 * @param {}
	 * @Method createSubjectTargetSettingForm
	 * @returns {Promise<void>}
	 */
	async createSubjectTargetSettingForm(): Promise<void> {
		try {
			this.subjectTargetSettingForm = this._untypedFormBuilder.group({
				currentAcademicYear: [this.currentAcademicYear, [Validators.required]],
				academicLevel: [null, [Validators.required]],
				subject: [null, [Validators.required]],
				currentSemester: [null, [Validators.required]],
				examTypeId: [null, [Validators.required]],
			});

			this.subjectTargetSettingForm.controls['currentAcademicYear'].disable();
		} catch (error) {}
	}

	/**
	 * @param {}
	 * @Method getSubjectsByAcademicLevelId
	 * @returns {Promise<void>}
	 */
	async getSubjectsByAcademicLevelId(): Promise<void> {
		try {
			this._spinner.show();
			this._spinnerMessageService.sendData('Loading Subjects...');
			await new Promise((resolve) => setTimeout(resolve, 1000));
			let response = await this._masterDataService.getSubjectMasterDataByAcademicLevelId(this.academicLevelId);

			if (response.length > 0) {
				this.subjects = response;
				this._spinnerMessageService.sendData('');
				this._spinnerMessageService.sendData('Loading...');
				this._spinner.hide();
			} else {
				this.subjects = [];
				this._toastr.warning('No subjects found for the selected academic level', 'Warning');
				this._spinnerMessageService.sendData('');
				this._spinnerMessageService.sendData('Loading...');
				this._spinner.hide();
			}
		} catch (error) {
			this._spinnerMessageService.sendData('');
			this._spinnerMessageService.sendData('Loading...');
			this._spinner.hide();
		}
	}

	async runService(): Promise<void> {
		try {
			this._confirmationService.confirm({
				message: `Hi ${this.currentUserName} , This operation may take some time to complete. Do you wish to proceed?`,
				header: 'Confirmation',
				icon: 'pi pi-exclamation-triangle',
				accept: async () => {
					await this.saveSubjectTargetSettingConfiguration();
				},
				reject: () => {},
			});
		} catch (error) {}
	}

	async saveSubjectTargetSettingConfiguration(): Promise<void> {
		try {
			this._spinner.show();
			this._spinnerMessageService.sendData('Processing Data...');
			this._subjectTargetSettingService
				.saveSubjectTargetSettingConfiguration(this.subjectTargetSettingForm.getRawValue())
				.subscribe(
					(event) => {
						if (event.type === HttpEventType.UploadProgress) {
							this.progressBarVisible = true;
							this.precentage = Math.round((100 * event.loaded) / event.total);
						} else if (event instanceof HttpResponse) {
							this.progressBarVisible = false;
							this.precentage = 0;
							setTimeout(() => {
								this._spinner.hide();
								this._spinnerMessageService.sendData('Loading...');
								this._toastr.success('Success', 'Subject target setting configuration saved successfully');
							}, 10000);
						}
					},
					(error) => {
						this._spinner.hide();
						this._spinnerMessageService.sendData('Loading...');
						this.progressBarVisible = false;
						this._toastr.error('Error', 'Error has been occurred while saving subject target setting configuration');
					}
				);
		} catch (error) {
			this._spinner.hide();
		}
	}

	// -----------------------------------------------------------------------------------------------------
	//@ Getters
	// -----------------------------------------------------------------------------------------------------

	get academicLevelId(): number {
		return this.subjectTargetSettingForm.get('academicLevel').value;
	}
}
