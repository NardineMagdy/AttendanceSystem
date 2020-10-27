using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AttendanceSystem.Areas.Identity.Pages.Account;
using AttendanceSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<QualityTime> QualityTime { get; set; }

        public DbSet<Memorization> Memorizations { get; set; }
        public DbSet<Events> Events { get; set; }



    }
}
