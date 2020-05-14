using Project.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
    public class InvoiceView
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int CusId { get; set; }
        public string CusName { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public DateTime DayCreate { get; set; }
        public decimal Total { get; set; }
        public bool Status { get; set; }
    }
}
