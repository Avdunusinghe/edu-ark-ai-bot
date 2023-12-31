import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationResponseModel } from './../models/authentication/authentication.response.model';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { AuthenticationModel } from './../models/authentication/authentication.model';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
@Injectable({
	providedIn: 'root',
})
export class AuthenticationService {
	baseUrl = environment.eduArkApiUrl;
	private currentUserSubject: BehaviorSubject<AuthenticationResponseModel>;
	public currentUser: Observable<AuthenticationResponseModel>;

	/**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */

	constructor(private _httpClient: HttpClient) {
		this.currentUserSubject = new BehaviorSubject<AuthenticationResponseModel>(
			JSON.parse(localStorage.getItem('eduArkCurrentUser'))
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
	/* async login(authenticationModel: AuthenticationModel): Promise<AuthenticationResponseModel> {
		return await this._httpClient
			.post<AuthenticationResponseModel>(`${this.baseUrl}Authentication/login`, authenticationModel)
			.pipe(
				map((eduArkCurrentUser) => {
					localStorage.setItem('eduArkCurrentUser', JSON.stringify(eduArkCurrentUser));
					this.currentUserSubject.next(eduArkCurrentUser);
					return eduArkCurrentUser;
				})
			)
			.toPromise();
	} */

	login(authenticationModel: AuthenticationModel): Observable<AuthenticationResponseModel> {
		return this._httpClient
			.post<AuthenticationResponseModel>(`${this.baseUrl}Authentication/login`, authenticationModel)
			.pipe(
				map((user) => {
					// store user details and jwt token in local storage to keep user logged in between page refreshes
					// console.log(JSON.stringify(user));
					localStorage.setItem('eduArkCurrentUser', JSON.stringify(user));
					this.currentUserSubject.next(user);
					return user;
				})
			);
	}

	/**
	 * Authetication Service
	 * @param {}
	 * @service log out user
	 * @returns {}
	 */
	async logout() {
		// remove user from local storage to log user out
		localStorage.removeItem('eduArkCurrentUser');
		this.currentUserSubject.next(null);
		return of({ success: false });
	}
}
