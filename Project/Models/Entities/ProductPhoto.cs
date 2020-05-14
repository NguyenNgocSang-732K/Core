using System;
using System.Collections.Generic;

namespace Project.Models.Entities
{
    public partial class ProductPhoto
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public int Proid { get; set; }
        public bool Main { get; set; }

        public virtual Product Pro { get; set; }
    }
}
