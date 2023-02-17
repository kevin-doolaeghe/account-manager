using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace frontend.Pages {

    public class RecordsModel : PageModel {

        public async Task OnGetAsync() {
            await FetchRecords();
        }

        public List<RecordGetDto> Records { get; set; } = new();

        public async Task FetchRecords() {
            using var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("http://backend/api/records");
            string json = await response.Content.ReadAsStringAsync();
            Records = JsonConvert.DeserializeObject<List<RecordGetDto>>(json) ?? new();
        }
    }
}
