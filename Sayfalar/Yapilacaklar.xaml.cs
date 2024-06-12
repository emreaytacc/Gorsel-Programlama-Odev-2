using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Emre.Sayfalar
{
    public partial class Yapilacaklar : ContentPage
    {
        public ObservableCollection<Not> Notlar { get; set; }

        public Yapilacaklar()
        {
            InitializeComponent();

            Notlar = new ObservableCollection<Not>();
            lstNotlar.ItemsSource = Notlar;
        }

        private async void NotEkle_Clicked(object sender, EventArgs e)
        {
            string metin = await DisplayPromptAsync("Not Ekle", "Not:");
            if (!string.IsNullOrWhiteSpace(metin))
            {
                Notlar.Add(new Not { Metin = metin });
            }
        }

        private async void Duzenle_Clicked(object sender, EventArgs e)
        {
            ImageButton duzenleButton = (ImageButton)sender;
            Not not = (Not)duzenleButton.BindingContext;
            string yeniMetin = await DisplayPromptAsync("Not Düzenle", "Not:", "Tamam", "İptal", not.Metin);
            if (!string.IsNullOrWhiteSpace(yeniMetin))
            {
                not.Metin = yeniMetin;
            }
        }

        private void Sil_Clicked(object sender, EventArgs e)
        {
            ImageButton silButton = (ImageButton)sender;
            Not not = (Not)silButton.BindingContext;
            Notlar.Remove(not);
        }
    }

    public class Not : INotifyPropertyChanged
    {
        private string metin;
        public string Metin
        {
            get { return metin; }
            set
            {
                if (value != metin)
                {
                    metin = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}