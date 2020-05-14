using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
    public class UserView
    {
        public int Id { get; set; }
        [Required, DataType(DataType.EmailAddress), StringLength(50, MinimumLength = 5)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Photo { get; set; }
        [Required]
        public int Gender { get; set; }
        public DateTime DayCreate { get; set; }
        public DateTime DayEdited { get; set; }
        public int EditerId { get; set; }
        [Required]
        public int Role { get; set; }
        [Required]
        public bool Active { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
    }
}
