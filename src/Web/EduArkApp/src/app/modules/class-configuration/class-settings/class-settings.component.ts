import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { ClassService } from 'src/app/core/service/class.service';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { UserService } from 'src/app/core/service/user.service';
import { ReportService } from './../../../core/service/report.service';
import { formatDate } from '@angular/common';
import { AuthenticationResponseModel } from 'src/app/core/models/authentication/authentication.response.model';

@Component({
	selector: 'app-class-settings',
	templateUrl: './class-settings.component.html',
	styleUrls: ['./class-settings.component.sass'],
	providers: [ConfirmationService, DialogService],
})
export class ClassSettingsComponent {
	menuItemsConfiguration: MenuItem[] | undefined;
	currentUser: AuthenticationResponseModel;

	@Input() currentClassNameId: number = 0;
	@Input() currentAcademicYearId: number = 0;
	@Input() currentAcademicLevelId: number = 0;
	@Output() getAllStudentsEvent = new EventEmitter<void>();

	precentage: any;
	progressBarVisible: boolean = false;
	/**
	 * Constructor
	 * @param {NgxSpinnerService} _spinner
	 * @param {ConfirmationService} _confirmationService
	 * @param {ToastrService} _toastr
	 * @param {ClassService} _classService
	 * @param {DialogService} _dialogService
	 * @param {MasterDataService} _masterDataService
	 * @param {UserService} _userService
	 *
	 */

	constructor(
		private _spinner: NgxSpinnerService,
		private _confirmationService: ConfirmationService,
		private _toastr: ToastrService,
		private _classService: ClassService,
		private _dialogService: DialogService,
		private _masterDataService: MasterDataService,
		private _userService: UserService,
		private _reportService: ReportService
	) {}

	// -----------------------------------------------------------------------------------------------------
	// @ Lifecycle hooks
	// -----------------------------------------------------------------------------------------------------

	/**
	 * On init
	 */

	async ngOnInit() {
		this.menuItemsConfiguration = [
			{
				label: 'Add New Student',
				icon: 'pi pi-fw pi-plus',
			},
			{
				label: 'Download Template',
				icon: 'pi pi-fw pi-download',
				command: () => this.downloadStudentExcelTemplate(),
			},
			{
				label: 'Import',
				icon: 'pi pi-fw pi-file-excel',
				command: () => this.uploadStudentData(),
			},
			{
				label: 'Export',
				icon: 'pi pi-fw pi-download',
				command: () => this.downloadStudentExcelTemplate(),
			},
		];
	}

	async uploadStudentData(): Promise<void> {
		try {
			let file: any;
			const formData = new FormData();
			const input = document.createElement('input');
			input.type = 'file';
			input.accept = '.csv, .xlsx';
			input.addEventListener('change', (fileEvent) => {
				file = (fileEvent.target as HTMLInputElement).files[0];

				formData.append('file', file, file.name);
				this.progressBarVisible = true;

				this._userService.uploadClassStudents(formData).subscribe(
					(event) => {
						if (event.type === HttpEventType.UploadProgress) {
							this.precentage = Math.round((100 * event.loaded) / event.total);
						} else if (event instanceof HttpResponse) {
							this.progressBarVisible = false;

							this.getAllStudentsEvent.emit();
						}
					},
					(error) => {
						this.progressBarVisible = false;
						this._toastr.error('Error', 'Error while uploading file');
					}
				);
			});

			input.click();
		} catch (error) {}
	}

	downloadStudentExcelTemplate() {
		let filter = {
			academicYearId: this.currentAcademicYearId,
			academicLevelId: this.currentAcademicLevelId,
			classNameId: this.currentClassNameId,
		};

		this._reportService.downloadStudentExcelTemplate(filter).subscribe(
			(response) => {
				if (response.type === HttpEventType.DownloadProgress) {
					this.precentage = Math.round((100 * response.loaded) / response.total);
				}

				if (response.type === HttpEventType.Response) {
					if (response.status == 204) {
						this.progressBarVisible = false;
						this.precentage = 0;
						this._spinner.hide();
					} else {
						const objectUrl: string = URL.createObjectURL(response.body);
						const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;

						a.href = objectUrl;
						a.download = 'studentupload' + this.dateTimeCovert(new Date()) + '.xlsx';
						document.body.appendChild(a);
						a.click();

						document.body.removeChild(a);
						URL.revokeObjectURL(objectUrl);
						this.progressBarVisible = false;
						this.precentage = 0;
						this._spinner.hide();
					}
				}
			},
			(error) => {
				this._spinner.hide();
				this.progressBarVisible = false;
				this.precentage = 0;
			}
		);
	}

	dateTimeCovert(convertedDateTime: Date) {
		return formatDate(convertedDateTime, 'yyyy-MM-dd', 'en-US');
	}
}
