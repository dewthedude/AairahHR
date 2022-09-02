import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  token: any = '';
  
  constructor(private httpClient: HttpClient) { 
    this.token = localStorage.getItem('token');
  }
  public saveData(token: string, expiry: any) {
    
    localStorage.setItem("token",token);
    localStorage.setItem("expiry",expiry);
  }
  public getData(key: string) {
    debugger
    return localStorage.getItem(key)
  }
  public removeData(key: string) {
    localStorage.removeItem(key);
  }
  public clearData() {
    localStorage.clear();
  }


  //**********************Default headers with access token*****************************/
  getDefaultHeaders() {
    const HTTP_OPTIONS = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.token}`
      })
    };
    debugger
    return HTTP_OPTIONS
  }  
}