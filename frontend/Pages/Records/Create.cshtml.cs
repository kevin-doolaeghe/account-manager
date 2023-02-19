using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.Pages.Records {

    public class CreateModel : PageModel {

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public RecordPostDto Record { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid || Record == null) {
                return Page();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.PostAsJsonAsync("http://backend/api/records", Record);
            response.EnsureSuccessStatusCode();

            return RedirectToPage("Index");
        }
    }
}
