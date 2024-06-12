using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Emre.Sayfalar
{
    public partial class Haberler : ContentPage
    {
        private const string ApiBaseUrl = "https://api.rss2json.com/v1/api.json?rss_url=";
        private readonly HttpClient _httpClient = new();
        private readonly string newUrl = "http://www.trthaber.com/xml_mobile.php?tur=xml_genel&kategori=[kat]&adet=20&selectEx=yorumSay,okunmaadedi,anasayfamanset,kategorimanset";
        

        public Haberler()
        {
            InitializeComponent();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            var categories = new List<string>
            {
                "Manset",
                "SonDakika",
                "Gundem",
                "Ekonomi",
                "Spor",
                "Bilim_Teknoloji",
                "Guncel"
            };

            foreach (var category in categories)
            {
                var button = new Button
                {
                    Text = category,
                    AutomationId = category 
                };

                button.Clicked += async (sender, args) =>
                {
                    await LoadNews(category);
                };

                categoriesLayout.Children.Add(button);
            }
        }
        private async void ReadMoreButton_Clicked(object sender, EventArgs e)
{
            var button = sender as Button;
            var newsItem = button?.BindingContext as Haber ;
            if (newsItem != null)
            {
                try
                {
                    await Launcher.OpenAsync(new Uri(newsItem.HaberLink));
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Hata", $"Bağlantı açılırken bir hata oluştu: {ex.Message}", "Tamam");
                }
        }
}

      private async Task LoadNews(string category)
        {
            try
            {

                
                var response =await _httpClient.GetAsync($"https://www.trthaber.com/xml_mobile.php?tur=xml_genel&kategori={category.ToLower()}&adet=20&selectEx=yorumSay,okunmaadedi,anasayfamanset,kategorimanset");
                var xmlData = await response.Content.ReadAsStringAsync();
            
                var haberler = ParseXml(xmlData);
            


                // Dönüştürülen haber verisini ListView'e ata
                if (haberler != null && haberler.HaberList.Count>0 )
                {
                    newsListView.ItemsSource = haberler.HaberList;
                }
                else
                {
                    await DisplayAlert("Hata", "Haberler yüklenirken bir hata oluştu.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bilgi ver
                await DisplayAlert("Hata", $"Bir hata oluştu: {ex.Message}", "Tamam");
            }

        } 
                 public static Haberlerim ParseXml(string xmlData)
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(Haberlerim));
                            using (StringReader reader = new StringReader(xmlData))
                            {
                                return (Haberlerim)serializer.Deserialize(reader);
                            }
                        }
         
                private async void NewsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
                {
                    if (e.SelectedItem == null)
                        return;

                    var selectedItem = e.SelectedItem as Haber;
                    await Launcher.OpenAsync(new Uri(selectedItem.HaberLink));
                    ((ListView)sender).SelectedItem = null;
                }
            }



[XmlRoot("haberler")]
public class Haberlerim
{
    [XmlElement("haber")]
    public List<Haber> HaberList { get; set; }
}

    public class Haber 
{
    [XmlElement("haber_manset")]
    public string HaberManset { get; set; }
    
    [XmlElement("haber_resim")]
    public string HaberResim { get; set; }
    
    [XmlElement("haber_link")]
    public string HaberLink { get; set; }
    
    [XmlElement("haber_id")]
    public string HaberId { get; set; }
    
    [XmlElement("haber_video")]
    public string HaberVideo { get; set; }
    
    [XmlElement("haber_aciklama")]
    public string HaberAciklama { get; set; }
    
    [XmlElement("haber_metni")]
    public string HaberMetni { get; set; }
    
    [XmlElement("haber_kategorisi")]
    public string HaberKategorisi { get; set; }
    
    [XmlElement("haber_tarihi")]
    public string HaberTarihiString { get; set; }
    
    public DateTime HaberTarihi
    {
        get
        {
            return DateTime.ParseExact(HaberTarihiString, "ddd, yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
    
    [XmlElement("manset_resim")]
    public string MansetResim { get; set; }
    
    [XmlElement("izles_id")]
    public string IzlesId { get; set; }
    
    [XmlElement("yorumSay")]
    public string YorumSayString { get; set; }
    
    public int YorumSay
    {
        get
        {
            return int.Parse(YorumSayString);
        }
    }
    
    [XmlElement("okunmaadedi")]
    public string OkunmaAdediString { get; set; }
    
    public int OkunmaAdedi
    {
        get
        {
            return int.Parse(OkunmaAdediString);
        }
    }
    
    [XmlElement("anasayfamanset")]
    public string AnasayfaMansetString { get; set; }
    
    public int AnasayfaManset
    {
        get
        {
            return int.Parse(AnasayfaMansetString);
        }
    }
    
    [XmlElement("kategorimanset")]
    public string KategoriMansetString { get; set; }
    
    public int KategoriManset
    {
        get
        {
            return int.Parse(KategoriMansetString);
        }
    }
}
}
