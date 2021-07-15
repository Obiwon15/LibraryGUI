using LibraryGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Data.Services
{
    public interface IBookService
    {
        Task<Book> GetBook(int? Id);
        ////Task<BookEntry> GetBookEntry(int? Id);
        List<Book> GetAllBooks();
        //IEnumerable<Book> GetAllBooks { get; }
    ////Physician
    //Task<List<Author>> GetAllAuthors();
    //Task<Author> GetAuthor(int? Id);
    void AddBook(Book Book);
        void RemoveBook(Book Book);
        void UpdateBook(Book Book);
        //List<PatientForm> GetAllPatients(string physician);
        //void AddBookEntry(BookEntry Entry);
        //void RemoveBookEntry(BookEntry Entry);
        //void UpdateBookEntry(BookEntry Entry);

        Task<bool> SaveChangesAsync();

        IEnumerable<Book> GetAllWithAuthor();
        IEnumerable<Book> FindWithAuthor(Func<Book, bool> predicate);
  

    }
}
