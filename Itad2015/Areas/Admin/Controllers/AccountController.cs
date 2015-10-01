using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Contract.Service.Entity;
using Itad2015.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Itad2015.Areas.Admin.Controllers
{
    public class AccountController : AdminBaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _userService.LoginAsync(model.Email, model.Password);
                if (loginResult.Result)
                {
                    await IdentitySignin(model.Email, model.RemeberMe);
                    return RedirectToLocal(ReturnUrl);
                }
                ModelState.AddModelError("Email", "Niepoprawna nazwa użytkownika lub hasło");
            }
            return View(model);
        }

        private async Task IdentitySignin(string email, bool isPersistent = false)
        {
            var x = await _userService.GetByEmailAsync(email);
            var userState = Mapper.Map<AppUserState>(x.Result);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userState.Email),
                new Claim("isSuperAdmin",userState.SuperAdmin.ToString()),
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }
    }
}