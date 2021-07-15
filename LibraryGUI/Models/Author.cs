using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Display(Name = "Author")]
        public string AuthorName { get; set; }
        public int? BookId { get; set; }
        public virtual IEnumerable<Book> Books { get; set; }

    }
}
