using LibraryGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.ViewModels
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Book { get; set; }

        public string CurrentCategory { get; set; }

    }
}
