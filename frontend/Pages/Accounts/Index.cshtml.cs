using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace frontend.Pages.Accounts {

    public class IndexModel : PageModel {

        public IList<AccountGetDto> Accounts { get; set; } = default!;

        public async Task OnGetAsync() {
            using var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://backend/api/accounts");
            Accounts = await response.Content.ReadFromJsonAsync<List<AccountGetDto>>() ?? new();
        }
    }
}
