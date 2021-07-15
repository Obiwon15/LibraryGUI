using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Models
{
    public class Owner : IdentityUser
    {
        public int UserId { get; set; }
        //public string UserName { get; set; }
        public string Mobile { get; set; }
        public string PostalCode { get; set; }
        //public string Role { get; set; }

    }
}
