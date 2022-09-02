import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CategoryModel } from '../imaster';
import { MasterServiceService } from '../master-service.service';
import { GetListCategoryModel } from '../imaster';
import { ToastService } from '../../../commonServices/toast.service';
import { CommonService } from '../../../commonServices/common.service'

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {
  Category: CategoryModel = new CategoryModel()
  brererToken: string = "";
  ListCategory: GetListCategoryModel[] = [];
  constructor(private _MasterServiceService: MasterServiceService, private _toastService: ToastService, private _commonService: CommonService) { }

  ngOnInit(): void {
    this.getAllCategory();
  }

  getAllCategory() {
    this._MasterServiceService.getAllCategory().subscribe(result => {
      this.ListCategory = result.data;
      console.log(this.ListCategory)
    })
  }
  key = "mytoken"
  value = "tokenvalue";
  onSubmit(form: NgForm) {
    
    console.log(form)
    this._MasterServiceService.createCategory(form.value).subscribe(res => {
      console.log(res)
      alert(res.message);
      this.getAllCategory();
    })
  }
  onClick(any: any) {
    console.log(any)
    this._MasterServiceService.activateCategory(any).subscribe(res => {
      console.log(res)
      alert(res.message)
      this.getAllCategory()
    })
  }
  DelCategory(any: any) {
    this._MasterServiceService.DelCategory(any).subscribe(res => {
      console.log(res)
      alert(res.message)
      this.getAllCategory()

    })
  }
  showSuccess() {
    this._toastService.show('I am a success toast', { classname: 'bg-success text-light', delay: 10000 });
  }
  ngOnDestroy(): void {
    this._toastService.clear();
  }
}