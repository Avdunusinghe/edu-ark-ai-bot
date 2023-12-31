import { Component, Input, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent, MenuItem } from 'primeng/api';
import { ClassStudentFilterModel } from 'src/app/core/models/class/class.student.filter.model';
import { DropDownModel } from 'src/app/core/models/common/drop.down.model';
import { StudentModel } from 'src/app/core/models/user/student.model';
import { MasterDataService } from 'src/app/core/service/master-data.service';
import { UserService } from 'src/app/core/service/user.service';

@Component({
	selector: 'app-class-students',
	templateUrl: './class-students.component.html',
	styleUrls: ['./class-students.component.sass'],
})
export class ClassStudentsComponent {
	@Input() currentClassNameId: number = 0;
	@Input() currentAcademicYearId: number = 0;
	@Input() currentAcademicLevelId: number = 0;

	@Input() classNames: DropDownModel[] = [];
	@Input() academicYears: DropDownModel[] = [];
	@Input() academicLevels: DropDownModel[] = [];

	menuItemsConfiguration: MenuItem[] | undefined;
	isSettingVisible: boolean = false;
	//filter properties
	name: string = '';

	//pagination meta data properties
	currentPage: number = 0;
	pageSize: number = 10;
	totalRecordCount: number = 0;

	//core data properties

	listOfStudents: StudentModel[] = [];

	//master data properties

	/**
	 * Constructor
	
	 * @param {Router} _router
	 * @param {MasterDataService} _masterDataService
	 * @param {NgxSpinnerService} _spinner
	 * @param {ToastrService} _toastr
   * @param {UserService} _userService
	 *
	 *
	 */

	constructor(
		private _router: Router,
		private _masterDataService: MasterDataService,
		private _spinner: NgxSpinnerService,
		private _toastr: ToastrService,
		private _userService: UserService
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
				label: 'Bulk Upload',
				icon: 'pi pi-fw pi-file',
				command: (event) => {},
				items: [
					{
						label: 'New',
						icon: 'pi pi-fw pi-plus',
						items: [
							{
								label: 'Bookmark',
								icon: 'pi pi-fw pi-bookmark',
							},
							{
								label: 'Video',
								icon: 'pi pi-fw pi-video',
							},
						],
					},
					{
						label: 'Delete',
						icon: 'pi pi-fw pi-trash',
					},
					{
						separator: true,
					},
					{
						label: 'Export',
						icon: 'pi pi-fw pi-external-link',
					},
				],
			},
			{
				label: 'Download Template',
				icon: 'pi pi-fw pi-download',
			},
		];
	}
	async ngOnChanges(changes: SimpleChanges) {}
	/**
	 * @param {LazyLoadEvent}
	 * @Method loadClassStudents
	 * @returns {Promise<void>}
	 */
	async loadClassStudents(event: LazyLoadEvent): Promise<void> {
		try {
			this.currentPage = event.first / event.rows;
			this.pageSize = event.rows;
			await this.getStudentsByFilter();
		} catch (error) {
			this._spinner.hide();
		}
	}

	async getStudentsByFilter(): Promise<void> {
		try {
			this._spinner.show();
			let classStudentFilter = new ClassStudentFilterModel();

			classStudentFilter.name = this.name == null ? '' : this.name;
			classStudentFilter.academicYearId = this.currentAcademicYearId;

			classStudentFilter.classNameId = this.currentClassNameId;
			classStudentFilter.currentPage = this.currentPage;
			classStudentFilter.pageSize = this.pageSize;
			classStudentFilter.academicLevelId = this.currentAcademicLevelId;
			console.log(classStudentFilter);

			console.log(this.currentAcademicYearId);
			console.log(this.currentAcademicLevelId);

			let response = await this._userService.getStudentsByFilter(classStudentFilter);

			this.listOfStudents = response.items;
			this.totalRecordCount = response.totalCount;
			this._spinner.hide();
		} catch (error) {
			this._spinner.hide();
		}
	}

	async filterStudentByName(event: any): Promise<void> {
		this.name = event;
		await this.getStudentsByFilter();
	}

	async showSetting(): Promise<void> {
		this.isSettingVisible = true;
	}

	async hideSetting(inActive: boolean): Promise<void> {
		this.isSettingVisible = false;
	}
}
