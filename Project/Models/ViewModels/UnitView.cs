using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
    public class UnitView
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string NameConvert { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
    }
}
