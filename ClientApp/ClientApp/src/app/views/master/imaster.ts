export class CategoryModel {
id:number=0;
    Name: string = '';
}
export class SubCategoryModel {
    Name: string = '';
    IdCategory: number = 0;
}
//categoryModel list for SubCategory dropdown 
export class GetSubCategoryModel {
    name: string = '';
    id: number = 0;
}
//CategoryMaster list 
export class GetListCategoryModel {
    id: number = 0;
    name: string = '';
    status: boolean = false;
    createdDate: string = '';
    updateDate: Date = new Date();
    addBy: string = '';
}

//CategoryMaster list 
export class GetListSubCategoryModel {
    id: number = 0;
    idSubCategory:number=0;
    name: string = '';
    status: boolean = false;
    createdDate: string = '';
    updateDate: Date = new Date();
    addBy: string = '';
}