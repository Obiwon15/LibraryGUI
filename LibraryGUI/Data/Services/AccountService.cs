using LibraryGUI.Data.Services;
using LibraryGUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Data
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _ctx;
        
        public AccountService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void DeleteNewUser(IdentityUser user)
        {
            _ctx.Users.Remove(user);
            //_ctx.SaveChangesAsync();
        }

        public List<Owner> GetAllUsers()
        {
            var result = _ctx.User.ToList();

            return result;
        }

        public IdentityUser GetUser(string userId)
        {
            var result = _ctx.Users.FirstOrDefault(m => m.Id == userId);
            return result;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void UpdateUser(IdentityUser user)
        {
            _ctx.Users.Update(user);
        }
    }
}
