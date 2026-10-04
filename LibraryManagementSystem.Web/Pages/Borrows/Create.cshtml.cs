using LibraryManagementSystem.Web.Models.Books;
using LibraryManagementSystem.Web.Models.Borrows;
using LibraryManagementSystem.Web.Models.User;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Borrows
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;

        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public CreateBorrowDto Borrow { get; set; } = new();

        public List<BookDto> Books { get; set; } = new();

        public List<UserDto> Members { get; set; } = new();

        public List<UserDto> Librarians { get; set; } = new();

        public async Task OnGetAsync()
        {
            await LoadDataAsync();
        }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            if (!ModelState.IsValid)
            {
                await LoadDataAsync();
                return Page();
            }

            var createdBorrow =
                await _apiService.PostAndGetAsync<CreateBorrowDto, BorrowDto>(
                    "api/Borrows",
                    Borrow);

            if (createdBorrow == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to create the borrowing record.");

                await LoadDataAsync();
                return Page();
            }

            if (action == "payment")
            {
                return RedirectToPage(
                    "/Payments/Create",
                    new { borrowId = createdBorrow.BorrowID });
            }

            return RedirectToPage("Details", new { id = createdBorrow.BorrowID });
        }

        private async Task LoadDataAsync()
        {
            var books = await _apiService.GetAsync<List<BookDto>>(
                "api/Books");

            var users = await _apiService.GetAsync<List<UserDto>>(
                "api/Users");

            Books = books?
                .Where(b => b.Availability)
                .ToList()
                ?? new();

            Members = users?
                .Where(u => u.RoleID == 2)
                .ToList()
                ?? new();

            Librarians = users?
                .Where(u => u.RoleID == 3)
                .ToList()
                ?? new();
        }
    }
}