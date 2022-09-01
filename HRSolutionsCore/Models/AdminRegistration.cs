using System;
using System.Collections.Generic;

namespace HRSolutionsCore.Models
{
    public partial class AdminRegistration
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Mobile { get; set; }
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
}
