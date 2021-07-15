using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Models
{
    public class BookEntry
    {
        [Key]
        public int EntryId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Author Author  { get; set; }

    }
}
