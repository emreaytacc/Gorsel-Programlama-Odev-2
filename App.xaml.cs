using Emre.Sayfalar;

namespace Emre
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new Giris();
        }
    }
}
