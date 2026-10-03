
using LibraryManagementSystem.Web.Models;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<AuthorDto> Authors { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public async Task OnGetAsync()
        {
            var authors = await _apiService.GetAsync<List<AuthorDto>>(
                "api/Authors");

            Authors = authors ?? new();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                Authors = Authors
                    .Where(a =>
                        a.Name.Contains(Search, StringComparison.OrdinalIgnoreCase) ||
                        (a.Email != null &&
                         a.Email.Contains(Search, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }
        }
    }
}