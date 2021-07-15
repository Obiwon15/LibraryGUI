using LibraryGUI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Data.Services
{
    public class BookService : Repository<Book>, IBookService
    {
        public readonly ApplicationDbContext _ctx;

        public BookService(ApplicationDbContext context) : base(context) {
            _ctx = context;
        }

        //public BookService(ApplicationDbContext ctx)
        //{
        //    _ctx = ctx;
        //}

        public void AddBook(Book Book)
        {
            _ctx.Books.Add(Book);
        }

        //    public void AddBookEntry(BookEntry Entry)
        //    {
        //        _ctx.BookEntries.Add(Entry);
        //    }

        //    public async Task<List<Author>> GetAllAuthors()
        //    {
        //        return await _ctx.Authors.Include(a => a.BookEntry).ToListAsync();
        //    }

        public List<Book> GetAllBooks()
        {
            return _ctx.Books.Include(v => v.Author).Include(x => x.Genre).ToList();
        }

        //public IEnumerable<Book> GetAllBooks => _ctx.Books.Include(v => v.Author).Include(x => x.Genre);

        //    public async Task<Author> GetAuthor(int? Id)
        //    {
        //        return await _ctx.Authors.FirstOrDefaultAsync(x => x.AuthorId == Id);
        //    }

        public async Task<Book> GetBook(int? Id)
        {
            return await _ctx.Books.Include(q => q.Author).Include(r => r.Genre).FirstOrDefaultAsync(r => r.BookId == Id);
        }

        //    public async Task<BookEntry> GetBookEntry(int? Id)
        //    {
        //        return await _ctx.BookEntries.Include(p => p.Book).Include(w => w.Author).FirstOrDefaultAsync(z => z.EntryId == Id);
        //    }

        public void RemoveBook(Book Book)
        {
            _ctx.Books.Remove(Book);
        }

        //    public void RemoveBookEntry(BookEntry Entry)
        //    {
        //        _ctx.BookEntries.Remove(Entry);
        //    }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void UpdateBook(Book Book)
        {
            _ctx.Books.Update(Book);
        }

        //    public void UpdateBookEntry(BookEntry Entry)
        //    {
        //        _ctx.BookEntries.Update(Entry);
        //    }
        //}


        public IEnumerable<Book> FindWithAuthor(Func<Book, bool> predicate)
        {
            return _context.Books
                .Include(a => a.Author)
                .Where(predicate);
        }

        public IEnumerable<Book> GetAllWithAuthor()
        {
            return _context.Books.Include(a => a.Author);
        }
    }

}

