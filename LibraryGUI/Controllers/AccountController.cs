using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryGUI.Data;
using LibraryGUI.Data.Services;
using LibraryGUI.Models;
using LibraryGUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryGUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly ApplicationDbContext _ctx;
        public AccountController(IAccountService accountService, ApplicationDbContext ctx)
        {
            this.accountService = accountService;
            _ctx = ctx;
        }

        // GET: AccountController
        [Authorize(Policy = "readonlypolicy")]
        public ActionResult Index()
        {
            var users = accountService.GetAllUsers();
          


            return View(users);
        }

        // GET: AccountController/Details/5
        [Authorize(Policy = "readonlypolicy")]
        public ActionResult Details(string id)
        {
            var user = accountService.GetUser(id);
            return View(user);
        }

        // GET: AccountController/Edit/5
        [Authorize(Policy = "writepolicy")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = accountService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: AccountController/Edit/5
        [Authorize(Policy = "writepolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id,  [Bind]IdentityUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    accountService.UpdateUser(user);
                    await accountService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: AccountController/Delete/5
        [Authorize(Policy = "writepolicy")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = accountService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
         
        }

        // POST: AccountController/Delete/5
        [Authorize(Policy = "writepolicy")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string userid)
        {
            //try
            //{
                var user = accountService.GetUser(userid);
                accountService.DeleteNewUser(user);
                await accountService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View("");
            //}
        }

        private bool UserExists(string id)
        {
            return _ctx.Users.Any(e => e.Id == id);
        }

    }
}
