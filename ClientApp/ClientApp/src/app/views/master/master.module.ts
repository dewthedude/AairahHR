import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MasterRoutingModule } from './master-routing.module';
import { CategoryComponent } from './category/category.component';
import { SubCategoryComponent } from './sub-category/sub-category.component';
import { SubSubCategoryComponent } from './sub-sub-category/sub-sub-category.component';
 


@NgModule({
  declarations: [
    CategoryComponent,
    SubCategoryComponent,
    SubSubCategoryComponent
  ],
  imports: [
    CommonModule,
    MasterRoutingModule,FormsModule
  ]
})
export class MasterModule { }
