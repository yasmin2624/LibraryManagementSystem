using LibraryManagementSystem.Web.Models.User;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<UserDto> Users { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? RoleID { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? IsActive { get; set; }

        public async Task OnGetAsync()
        {
            var users = await _apiService.GetAsync<List<UserDto>>(
                "api/Users");

            Users = users ?? new();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                Users = Users
                    .Where(u =>
                        u.Name.Contains(Search, StringComparison.OrdinalIgnoreCase) ||
                        u.Email.Contains(Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (RoleID.HasValue)
            {
                Users = Users
                    .Where(u => u.RoleID == RoleID.Value)
                    .ToList();
            }

            if (IsActive.HasValue)
            {
                Users = Users
                    .Where(u => u.IsActive == IsActive.Value)
                    .ToList();
            }
        }
    }
}