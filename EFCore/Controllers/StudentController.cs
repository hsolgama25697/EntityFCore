using EFCore.Data;
using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Controllers
{
    public class StudentController : Controller
    {
        public readonly EFCoreDBContext dBContext;
        public StudentController(EFCoreDBContext _dBContext)
        {
            this.dBContext = _dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Students(string search, int page = 1, int pagesize = 10)
        {
            search = search == null ? "" : search;
            try
            {
                List<Student> students = await dBContext.Students.Where(t => t.Name.Contains(search) ||
               t.Email.Contains(search) || t.Class.Contains(search)).ToListAsync();
                if (page < 1)
                {
                    page = 1;
                }
                int recscount = students.Count();
                var pager = new Pager(recscount, page, pagesize);
                var recskip = (page - 1) * pagesize;
                students = students.Skip(recskip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                this.ViewBag.Search = search;
                return View(students);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> StudentGrid(string search = "", int page = 1, int pagesize = 10)
        {
            search = search == null ? "" : search;
            try
            {
                List<Student> students = await dBContext.Students.Where(t => t.Name.Contains(search) ||
                t.Email.Contains(search) || t.Class.Contains(search)).ToListAsync();
                if (page < 1)
                {
                    page = 1;
                }
                int recscount = students.Count();
                var pager = new Pager(recscount, page, pagesize);
                var recskip = (page - 1) * pagesize;
                students = students.Skip(recskip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                this.ViewBag.Search = search;
                return PartialView("StudentGrid", students);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            if (student.Id == 0)
            {
                await this.dBContext.AddAsync(student);
            }
            else
            {
                var dstudent = await dBContext.Students.FindAsync(student.Id);
                if (dstudent != null)
                {
                    dstudent.RollNumber = student.RollNumber;
                    dstudent.Name = student.Name;
                    dstudent.Email = student.Email;
                    dstudent.Class = student.Class;
                    dstudent.BirthDate = student.BirthDate;
                }

            }
            await this.dBContext.SaveChangesAsync();
            return RedirectToAction("Students");
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            Student student = new Student();
            student = await dBContext.Students.FirstOrDefaultAsync(x => x.Id == id) ?? new Student();
            return View("AddStudent", student);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            Student student = new Student();
            student = await dBContext.Students.FindAsync(id) ?? new Student();
            if (student != null)
            {
                dBContext.Students.Remove(student);
                await dBContext.SaveChangesAsync();
            }
            return RedirectToAction("Students");
        }
    }
}
