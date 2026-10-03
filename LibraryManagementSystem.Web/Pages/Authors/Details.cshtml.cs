using LibraryManagementSystem.Web.Models;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Authors;

public class DetailsModel : PageModel
{
    private readonly ApiService _apiService;

    public DetailsModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public AuthorDto? Author { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Author = await _apiService.GetAsync<AuthorDto>(
            $"api/Authors/{id}");

        if (Author == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var result = await _apiService.DeleteWithMessageAsync(
            $"api/Authors/{id}");

        if (!result.Success)
        {
            TempData["DeleteError"] = result.ErrorMessage;
            return RedirectToPage("Details", new { id });
        }

        return RedirectToPage("Index");
    }
}