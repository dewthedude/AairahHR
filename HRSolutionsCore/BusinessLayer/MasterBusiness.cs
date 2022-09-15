using HRSolutionsCore.ResponseModel;
using HRSolutionsCore.Models;
using HRSolutionsCore.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace HRSolutions.BusinessLayer
{
    //public interface IMasterBusiness
    //{
    //    AddUpdateDeleteResponse AddCategory(MasterCategory req);
    //}
    public class MasterBusiness
    {
        private readonly HRManagementDbContext _dataContext;
        public MasterBusiness(HRManagementDbContext dataContext)
        {
            this._dataContext = dataContext;
        }
        #region Category
        public responseModel<addUpdateDeleteResponse, errorResponseModel> AddCategory(MasterCategoryModel req)
        {
            int i = 0;
            bool isExist = _dataContext.MstCategories.Any(x => x.Name == req.Name);
            if (isExist)
            {
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { code = "400", message = "Category already exist", Success = false } } };
            }
            MstCategory _mstCategory = new MstCategory();
            _mstCategory.AddBy = "Admin";
            _mstCategory.CreatedDate = DateTime.Now;
            _mstCategory.Status = false;
            _mstCategory.Name = req.Name;
            _dataContext.MstCategories.Add(_mstCategory);
            i = _dataContext.SaveChanges();
            if (i > 0)
            {
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { successResponse = new addUpdateDeleteResponse { Data = "", Message = "Category added successfully", Success = true } };
            }
            return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { code = "400", message = "Failed to insert category", Success = false } } };
        }
        public bool CheckCategory(MasterCategoryModel req)
        {
            bool isExist = _dataContext.MstCategories.Any(x => x.Name == req.Name);
            return isExist;
        }
        public responseModel<addUpdateDeleteResponse, errorResponseModel> GetCategory()
        {
            var CatList = _dataContext.MstCategories.ToList();
            if (CatList != null)
            {
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { successResponse = new addUpdateDeleteResponse { Data = CatList, Message = "Data found successfully", Success = true } };
            }
            return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { code = "400", message = "Data not found", Success = false } } };
        }
        public responseModel<addUpdateDeleteResponse, errorResponseModel> ChangeCategoryStatus(CategoryStatusModel req)
        {
            var catDetails = _dataContext.MstCategories.FirstOrDefault(x => x.Id == req.id);
            if (catDetails != null)
            {
                int i = 0;
                if (catDetails.Status == true)
                {
                    catDetails.Status = false;
                }
                else
                {
                    catDetails.Status = true;
                }
                catDetails.UpdateDate = DateTime.Now;
                _dataContext.MstCategories.Update(catDetails);
                i = _dataContext.SaveChanges();
                if (i > 0)
                {
                    return new responseModel<addUpdateDeleteResponse, errorResponseModel> { successResponse = new addUpdateDeleteResponse { Data = "", Message = "Category status updated successfully", Success = true } };

                }

            }
            return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { code = "400", message = "Faile to changes status", Success = false } } };
        }
        public responseModel<addUpdateDeleteResponse, errorResponseModel> DeleteCategory(int id)
        {
            var catDetails = _dataContext.MstCategories.First(x => x.Id == id);
            bool isUsed = _dataContext.MstSubCategories.Any(x => x.IdCategory == id);
            if (isUsed)
            {
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { Success = false, message = catDetails.Name + " having a sub_categoyr name please remove it first ", code = "400" } } };
            }
            if (catDetails != null)
            {
                _dataContext.MstCategories.Remove(catDetails);
                _dataContext.SaveChanges();
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { successResponse = new addUpdateDeleteResponse { Data = catDetails, Message = "Category remove successfully", Success = true } };
            }
            return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { message = "failed to remove category", Success = false, code = "400" } } };
        }
        #endregion
        #region SubCategory
        #endregion
        public responseModel<addUpdateDeleteResponse, errorResponseModel> AddSubCategory(MasterSubCategoryModel req)
        {
            int i = 0;
            bool isExist = _dataContext.MstSubCategories.Any(x => x.Name == req.Name);
            if (isExist)
            {
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { message = "SubCategory already exist", Success = false, code = "400" } } };
            }
            MstSubCategory _mstSubCategory = new MstSubCategory();
            _mstSubCategory.AddBy = "Admin";
            _mstSubCategory.CreatedDate = DateTime.Now;
            _mstSubCategory.UpdateDate = DateTime.Now;
            _mstSubCategory.IdCategory = req.IdCategory;
            _mstSubCategory.Name = req.Name;
            _mstSubCategory.Status = false;
            _dataContext.MstSubCategories.Add(_mstSubCategory);
            i = _dataContext.SaveChanges();
            if (i > 0)
            {
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { successResponse = { Message = "SubCategory added successfully", Success = true, Data = "" } };
            }
            return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { message = "Failed to insert Subcategory", Success = false, code = "400" } } };
        }
        //getting list of all category
        public responseModel<addUpdateDeleteResponse, errorResponseModel> GetSubCategory()
        {
            var subCatDetails = (from t1 in _dataContext.MstCategories
                                 join
                                 t2 in _dataContext.MstSubCategories
                                 on t1.Id equals t2.IdCategory
                                 select new
                                 {
                                     catName = t1.Name,
                                     subCatName = t2.Name,
                                     Id = t2.Id,
                                     Status = t2.Status,
                                     AddBy = t2.AddBy,
                                     CreatedDate = t2.CreatedDate,
                                     UpdateDate = t2.UpdateDate,
                                 }).ToList();

            if (subCatDetails.Count > 0)
            {
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { successResponse = new addUpdateDeleteResponse { Message = "SubCategory found", Data = subCatDetails, Success = true } };
            }
            return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { message = "SubCategory not found", code = "400", Success = false } } };
        }
        //getting list of all active categories
        public responseModel<addUpdateDeleteResponse, errorResponseModel> GetActiveCategory()
        {
            var activeCategories = _dataContext.MstCategories.Where(x => x.Status == true).ToList();
            if (activeCategories.Count > 0)
            {
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { successResponse = new addUpdateDeleteResponse { Message = "Active Categories found", Data = activeCategories, Success = true } };
            }
            return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { message = "active categories not found", code = "400", Success = false } } };
        }
        public responseModel<addUpdateDeleteResponse,errorResponseModel> ActivateSubCategory(SubCategoryStatusModel req)
        {
            var subCatDetails = _dataContext.MstSubCategories.FirstOrDefault(x => x.Id == req.id);
            if (subCatDetails != null)
            {
                int i = 0;
                if (subCatDetails.Status == true)
                {
                    subCatDetails.Status = false;
                }
                else
                {
                    subCatDetails.Status = true;
                }
                subCatDetails.UpdateDate = DateTime.Now;
                _dataContext.MstSubCategories.Update(subCatDetails);
                i = _dataContext.SaveChanges();
                if (i > 0)
                {
                    return new responseModel<addUpdateDeleteResponse, errorResponseModel> {successResponse = new addUpdateDeleteResponse { Data = "", Message = subCatDetails.Name + ": status updated successfully", Success = true } };
                }
            }
            return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { code = "400", message = "Failed to update status", Success = false } } };
        }
        public responseModel<addUpdateDeleteResponse,errorResponseModel> DeleteSubCategory(int id)
        {
            var subCatDetails = _dataContext.MstSubCategories.First(x => x.Id == id);
            bool isUsed = _dataContext.MstSubSubCategories.Any(x => x.IdSubCategory == id);
            if (isUsed)
            {
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { code = "400", Success = false, message = subCatDetails.Name + " having a sub_categoyr name please remove it first " } } };
            }
            if (subCatDetails != null)
            {
                _dataContext.MstSubCategories.Remove(subCatDetails);
                _dataContext.SaveChanges();
                return new responseModel<addUpdateDeleteResponse, errorResponseModel> { successResponse = new addUpdateDeleteResponse { Data = "", Message = "sub category remove successfully", Success = true } };
            }
            return new responseModel<addUpdateDeleteResponse, errorResponseModel> { errorResponse = new errorResponseModel { error = new errorModel { code = "400", message = "failed to remove subcategory", Success = false } } };
        }
    }
}