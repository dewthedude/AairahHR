using System;
using System.Collections.Generic;

namespace HRSolutionsCore.Models
{
    public partial class MstSubCategory
    {
        public MstSubCategory()
        {
            MstSubSubCategories = new HashSet<MstSubSubCategory>();
        }

        public int Id { get; set; }
        public int? IdCategory { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? AddBy { get; set; }

        public virtual MstCategory? IdCategoryNavigation { get; set; }
        public virtual ICollection<MstSubSubCategory> MstSubSubCategories { get; set; }
    }
}
