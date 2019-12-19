using SalesOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesOrders.Controllers
{
    public class ItemDetailsController : Controller
    {
        private SalesOrdersModel db = new SalesOrdersModel();

        // GET: ItemDetails
        public ActionResult Index(int id)
        {
            var data = db.Products.SingleOrDefault(p => p.ProductID == id);

            return View(data);
        }
    }
}