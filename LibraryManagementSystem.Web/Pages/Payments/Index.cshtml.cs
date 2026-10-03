using LibraryManagementSystem.Web.Models.Payment;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Payments
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<PaymentDto> Payments { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? BorrowID { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }

        public async Task OnGetAsync()
        {
            var payments = await _apiService.GetAsync<List<PaymentDto>>(
                "api/Payments");

            Payments = payments ?? new();

            if (BorrowID.HasValue)
            {
                Payments = Payments
                    .Where(p => p.BorrowID == BorrowID.Value)
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                Payments = Payments
                    .Where(p => string.Equals(
                        p.Status,
                        Status,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }
    }
}