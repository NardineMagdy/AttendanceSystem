using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AcademicYear { get; set; }
        public DateTime Birthday { get; set; }
        public string ParentNumber { get; set; }


        public int? EventId { get; set; }
        public virtual Events Event { get; set; }


        public List<Attendance> Attendance { get; set; } = new List<Attendance>();
        public List<QualityTime> QualityTime { get; set; } = new List<QualityTime>();
        public List<Memorization> Memorization { get; set; } = new List<Memorization>();

    }
}
