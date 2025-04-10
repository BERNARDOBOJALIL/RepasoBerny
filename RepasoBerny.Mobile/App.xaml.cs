using Microsoft.Maui.Controls;
using RepasoBerny.Mobile.Pages;

namespace RepasoBerny.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        public void NavigateToShell()
        {
            var shell = new AppShell();
            Application.Current.MainPage = shell;
            shell.GoToAsync("//reportes");
        }
    }
}
