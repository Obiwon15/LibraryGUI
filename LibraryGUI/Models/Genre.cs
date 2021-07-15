using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Display(Name = "Theme")]
        public string GenreName { get; set; }
        public int? BookId { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
