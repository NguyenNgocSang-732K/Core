using System;
using System.Collections.Generic;

namespace Project.Models.Entities
{
    public partial class InvoiceDetail
    {
        public int Invoiceid { get; set; }
        public int Proid { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Product Pro { get; set; }
    }
}
