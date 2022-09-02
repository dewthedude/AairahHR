import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AdminLoginModel } from './pages-model';
@Injectable({
  providedIn: 'root'
})
export class PagesService {
  private baseURL = "https://localhost:7052";

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private httpClient: HttpClient) { }
  //Adding Category
  adminLogIn(adminLoginDetails: AdminLoginModel): Observable<any> {
    debugger
    return this.httpClient.post<AdminLoginModel>(this.baseURL + '/api/Token/adminLogin/', adminLoginDetails)
      .pipe(catchError(this.errorHandler))
  }
  //Handling error
  errorHandler(error: any) {
    debugger
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      debugger
      errorMessage = error.error.message;
    } else {
      debugger
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      alert(error.error)
      return throwError(errorMessage);
  
    }
    alert(error.error.message)
    return throwError(errorMessage);
  }
}
