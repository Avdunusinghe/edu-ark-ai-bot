import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ResultModel } from '../models/common/result.model';
import { LessonDetailsModel } from '../models/lesson.units/lesson.details.model';
import { Observable } from 'rxjs';
import { Upload, upload } from '../models/common/upload';

@Injectable({
  providedIn: 'root'
})
export class LessonService {

  baseUrl = environment.eduArkApiUrl;

  /**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
    constructor(private _httpClient: HttpClient) {}

  /**
	 * Units Service
	 * @param {number}
	 * @service deleteLessonUnit
	 * @returns {Promise<ResultModel>}
	 */
    async deleteLessonUnit (id : number) :Promise<ResultModel>{
        return await this._httpClient
            .delete<ResultModel>(`${this.baseUrl}Lesson/deleteLesson/${id}`)
            .toPromise();
    }

	/**
	 * Units Service
	 * @param {}
	 * @service getLessonUnits
	 * @returns {Promise<LessonDetailsModel[]>}
	 */
    async getLessonUnits() : Promise<LessonDetailsModel[]>{
        return await this._httpClient
        .get<LessonDetailsModel[]>(`${this.baseUrl}Lesson/getLesson`)
        .toPromise();
    }


  /**
	 * Units Service
	 * @param {}
	 * @service getByIdLessonUnits
	 * @returns {Promise<LessonDetailsModel>}
	 */
      async getByIdLessonUnits(id : number) : Promise<LessonDetailsModel>{
        return await this._httpClient
        .get<LessonDetailsModel>(`${this.baseUrl}Lesson/getBYIdLesson/${id}`)
        .toPromise();
    }

  /**
   * Units Service
   * @param {number} id - The ID of the lesson unit to be updated.
   * @param {LessonDetailsModel} updatedLesson - The updated lesson data.
   * @service updateLessonUnit
   * @returns {Promise<ResultModel>}
   */
  async updateLessonUnit(id : number, updatedLesson : LessonDetailsModel) : Promise<ResultModel> {
    return await this._httpClient
        .put<ResultModel>(`${this.baseUrl}Lesson/updateLesson/${id}`, updatedLesson)              
        .toPromise();
  }

  /**
   * Units Service
   * @param {LessonDetailsModel} newLesson - The new lesson data to be saved.
   * @service saveLessonUnit
   * @returns {Promise<ResultModel>}
   */
  async saveLessonUnit (newLesson: LessonDetailsModel) : Promise<ResultModel> {
    return await this._httpClient
        .post<ResultModel>(`${this.baseUrl}Lesson/saveLesson`, newLesson)
        .toPromise();
  }

  /**
	 * Lesson Service
	 * @param {FormData}
	 * @service uploadLessonFile
	 * @returns {Observable<Upload>}
	 */
  uploadLessonFile(formData: FormData) : Observable<Upload>{
    return this._httpClient
        .post<ResultModel>(`${this.baseUrl}Lesson/uploadLessonFile`, formData, {
          reportProgress: true,
          observe: 'events',
        })
        .pipe(upload());
  }

}
