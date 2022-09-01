using FluentValidation;

namespace HRSolutionsCore.RequestModel
{
    public class AdminRegistrationReqModel
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Street { get; set; }
        public string? ProfilePic { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? AddedBy { get; set; }
    }
    public class AdminLoginReqModel
    {
        public string? UserName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string? Password { get; set; }
    }
    public class AdminLoginReqModelValidator : AbstractValidator<AdminLoginReqModel>
    {
        public AdminLoginReqModelValidator()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Please provide email or mobile no");
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Please enter password");
        }
    }
}

