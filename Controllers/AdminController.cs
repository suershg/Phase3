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
    public class AdminController : Controller
    {
        UserManager<ApplicationUser> _userManager = null;
        SignInManager<ApplicationUser> _signInManager = null;

        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;

        }
        // GET: AdminController
        public ActionResult Index()
        {
            return View(this._userManager.Users);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Error = $"User with Id={id} cannot be found";
                return View();
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.PasswordHash,
                SellerOrCustomer = user.SellerOrCustomer

            };
            return View(model);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(EditUserViewModel model)
        {
            var user = await this._userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.Error = $"User with Id={model.Id} cannot be found";
                return View();
            }
            else
            {
                var result = await this._userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Error = $"User with Id={id} cannot be found";
                return View();
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.PasswordHash,
                SellerOrCustomer = user.SellerOrCustomer

            };
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await this._userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.Error = $"User with Id={model.Id} cannot be found";
                return View();
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                var result = await this._userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
    }
}