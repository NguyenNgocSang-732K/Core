using System;
using System.Collections.Generic;

namespace Project.Models.Entities
{
    public partial class Unit
    {
        public Unit()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Nameconvert { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
