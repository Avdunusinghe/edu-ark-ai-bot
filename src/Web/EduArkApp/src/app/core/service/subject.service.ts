import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ResultModel } from '../models/common/result.model';
import { SubjectModel } from './../models/subject/subject.model';
import { SubjectPaginatedItemModel } from './../models/subject/subject.paginated.item.model';
import { SubjectFilterModel } from './../models/subject/subject.filter.model';
import { SubjectTeacherModel } from './../models/subject-teacher/subject.teacher.model';
import { SubjectTeacherFilterModel } from './../models/subject-teacher/subject.teacher.filter.model';

@Injectable({
	providedIn: 'root',
})
export class SubjectService {
	baseUrl = environment.eduArkApiUrl;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
	constructor(private _httpClient: HttpClient) {}

	/**
	 * Subject Service
	 * @param {SubjectModel}
	 * @service saveSubject
	 * @returns {Promise<ResultModel>}
	 */
	async saveSubject(subjectDetails: SubjectModel): Promise<ResultModel> {
		return await this._httpClient.post<ResultModel>(`${this.baseUrl}Subject/saveSubject`, subjectDetails).toPromise();
	}

	/**
	 * Subject Service
	 * @param {number}
	 * @service deleteSubject
	 * @returns {Promise<ResultModel>}
	 */
	async deleteSubject(id: number): Promise<ResultModel> {
		return await this._httpClient.delete<ResultModel>(`${this.baseUrl}Subject/deleteSubject/${id}`).toPromise();
	}

	/**
	 * Subject Service
	 * @param {SubjectFilterModel}
	 * @service deleteSubject
	 * @returns {Promise<SubjectPaginatedItemModel>}
	 */
	async getSubjectsByFilter(subjectFilter: SubjectFilterModel): Promise<SubjectPaginatedItemModel> {
		return await this._httpClient
			.post<SubjectPaginatedItemModel>(`${this.baseUrl}Subject/getSubjectsByFilter`, subjectFilter)
			.toPromise();
	}

	/**
	 * Subject Service
	 * @param {SubjectTeacherModel}
	 * @service saveSubjectTeachers
	 * @returns {Promise<ResultModel>}
	 */
	async saveSubjectTeachers(subjectTeachersDetails: SubjectTeacherModel): Promise<ResultModel> {
		return await this._httpClient
			.post<ResultModel>(`${this.baseUrl}Subject/saveSubjectTeachers`, subjectTeachersDetails)
			.toPromise();
	}

	/**
	 * Subject Service
	 * @param {SubjectTeacherFilterModel}
	 * @service saveSubjectTeachers
	 * @returns {Promise<SubjectTeacherModel[]>}
	 */
	async getAllSubjectTeachersByFilter(subjectTeacherFilter: SubjectTeacherFilterModel): Promise<SubjectTeacherModel[]> {
		return await this._httpClient
			.post<SubjectTeacherModel[]>(`${this.baseUrl}Subject/getAllSubjectTeachersByFilter`, subjectTeacherFilter)
			.toPromise();
	}
}
