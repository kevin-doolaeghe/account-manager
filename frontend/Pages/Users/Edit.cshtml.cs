using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace frontend.Pages.Users {

    public class EditModel : PageModel {

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            HttpClient client = new();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"http://backend/api/users/{User.UserId}",
                new UserPostDto() {
                    Name = User.Name,
                    Email = User.Email,
                }
            );
            UserGetDto? user = await response.Content.ReadFromJsonAsync<UserGetDto>();

            return RedirectToPage("Index");
        }
    }
}
