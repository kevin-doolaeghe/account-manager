using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace frontend.Pages.Accounts {

    public class EditModel : PageModel {

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"http://backend/api/accounts/{Account.AccountId}",
                new AccountPostDto() {
                    Name = Account.Name,
                    UserId = Account.UserId,
                }
            );
            AccountGetDto? account = await response.Content.ReadFromJsonAsync<AccountGetDto>();

            return RedirectToPage("Index");
        }
    }
}
