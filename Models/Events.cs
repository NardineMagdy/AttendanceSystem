using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Models
{
    public class Events
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public virtual List<Student> Students { get; set; } = new List<Student>();

    }
}
