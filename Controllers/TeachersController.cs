using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Data.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace AttendanceSystem.Controllers
{
    [Authorize]
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
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
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        } 
        public IActionResult CreateEvent()
        {
           EventViewModel SelectList = new EventViewModel();
           MultiSelectList StudentList = new MultiSelectList(_context.Students.ToList(), "Id", "Name");
           SelectList.Students = StudentList;
            return View(SelectList);
        }
        // CreateEvent 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(EventViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                foreach(var studentId in viewModel.StudentID)
                {
                    var student = _context.Students.SingleOrDefault(s => s.Id == studentId);
                    viewModel.Event.Students.Add(student);
                }
                 _context.Add(viewModel.Event);
                await _context.SaveChangesAsync();
                return RedirectToAction("EventIndex");
            }
            return RedirectToAction("EventIndex");
          

        }
        public async Task<IActionResult> EventIndex()
        {
            return View(await _context.Events.ToListAsync());
        }

        public async Task<IActionResult> EventDetails(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newevent = await _context.Events.Include(e => e.Students).FirstOrDefaultAsync(m => m.Id == id);
            if (newevent == null)
            {
                return NotFound();
            }

            return View(newevent);
        }


    //public IActionResult MultiSelectListForStudent(ClassViewModelcs SelectList)
    //{
    //    return View ( );
    //}
    }
}
