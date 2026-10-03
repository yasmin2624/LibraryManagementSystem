using LibraryManagementSystem.Web.Models.Payment;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Payments
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;

        public DetailsModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public PaymentDto? Payment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Payment = await _apiService.GetAsync<PaymentDto>(
                $"api/Payments/{id}");

            if (Payment == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _apiService.DeleteAsync(
                $"api/Payments/{id}");

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to delete the payment.");

                Payment = await _apiService.GetAsync<PaymentDto>(
                    $"api/Payments/{id}");

                if (Payment == null)
                    return NotFound();

                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}