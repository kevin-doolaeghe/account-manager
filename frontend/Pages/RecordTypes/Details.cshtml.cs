using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.Pages.RecordTypes {

    public class DetailsModel : PageModel {

        public RecordTypeGetDto RecordType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync($"http://backend/api/record_types/{id}");
            RecordTypeGetDto? recordType = await response.Content.ReadFromJsonAsync<RecordTypeGetDto>();

            if (recordType == null) {
                return NotFound();
            } else {
                RecordType = recordType;
            }
            return Page();
        }
    }
}
