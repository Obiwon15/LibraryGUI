using LibraryGUI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryMenu(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryService.Genre.OrderBy(p => p.GenreName);

            return View(categories);
        }

    }
}
