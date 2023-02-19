using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.Pages.Records {

    public class DetailsModel : PageModel {

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
    }
}
