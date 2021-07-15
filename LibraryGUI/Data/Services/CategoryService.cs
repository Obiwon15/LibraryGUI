using LibraryGUI.Data.Interfaces;
using LibraryGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _ctx;

        public CategoryService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Genre> Genre => _ctx.Genres;
    }
}
