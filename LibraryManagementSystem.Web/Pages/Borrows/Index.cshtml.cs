using LibraryManagementSystem.Web.Models.Borrows;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Borrows
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<BorrowDto> Borrows { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? BookID { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MemberID { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }

        public async Task OnGetAsync()
        {
            if (BookID.HasValue ||
                MemberID.HasValue ||
                !string.IsNullOrWhiteSpace(Status))
            {
                var endpoint = "api/Borrows/search";

                var parameters = new List<string>();

                if (BookID.HasValue)
                {
                    parameters.Add($"bookId={BookID.Value}");
                }

                if (MemberID.HasValue)
                {
                    parameters.Add($"memberId={MemberID.Value}");
                }

                if (!string.IsNullOrWhiteSpace(Status))
                {
                    parameters.Add(
                        $"status={Uri.EscapeDataString(Status)}");
                }

                endpoint += "?" + string.Join("&", parameters);

                var result =
                    await _apiService.GetAsync<List<BorrowDto>>(endpoint);

                Borrows = result ?? new();

                return;
            }

            var books =
                await _apiService.GetAsync<List<BorrowDto>>(
                    "api/Borrows");

            Borrows = books ?? new();
        }
    }
}