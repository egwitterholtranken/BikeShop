using SalesOrders.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace SalesOrders.Controllers
{
    public class ShopController : Controller
    {
        private SalesOrdersModel db = new SalesOrdersModel();
        private const int PAGESIZE = 3;

        [HttpGet]
        public ActionResult Index(int page = 1, int categoryID = 0, string searchString = "")
        {
            return View(GetModel(page, categoryID, searchString));
            //var data = db.Products.Select(p => p)
            //             .OrderBy(p => p.ProductName)
            //             .Skip((page - 1) * PAGESIZE).Take(PAGESIZE);

            //ProductsModel model = new ProductsModel
            //{
            //    Products = data,
            //    Pagination = new PaginationModel
            //    {
            //        CurrentPage = page,
            //        ItemsOnPage = PAGESIZE,
            //        TotalItems = db.Products.ToList().Count()
            //    }
            //};

            //return View(model);
        }

        [HttpPost]
        public ActionResult Index(ProductsModel productsModel)
        {
            return View(GetModel(1, productsModel.CategoryID,
                                    productsModel.SearchString));
        }

        private ProductsModel GetModel(int page, int categoryID, string searchString)
        {
            var data = db.Products.Select(p => p)
                         .Where(p => categoryID == 0 ||
                                p.CategoryID == categoryID)
                         .Where(p => string.IsNullOrEmpty(searchString) ||
                                p.ProductDescShort.Contains(searchString))
                         .OrderBy(p => p.ProductName)
                         .Skip((page - 1) * PAGESIZE).Take(PAGESIZE);

            ProductsModel model = new ProductsModel
            {
                Products = data,
                Pagination = new PaginationModel
                {
                    CurrentPage = page,
                    ItemsOnPage = PAGESIZE,
                    TotalItems = categoryID == 0
                                             ? db.Products.Count()
                                             : db.Products.Select(p => p)
                                                          .Where(p => p.CategoryID == categoryID)
                                                          .Where(p => p.ProductDescShort.Contains(searchString)).Count()
                },

                CategoryID = categoryID
            };

            return model;
        }

    }
}

// GET: Shop
//public ActionResult Index()
//{
//    var data = db.Products.Select(p => p).OrderBy(p => p.ProductName);

//    return View(data);
//}
