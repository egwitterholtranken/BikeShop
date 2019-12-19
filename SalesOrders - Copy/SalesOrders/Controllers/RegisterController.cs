using SalesOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesOrders.Controllers
{
    public class RegisterController : Controller
    {
        private SalesOrdersModel db = new SalesOrdersModel();

        [HttpGet]
        // GET: Register
        public ActionResult Index()
        {
            return View(new Customer());
        }

        [HttpPost]
        public RedirectToRouteResult Register(Customer customer)
        {
            customer.Password = SHA256.Encode(customer.CustomerPassword);

            db.Customers.Add(customer);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}