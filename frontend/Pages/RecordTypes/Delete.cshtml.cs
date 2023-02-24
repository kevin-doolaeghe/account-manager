using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace frontend.Pages.RecordTypes {

    public class DeleteModel : PageModel {

        [BindProperty]
        public RecordTypeGetDto RecordType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync($"http://backend/api/record_types/{id}");
            RecordTypeGetDto? recordTypeType = await response.Content.ReadFromJsonAsync<RecordTypeGetDto>();

            if (recordTypeType == null) {
                return NotFound();
            } else {
                RecordType = recordTypeType;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.DeleteAsync($"http://backend/api/record_types/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToPage("Index");
        }
    }
}
