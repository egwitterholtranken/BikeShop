using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesOrders.Models
{
    public class ShoppingCartModel
    {
        private List<ShoppingCartItemModel> items = new List<ShoppingCartItemModel>();

        public IEnumerable<ShoppingCartItemModel> Items
        {
            get { return items; }
        }

        public void AddItem(Product product, int quantity)
        {
            //Check to see if item is already in shopping cart
            ShoppingCartItemModel item = items.SingleOrDefault(p => p.Product.ProductID == product.ProductID);

            //Item does not exist in shopping cart
            if(item == null)
            {
                items.Add(new ShoppingCartItemModel
                {
                    Product = product,
                    Quantity = quantity
                });
            }

            //Otherwise, product exists in current shopping cart
            else
            {
                item.Quantity += quantity;
            }
        }

        //Remove item from shopping cart
        public void RemoveItem(int productID)
        {
            items.RemoveAll(p => p.Product.ProductID == productID);
        }

        public decimal GetCartTotal()
        {
            return (decimal)items.Sum(s => s.Product.ProductPrice * s.Quantity);
        }

        public void Clear()
        {
            items.Clear();
        }

        public ShippingInfo ShippingInfo { get; set; }
        public BillingInfo BillingInfo { get; set; }

        
    }
}