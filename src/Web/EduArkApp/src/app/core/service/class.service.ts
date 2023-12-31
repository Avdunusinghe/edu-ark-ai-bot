import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ClassNameModel } from './../models/class/class.name.model';
import { ResultModel } from '../models/common/result.model';
import { ClassNameFilterModel } from './../models/class/class.name.filter.model';
import { ClassNamePaginatedItemModel } from './../models/class/class.name.paginated.item.model';
import { ClassFilterModel } from './../models/class/class.filter.model';
import { ClassPaginatedItemModel } from './../models/class/class.paginated.item.model';
import { ClassModel } from './../models/class/class.model';
import { ClassMasterDataModel } from './../models/class/class.master.data.model';
import { ClassSubjectTeacherModel } from '../models/class/class.subject.teacher.model';
import { TeacherClassFilterModel } from './../models/class/teacher.class.filter.model';
import { BasicTeacherClassPaginatedItemModel } from './../models/class/basic.teacher.class.paginated.item.model';

@Injectable({
	providedIn: 'root',
})
export class ClassService {
	baseUrl = environment.eduArkApiUrl;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
	constructor(private _httpClient: HttpClient) {}

	/**
	 * Class Service
	 * @param {ClassNameModel}
	 * @service saveClassName
	 * @returns {Promise<ResultModel>}
	 */
	async saveClassName(classNameDetails: ClassNameModel): Promise<ResultModel> {
		return await this._httpClient.post<ResultModel>(`${this.baseUrl}Class/saveClassName`, classNameDetails).toPromise();
	}

	/**
	 * Class Service
	 * @param {number}
	 * @service deleteClassName
	 * @returns {Promise<ResultModel>}
	 */
	async deleteClassName(classNameId: number): Promise<ResultModel> {
		return await this._httpClient
			.delete<ResultModel>(`${this.baseUrl}Class/deleteClassName/${classNameId}`)
			.toPromise();
	}

	/**
	 * Class Service
	 * @param {ClassNameFilterModel}
	 * @service deleteClassName
	 * @returns {Promise<ClassNamePaginatedItemModel>}
	 */
	async getClassNamesByFilter(classNameFilter: ClassNameFilterModel): Promise<ClassNamePaginatedItemModel> {
		return await this._httpClient
			.post<ClassNamePaginatedItemModel>(`${this.baseUrl}Class/getClassNamesByFilter`, classNameFilter)
			.toPromise();
	}

	/**
	 * Class Service
	 * @param {ClassFilterModel}
	 * @service getClassesByFilter
	 * @returns {Promise<ClassPaginatedItemModel>}
	 */
	async getClassesByFilter(classFilter: ClassFilterModel): Promise<ClassPaginatedItemModel> {
		return await this._httpClient
			.post<ClassPaginatedItemModel>(`${this.baseUrl}Class/getClassesByFilter`, classFilter)
			.toPromise();
	}

	/**
	 * Class Service
	 * @param {ClassModel}
	 * @service saveClass
	 * @returns {Promise<ResultModel>}
	 */
	async saveClass(classDetails: ClassModel): Promise<ResultModel> {
		return await this._httpClient.post<ResultModel>(`${this.baseUrl}Class/saveClass`, classDetails).toPromise();
	}

	/**
	 * Class Service
	 * @param {}
	 * @service getClassMasterData
	 * @returns {Promise<ClassMasterDataModel>}
	 */
	async getClassMasterData(): Promise<ClassMasterDataModel> {
		return await this._httpClient.get<ClassMasterDataModel>(`${this.baseUrl}Class/getClassMasterData`).toPromise();
	}

	/**
	 * Class Service
	 * @param {number, number}
	 * @service getClassSubjectsForSelectedAcademicLevel
	 * @returns {Promise<ClassSubjectTeacherModel[]>}
	 */
	async getClassSubjectsForSelectedAcademicLevel(
		academicYearId: number,
		academicLevelId: number
	): Promise<ClassSubjectTeacherModel[]> {
		return await this._httpClient
			.post<ClassSubjectTeacherModel[]>(`${this.baseUrl}Class/getClassSubjectsForSelectedAcademicLevel`, {
				academicYearId,
				academicLevelId,
			})
			.toPromise();
	}

	/**
	 * Class Service
	 * @param {number, number, number}
	 * @service getClassDetails
	 * @returns {Promise<ClassModel>}
	 */
	async getClassDetails(academicYearId: number, academicLevelId: number, classNameId: number): Promise<ClassModel> {
		return await this._httpClient
			.get<ClassModel>(`${this.baseUrl}Class/getClassDetails/${academicYearId}/${academicLevelId}/${classNameId}`)
			.toPromise();
	}

	/**
	 * Class Service
	 * @param {TeacherClassFilterModel}
	 * @service getTeacherClassesByFilter
	 * @returns {Promise<BasicTeacherClassPaginatedItemModel>}
	 */
	async getTeacherClassesByFilter(filter: TeacherClassFilterModel): Promise<BasicTeacherClassPaginatedItemModel> {
		return await this._httpClient
			.post<BasicTeacherClassPaginatedItemModel>(`${this.baseUrl}Class/getTeacherClassesByFilter`, { filter: filter })
			.toPromise();
	}
}
