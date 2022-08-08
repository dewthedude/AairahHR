using System;
using System.Collections.Generic;

namespace HRSolutionsCore.Models
{
    public partial class MstCategory
    {
        public MstCategory()
        {
            MstSubCategories = new HashSet<MstSubCategory>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? AddBy { get; set; }

        public virtual ICollection<MstSubCategory> MstSubCategories { get; set; }
    }
}
