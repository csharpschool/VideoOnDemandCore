using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using VideoOnDemandCore.Models;
using AutoMapper;
using VideoOnDemandCore.Repositories;
using System.Collections.Generic;
using VideoOnDemandCore.Models.DTOModels;
using VideoOnDemandCore.Models.MembershipViewModels;
using System.Linq;

namespace VideoOnDemandCore.Controllers
{
    public class MembershipController : Controller
    {
        private string _userId;
        private IMapper _mapper;
        private readonly IReadRepository _db;

        public MembershipController(IHttpContextAccessor httpContextAccessor, 
        UserManager<ApplicationUser> userManager, IMapper mapper, IReadRepository db)
        {
            var user = httpContextAccessor.HttpContext.User;
            _userId = userManager.GetUserId(user);
            _mapper = mapper;
            _db = db;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var courseDtoObjects = _mapper.Map<List<CourseDTO>>(_db.GetCourses(_userId));
            var dashboardModel = new DashboardViewModel();
            dashboardModel.Courses = new List<List<CourseDTO>>();
            var noOfRows = courseDtoObjects.Count <= 3 ? 1 : courseDtoObjects.Count / 3;
            for (var i = 0; i < noOfRows; i++)
            {
                dashboardModel.Courses.Add(courseDtoObjects.Take(3).ToList());
            }

            return View(dashboardModel);
        }

        [HttpGet]
        public IActionResult Course(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Video(int id)
        {
            return View();
        }
    }
}