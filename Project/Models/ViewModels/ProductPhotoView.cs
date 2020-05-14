using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
    public class ProductPhotoView
    {
        public int Id{ get; set; }
        public string Photo { get; set; }
        public int ProId { get; set; }
        public bool Main { get; set; }
    }
}
