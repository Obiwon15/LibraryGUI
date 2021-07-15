using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryGUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class SuccessRegistrationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public SuccessRegistrationModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult OnGet()
        { 
            return Page();
        }

    }
}
