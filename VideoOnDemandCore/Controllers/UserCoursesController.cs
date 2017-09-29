using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using VideoOnDemandCore.Data;
using VideoOnDemandCore.Models;

namespace VideoOnDemandCore.Controllers
{
    public class UserCoursesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserStore<ApplicationUser, IdentityRole, ApplicationDbContext> _userStore;

        public UserCoursesController(UserStore<ApplicationUser, IdentityRole, ApplicationDbContext> userStore)
        {
            _db = userStore.Context;
            _userStore = userStore;
        }

    }
}
