using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace Emre.Sayfalar
{
    public partial class HavaDurumu : ContentPage
    {
        public List<WeatherResponse> WeatherItems { get; set; }

        public HavaDurumu()
        {
            InitializeComponent();
            LoadWeatherData();
        }

        private async void LoadWeatherData()
        {
            var cities = new List<(double lat, double lon)>
            {
                (41.015137, 28.979530), // Istanbul
                (39.933365, 32.859741), // Ankara
                (41.581051, 32.461014)  // Bartın
            };

            WeatherItems = new List<WeatherResponse>();

            foreach (var (lat, lon) in cities)
            {
                var weather = await GetWeatherAsync(lat, lon);
                WeatherItems.Add(weather);
            }

            BindingContext = this;
        }
        public async Task<WeatherResponse> GetWeatherAsync(double lat, double lon)

        {
            string ApiKey = "ecd8c9b818540db5b9c3e957baad0fe4";
            string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
            using var client = new HttpClient();
            var url = $"{BaseUrl}?lat={lat}&lon={lon}&appid={ApiKey}&units=metric";
            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<WeatherResponse>(response);
        }
    }

public class Coord
{
    public double Lon { get; set; }
    public double Lat { get; set; }
}

public class Weather
{
    public int Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

public class Main
{
    public double Temp { get; set; }
    public double Feels_like { get; set; }
    public double Temp_min { get; set; }
    public double Temp_max { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public int Sea_level { get; set; }
    public int Grnd_level { get; set; }
}

public class Wind
{
    public double Speed { get; set; }
    public int Deg { get; set; }
    public double Gust { get; set; }
}

public class Clouds
{
    public int All { get; set; }
}

public class Sys
{
    public int Type { get; set; }
    public int Id { get; set; }
    public string Country { get; set; }
    public int Sunrise { get; set; }
    public int Sunset { get; set; }
}

public class WeatherResponse
{
    public Coord Coord { get; set; }
    public List<Weather> Weather { get; set; }
    public string Base { get; set; }
    public Main Main { get; set; }
    public int Visibility { get; set; }
    public Wind Wind { get; set; }
    public Clouds Clouds { get; set; }
    public int Dt { get; set; }
    public Sys Sys { get; set; }
    public int Timezone { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Cod { get; set; }
}

}

