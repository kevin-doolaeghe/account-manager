using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace frontend.Pages.RecordTypes {

    public class EditModel : PageModel {

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"http://backend/api/record_types/{RecordType.RecordTypeId}",
                new RecordTypePostDto() {
                    Name = RecordType.Name,
                    Icon = RecordType.Icon,
                    Color = RecordType.Color,
                }
            );
            RecordTypeGetDto? recordType = await response.Content.ReadFromJsonAsync<RecordTypeGetDto>();

            return RedirectToPage("Index");
        }
    }
}
