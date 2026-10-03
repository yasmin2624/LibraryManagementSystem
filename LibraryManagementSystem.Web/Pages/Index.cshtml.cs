using LibraryManagementSystem.Web.Models.Books;
using LibraryManagementSystem.Web.Models.Borrows;
using LibraryManagementSystem.Web.Models.User;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public int TotalBooks { get; set; }
        public int AvailableBooks { get; set; }
        public int BorrowedBooks { get; set; }

        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }

        public int ActiveBorrows { get; set; }
        public int ReturnedBorrows { get; set; }
        public int OverdueBorrows { get; set; }

        public async Task OnGetAsync()
        {
            var books = await _apiService.GetAsync<List<BookDto>>(
                "api/Books");

            var users = await _apiService.GetAsync<List<UserDto>>(
                "api/Users");

            var borrows = await _apiService.GetAsync<List<BorrowDto>>(
                "api/Borrows");

            books ??= new List<BookDto>();
            users ??= new List<UserDto>();
            borrows ??= new List<BorrowDto>();

            TotalBooks = books.Count;

            AvailableBooks = books.Count(b => b.Availability);

            BorrowedBooks = books.Count(b => !b.Availability);

            TotalUsers = users.Count;

            ActiveUsers = users.Count(u => u.IsActive);

            ActiveBorrows = borrows.Count(b =>
                !string.Equals(
                    b.Status,
                    "Returned",
                    StringComparison.OrdinalIgnoreCase));

            ReturnedBorrows = borrows.Count(b =>
                string.Equals(
                    b.Status,
                    "Returned",
                    StringComparison.OrdinalIgnoreCase));

            OverdueBorrows = borrows.Count(b =>
                !string.Equals(
                    b.Status,
                    "Returned",
                    StringComparison.OrdinalIgnoreCase)
                && b.DueDate < DateTime.Now);
        }
    }
}