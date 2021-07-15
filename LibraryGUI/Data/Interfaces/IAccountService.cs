using LibraryGUI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Data.Services
{
    public interface IAccountService
    {
        List<Owner> GetAllUsers();
        IdentityUser GetUser(string userId);

        void DeleteNewUser(IdentityUser user);
        void UpdateUser(IdentityUser user);
        Task<bool> SaveChangesAsync();

    }
}
