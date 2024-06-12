using Microsoft.Maui.Controls;

namespace Emre.Sayfalar
{
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
            DarkModeSwitch.IsToggled = Application.Current.UserAppTheme == AppTheme.Dark;
        }

        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }

            Preferences.Set("IsDarkMode", e.Value);
        }
    }
}

