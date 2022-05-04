import { Injectable } from '@angular/core'
import {
  HttpClient,
  HttpRequest,
  HttpEventType,
  HttpResponse,
  HttpHeaders,
} from '@angular/common/http'
import { Subject, Observable, throwError } from 'rxjs'
import { catchError } from 'rxjs/operators'
import { environment } from 'src/environments/environment'

@Injectable()
export class ApiClient {
  url = '';
  
  constructor(private http: HttpClient) {
    this.url = environment.commoditiesApi;
  }

  get<T>(endpointUrl:string) : Observable<T>
  {
    return this.http.get<T>(this.url + endpointUrl).pipe(catchError(error => {
        console.log(error);
        return throwError(error);
    }));
  }
}