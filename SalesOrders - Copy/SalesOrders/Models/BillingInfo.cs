using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesOrders.Models
{
    public class BillingInfo
    {
        [Required(ErrorMessage = "Billing First Name is Mandatory")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Billing Last Name is Mandatory")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Billing Credit Card Number is Mandatory")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Billing Address is Mandatory")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Billing City is Mandatory")]
        public string City { get; set; }

        [Required(ErrorMessage = "Billing State is Mandatory")]
        public string State { get; set; }

        [Required(ErrorMessage = "Billing Postal Code is Mandatory")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Billing Expire Month is Mandatory")]
        public string ExpireMonth { get; set; }

        public SelectList Months()
        {
            return new SelectList(new string[]
            {
                "01","02","03","04",
                "05","06","07","08",
                "09","10","11","12"
            });
        }


        [Required(ErrorMessage = "Billing Expire Year is Mandatory")]
        public string ExpireYear { get; set; }

        //The Expiration Year Select Box
        public SelectList Years()
        {
            string[] validYears = new string[12];
            int year;
            
            //Increments year for 11 years
            for(int i = 0; i < 12; i++)
            {
                year = DateTime.Now.Year;
                year = (year + i);

                validYears[i] = year.ToString();
            }

            return new SelectList(validYears);       
        }
    }
}