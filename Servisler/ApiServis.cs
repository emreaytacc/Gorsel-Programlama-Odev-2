using Emre.Models;
using System.Net.Http.Json;

namespace Emre.Servisler
{
    public class ApiServisi
    {
        private readonly HttpClient _httpClient;

        public ApiServisi()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Kur> KurBilgileriniGetirAsync()
        {
            var url = "https://api.genelpara.com/embed/altin.json";
            return await _httpClient.GetFromJsonAsync<Kur>(url);
        }
    }
}
