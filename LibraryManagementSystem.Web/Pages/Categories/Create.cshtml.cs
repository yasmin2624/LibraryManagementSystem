using LibraryManagementSystem.Web.Models.Category;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;

        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public CreateCategoryDto Category { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var success = await _apiService.PostAsync(
                "api/Categories",
                Category);

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to create the category.");

                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}