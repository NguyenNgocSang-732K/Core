using System;
using System.Collections.Generic;

namespace Project.Models.Entities
{
    public partial class Product
    {
        public Product()
        {
            InvoiceDetail = new HashSet<InvoiceDetail>();
            ProductPhoto = new HashSet<ProductPhoto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Subcateid { get; set; }
        public int Unitid { get; set; }
        public string Description { get; set; }
        public DateTime Daycreate { get; set; }
        public DateTime Dayedited { get; set; }
        public string Color { get; set; }
        public int? EditerId { get; set; }
        public bool Active { get; set; }
        public bool Status { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetail { get; set; }
        public virtual ICollection<ProductPhoto> ProductPhoto { get; set; }
    }
}
