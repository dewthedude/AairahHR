import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CategoryModel } from './imaster';
import { SubCategoryModel } from './imaster';
import { GetListCategoryModel } from './imaster';
import { CommonService } from 'src/app/commonServices/common.service';
@Injectable({
  providedIn: 'root'
})
export class MasterServiceService {
  private baseURL = "https://localhost:7052";
  token: any = '';
  myHeaders: any;



  constructor(private httpClient: HttpClient, private _commonService: CommonService) {
    this.token = localStorage.getItem('token');
    this.myHeaders = this._commonService.getDefaultHeaders();
  }

  //--*********************Region Category API's****************--//
  //Deleting a category 
  DelCategory(id: number): Observable<any> {
    debugger
    return this.httpClient.delete<any>(this.baseURL + '/api/Master/Category?id=' + id, this.myHeaders).pipe(catchError(this.errorHandler))
  }
  //Getting List of all categories
  getAllCategory(): Observable<any> {
    return this.httpClient.get<GetListCategoryModel[]>(this.baseURL + '/api/Master/Category', this.myHeaders).pipe(catchError(this.errorHandler))
  }
  //activating and deactivating categories
  activateCategory(id: number): Observable<any> {
    return this.httpClient.patch<any>(this.baseURL + '/api/Master/ActivateCategory', {
      "id": id
    }, this.myHeaders).pipe(catchError(this.errorHandler))
  }
  //Adding Category
  createCategory(cat: CategoryModel): Observable<any> {

    return this.httpClient.post<CategoryModel>(this.baseURL + '/api/Master/Category/', cat, this.myHeaders)
      .pipe(catchError(this.errorHandler))
  }

  //--*********************Region Sub-Category API's****************--//  
  //Getting List of all active and deactive sub categories
  getAllSubCategory(): Observable<any> {
    return this.httpClient.get<any[]>(this.baseURL + '/api/Master/SubCategory', this.myHeaders).pipe(catchError(this.errorHandler))
  }
  //Adding Sub Category
  createSubCategory(subcat: SubCategoryModel): Observable<any> {
    return this.httpClient.post<SubCategoryModel>(this.baseURL + '/api/Master/SubCategory/', subcat, this.myHeaders)
      .pipe(catchError(this.errorHandler))
  }
  //Getting list of active Category
  getActiveCategory(): Observable<any> {

    return this.httpClient.get<any[]>(this.baseURL + '/api/Master/ActiveCategory', this.myHeaders).pipe(catchError(this.errorHandler))
  }

  //activating and deactivating categories
  activateSubCategory(id: number): Observable<any> {
    ;
    return this.httpClient.patch<any>(this.baseURL + '/api/Master/ActivateSubCategory', {
      "id": id
    }, this.myHeaders).pipe(catchError(this.errorHandler))
  }
  //Handling error
  errorHandler(error: any) {
    debugger
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    if (error.error.message != null) {
      alert(error.error.message)
    }
    else {
      alert(error.error)
    }
    return throwError(errorMessage);
  }
}