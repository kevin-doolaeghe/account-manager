using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace frontend.Pages.Records {

    public class IndexModel : PageModel {

        public IList<RecordGetDto> Records { get; set; } = default!;

        public async Task OnGetAsync() {
            using var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://backend/api/records");
            Records = await response.Content.ReadFromJsonAsync<List<RecordGetDto>>() ?? new();
        }
    }
}
