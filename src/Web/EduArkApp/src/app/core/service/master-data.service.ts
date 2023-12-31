import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DropDownModel } from './../models/common/drop.down.model';
import { UserMasterDataModel } from './../models/user/user.master.data.model';
import { SubjectMasterDataModel } from './../models/subject/subject.master.data.model';
import { BaseAcademicMasterDataModel } from './../models/common/base.academic.master.data.model';
import { Observable } from 'rxjs';
import { UserDetailsMasterDataFilterModel } from './../models/user/user.details.master.data.model';
import { TeacherClassesMasterDataModel } from './../models/class/teacher.classes.master.data.model';
import { ExamMasterDataFilterModel } from '../models/exam/exam.master.data.filter.model';

@Injectable({
	providedIn: 'root',
})
export class MasterDataService {
	baseUrl = environment.eduArkApiUrl;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
	constructor(private _httpClient: HttpClient) {}

	/**
	 * MasterData Service
	 * @param {}
	 * @service getUserMasterData
	 * @returns {Promise<DropDownModel[]>}
	 */
	async getUserMasterData(): Promise<UserMasterDataModel> {
		try {
			return await this._httpClient.get<UserMasterDataModel>(`${this.baseUrl}MasterData/GetUserMasterData`).toPromise();
		} catch (error) {
			console.log('====================================');
			console.log(error);
			console.log('====================================');
		}
	}

	/**
	 * MasterData Service
	 * @param {string}
	 * @service getLevelHeadsByFilter
	 * @returns {Promise<DropDownModel[]>}
	 */
	async getLevelHeadsByFilter(name: string): Promise<DropDownModel[]> {
		return await this._httpClient
			.post<DropDownModel[]>(`${this.baseUrl}MasterData/GetLevelHeadsByFilter`, { name: name })
			.toPromise();
	}

	/**
	 * MasterData Service
	 * @param {}
	 * @service getSubjectMasterData
	 * @returns {Promise<SubjectMasterDataModel>}
	 */
	async getSubjectMasterData(): Promise<SubjectMasterDataModel> {
		return await this._httpClient
			.get<SubjectMasterDataModel>(`${this.baseUrl}MasterData/getSubjectMasterData`)
			.toPromise();
	}

	/**
	 * MasterData Service
	 * @param {}
	 * @service getBaseAcademicMasterData
	 * @returns {Promise<SubjectMasterDataModel>}
	 */
	async getBaseAcademicMasterData(): Promise<BaseAcademicMasterDataModel> {
		return await this._httpClient
			.get<BaseAcademicMasterDataModel>(`${this.baseUrl}MasterData/GetBaseAcademicMasterData`)
			.toPromise();
	}

	/**
	 * MasterData Service
	 * @param {}
	 * @service getSubjectMasterDataByAcademicLevelId
	 * @returns {Promise<DropDownModel[]>}
	 */
	async getSubjectMasterDataByAcademicLevelId(academicLevelId: number): Promise<DropDownModel[]> {
		return await this._httpClient
			.get<DropDownModel[]>(`${this.baseUrl}MasterData/getSubjectMasterDataByAcademicLevelId/${academicLevelId}`)
			.toPromise();
	}

	/**
	 * MasterData Service
	 * @param {}
	 * @service getUserDetailMasterDataByFilter
	 * @returns {Promise<DropDownModel[]>}
	 */
	async getUserDetailMasterDataByFilter(
		userMasterDataFilter: UserDetailsMasterDataFilterModel
	): Promise<DropDownModel[]> {
		return await this._httpClient
			.post<DropDownModel[]>(`${this.baseUrl}MasterData/getUserDetailMasterDataByFilter`, userMasterDataFilter)
			.toPromise();
	}

	/**
	 * MasterData Service
	 * @param {string}
	 * @service getSubjectMasterDataByFilter
	 * @returns {Promise<DropDownModel[]>}
	 */
	async getSubjectMasterDataByFilter(filter: string): Promise<DropDownModel[]> {
		return await this._httpClient
			.post<DropDownModel[]>(`${this.baseUrl}MasterData/getSubjectMasterDataByFilter`, filter)
			.toPromise();
	}

	/**
	 * MasterData Service
	 * @param {}
	 * @service getTeacherClassesMasterData
	 * @returns {Promise<TeacherClassesMasterDataModel>}
	 */
	async getTeacherClassesMasterData(): Promise<TeacherClassesMasterDataModel> {
		return await this._httpClient
			.get<TeacherClassesMasterDataModel>(`${this.baseUrl}MasterData/getTeacherClassesMasterData`)
			.toPromise();
	}

	/**
	 * MasterData Service
	 * @param {}
	 * @service getSubjectsMasterDataByAcademicLevelId
	 * @returns {Promise<DropDownModel[]>}
	 */
	async getSubjectsMasterDataByAcademicLevelId(academicLevelId: number): Promise<DropDownModel[]> {
		return await this._httpClient
			.get<DropDownModel[]>(`${this.baseUrl}MasterData/getSubjectsMasterDataByAcademicLevelId/${academicLevelId}`)
			.toPromise();
	}

	/**
	 * MasterData Service
	 * @param {}
	 * @service getExamMasterData
	 * @returns {Promise<DropDownModel[]>}
	 */
	async getExamMasterData(filter: ExamMasterDataFilterModel): Promise<DropDownModel[]> {
		return await this._httpClient
			.post<DropDownModel[]>(`${this.baseUrl}MasterData/getExamMasterData`, filter)
			.toPromise();
	}
}
