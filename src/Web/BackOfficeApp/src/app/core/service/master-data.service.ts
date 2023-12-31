import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TenantMasterDataModel } from './../models/tenant/tenant.master.data.model';

@Injectable({
	providedIn: 'root',
})
export class MasterDataService {
	baseUrl = environment.apiUrl;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */

	constructor(private _httpClient: HttpClient) {}

	/**
	 * MasterData Service
	 * @param {}
	 * @service getTenantsByFilter
	 * @returns {Promise<TenantMasterDataModel>}
	 */

	async getTenantMasterData(): Promise<TenantMasterDataModel> {
		return await this._httpClient
			.get<TenantMasterDataModel>(`${this.baseUrl}MasterData/getTenantMasterData`)
			.toPromise();
	}
}
