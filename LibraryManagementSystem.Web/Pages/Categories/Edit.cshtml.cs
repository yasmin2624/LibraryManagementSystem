using LibraryManagementSystem.Web.Models;
using LibraryManagementSystem.Web.Models.Category;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public UpdateCategoryDto Category { get; set; } = new();

        public int CategoryID { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var category = await _apiService.GetAsync<CategoryDto>(
                $"api/Categories/{id}");

            if (category == null)
                return NotFound();

            CategoryID = category.CategoryID;

            Category = new UpdateCategoryDto
            {
                Name = category.Name,
                Description = category.Description
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                CategoryID = id;
                return Page();
            }

            var success = await _apiService.PutAsync(
                $"api/Categories/{id}",
                Category);

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to update the category.");

                CategoryID = id;
                return Page();
            }

            return RedirectToPage("Details", new { id });
        }
    }
}