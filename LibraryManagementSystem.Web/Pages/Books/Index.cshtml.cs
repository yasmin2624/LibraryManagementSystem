using LibraryManagementSystem.Web.Models;
using LibraryManagementSystem.Web.Models.Books;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<BookDto> Books { get; set; } = new();

        public List<AuthorDto> Authors { get; set; } = new();

        public List<CategoryDto> Categories { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? AuthorID { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryID { get; set; }

        public async Task OnGetAsync()
        {
            await LoadAuthorsAndCategoriesAsync();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                var result = await _apiService.GetAsync<List<BookDto>>(
                    $"api/Books/search/title?title={Uri.EscapeDataString(Search)}");

                Books = result ?? new();

                return;
            }

            if (AuthorID.HasValue)
            {
                var result = await _apiService.GetAsync<List<BookDto>>(
                    $"api/Books/author/{AuthorID.Value}");

                Books = result ?? new();

                return;
            }

            if (CategoryID.HasValue)
            {
                var result = await _apiService.GetAsync<List<BookDto>>(
                    $"api/Books/category/{CategoryID.Value}");

                Books = result ?? new();

                return;
            }

            var books = await _apiService.GetAsync<List<BookDto>>(
                "api/Books");

            Books = books ?? new();
        }

        private async Task LoadAuthorsAndCategoriesAsync()
        {
            var authors = await _apiService.GetAsync<List<AuthorDto>>(
                "api/Authors");

            var categories = await _apiService.GetAsync<List<CategoryDto>>(
                "api/Categories");

            Authors = authors ?? new();
            Categories = categories ?? new();
        }
    }
}