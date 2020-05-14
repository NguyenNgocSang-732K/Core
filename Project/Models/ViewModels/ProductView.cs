using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
    public class ProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public SubCateView SubCategory { get; set; } // get business
        public UnitView Unit { get; set; } // get business
        public List<string> Colors { get; set; } // get business
        public string Photo { get; set; } // get business
        public List<string> ListPhoto { get; set; }
        public DateTime DayCreate { get; set; }
        public DateTime DayEdited { get; set; }
        public int EditerId { get; set; }
        public string EditerName { get; set; }
        public bool Active { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
    }
}
