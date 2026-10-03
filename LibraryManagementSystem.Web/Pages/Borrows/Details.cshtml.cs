using LibraryManagementSystem.Web.Models.Borrows;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Borrows
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;

        public DetailsModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public BorrowDto? Borrow { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Borrow = await _apiService.GetAsync<BorrowDto>(
                $"api/Borrows/{id}");

            if (Borrow == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostReturnAsync(int id)
        {
            var success = await _apiService.ReturnBookAsync(
                $"api/Borrows/{id}/return");

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to return the book.");

                Borrow = await _apiService.GetAsync<BorrowDto>(
                    $"api/Borrows/{id}");

                if (Borrow == null)
                {
                    return NotFound();
                }

                return Page();
            }

            return RedirectToPage("Details", new { id });
        }
    }
}