using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("profile")]
    public class ProfileController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;

        public ProfileController(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        [HttpGet("index", Name = "client-profile-index")]
        public IActionResult Index()
        {
            if (!_userService.IsAuthenticated)
            {
                return RedirectToRoute("client-auth-login");
            }
            ViewBag.Name = _userService.CurrentUser.FirstName;
            ViewBag.LastName = _userService.CurrentUser.LastName;
            ViewBag.Email = _userService.CurrentUser.Email;
            return View();
        }
    }
}
