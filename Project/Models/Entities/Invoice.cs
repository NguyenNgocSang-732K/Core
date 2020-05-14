using System;
using System.Collections.Generic;

namespace Project.Models.Entities
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetail = new HashSet<InvoiceDetail>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int Cusid { get; set; }
        public int Empid { get; set; }
        public DateTime Daycreate { get; set; }
        public decimal Total { get; set; }
        public bool Status { get; set; }

        public virtual User Cus { get; set; }
        public virtual User Emp { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetail { get; set; }
    }
}
