import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, flatMap, switchMap } from 'rxjs/operators';
import 'rxjs';
import { throwError as _throw } from 'rxjs';
import { SessionService } from './session.service';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  httpParams: any;
  appSettings: any;
  constructor(private http: HttpClient, private sessionService: SessionService) {
    this.httpParams = {
      headers: new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
      })
    };
  }

  get(url: string): Observable<ResponseModel> {
    return this.getConfig()
      .pipe(flatMap(appSettings => {
        this.appSettings = appSettings;
        return this.http.get<ResponseModel>(appSettings.apiUrl + url)
          .pipe(switchMap((resp) => {
            return of(resp)
          }));
      }));
  }

  post(url: string, body: any): Observable<ResponseModel> {
    return this.getConfig()
      .pipe(flatMap(appSettings => {
        this.appSettings = appSettings;
        return this.http.post<ResponseModel>(appSettings.apiUrl + url, body)
          .pipe(switchMap((resp) => {
            return of(resp)
          }));
      }));
  }

  getConfig(): Observable<any> {
    if (this.appSettings && this.appSettings.apiUrl) {
      return of(this.appSettings);
    } else {
      return this.http.get('/assets/config.json?v=' + new Date());
    }
  }

}
