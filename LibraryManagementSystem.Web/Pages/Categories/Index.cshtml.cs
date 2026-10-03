using LibraryManagementSystem.Web.Models;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<CategoryDto> Categories { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public async Task OnGetAsync()
        {
            var categories = await _apiService.GetAsync<List<CategoryDto>>(
                "api/Categories");

            Categories = categories ?? new();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                Categories = Categories
                    .Where(c =>
                        c.Name.Contains(
                            Search,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }
    }
}