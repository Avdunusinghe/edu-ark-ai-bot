import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticationResponseModel } from './../models/authentication/authentication.response.model';
import { HttpClient } from '@angular/common/http';
import { AuthenticationModel } from './../models/authentication/authentication.model';

@Injectable({
	providedIn: 'root',
})
export class AuthenticationService {
	baseUrl = environment.apiUrl;

	private currentUserSubject: BehaviorSubject<AuthenticationResponseModel>;
	public currentUser: Observable<AuthenticationResponseModel>;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */

	constructor(private _httpClient: HttpClient) {
		this.currentUserSubject = new BehaviorSubject<AuthenticationResponseModel>(
			JSON.parse(localStorage.getItem('eduArkMasterUser'))
		);
		this.currentUser = this.currentUserSubject.asObservable();
	}

	public get currentUserValue(): AuthenticationResponseModel {
		return this.currentUserSubject.value;
	}

	/**
	 * Authetication Service
	 * @param {AuthenticationModel}
	 * @service User Authentication
	 * @returns {Promise<AuthenticationResponseModel>}
	 */
	async login(authenticationModel: AuthenticationModel): Promise<AuthenticationResponseModel> {
		return await this._httpClient
			.post<AuthenticationResponseModel>(`${this.baseUrl}Authentication/masterLogin`, authenticationModel)
			.pipe(
				map((eduArkMasterUser) => {
					localStorage.setItem('eduArkMasterUser', JSON.stringify(eduArkMasterUser));
					this.currentUserSubject.next(eduArkMasterUser);
					return eduArkMasterUser;
				})
			)
			.toPromise();
	}

	/**
	 * Authetication Service
	 * @param {}
	 * @service log out user
	 * @returns {}
	 */
	async logout() {
		// remove user from local storage to log user out
		localStorage.removeItem('eduArkMasterUser');
		this.currentUserSubject.next(null);
		return of({ success: false });
	}
}
