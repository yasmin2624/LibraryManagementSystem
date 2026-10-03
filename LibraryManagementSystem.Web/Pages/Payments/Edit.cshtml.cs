using LibraryManagementSystem.Web.Models.Payment;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Payments
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public UpdatePaymentDto Payment { get; set; } = new();

        public int PaymentID { get; set; }

        public int BorrowID { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var payment = await _apiService.GetAsync<PaymentDto>(
                $"api/Payments/{id}");

            if (payment == null)
                return NotFound();

            PaymentID = payment.PaymentID;
            BorrowID = payment.BorrowID;

            Payment = new UpdatePaymentDto
            {
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.PaymentDate,
                Status = payment.Status,
                Notes = payment.Notes
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                PaymentID = id;

                var existing = await _apiService.GetAsync<PaymentDto>(
                    $"api/Payments/{id}");

                if (existing != null)
                    BorrowID = existing.BorrowID;

                return Page();
            }

            var success = await _apiService.PutAsync(
                $"api/Payments/{id}",
                Payment);

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to update the payment.");

                PaymentID = id;

                var existing = await _apiService.GetAsync<PaymentDto>(
                    $"api/Payments/{id}");

                if (existing != null)
                    BorrowID = existing.BorrowID;

                return Page();
            }

            return RedirectToPage("Details", new { id });
        }
    }
}