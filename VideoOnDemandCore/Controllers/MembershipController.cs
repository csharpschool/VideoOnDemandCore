using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using VideoOnDemandCore.Models;
using AutoMapper;
using VideoOnDemandCore.Repositories;

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
            return View();
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