using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading.Tasks;
using VideoOnDemandCore.Data;
using VideoOnDemandCore.Entities;
using VideoOnDemandCore.Models;
using VideoOnDemandCore.Models.DTOModels;

namespace VideoOnDemandCore.Controllers
{
    public class UserCoursesController : Controller
    {
        private readonly ApplicationDbContext _db;
        //private readonly UserStore<ApplicationUser, IdentityRole, ApplicationDbContext> _userStore;

        public UserCoursesController(UserStore<ApplicationUser, IdentityRole, ApplicationDbContext> userStore)
        {
            _db = userStore.Context;
            //_userStore = userStore;
        }

        public IActionResult Index()
        {
            var model = _db.Courses
                .Join(_db.UserCourses, c => c.Id, uc => uc.CourseId, (c, uc) => new { Courses = c, UserCourses = uc })
                .Select(s => new UserCourseDTO
                {
                    CourseId = s.Courses.Id,
                    CourseTitle = s.Courses.Title,
                    UserId = s.UserCourses.UserId,
                    UserEmail = _db.Users.FirstOrDefault(u => u.Id.Equals(s.UserCourses.UserId)).Email
                });

            return View(model);
        }

        public async Task<IActionResult> Details(string userId, int courseId)
        {
            if (userId == null || courseId.Equals(default(int)))
            {
                return NotFound();
            }

            var model = await _db.Courses
                .Join(_db.UserCourses, c => c.Id, uc => uc.CourseId, (c, uc) => new { Courses = c, UserCourses = uc })
                .Select(s => new UserCourseDTO
                {
                    CourseId = s.Courses.Id,
                    CourseTitle = s.Courses.Title,
                    UserId = s.UserCourses.UserId,
                    UserEmail = _db.Users.FirstOrDefault(u =>
                        u.Id.Equals(s.UserCourses.UserId)).Email
                })
                .FirstOrDefaultAsync(w => w.CourseId.Equals(courseId) &&
                    w.UserId.Equals(userId));

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_db.Courses, "Id", "Title");
            ViewData["UserId"] = new SelectList(_db.Users, "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CourseId")] UserCourse userCourse)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Add(userCourse);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("",
                        "That combination already exist.");
                }
            }

            ViewData["CourseId"] = new SelectList(_db.Courses, "Id", "Title", userCourse.CourseId);
            ViewData["UserId"] = new SelectList(_db.Users, "Id", "Email");

            return View();
        }

    }
}
