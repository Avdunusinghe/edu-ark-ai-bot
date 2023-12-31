import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/core/service/authentication.service';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { SpinnerMessageService } from 'src/app/core/service/spinner-message.service';
import { DashboardService } from './../../core/service/dashboard.service';
import { DashboardModel } from './../../core/models/dashboard/dashboard.model';

@Component({
	selector: 'app-admin',
	templateUrl: './admin.component.html',
	styleUrls: ['./admin.component.sass'],
})
export class AdminComponent {
	dashboardModel: DashboardModel;
	currentDateTime: string;
	/**
	 * Constructor
	 * @param {NgxSpinnerService} _spinner
	 * @param {MasterDataService} _masterDataService
	 * @param {Router} _router
	 * @param {ToastrService} _toastr
	 * @param {SpinnerMessageService} _spinnerMessageService
	 * @param {AuthenticationService} _authService
	 *
	 */

	constructor(
		private _spinner: NgxSpinnerService,
		private _masterDataService: MasterDataService,
		private _router: Router,
		private _toastr: ToastrService,
		private _spinnerMessageService: SpinnerMessageService,
		private _authService: AuthenticationService,
		private _dashboardService: DashboardService
	) {
		this.getAdminDashboard();
	}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		this.updateTime();
		setInterval(() => this.updateTime(), 1000);
	}

	async getAdminDashboard(): Promise<void> {
		try {
			this._spinner.show();

			const timeoutPromise = new Promise<void>((resolve) => {
				setTimeout(() => {
					resolve();
				}, 3000);
			});

			const requestPromise = this._dashboardService.getAdminDashboard();

			let response = await Promise.race([requestPromise, timeoutPromise]);

			if (response) {
				this.dashboardModel = response;
			}

			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}
	updateTime() {
		const now = new Date();
		const year = now.getFullYear();
		const month = (now.getMonth() + 1).toString().padStart(2, '0');
		const day = now.getDate().toString().padStart(2, '0');
		const hours = now.getHours().toString().padStart(2, '0');
		const minutes = now.getMinutes().toString().padStart(2, '0');
		const seconds = now.getSeconds().toString().padStart(2, '0');

		this.currentDateTime = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
	}

	async navigateUserController(): Promise<void> {
		this._router.navigate(['/admin/user']);
	}

	async navigateSubjectController(): Promise<void> {
		this._router.navigate(['/admin/subject']);
	}

	async classController(): Promise<void> {
		this._router.navigate(['/admin/class']);
	}

	async subjectTeachersController(): Promise<void> {
		this._router.navigate(['/admin/subject-teacher']);
	}

	async academicLevelController(): Promise<void> {
		this._router.navigate(['/admin/academic-level']);
	}

	async navigateStudentMarkAnalysis(): Promise<void> {
		this._router.navigate(['/admin/subject-target-setting']);
	}
}
