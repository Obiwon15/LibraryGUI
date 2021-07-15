using LibraryGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.ViewModels
{
    public class BookEditViewModel
    {

        public Book Book { get; set; }

        public IEnumerable<Author> Authors { get; set; }

    }
}
