using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
    public class InvoiceDTView
    {
        public int InvoiceId{ get; set; }
        public int ProId { get; set; }
        public string ProName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
