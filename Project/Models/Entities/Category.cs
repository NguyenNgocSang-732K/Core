using System;
using System.Collections.Generic;

namespace Project.Models.Entities
{
    public partial class Category
    {
        public Category()
        {
            SubCategory = new HashSet<SubCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<SubCategory> SubCategory { get; set; }
    }
}
