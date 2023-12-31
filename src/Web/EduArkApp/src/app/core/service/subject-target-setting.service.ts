import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StudentSubjectTargetSettingsComponent } from 'src/app/components/teacher/my-classes/student-subject-target-settings/student-subject-target-settings.component';
import { environment } from 'src/environments/environment';
import { StuddentTargetSettingFilterModel } from '../models/student-target-settings/student.target.settings.filter.model';
import { StudentTargetSettingDetailPaginatedItemModel } from '../models/student-target-settings/student.target.setting.detail.paginated.item.model';
import { Observable } from 'rxjs';
import { TeacherTargetScoreContainerModel } from '../models/student-target-settings/teacher.target.score.container.model';
import { ResultModel } from '../models/common/result.model';

@Injectable({
	providedIn: 'root',
})
export class SubjectTargetSettingService {
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
	 * @returns {Observable<any>}
	 */
	saveSubjectTargetSettingConfiguration(model: StudentSubjectTargetSettingsComponent): Observable<any> {
		return this._httpClient.post(`${this.baseUrl}SubjectTargetSetting/saveSubjectTargetSettingConfiguration`, model, {
			reportProgress: true,
			observe: 'events',
		});
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
				`${this.baseUrl}SubjectTargetSetting/getStudentsTargetSettingsByFilter`,
				{ filter: filter }
			)
			.toPromise();
	}

	/**
	 * StudentTargetSetting Service
	 * @param {TeacherTargetScoreContainerModel}
	 * @service saveTeacherTargetScore
	 * @returns {Promise<ResultModel>}
	 */
	async saveTeacherTargetScore(teacherTargetScores: TeacherTargetScoreContainerModel): Promise<ResultModel> {
		return await this._httpClient
			.post<ResultModel>(`${this.baseUrl}SubjectTargetSetting/saveTeacherTargetScore`, teacherTargetScores)
			.toPromise();
	}
}
