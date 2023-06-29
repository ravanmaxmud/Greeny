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


        [HttpGet("forgetPassword", Name = "client-auth-forgetPassword")]
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        [HttpPost("forgetPassword", Name = "client-auth-forgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user is null)
            {
                ModelState.AddModelError(String.Empty, "Email is not correct");
                _logger.LogWarning($"({model.Email}) This Email is not correct.");
                return View(model);
            }

            await _userActivationService.SendChangePasswordUrlAsync(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-home-index");
        }




        [HttpGet("forgetPasswordToken/{token}", Name = "client-auth-forgetPasswordToken")]
        public async Task<IActionResult> ForgetPasswordToken([FromRoute] string token)
        {
            var forgetPassword = await _dbContext.PasswordForgets.Include(u => u.User)
                .FirstOrDefaultAsync(u => u.ActivationToken == token);

            if (forgetPassword is null)
            {
                return NotFound("Forget Password Token Not Found");
            }

            if (DateTime.Now > forgetPassword.ExpiredDate)
            {
                return Ok("Token expired olub teessufler");
            }


            return RedirectToRoute("client-auth-changePassword", new { token = token });
        }

        [HttpGet("changePassword", Name = "client-auth-changePassword")]
        public async Task<IActionResult> ChangePassword(string token)
        {
            var model = new ChangePasswordViewModel
            {
                Token = token
            };
            return View(model);
        }
        [HttpPost("changePassword", Name = "client-auth-changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var forgetPassword = await _dbContext.PasswordForgets.Include(u => u.User)
             .FirstOrDefaultAsync(u => u.ActivationToken == model.Token);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == forgetPassword.UserId);

            if (user == null)
            {
                return NotFound();
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-auth-login");
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
