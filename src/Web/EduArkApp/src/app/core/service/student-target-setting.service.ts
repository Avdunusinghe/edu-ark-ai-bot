import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StudentSubjectTargetSettingsComponent } from 'src/app/components/teacher/my-classes/student-subject-target-settings/student-subject-target-settings.component';
import { environment } from 'src/environments/environment';
import { ResultModel } from '../models/common/result.model';
import { StuddentTargetSettingFilterModel } from './../models/student-target-settings/student.target.settings.filter.model';
import { StudentTargetSettingDetailPaginatedItemModel } from './../models/student-target-settings/student.target.setting.detail.paginated.item.model';
import { TeacherTargetScoreModel } from './../models/student-target-settings/teacher.target.score.model';
import { TeacherTargetScoreContainerModel } from './../models/student-target-settings/teacher.target.score.container.model';

@Injectable({
	providedIn: 'root',
})
export class StudentTargetSettingService {
	baseUrl = environment.eduArkApiUrl;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
	constructor(private _httpClient: HttpClient) {}

	/**
	 * StudentTargetSetting Service
	 * @param {StudentSubjectTargetSettingsComponent}
	 * @service saveSubjectTargetSettingConfuguration
	 * @returns {Promise<any>}
	 */
	async saveSubjectTargetSettingConfuguration(model: StudentSubjectTargetSettingsComponent): Promise<any> {
		return await this._httpClient
			.post<any>(`${this.baseUrl}StudentTargetSetting/saveSubjectTargetSettingConfuguration`, model, {
				reportProgress: true,
				observe: 'events',
			})
			.toPromise();
	}

	/**
	 * StudentTargetSetting Service
	 * @param {StuddentTargetSettingFilterModel}
	 * @service saveSubjectTargetSettingConfuguration
	 * @returns {Promise<StudentTargetSettingDetailPaginatedItemModel>}
	 */
	async getStudentsTargetSettingsByFilter(
		filter: StuddentTargetSettingFilterModel
	): Promise<StudentTargetSettingDetailPaginatedItemModel> {
		return await this._httpClient
			.post<StudentTargetSettingDetailPaginatedItemModel>(
				`${this.baseUrl}StudentTargetSetting/getStudentsTargetSettingsByFilter`,
				filter
			)
			.toPromise();
	}
}
