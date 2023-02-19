using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace frontend.Pages.Records {

    public class EditModel : PageModel {

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"http://backend/api/records/{Record.RecordId}",
                new RecordPostDto() {
                    Description = Record.Description,
                    Amount = Record.Amount,
                    Status = Record.Status,
                    Date = Record.Date,
                    RecordTypeId = Record.RecordTypeId,
                    AccountId = Record.AccountId,
                }
            );
            RecordGetDto? record = await response.Content.ReadFromJsonAsync<RecordGetDto>();

            return RedirectToPage("Index");
        }
    }
}
