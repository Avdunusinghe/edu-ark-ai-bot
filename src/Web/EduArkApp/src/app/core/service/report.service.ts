import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
	providedIn: 'root',
})
export class ReportService {
	baseUrl = environment.eduArkApiUrl;
	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
	constructor(private _httpClient: HttpClient) {}

	/**
	 * Report Service
	 * @param {any}
	 * @service downloadStudentExcelTemplate
	 * @returns {Observable<Upload>}
	 */
	downloadStudentExcelTemplate(filter: any): Observable<any> {
		return this._httpClient.post(`${this.baseUrl}Report/downloadStudentExcelTemplate`, filter, {
			headers: { filedownload: '' },
			observe: 'events',
			reportProgress: true,
			responseType: 'blob',
		});
	}

	/**
	 * Report Service
	 * @param {any}
	 * @service downloadStudentTargetSettingReport
	 * @returns {Observable<Upload>}
	 */
	downloadStudentTargetSettingReport(filter: any): Observable<any> {
		return this._httpClient.post(`${this.baseUrl}Report/downloadStudentTargetSettingReport`, filter, {
			headers: { filedownload: '' },
			observe: 'events',
			reportProgress: true,
			responseType: 'blob',
		});
	}
}
