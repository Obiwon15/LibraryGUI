using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryGUI.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Display(Name = "Title")]
        public string BookTitle { get; set; }
        [Display(Name = "ISDN")]
        public string ISDN { get; set; }
        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }
        [Display(Name = "Place of Publication")]
        public string PlaceOfPublication { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }
        [Display(Name = "Book Rating")]
        public int BookRating { get; set; }
        //public int? GenreId { get; set; } = 0;
        //public virtual IEnumerable<BookEntry> BookEntry { get; set; }
        //public string BookCover { get; set; }
        //public string BookFile { get; set; }
        public Genre Genre { get; set; }
        //public int? AuthorId { get; set; } = 0;
        public Author Author { get; set; }

    }
}
