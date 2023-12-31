import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserDetailsFilterModel } from './../models/user/user.details.filter.model';
import { UserDetailsModel } from './../models/user/user.details.model';
import { ContractPaginatedItemModel } from './../models/user/user.paginated.item.model';
import { ResultModel } from './../models/common/result.model';
import { Observable } from 'rxjs';
import { Upload, upload } from '../models/common/upload';
import { ClassStudentFilterModel } from './../models/class/class.student.filter.model';
import { StudentPaginatedItemModel } from './../models/user/student.paginated.item.model';
import { StudentAcademicBehaviorModel } from '../models/student/student.academic.behavior.model';
import { StudentBasicDataModel } from './../models/student/student.basic.data.model';

@Injectable({
	providedIn: 'root',
})
export class UserService {
	baseUrl = environment.eduArkApiUrl;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
	constructor(private _httpClient: HttpClient) {}

	/**
	 * User Service
	 * @param {UserDetailsFilterModel}
	 * @service GetAllUsersByFilter
	 * @returns {Promise<UserDetailsModel[]>}
	 */
	async getAllUsersByFilter(filter: UserDetailsFilterModel): Promise<ContractPaginatedItemModel> {
		return await this._httpClient
			.post<ContractPaginatedItemModel>(`${this.baseUrl}User/GetAllUsersByFilter`, filter)
			.toPromise();
	}

	/**
	 * User Service
	 * @param {number}
	 * @service deleteUser
	 * @returns {Promise<ResultModel>}
	 */
	async deleteUser(id: number): Promise<ResultModel> {
		return await this._httpClient.delete<ResultModel>(`${this.baseUrl}User/DeleteUser/${id}`).toPromise();
	}

	/**
	 * User Service
	 * @param {UserDetailsModel}
	 * @service saveUser
	 * @returns {Promise<ResultModel>}
	 */
	async saveUser(userDetails: UserDetailsModel): Promise<ResultModel> {
		return await this._httpClient.post<ResultModel>(`${this.baseUrl}User/saveUser`, userDetails).toPromise();
	}

	/**
	 * User Service
	 * @param {}
	 * @service getUserByCurrentUserId
	 * @returns {Promise<UserDetailsModel>}
	 */
	async getUserByCurrentUserId(): Promise<UserDetailsModel> {
		return await this._httpClient.get<UserDetailsModel>(`${this.baseUrl}User/getUserByCurrentUserId`).toPromise();
	}

	/**
	 * User Service
	 * @param {FormData}
	 * @service uploadUserProfileImage
	 * @returns {Observable<Upload>}
	 */
	uploadUserProfileImage(formData: FormData): Observable<Upload> {
		return this._httpClient
			.post<ResultModel>(`${this.baseUrl}User/uploadUserProfileImage`, formData, {
				reportProgress: true,
				observe: 'events',
			})
			.pipe(upload());
	}

	/**
	 * User Service
	 * @param {FormData}
	 * @service uploadClassStudents
	 * @returns {Observable<Upload>}
	 */
	uploadClassStudents(data: FormData): Observable<any> {
		return this._httpClient.post(`${this.baseUrl}User/uploadClassStudents`, data, {
			reportProgress: true,
			observe: 'events',
		});
	}

	/**
	 * User Service
	 * @param {ClassStudentFilterModel}
	 * @service getStudentsByFilter
	 * @returns {Promise<StudentPaginatedItemModel>}
	 */
	async getStudentsByFilter(filter: ClassStudentFilterModel): Promise<StudentPaginatedItemModel> {
		return await this._httpClient
			.post<StudentPaginatedItemModel>(`${this.baseUrl}User/getStudentsByFilter`, filter)
			.toPromise();
	}

	/**
	 * User Service
	 * @param {number}
	 * @service getStudentAcademicBehavior
	 * @returns {Promise<StudentAcademicBehaviorModel>}
	 */
	async getStudentAcademicBehavior(studentId: number): Promise<StudentAcademicBehaviorModel> {
		return await this._httpClient
			.get<StudentAcademicBehaviorModel>(`${this.baseUrl}User/getStudentAcademicBehavior/${studentId}`)
			.toPromise();
	}

	/**
	 * User Service
	 * @param {StudentAcademicBehaviorModel}
	 * @service saveStudentAcademicBehavior
	 * @returns {Promise<ResultModel>}
	 */
	async saveStudentAcademicBehavior(studentAcademicBehaviorModel: StudentAcademicBehaviorModel): Promise<ResultModel> {
		return await this._httpClient
			.post<ResultModel>(`${this.baseUrl}User/saveStudentAcademicBehavior`, studentAcademicBehaviorModel)
			.toPromise();
	}

	/**
	 * User Service
	 * @param {number}
	 * @service getStudentDataById
	 * @returns {Promise<StudentBasicDataModel>}
	 */
	async getStudentDataById(studentId: number): Promise<StudentBasicDataModel> {
		return await this._httpClient
			.get<StudentBasicDataModel>(`${this.baseUrl}User/getStudentDataById/${studentId}`)
			.toPromise();
	}
}
