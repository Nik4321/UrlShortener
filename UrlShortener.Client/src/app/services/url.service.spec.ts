import { TestBed } from '@angular/core/testing';

import { UrlService } from './url.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { LongUrlModel } from '../models/input/long-url.model';
import { environment } from 'src/environments/environment';

describe('UrlService', () => {
	let service: UrlService;
	let httpMock: HttpTestingController;

	beforeEach(() => {
		TestBed.configureTestingModule({
			imports: [HttpClientTestingModule],
			providers: [UrlService]
		});

		service = TestBed.get(UrlService);
		httpMock = TestBed.get(HttpTestingController);
	});

	afterEach(() => {
		httpMock.verify();
	});

	it('should be created', () => {
		expect(service).toBeDefined();
	});

	it('should shorten url', () => {
		const googleUrl = 'http://google.com';
		const longUrl = new LongUrlModel(googleUrl);
		service.shortenUrl(longUrl).subscribe((response) => {
			expect(response.longUrl).toBe(googleUrl);
			expect(response.shortUrl).not.toBeNull();
			expect(response.shortUrl).not.toBeNull();
		});

		const req = httpMock.expectOne(
			environment.apiUrl,
			'post to shorten url endpoint'
		);
		expect(req.request.method).toBe('POST');

		req.flush({
			longUrl: googleUrl,
			shortUrl: 'localhost:5000/9ht74kb5',
		});
	});
});
