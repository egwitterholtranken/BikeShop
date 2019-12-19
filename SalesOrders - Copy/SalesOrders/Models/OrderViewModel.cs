using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesOrders.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public OrderDetail Details { get; set; }
    }
}