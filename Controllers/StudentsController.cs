using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Data;
using AttendanceSystem.Models;

namespace AttendanceSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

     
        
            var student = await _context.Students
            .FirstOrDefaultAsync(m => m.Id == id);
            student.Attendance = _context.Attendances.Where(a => a.StudentId == student.Id).ToList();
            student.QualityTime = _context.QualityTime.Where(a => a.StudentId == student.Id).ToList();
            student.Memorization = _context.Memorizations.Where(a => a.StudentId == student.Id).ToList();



            { return View(student); }
           
           
       

        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AcademicYear,Birthday,ParentNumber")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AcademicYear,Birthday")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
        public async Task<IActionResult> TakeAttendanceAsync()
        {
            var students = await _context.Students.ToListAsync();
            foreach (var student in students)
            {
                student.Attendance = await _context.Attendances.Where(a => a.StudentId == student.Id).ToListAsync();
                student.Attendance = Get3MonthsAttendance(student.Attendance);
            }

            return View(students);
        }
        public async Task<IActionResult> MemorizationAsync()
        {
            var Memo = await _context.Students.ToListAsync();
            foreach (var student in Memo)
            {
                student.Memorization = await _context.Memorizations.Where(a => a.StudentId == student.Id).ToListAsync();
                student.Memorization = GetMemorization(student.Memorization);
            }

            return View(Memo);
        }
        public async Task<IActionResult> QualityTimeAsync()
        {
            var Qualitytime = await _context.Students.ToListAsync();
            foreach (var student in Qualitytime)
            {
                student.QualityTime = await _context.QualityTime.Where(a => a.StudentId == student.Id).ToListAsync();
                student.QualityTime = GetQualityTime(student.QualityTime);
            }

            return View(Qualitytime);
        }
        public async Task<IActionResult> StudentAttendance(int id)
        {
            Attendance attend = new Attendance();
            attend.Student = _context.Students.SingleOrDefault(S => S.Id == id);
            attend.Time = DateTime.Now;
            _context.Attendances.Add(attend);
            _context.SaveChanges();
            TempData["Attendance"] = "taken";
            TempData["CheckButton"] = id.ToString();
            return RedirectToAction("TakeAttendance");
        }

        private List<Attendance> Get3MonthsAttendance(List<Attendance> Attendances)
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now;

            if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
            {
                TempData["Region"] = "January to March";
                fromDate = new DateTime(DateTime.Now.Year, 1, 1);
                toDate = new DateTime(DateTime.Now.Year, 3, 30);
            }
            else if (DateTime.Now.Month == 4 || DateTime.Now.Month == 5 || DateTime.Now.Month == 6)
            {
                TempData["Region"] = "April to June";

                fromDate = new DateTime(DateTime.Now.Year, 4, 1);
                toDate = new DateTime(DateTime.Now.Year, 6, 30);
            }
            else if (DateTime.Now.Month == 7 || DateTime.Now.Month == 8 || DateTime.Now.Month == 9)
            {
                TempData["Region"] = "July to September";
                fromDate = new DateTime(DateTime.Now.Year, 7, 1);
                toDate = new DateTime(DateTime.Now.Year, 9, 30);
            }
            else if (DateTime.Now.Month == 10 || DateTime.Now.Month == 11 || DateTime.Now.Month == 12)
            {
                TempData["Region"] = "October to December";

                fromDate = new DateTime(DateTime.Now.Year, 10, 1);
                toDate = new DateTime(DateTime.Now.Year, 12, 30);
            }
            List<Attendance> returnList = new List<Attendance>();

            foreach (var attendance in Attendances)
            {
                if (attendance.Time >= fromDate && attendance.Time <= toDate)
                {
                    returnList.Add(attendance);
                }
            }
            return returnList;
        }
        public async Task<IActionResult> TakeQualityTime(int id)
        {
            QualityTime time = new QualityTime();
            time.Student = _context.Students.SingleOrDefault(S => S.Id == id);
            time.Time = DateTime.Now;
            _context.QualityTime.Add(time);
            _context.SaveChanges();
            TempData["QualityTime"] = "taken";
            TempData["CheckButton"] = id.ToString();
            return RedirectToAction("QualityTime");
        }
        public async Task<IActionResult> TakeMemorization(int id)
        {
            Memorization mem = new Memorization();
            mem.Student = _context.Students.SingleOrDefault(S => S.Id == id);
            mem.Time = DateTime.Now;
            _context.Memorizations.Add(mem);
            _context.SaveChanges();
            TempData["Memorization"] = "taken";
            TempData["CheckButton"] = id.ToString();
            return RedirectToAction("Memorization");
        }

        private List<QualityTime> GetQualityTime(List<QualityTime> qualityTimes)
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now;

            if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
            {
                TempData["Region"] = "January to March";
                fromDate = new DateTime(DateTime.Now.Year, 1, 1);
                toDate = new DateTime(DateTime.Now.Year, 3, 30);
            }
            else if (DateTime.Now.Month == 4 || DateTime.Now.Month == 5 || DateTime.Now.Month == 6)
            {
                TempData["Region"] = "April to June";

                fromDate = new DateTime(DateTime.Now.Year, 4, 1);
                toDate = new DateTime(DateTime.Now.Year, 6, 30);
            }
            else if (DateTime.Now.Month == 7 || DateTime.Now.Month == 8 || DateTime.Now.Month == 9)
            {
                TempData["Region"] = "July to September";
                fromDate = new DateTime(DateTime.Now.Year, 7, 1);
                toDate = new DateTime(DateTime.Now.Year, 9, 30);
            }
            else if (DateTime.Now.Month == 10 || DateTime.Now.Month == 11 || DateTime.Now.Month == 12)
            {
                TempData["Region"] = "October to December";

                fromDate = new DateTime(DateTime.Now.Year, 10, 1);
                toDate = new DateTime(DateTime.Now.Year, 12, 30);
            }
            List<QualityTime> returnList = new List<QualityTime>();

            foreach (var QA in qualityTimes)
            {
                if (QA.Time >= fromDate && QA.Time <= toDate)
                {
                    returnList.Add(QA);
                }
            }
            return returnList;
        }

        private List<Memorization> GetMemorization(List<Memorization> memos)
        {
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now;

            if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
            {
                TempData["Region"] = "January to March";
                fromDate = new DateTime(DateTime.Now.Year, 1, 1);
                toDate = new DateTime(DateTime.Now.Year, 3, 30);
            }
            else if (DateTime.Now.Month == 4 || DateTime.Now.Month == 5 || DateTime.Now.Month == 6)
            {
                TempData["Region"] = "April to June";

                fromDate = new DateTime(DateTime.Now.Year, 4, 1);
                toDate = new DateTime(DateTime.Now.Year, 6, 30);
            }
            else if (DateTime.Now.Month == 7 || DateTime.Now.Month == 8 || DateTime.Now.Month == 9)
            {
                TempData["Region"] = "July to September";
                fromDate = new DateTime(DateTime.Now.Year, 7, 1);
                toDate = new DateTime(DateTime.Now.Year, 9, 30);
            }
            else if (DateTime.Now.Month == 10 || DateTime.Now.Month == 11 || DateTime.Now.Month == 12)
            {
                TempData["Region"] = "October to December";

                fromDate = new DateTime(DateTime.Now.Year, 10, 1);
                toDate = new DateTime(DateTime.Now.Year, 12, 30);
            }
            List<Memorization> returnList = new List<Memorization>();

            foreach (var Memo in memos)
            {
                if (Memo.Time >= fromDate && Memo.Time <= toDate)
                {
                    returnList.Add(Memo);
                }
            }
            return returnList;
        }

    }
}
