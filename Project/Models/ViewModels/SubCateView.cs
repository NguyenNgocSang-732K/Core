using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
    public class SubCateView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CateId { get; set; }
        public bool Status { get; set; }
    }
}
