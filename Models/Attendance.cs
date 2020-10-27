using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int StudentId { get; set; }
        public  virtual Student Student { get; set; } = new Student();
    }
}
