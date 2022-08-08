using FluentValidation;

namespace HRSolutionsCore.RequestModel
{
    public class MasterCategoryModel
    {
        public int? id { get; set; }
        public string? Name { get; set; }
    }
    public class CategoryStatusModel
    {
        public int id { get; set; }
    }
    public class CategoryStatusModelValidator :AbstractValidator<CategoryStatusModel>
    {
        public CategoryStatusModelValidator()
        {
            RuleFor(x => x.id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Category id is required..!");
        }
    }
    public class MasterCategoryValidator : AbstractValidator<MasterCategoryModel>
    {
        public MasterCategoryValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
    public class MasterSubCategoryModel
    {
        public string? Name { get; set; }
        public int IdCategory { get; set; }
    }
    public class MasterSubCategoryValidator : AbstractValidator<MasterSubCategoryModel>
    {
        public MasterSubCategoryValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Name is required");
            RuleFor(x => x.IdCategory)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Category Id is required");
        }
    }
}