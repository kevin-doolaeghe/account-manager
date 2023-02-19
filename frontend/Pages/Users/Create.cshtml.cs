using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.Pages.Users {

    public class CreateModel : PageModel {

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public UserPostDto User { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid || User == null) {
                return Page();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.PostAsJsonAsync("http://backend/api/users", User);
            response.EnsureSuccessStatusCode();

            return RedirectToPage("Index");
        }
    }
}
