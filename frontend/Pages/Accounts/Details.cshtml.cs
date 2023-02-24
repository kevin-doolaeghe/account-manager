using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.Pages.Accounts {

    public class DetailsModel : PageModel {

        public AccountGetDto Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync($"http://backend/api/accounts/{id}");
            AccountGetDto? account = await response.Content.ReadFromJsonAsync<AccountGetDto>();

            if (account == null) {
                return NotFound();
            } else {
                Account = account;
            }
            return Page();
        }
    }
}
