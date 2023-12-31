import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LessonUnitAddServiceService {
  baseUrl = environment.eduArkApiUrl;

  /**
	 * Constructor
	 * @param {HttpClient} _httpClient
	 */
  constructor(private _httpClient: HttpClient) {}
}
