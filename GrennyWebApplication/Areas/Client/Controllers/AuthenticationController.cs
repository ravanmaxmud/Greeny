﻿using GrennyWebApplication.Areas.Client.ViewModels.Authentication;
using GrennyWebApplication.Contracts.Identity;
using GrennyWebApplication.Database;
using GrennyWebApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrennyWebApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("authentication")]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
        private readonly IUserActivationService _userActivationService;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(DataContext dbContext, IUserService userService, ILogger<AuthenticationController> logger, IUserActivationService userActivationService)
        {
            _dbContext = dbContext;
            _userService = userService;
            _logger = logger;
            _userActivationService = userActivationService;
        }


        [HttpGet("login", Name = "client-auth-login")]
        public async Task<IActionResult> Login()
        {
            if (_userService.IsAuthenticated)
            {
                return RedirectToRoute("client-home-index");
            }

            return View();
        }

        [HttpPost("login", Name = "client-auth-login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password is not correct");
                _logger.LogWarning($"({model.Email}{model.Password}) This Email and Password  is not correct.");
                return View(model);
            }
            if (await _dbContext.Users.AnyAsync(u => u.Email == model.Email && u.Role.Name == RoleNames.ADMIN))
            {
                await _userService.SignInAsync(model!.Email, model!.Password, RoleNames.ADMIN);
                return RedirectToRoute("admin-auth-login");
            }

            await _userService.SignInAsync(model!.Email, model!.Password);

            return RedirectToRoute("client-home-index");
        }

       


        [HttpGet("register", Name = "client-auth-register")]
        //[ServiceFilter(typeof(ValidationCurrentUserAttribute))]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost("register", Name = "client-auth-register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var users = await _dbContext.Users.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (await _dbContext.Users.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "Email Address is already un use.");
                _logger.LogWarning($"({model.Email}) This Email Address is already un use.");
                return View(model);
            }
            var emails = new List<string>();
            emails.Add(model.Email);

            await _userService.CreateAsync(model);

            return RedirectToRoute("client-home-index");
        }

        [HttpGet("logout", Name = "client-auth-logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("client-home-index");
        }



        //[HttpGet("activate/{token}", Name = "client-auth-activate")]
        //public async Task<IActionResult> Activate([FromRoute] string token)
        //{
        //    var userActivation = await _dbContext.UserActivations.Include(u => u.User)
        //        .FirstOrDefaultAsync(u => !u.User!.IsEmailConfirmed && u.ActivationToken == token);

        //    if (userActivation is null)
        //    {
        //        return NotFound("Activation Token Not Found");
        //    }

        //    if (DateTime.Now > userActivation.ExpireDate)
        //    {
        //        return Ok("Token expired olub teessufler");
        //    }

        //    userActivation.User.IsEmailConfirmed = true;

        //    await _dbContext.SaveChangesAsync();

        //    return RedirectToRoute("client-auth-login");
        //}
    }
}
