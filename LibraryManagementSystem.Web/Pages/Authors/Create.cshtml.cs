using LibraryManagementSystem.Web.Models;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Authors;

public class CreateModel : PageModel
{
    private readonly ApiService _apiService;

    public CreateModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public CreateAuthorDto Author { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await _apiService.PostAsync(
            "api/Authors",
            Author);

        if (!result)
        {
            ModelState.AddModelError(
                string.Empty,
                "Failed to create author.");

            return Page();
        }

        return RedirectToPage("Index");
    }
}