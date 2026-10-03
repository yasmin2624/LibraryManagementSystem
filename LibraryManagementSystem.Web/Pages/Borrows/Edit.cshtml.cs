using LibraryManagementSystem.Web.Models.Borrows;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Borrows
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public int BorrowID { get; set; }

        [BindProperty]
        public UpdateBorrowDto Borrow { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var borrow = await _apiService.GetAsync<BorrowDto>(
                $"api/Borrows/{id}");

            if (borrow == null)
            {
                return NotFound();
            }

            BorrowID = borrow.BorrowID;

            Borrow.DueDate = borrow.DueDate;
            

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                BorrowID = id;
                return Page();
            }

            var success = await _apiService.PutAsync(
                $"api/Borrows/{id}",
                Borrow);

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to update the borrowing record.");

                BorrowID = id;
                return Page();
            }

            return RedirectToPage("Details", new { id });
        }
    }
}