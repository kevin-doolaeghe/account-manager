using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace frontend.Pages.Records {

    public class DeleteModel : PageModel {

        [BindProperty]
        public RecordGetDto Record { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync($"http://backend/api/records/{id}");
            RecordGetDto? record = await response.Content.ReadFromJsonAsync<RecordGetDto>();

            if (record == null) {
                return NotFound();
            } else {
                Record = record;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.DeleteAsync($"http://backend/api/records/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToPage("Index");
        }
    }
}
