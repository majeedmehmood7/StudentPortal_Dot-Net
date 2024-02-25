using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext dbcontext;

        public TeachersController(ApplicationDbContext dbContext)
        {
            this.dbcontext = dbContext;
            
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTeacherViewModel viewmodel1)
        {
            var teacher = new Teacher
            {
                Name = viewmodel1.Name,
                Subject = viewmodel1.Subject,
                Description = viewmodel1.Description,
                Phone = viewmodel1.Phone,
            };
            await dbcontext.teachers.AddAsync(teacher);
            await dbcontext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var teacher = await dbcontext.teachers.ToListAsync();
            return View(teacher);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await dbcontext.teachers.FindAsync(id);
                return View(teacher);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Teacher viewmodel1)
        {
            var teacher = await dbcontext.teachers.FindAsync(viewmodel1.Id);
            if(teacher is not null)
            {
                teacher.Name = viewmodel1.Name;
                teacher.Subject = viewmodel1.Subject;
                teacher.Description = viewmodel1.Description;
                teacher.Phone = viewmodel1.Phone;

                await dbcontext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Teachers");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Teacher viewmodel1)
        {
            var teacher = await dbcontext.teachers.AsNoTracking().
                FirstOrDefaultAsync(x => x.Id == viewmodel1.Id);

            if(teacher is not null)
            {
                dbcontext.teachers.Remove(viewmodel1);
                await dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Teachers");
        }
    }
}
