using LibraryManagementSystem.Web.Models.User;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly ApiService _apiService;

        public EditModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public int UserID { get; set; }

        [BindProperty]
        public UpdateUserDto User { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _apiService.GetAsync<UserDto>(
                $"api/Users/{id}");

            if (user == null)
                return NotFound();

            UserID = user.UserID;

            User.Name = user.Name;
            User.Email = user.Email;
            User.RoleID = user.RoleID;
            User.PhoneNumber = user.PhoneNumber;
            User.IsActive = user.IsActive;
            User.EmailConfirmed = user.EmailConfirmed;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                UserID = id;
                return Page();
            }

            var success = await _apiService.PutAsync(
                $"api/Users/{id}",
                User);

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to update the user.");

                UserID = id;
                return Page();
            }

            return RedirectToPage("Details", new { id });
        }
    }
}