using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.Pages.Users {

    public class DetailsModel : PageModel {

        public UserGetDto User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync($"http://backend/api/Users/{id}");
            UserGetDto? user = await response.Content.ReadFromJsonAsync<UserGetDto>();

            if (user == null) {
                return NotFound();
            } else {
                User = user;
            }
            return Page();
        }
    }
}
