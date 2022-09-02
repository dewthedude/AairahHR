import { Component, OnInit } from '@angular/core';
import { SubCategoryModel } from '../imaster';
import { MasterServiceService } from '../master-service.service';
import { NgForm } from '@angular/forms';
import { GetSubCategoryModel } from '../imaster';
import { GetListSubCategoryModel } from '../imaster';
import { CommonService } from '../../../commonServices/common.service'
@Component({
  selector: 'app-sub-category',
  templateUrl: './sub-category.component.html',
  styleUrls: ['./sub-category.component.scss']
})
export class SubCategoryComponent implements OnInit {
  _subCategoryModel: SubCategoryModel = new SubCategoryModel();
  _getSubCategoryModel: GetSubCategoryModel[] = [];
  _getListSubCategoryModel: GetListSubCategoryModel[] = [];
  constructor(private _MasterService: MasterServiceService, private _commonService: CommonService) {
  }
  aany: any;
  ngOnInit(): void {
    this._MasterService.getActiveCategory().subscribe(result => {
      this._getSubCategoryModel = result.data
   
    })

    this.getAllSubCategory();
  }
  getAllSubCategory() {
    debugger
    this._MasterService.getAllSubCategory().subscribe(result => {
      debugger
      this._getListSubCategoryModel = result.data
    })
  }
  activateSubCategory(id: number): void {
    this._MasterService.activateSubCategory(id).subscribe(res => {
      console.log(res)
      alert(res.message)
      this.getAllSubCategory()
    })
  }
  onSubmit(form: NgForm) {
    debugger;
    console.log(form)
    this._MasterService.createSubCategory(form.value).subscribe(res => {
      debugger
      console.log(res)
      alert(res.message);
      this.getAllSubCategory()
    })
  }
}