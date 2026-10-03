using LibraryManagementSystem.Web.Models.User;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly ApiService _apiService;

        public CreateModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public CreateUserDto User { get; set; } = new();

        public void OnGet()
        {
            User.RoleID = 2;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var success = await _apiService.PostAsync(
                "api/Users",
                User);

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to create the user.");

                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}