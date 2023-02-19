using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace frontend.Pages.Users {

    public class DeleteModel : PageModel {

        [BindProperty]
        public UserGetDto User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync($"http://backend/api/users/{id}");
            UserGetDto? user = await response.Content.ReadFromJsonAsync<UserGetDto>();

            if (user == null) {
                return NotFound();
            } else {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.DeleteAsync($"http://backend/api/users/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToPage("Index");
        }
    }
}
