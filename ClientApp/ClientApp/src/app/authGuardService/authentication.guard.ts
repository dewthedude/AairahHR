import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {
  token: any 
  expiry: any
  CurrentDate = new Date();

  constructor(private _router: Router,public datePipe: DatePipe) { }
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    
    this.token = localStorage.getItem('token');
    this.expiry = localStorage.getItem('expiry');
    const expirydate = new Date(this.expiry);

    alert(expirydate);
    if (expirydate >= this.CurrentDate) {
      return true;
    }
    this._router.navigate(['']);
    return false;
  }
}

