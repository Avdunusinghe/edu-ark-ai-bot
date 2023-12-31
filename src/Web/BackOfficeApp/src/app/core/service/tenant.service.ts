import { Injectable } from '@angular/core';
import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ResultModel } from './../models/common/result.model';
import { TenantDetailModel } from './../models/tenant/tenant.detail.model';
import { TenantFilterModel } from './../models/tenant/tenant.filter.moel';
import { TenantPaginatedItemModel } from './../models/tenant/tenant.paginated.item.model';

@Injectable({
	providedIn: 'root',
})
export class TenantService {
	baseUrl = environment.apiUrl;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */

	constructor(private _httpClient: HttpClient) {}

	/**
	 * Tenant Service
	 * @param {TenantDetailModel}
	 * @service saveTenant
	 * @returns {Promise<ResultModel>}
	 */

	async saveTenant(tenantDetail: TenantDetailModel): Promise<ResultModel> {
		return await this._httpClient.post<ResultModel>(`${this.baseUrl}Tenant/saveTenant`, tenantDetail).toPromise();
	}

	/**
	 * Tenant Service
	 * @param {TenantFilterModel}
	 * @service getTenantsByFilter
	 * @returns {Promise<TenantPaginatedItemModel>}
	 */
	async getTenantsByFilter(filter: TenantFilterModel): Promise<TenantPaginatedItemModel> {
		return await this._httpClient
			.post<TenantPaginatedItemModel>(`${this.baseUrl}Tenant/getTenantsByFilter`, filter)
			.toPromise();
	}

	/**
	 * Tenant Service
	 * @param {number}
	 * @service getTenantById
	 * @returns {Promise<TenantDetailModel>}
	 */
	async getTenantById(id: number): Promise<TenantDetailModel> {
		return await this._httpClient.get<TenantDetailModel>(`${this.baseUrl}Tenant/getTenantById/${id}`).toPromise();
	}

	/**
	 * Tenant Service
	 * @param {number}
	 * @service validateTenantDomain
	 * @returns {Promise<ResultModel>}
	 */
	async validateTenantDomain(domain: string): Promise<ResultModel> {
		return await this._httpClient
			.post<ResultModel>(`${this.baseUrl}Tenant/validateTenantDomain`, { domain: domain })
			.toPromise();
	}

	/**
	 * Tenant Service
	 * @param {number}
	 * @service deleteTenant
	 * @returns {Promise<ResultModel>}
	 */
	async deleteTenant(id: number): Promise<ResultModel> {
		return await this._httpClient.delete<ResultModel>(`${this.baseUrl}Tenant/deleteTenant/${id}`).toPromise();
	}
}
