using Microsoft.Maui.Controls;
using RepasoBerny.Mobile.Pages;
using RepasoBerny.Mobile.Services;

namespace RepasoBerny.Mobile
{
    public partial class App : Application
    {
        public static UsuarioDatabase Database { get; private set; }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Chat());
        }

        public void NavigateToShell()
        {
            var shell = new AppShell();
            Application.Current.MainPage = shell;
            shell.GoToAsync("//reportes");
        }
    }
}
