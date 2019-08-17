import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment as env } from './../../environments/environment';
import { Observable } from 'rxjs';
import { LongUrl } from '../models/LongUrl';

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

  shortenUrl(url: LongUrl): Observable<object> {
    return this.http.post(this.baseUrl, url);
  }
}
