using Newtonsoft.Json;
using System.Net.Http.Json;
namespace Emre.Sayfalar
{
    public partial class Kurlar : ContentPage
    {
        public Kurlar()
        {
            InitializeComponent();
            LoadData();
        }
        public async void LoadData()
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync("https://finans.truncgil.com/today.json");

            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            // Update_Date'i ayrı olarak al
            var updateDate = data["Update_Date"].ToString();

            // Kurlar kısmını Dictionary<string, DovizKuru> olarak al
            var kurlarJson = JsonConvert.SerializeObject(data.Where(kv => kv.Key != "Update_Date").ToDictionary(kv => kv.Key, kv => kv.Value));
            var kurlar = JsonConvert.DeserializeObject<Dictionary<string, DovizKuru>>(kurlarJson);

            // BindingContext'i güncelle
            var doviz = new Doviz
            {
                Update_Date = updateDate,
                Kurlar = kurlar
            };

            BindingContext = doviz;
        }
    }
    public class DovizKuru
    {
        [JsonProperty("Alış")]
        public string Alis { get; set; }

        [JsonProperty("Tür")]
        public string Tur { get; set; }

        [JsonProperty("Satış")]
        public string Satis { get; set; }

        [JsonProperty("Değişim")]
        public string Degisim { get; set; }
    }

    public class Doviz
    {
        public string Update_Date { get; set; }
        public Dictionary<string, DovizKuru> Kurlar { get; set; }
    }
}