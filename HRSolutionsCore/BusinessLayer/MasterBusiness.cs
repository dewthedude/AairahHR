using HRSolutionsCore.ResponseModel;
using HRSolutionsCore.Models;
using HRSolutionsCore.RequestModel;

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
                _dataContext.MstCategories.Update(catDetails);
                i = _dataContext.SaveChanges();
                if (i > 0)
                {
                    return new AddUpdateDeleteResponse { Data = "", Message = "Category status updated successfully", Success = true };
                }
            }
            return new AddUpdateDeleteResponse { Data = "", Message = "Failed to update status", Success = true };
        }
    }
}

