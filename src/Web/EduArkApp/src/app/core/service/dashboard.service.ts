import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DashboardModel } from './../models/dashboard/dashboard.model';

@Injectable({
	providedIn: 'root',
})
export class DashboardService {
	baseUrl = environment.eduArkApiUrl;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
	constructor(private _httpClient: HttpClient) {}

	/**
	 * Dashboard Service
	 * @param {}
	 * @service getAdminDashboard
	 * @returns {Promise<DashboardModel>}
	 */
	async getAdminDashboard(): Promise<DashboardModel> {
		return await this._httpClient.get<DashboardModel>(`${this.baseUrl}DashBoard/getAdminDashboard`).toPromise();
	}
}
