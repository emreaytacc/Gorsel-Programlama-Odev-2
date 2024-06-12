using Microsoft.Maui.Controls;

namespace Emre.Sayfalar
{
    public partial class Giris : ContentPage
    {
        public Giris()
        {
            InitializeComponent();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (username == "emre" && password == "emre123")
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Hata", "Geçersiz kullanıcı adı veya şifre.", "Tamam");
            }
        }
    }
}

