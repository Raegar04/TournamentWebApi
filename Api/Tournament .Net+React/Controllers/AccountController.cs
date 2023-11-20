﻿using KnightTournament.Models;
using KnightTournament.Models.Enums;
using KnightTournament.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var user = new AppUser() { UserName = registerViewModel.UserName, Email = registerViewModel.Email };
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                return Problem(string.Join(". ", result.Errors));
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            return Ok("signed in");
        }

        [HttpGet("Detail")]
        public async Task<IActionResult> Detail()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(
                                loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Problem("Invalid credentials");
            }

            return Ok("Logged in");
        }

        [HttpGet("LogOff")]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged off");
        }

        //[HttpGet]
        //[Route("ForgotPassword")]
        //public async Task<IActionResult> ForgotPassword()
        //{
        //    var user = (await _userManager.GetUserAsync(User));
        //    var forgotPasswordVM = new ForgotPasswordViewModel() { Email = user.Email };
        //    ForgotPasswordViewModel.CorrectCode = new Random().Next(1000, 9999).ToString();
        //    return View(forgotPasswordVM);
        //}
        //[HttpPost]
        //[Route("ForgotPassword")]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        //{
        //    if (forgotPasswordViewModel.Code == ForgotPasswordViewModel.CorrectCode)
        //    {
        //        return RedirectToAction("ChangePassword");
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        //[HttpGet]
        //[Route("ChangePassword")]
        //public async Task<IActionResult> ChangePassword()
        //{
        //    return View(new ChangePasswordViewModel());
        //}
        //[HttpPost]
        //[Route("ChangePassword")]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    await _userManager.ChangePasswordAsync(user, user.PasswordHash, changePasswordViewModel.Password);
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
