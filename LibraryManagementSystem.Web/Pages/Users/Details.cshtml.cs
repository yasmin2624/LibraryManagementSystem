using LibraryManagementSystem.Web.Models.User;
using LibraryManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Web.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly ApiService _apiService;

        public DetailsModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public UserDto? User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _apiService.GetAsync<UserDto>(
                $"api/Users/{id}");

            if (User == null)
                return NotFound();

            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _apiService.DeleteWithMessageAsync(
                $"api/Users/{id}");

            if (!result.Success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    result.ErrorMessage ?? "Unable to delete the user.");

                User = await _apiService.GetAsync<UserDto>(
                    $"api/Users/{id}");

                if (User == null)
                    return NotFound();

                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}