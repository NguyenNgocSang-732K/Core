using System;
using System.Collections.Generic;

namespace Project.Models.Entities
{
    public partial class User
    {
        public User()
        {
            InvoiceCus = new HashSet<Invoice>();
            InvoiceEmp = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public byte? Gender { get; set; }
        public DateTime DayCreate { get; set; }
        public DateTime DayEdited { get; set; }
        public int? EditerId { get; set; }
        public bool Status { get; set; }
        public byte Role { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public string Forgotpw { get; set; }

        public virtual ICollection<Invoice> InvoiceCus { get; set; }
        public virtual ICollection<Invoice> InvoiceEmp { get; set; }
    }
}
