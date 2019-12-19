using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesOrders.Models
{
    public class ContactModel
    {

        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is Required")]
        public string State { get; set; }

        public SelectList States()
        {
            return new SelectList(new string[]
            {
                "Please Select A State","Alabama","Alaska", "Arizona", "Arkansas", "California",
                "Colorado","Connecticut", "Delaware", "Distict of Columbia",
                "Florida","Georgia","Hawaii","Idaho","Illinois","Indiana",
                "Iowa","Kansas","Kentucky","Louisiana","Maine","Maryland",
                "Massachusetts","Michigan","Minnesota","Mississippi","Missouri",
                "Montana","Nebraska","Nevada","New Hampshire","New Jersey",
                "New Mexico","New York","North Carolina","North Dakota","Ohio",
                "Oklahoma","Oregon","Pennsylvania","Rhode Island","South Carolina",
                "South Dakota","Tennesse","Texas","Utah","Vermont","Virginia",
                "Washington","West Virgina","Wisconsin","Wyoming"
            });
        }

        [Required(ErrorMessage = "Zip Code is Required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Reason for Contact is Required")]
        public string Reason { get; set; }
    }
}