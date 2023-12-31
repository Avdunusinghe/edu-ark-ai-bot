import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { ClassService } from 'src/app/core/service/class.service';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { UserService } from './../../../core/service/user.service';
import { ReportService } from 'src/app/core/service/report.service';

@Component({
	selector: 'app-exam-mark-setting',
	templateUrl: './exam-mark-setting.component.html',
	styleUrls: ['./exam-mark-setting.component.sass'],
})
export class ExamMarkSettingComponent {
	menuItemsConfiguration: MenuItem[] | undefined;

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
	uploadStudentData(): void {
		throw new Error('Method not implemented.');
	}
	downloadStudentExcelTemplate(): void {
		throw new Error('Method not implemented.');
	}
}
