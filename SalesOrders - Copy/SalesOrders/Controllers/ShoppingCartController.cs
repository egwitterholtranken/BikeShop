using SalesOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesOrders.Controllers
{
    public class ShoppingCartController : Controller
    {
        private SalesOrdersModel db = new SalesOrdersModel();

        public ShoppingCartController()
        { }

        // GET: Cart
        public ViewResult Index(string returnUrl)
        {
            return View(new ShoppingCartViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(int productID, string returnUrl)
        {
            Product product = db.Products.SingleOrDefault(p => p.ProductID == productID);

            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productID, string returnUrl)
        {
            GetCart().RemoveItem(productID);
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult CartWidget(ShoppingCartModel cart)
        {
            return PartialView(cart);
        }

        private ShoppingCartModel GetCart()
        {
            ShoppingCartModel cart = (ShoppingCartModel)Session["Cart"];

                if (cart == null)
                {
                    cart = new ShoppingCartModel();
                    Session["Cart"] = cart;
                }

            return cart;
        }

        [Authorize]
        [HttpGet]
        public ViewResult ShippingInfo()
        {
            return View(new ShippingInfo());
        }

        [Authorize]
        [HttpPost]
        public ActionResult ShippingInfo(ShippingInfo shippingInfo)
        {
            if (ModelState.IsValid)
            {   
                ShoppingCartModel cart = GetCart();
                cart.ShippingInfo = shippingInfo;

                return RedirectToAction("BillingInfo");
            }

            else
            {
                return View(shippingInfo);
            }
        }

        [Authorize]
        [HttpGet]
        public ViewResult BillingInfo()
        {
            return View(new BillingInfo());
        }

        [Authorize]
        [HttpPost]
        public ViewResult BillingInfo(BillingInfo billingInfo)
        {
            if (ModelState.IsValid)
            {
                ShoppingCartModel cart = GetCart();
                
                cart.BillingInfo = billingInfo;
               
                ProcessOrder(cart);
                //cart.Clear();

                return View("OrderComplete", cart);

            }
            else
            {
                return View(billingInfo);
            }
        }

        private void ClearCart(ShoppingCartModel cart)
        {
            cart.Clear();
        }

        private void ProcessOrder(ShoppingCartModel cart)
        {
            string idString = System.Web.HttpContext.Current.User.Identity.Name;
            //int customerId = int.Parse(idString);

            int customerId = 7020;

            Customer customer = db.Customers.SingleOrDefault(c => c.CustomerID == customerId);

            customer.CustomerFirstName       = cart.BillingInfo.FirstName;
            customer.CustomerLastName        = cart.BillingInfo.LastName;
            customer.BillingAddress          = cart.BillingInfo.Address;
            customer.BillingCity             = cart.BillingInfo.City;
            customer.BillingState            = cart.BillingInfo.State;
            customer.BillingPostalCode       = cart.BillingInfo.PostalCode;
            customer.BillingCreditCardNumber = cart.BillingInfo.CreditCardNumber;
            customer.BillingExpireMonth      = cart.BillingInfo.ExpireMonth;
            customer.BillingExpireYear       = cart.BillingInfo.ExpireYear;
            
            db.SaveChanges();

            Order order = new Order
            {
                CustomerID              = customer.CustomerID,
                OrderDate               = DateTime.Now,
                ShippingAddress         = cart.ShippingInfo.Address,
                ShippingCity            = cart.ShippingInfo.City,
                ShippingState           = cart.ShippingInfo.State,
                ShippingPostalCode      = cart.ShippingInfo.PostalCode
            };

            db.Orders.Add(order);
            db.SaveChanges();

            foreach(ShoppingCartItemModel item in cart.Items)
            {
                OrderDetail orderItem = new OrderDetail
                {
                    OrderID                     = order.OrderID,
                    ProductID                   = item.Product.ProductID,
                    OrderDetailQuotedPrice      = item.Product.ProductPrice,
                    OrderDetailQuantityOrdered  = item.Quantity
                };

                db.OrderDetails.Add(orderItem);
            }
            db.SaveChanges();
        }
    }
}