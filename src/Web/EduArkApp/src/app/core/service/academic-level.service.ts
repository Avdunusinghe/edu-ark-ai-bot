import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AcademicLevelModel } from './../models/academic-level/academic.level.model';
import { ResultModel } from '../models/common/result.model';
import { AcademicLevelPaginatedItemModel } from './../models/academic-level/academic.level.paginated.item.model';
import { AcademicLevelFilterModel } from './../models/academic-level/academic.level.filter.model';

@Injectable({
	providedIn: 'root',
})
export class AcademicLevelService {
	baseUrl = environment.eduArkApiUrl;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
	constructor(private _httpClient: HttpClient) {}

	/**
	 * AcademicLevel Service
	 * @param {}
	 * @service saveAcademicLevel
	 * @returns {Promise<ResultModel>}
	 */
	async saveAcademicLevel(academicLevelDetails: AcademicLevelModel): Promise<ResultModel> {
		return await this._httpClient
			.post<ResultModel>(`${this.baseUrl}AcademicLevel/saveAcademicLevel`, academicLevelDetails)
			.toPromise();
	}

	/**
	 * AcademicLevel Service
	 * @param {AcademicLevelFilterModel}
	 * @service getAllAcademicLevels
	 * @returns {Promise<AcademicLevelPaginatedItemModel>}
	 */
	async getAllAcademicLevels(academicLevelFilter: AcademicLevelFilterModel): Promise<AcademicLevelPaginatedItemModel> {
		return await this._httpClient
			.post<AcademicLevelPaginatedItemModel>(`${this.baseUrl}AcademicLevel/getAcademicLevels`, academicLevelFilter)
			.toPromise();
	}

	/**
	 * AcademicLevel Service
	 * @param {number}
	 * @service deleteAcademicLevel
	 * @returns {Promise<ResultModel>}
	 */
	async deleteAcademicLevel(id: number): Promise<ResultModel> {
		return await this._httpClient
			.delete<ResultModel>(`${this.baseUrl}AcademicLevel/deleteAcademicLevel/${id}`)
			.toPromise();
	}
}
