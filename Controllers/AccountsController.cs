using IdentitySecureApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySecureApp.Controllers
{
    public class AccountsController : Controller
    {
        UserManager<ApplicationUser> _userManager = null;
        SignInManager<ApplicationUser> _signInManager = null;
        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, SellerOrCustomer=model.SellerOrCustomer };
            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                ViewBag.msg = "User Registered Successfully...";
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if(result.Succeeded)
            {
                if (model.Email == "Admin@gmail.com")
                {
                    return RedirectToAction("Index", "Admin");
                }
                var user = this._userManager.Users.Single(x => x.Email == model.Email);
                var role = user.SellerOrCustomer;
                HttpContext.Session.SetString("uname", model.Email);
                HttpContext.Session.SetString("role", role);
                return RedirectToAction("Index", "Products");
            }
            else
            {
                ViewBag.msg = "invalid input credentials...";
                return View();
            }
            
        }

        public IActionResult Dashboard(string role)
        {
            if(HttpContext.Session.GetString("uname") != null)
            {
                var msg = HttpContext.Session.GetString("uname");
                ViewBag.msg = $"Hello {msg}, Welcome to Dashboard {role}";
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("uname") != null)
            {
                var name = HttpContext.Session.GetString("uname");
                ViewBag.msg = $"hello {name}, you logged out successfully";
                HttpContext.Session.SetString("uname", "");
            }
            else
                ViewBag.msg = "No user available to logout ....";
            return View();
        }
    }
}
