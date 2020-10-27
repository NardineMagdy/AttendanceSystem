using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Models
{
    public class EventViewModel
    {
        [Key]
        public int Id { get; set; }

        public Events Event { get; set; }
        public List<int> StudentID { get; set; }

        public MultiSelectList Students { get; set; }
    }
}
