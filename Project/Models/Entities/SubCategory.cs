using System;
using System.Collections.Generic;

namespace Project.Models.Entities
{
    public partial class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Cateid { get; set; }
        public bool Status { get; set; }

        public virtual Category Cate { get; set; }
    }
}
