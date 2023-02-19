using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace frontend.Pages.Users {

    public class IndexModel : PageModel {

        public IList<UserGetDto> Users { get; set; } = default!;

        public async Task OnGetAsync() {
            using var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://backend/api/users");
            Users = await response.Content.ReadFromJsonAsync<List<UserGetDto>>() ?? new();
        }
    }
}
