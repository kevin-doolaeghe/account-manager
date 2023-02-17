using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace frontend.Pages {

    public class AccountsModel : PageModel {

        public async Task OnGetAsync() {
            await FetchAccounts();
        }

        public List<AccountGetDto> Accounts { get; set; } = new();

        public async Task FetchAccounts() {
            using var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://backend/api/accounts");
            string json = await response.Content.ReadAsStringAsync();
            Accounts = JsonConvert.DeserializeObject<List<AccountGetDto>>(json) ?? new();
        }
    }
}
