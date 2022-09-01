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
        public AddUpdateDeleteResponse AddCategory(MasterCategoryModel req)
        {
            int i = 0;
            bool isExist = _dataContext.MstCategories.Any(x => x.Name == req.Name);
            if (isExist)
            {
                return new AddUpdateDeleteResponse { Message = "Category already exist", Success = false, Data = "" };
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
                return new AddUpdateDeleteResponse { Message = "Category added successfully", Success = true, Data = "" };
            }
            return new AddUpdateDeleteResponse { Message = "Failed to insert category", Success = false, Data = "" };
        }
        public bool CheckCategory(MasterCategoryModel req)
        {
            bool isExist = _dataContext.MstCategories.Any(x => x.Name == req.Name);
            return isExist;
        }
        public AddUpdateDeleteResponse GetCategory()
        {
            var CatList = _dataContext.MstCategories.ToList();
            if (CatList != null)
            {
                return new AddUpdateDeleteResponse { Message = "Data found successfully", Success = true, Data = CatList };
            }
            return new AddUpdateDeleteResponse { Data = "", Message = "Data not found", Success = false };
        }
        public AddUpdateDeleteResponse ChangeCategoryStatus(CategoryStatusModel req)
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
                    return new AddUpdateDeleteResponse { Data = "", Message = "Category status updated successfully", Success = true };
                }
            }
            return new AddUpdateDeleteResponse { Data = "", Message = "Failed to update status", Success = true };
        }
        public AddUpdateDeleteResponse DeleteCategory(int id)
        {
            var catDetails = _dataContext.MstCategories.First(x => x.Id == id);
            bool isUsed = _dataContext.MstSubCategories.Any(x => x.IdCategory == id);
            if (isUsed)
            {
                return new AddUpdateDeleteResponse { Data = "", Success = false, Message = catDetails.Name + " having a sub_categoyr name please remove it first " };
            }
            if (catDetails != null)
            {
                _dataContext.MstCategories.Remove(catDetails);
                _dataContext.SaveChanges();
                return new AddUpdateDeleteResponse { Data = catDetails, Message = "Category remove successfully", Success = true };
            }
            return new AddUpdateDeleteResponse { Data = "", Message = "failed to remove category", Success = false };
        }
        #endregion
        #region SubCategory
        #endregion
        public AddUpdateDeleteResponse AddSubCategory(MasterSubCategoryModel req)
        {
            int i = 0;
            bool isExist = _dataContext.MstSubCategories.Any(x => x.Name == req.Name);
            if (isExist)
            {
                return new AddUpdateDeleteResponse { Message = "SubCategory already exist", Success = false, Data = "" };
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
                return new AddUpdateDeleteResponse { Message = "SubCategory added successfully", Success = true, Data = "" };
            }
            return new AddUpdateDeleteResponse { Message = "Failed to insert Subcategory", Success = false, Data = "" };
        }
        //getting list of all category
        public AddUpdateDeleteResponse GetSubCategory()
        {
            var subCatDetails = _dataContext.MstCategories.Include("MstSubCategory").ToList();

            if (subCatDetails.Count > 0)
            {
                return new AddUpdateDeleteResponse { Message = "SubCategory found", Data = subCatDetails, Success = true };
            }
            return new AddUpdateDeleteResponse { Message = "SubCategory not found", Data = "", Success = false };
        }
        //getting list of all active categories
        public AddUpdateDeleteResponse GetActiveCategory()
        {
            var activeCategories = _dataContext.MstCategories.Where(x => x.Status == true).ToList();
            if (activeCategories.Count > 0)
            {
                return new AddUpdateDeleteResponse { Message = "Active Categories found", Data = activeCategories, Success = true };
            }
            return new AddUpdateDeleteResponse { Message = "active categories not found", Data = activeCategories, Success = false };
        }


        public AddUpdateDeleteResponse ActivateSubCategory(SubCategoryStatusModel req)
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
                    return new AddUpdateDeleteResponse { Data = "", Message = subCatDetails.Name + ": status updated successfully", Success = true };
                }
            }
            return new AddUpdateDeleteResponse { Data = "", Message = "Failed to update status", Success = true };
        }
    }
}