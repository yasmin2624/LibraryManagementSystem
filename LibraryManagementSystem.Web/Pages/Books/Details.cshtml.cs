using LibraryManagementSystem.Web.Models.Books;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Books;

public class DetailsModel : PageModel
{
    private readonly ApiService _apiService;

    public DetailsModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public BookDto? Book { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Book = await _apiService.GetAsync<BookDto>(
            $"api/Books/{id}");

        if (Book == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var result = await _apiService.DeleteWithMessageAsync(
            $"api/Books/{id}");

        if (!result.Success)
        {
            Book = await _apiService.GetAsync<BookDto>(
                $"api/Books/{id}");

            if (Book == null)
                return NotFound();

            ModelState.AddModelError(
                string.Empty,
                result.ErrorMessage ?? "Unable to delete the book.");

            return Page();
        }

        return RedirectToPage("Index");
    }
}