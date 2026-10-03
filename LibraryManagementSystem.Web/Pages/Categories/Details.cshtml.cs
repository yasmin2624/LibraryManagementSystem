using LibraryManagementSystem.Web.Models;
using LibraryManagementSystem.Web.Models.Books;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;

        public DetailsModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public CategoryDto? Category { get; set; }

        public List<BookDto> Books { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _apiService.GetAsync<CategoryDto>(
                $"api/Categories/{id}");

            if (Category == null)
                return NotFound();

            var books = await _apiService.GetAsync<List<BookDto>>(
                $"api/Categories/{id}/books");

            Books = books ?? new();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _apiService.DeleteWithMessageAsync(
                $"api/Categories/{id}");

            if (!result.Success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    result.ErrorMessage ??
                    "Unable to delete the category.");

                Category = await _apiService.GetAsync<CategoryDto>(
                    $"api/Categories/{id}");

                if (Category == null)
                    return NotFound();

                var books = await _apiService.GetAsync<List<BookDto>>(
                    $"api/Categories/{id}/books");

                Books = books ?? new();

                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}