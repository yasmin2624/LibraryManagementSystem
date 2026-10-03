
using LibraryManagementSystem.Web.Models;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryManagementSystem.Web.Models.Payment;
using LibraryManagementSystem.Web.Models.Borrows;

namespace LibraryManagementSystem.Web.Pages.Payments
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;

        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public CreatePaymentDto Payment { get; set; } = new();

        public List<BorrowDto> Borrows { get; set; } = new();

        public async Task OnGetAsync()
        {
            await LoadBorrowsAsync();

            Payment.PaymentDate = DateTime.Now;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadBorrowsAsync();
                return Page();
            }

            var success = await _apiService.PostAsync(
                "api/Payments",
                Payment);

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to record the payment.");

                await LoadBorrowsAsync();
                return Page();
            }

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to record the payment.");

                await LoadBorrowsAsync();
                return Page();
            }

            return RedirectToPage("Index");
        }

        private async Task LoadBorrowsAsync()
        {
            var borrows = await _apiService.GetAsync<List<BorrowDto>>(
                "api/Borrows");

            Borrows = borrows ?? new();
        }
    }
}