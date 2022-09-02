
import { PagesService } from '../pages.service';
import { AdminLoginModel } from '../pages-model'
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CommonService } from '../../../commonServices/common.service'
import { AuthGuardServiceService } from 'src/app/authGuardService/auth-guard-service.service';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  pagesModel: AdminLoginModel = new AdminLoginModel();
  constructor(private _pagesService: PagesService, private _commonService: CommonService,private _auth: AuthGuardServiceService, private _router: Router) {
  }
  aany: any;
  ngOnInit(): void {
    // this.aany = this._commonService.getData("token");
    // alert(this.aany)
  }
  onLogin(form: NgForm) {
    console.log(form)
    this._pagesService.adminLogIn(form.value).subscribe(res => {
      console.log(res)
      if (res.success) {
        this._commonService.clearData();
        this._commonService.saveData(res.data.token, res.data.expiration)
        this._router.navigate(["dashboard"]);  
      }
      debugger
      alert("Your access token is :"+this._commonService.getData("token")+"and expiry date is"+this._commonService.getData("expiry"));
    })
  }
}