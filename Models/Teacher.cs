using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace AttendanceSystem.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string  UserId { get; set; }
        public virtual IdentityUser User { get; set; } = new IdentityUser();
    }
}
