using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryGUI.Data;
using LibraryGUI.Models;
using LibraryGUI.Data.Services;
using LibraryGUI.Data.Interfaces;
using LibraryGUI.ViewModels;

namespace LibraryGUI.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BooksController(ApplicationDbContext context, IBookService bookService, ICategoryService categoryService)
        {
            _context = context;
            _bookService = bookService;
            _categoryService = categoryService;
        }

        // GET: Books
        public IActionResult Index(string category)
        {
            //string _category = category;
            //IEnumerable<Book> Books;

            //string currentCategory = string.Empty;

            //if (string.IsNullOrEmpty(category))
            //{
            //    Books = _bookService.GetAllBooks.OrderBy(n => n.BookId);
            //    currentCategory = "All Books";
            //}
            //else
            //{
            //    if (string.Equals("Action", _category, StringComparison.OrdinalIgnoreCase))
            //    {
            //        Books = _bookService.GetAllBooks.Where(p => p.Genre.GenreName.Equals("Action")).OrderBy(p => p.BookTitle);
            //    }
            //    else if (string.Equals("Thriller", _category, StringComparison.OrdinalIgnoreCase))
            //    {
            //        Books = _bookService.GetAllBooks.Where(p => p.Genre.GenreName.Equals("Thriller")).OrderBy(p => p.BookTitle);
            //    }
            //    else
            //    {
            //        Books = _bookService.GetAllBooks.Where(p => p.Genre.GenreName.Equals("Romance")).OrderBy(p => p.BookTitle);
            //    }
            //    currentCategory = _category;
            //}


            //return View(new BookListViewModel
            //{
            //    Book = Books,
            //    CurrentCategory = currentCategory
            //});

            var entries = _bookService.GetAllBooks();
            return View("Index", entries);
        }

        [HttpGet("Books/Search/{searchString}")]
        public IActionResult Search(string searchString)
        {
            var result = _context.Books.Where(p => p.BookTitle.Contains(searchString)).Select(p => p.BookTitle).ToList();
            return View("Index", result);
        }
        // GET: Books/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {

            return View(new Book());
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,BookTitle,ISDN,PublicationDate,PlaceOfPublication,Category,BookRating")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.AddBook(book);
                await _bookService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,BookTitle,ISDN,PublicationDate,PlaceOfPublication,Category,BookRating")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.UpdateBook(book);
                    await _bookService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            return View(book);
        }

        // GET: Books/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBook(id);
              
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookService.GetBook(id);
            _bookService.RemoveBook(book);
            await _bookService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
