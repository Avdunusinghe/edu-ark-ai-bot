import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { StudentExamMarksFilterModel } from '../models/exam/student.exam.mark.filter';
import { StudentMarksModel } from '../models/exam/student.mark.model';
import { PaginatedStudentExamMarksModel } from './../models/exam/paginated.student.exam.marks.model';
import { ExamMarkContainerModel } from './../models/exam/exam.mark.container.model';
import { ResultModel } from '../models/common/result.model';

@Injectable({
	providedIn: 'root',
})
export class ExamService {
	baseUrl = environment.eduArkApiUrl;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
	constructor(private _httpClient: HttpClient) {}

	/**
	 * Exam Service
	 * @param {FormData}
	 * @service uploadExamMarks
	 * @returns {Observable<Upload>}
	 */
	uploadExamMarks(data: FormData): Observable<any> {
		return this._httpClient.post(`${this.baseUrl}Exam/uploadExamMarks`, data, {
			reportProgress: true,
			observe: 'events',
		});
	}

	/**
	 * Exam Service
	 * @param {FormData}
	 * @service getExamMarksByFilter
	 * @returns {Promise<PaginatedStudentExamMarksModel>}
	 */
	async getExamMarksByFilter(filter: StudentExamMarksFilterModel): Promise<PaginatedStudentExamMarksModel> {
		return await this._httpClient
			.post<PaginatedStudentExamMarksModel>(`${this.baseUrl}Exam/getExamMarksByFilter`, filter)
			.toPromise();
	}

	/**
	 * Exam Service
	 * @param {ExamMarkContainerModel}
	 * @service saveExamMarks
	 * @returns {Promise<ResultModel>}
	 */
	async saveExamMarks(examMarkContainer: ExamMarkContainerModel): Promise<ResultModel> {
		return await this._httpClient.post<ResultModel>(`${this.baseUrl}Exam/saveExamMarks`, examMarkContainer).toPromise();
	}
}
