using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace frontend.Pages.RecordTypes {

    public class IndexModel : PageModel {

        public IList<RecordTypeGetDto> RecordTypes { get; set; } = default!;

        public async Task OnGetAsync() {
            using var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://backend/api/record_types");
            RecordTypes = await response.Content.ReadFromJsonAsync<List<RecordTypeGetDto>>() ?? new();
        }
    }
}
