import { Injectable } from '@angular/core';  
@Injectable({  
  providedIn: 'root'  
})  
export class AuthGuardServiceService {  
  login(username: string, password: string):boolean  {  
    if (username == "admin" && password == "admin") {  
      localStorage.setItem('token', "loggedin");  
      return true;  
    }  
    return false;
  } 
  logout() {  
    localStorage.removeItem('token');  
  }  
  public get loggedIn(): boolean {  
    return (localStorage.getItem('token') !== null);  
  }  
} 