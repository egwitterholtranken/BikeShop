﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesOrders.Models
{
    public class PaginationModel
    {
        public int TotalItems { get; set; }
        public int ItemsOnPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsOnPage);
            }
        }
    }
}