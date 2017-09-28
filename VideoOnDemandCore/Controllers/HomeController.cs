using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VideoOnDemandCore.Models;
using VideoOnDemandCore.Repositories;

namespace VideoOnDemandCore.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;

        public HomeController(SignInManager<ApplicationUser> signInMgr)
        {
            _signInManager = signInMgr;
        }

        public IActionResult Index()
        {
            var rep = new MockReadRepository();
            var courses = rep.GetCourses("40a9c4c5-b362-45bf-95fb-3b80eb2afcf4");
            var course = rep.GetCourse("40a9c4c5-b362-45bf-95fb-3b80eb2afcf4", 1);
            var video = rep.GetVideo("40a9c4c5-b362-45bf-95fb-3b80eb2afcf4", 1);

            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
