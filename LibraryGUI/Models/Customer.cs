using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Display(Name = "Number of Books read")]
        public int NoOfBooksConsumed { get; set; }

    }
}
