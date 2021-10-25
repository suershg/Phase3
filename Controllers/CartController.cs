using IdentitySecureApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySecureApp.Controllers
{
    public class CartController : Controller
    {
        LaptopDBContext context = new LaptopDBContext();
        LaptopDBContext context1 = new LaptopDBContext();

        // GET: Cart
        public ActionResult Index()
        {
            string name = HttpContext.Session.GetString("uname");
            var rows = context.Tblorders.Where(x => x.CustomerMail == name);
            var products = new List<ordersViewModel>();
            foreach(var row in rows)
            {
                ordersViewModel obj = new ordersViewModel();
                 obj.OrderId = row.Id;
                 var product = context1.Tbllaptops.Single(x => x.Id == row.LaptopId);
                obj.Company = product.Company;
                obj.Price = product.Price;
                obj.Model = product.Model;
                obj.SellerEmail = product.SellerEmail;
                 products.Add(obj);
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult AddToCart(int id)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            var CustomerMail = HttpContext.Session.GetString("uname");
            Tblorders cartItem = new Tblorders() { Date = dateTime, LaptopId = id, CustomerMail = CustomerMail };
            context.Tblorders.Add(cartItem);
            context.SaveChanges();
            return RedirectToAction("Index");
            //return RedirectToAction(nameof(Index), "Products");
        }
        

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
      
        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            string name = HttpContext.Session.GetString("uname");
            return View(context1.Tblorders.Single(x => x.Id == id));
        }

        // POST: Cart/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tblorders o)
        {           
                context1.Tblorders.Remove(o);
                context1.SaveChanges();
                return RedirectToAction(nameof(Index));   
        }
        public ActionResult Order()
        {
            string name = HttpContext.Session.GetString("uname");
            var rows = context1.Tblorders.Where(x => x.CustomerMail == name);
            foreach(var row in rows)
            {
                context1.Tblorders.Remove(row);
                
            }
            context1.SaveChanges();
            return View();
        }
    }
}
