using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace frontend.Pages {

    public class UsersModel : PageModel {

        public async Task OnGetAsync() {
            await FetchUsers();
        }

        public List<UserGetDto> Users { get; set; } = new();

        public async Task FetchUsers() {
            using var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://backend/api/users");
            string json = await response.Content.ReadAsStringAsync();
            Users = JsonConvert.DeserializeObject<List<UserGetDto>>(json) ?? new();
        }
    }
}
