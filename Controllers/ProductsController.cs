using IdentitySecureApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySecureApp.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        LaptopDBContext context = new LaptopDBContext();
        LaptopDBContext context1 = new LaptopDBContext();
        public ActionResult Index()
        {
            ViewBag.role = HttpContext.Session.GetString("role");
            return View(context.Tbllaptops);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View(context.Tbllaptops.Single(x => x.Id == id));
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            ViewBag.Seller = HttpContext.Session.GetString("uname");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbllaptops p)
        {
            try
            {
                p.SellerEmail = HttpContext.Session.GetString("uname");
                context.Tbllaptops.Add(p);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Seller = HttpContext.Session.GetString("uname");
            return View(context.Tbllaptops.Single(x => x.Id == id));
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tbllaptops lp)
        {
            try
            {
                context.Tbllaptops.Update(lp);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Seller = HttpContext.Session.GetString("uname");
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(context.Tbllaptops.Single(x => x.Id == id));
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tbllaptops lp)
        {
            var rows = context.Tblorders.Where(x => x.LaptopId == lp.Id);
            foreach(var row in rows)
            {
                context.Tblorders.Remove(row);
            }
                context.SaveChanges();
                context.Tbllaptops.Remove(lp);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));    
        }

        
    }
}