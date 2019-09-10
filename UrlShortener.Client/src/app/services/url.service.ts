import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment as env } from './../../environments/environment';
import { Observable } from 'rxjs';
import { LongUrlModel } from '../models/input/long-url.model';
import { UrlResponseModel } from '../models/view/url-response.model';

@Injectable({
	providedIn: 'root'
})
export class UrlService {
	private baseUrl: string;

	constructor(
		private http: HttpClient
	) {
		this.baseUrl = env.apiUrl;
	}

	public shortenUrl(url: LongUrlModel): Observable<UrlResponseModel> {
		return this.http.post<UrlResponseModel>(this.baseUrl, url);
	}
}
