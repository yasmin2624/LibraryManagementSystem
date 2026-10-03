using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Book
    {
        public int BookID { get; set; }

        public string ISBN { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Genre { get; set; }

        public string? Language { get; set; }

        public bool Availability { get; set; }

        public int AuthorID { get; set; }

        public int CategoryID { get; set; }

        public Author Author { get; set; } = null!;

        public Category Category { get; set; } = null!;

        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
    }
}
