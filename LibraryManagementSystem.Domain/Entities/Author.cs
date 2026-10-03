using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Author
    {
        public int AuthorID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Nationality { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
